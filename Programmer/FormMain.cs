using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using RaGae.Projects.RCC.Core;
using RaGae.Projects.RCC.Domain.Extensions;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RaGae.Projects.RCC.Programmer
{
    public partial class FormMain : Form
    {
        private readonly Firmware firmware = Program.Configuration.GetSection(nameof(Firmware)).Get<Firmware>();
        private readonly DudeConfig dudeConfig = Program.Configuration.GetSection(nameof(DudeConfig)).Get<DudeConfig>();
        private readonly List<CubeColor> cubeColor = Program.Configuration.GetSection(nameof(CubeColor)).Get<List<CubeColor>>();

        private readonly string[] ports;
        private string tempZipPath = Path.Combine(Path.GetTempPath(), "firmware.zip");
        string tempPath = Path.Combine(Path.GetTempPath(), "RCC_" + Guid.NewGuid().ToString());

        public FormMain()
        {
            InitializeComponent();

            try
            {
                this.ports = System.IO.Ports.SerialPort.GetPortNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{StringResource.ErrorLoadingSerialPorts} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
            comboBoxPort.Items.AddRange(this.ports);
            comboBoxPort.SelectedIndex = (comboBoxPort.Items.Count) > 0 ? (comboBoxPort.Items.Count - 1) : -1;

            buttonProgram.Enabled = (comboBoxPort.SelectedIndex != -1);
        }

        private async Task DownloadZipAndExtractAsync(string downloadUrl, string extractPath)
        {
            string temporaryFileName = $"{Guid.NewGuid().ToString()}.zip";

            using (HttpClient httpClient = new HttpClient())
            {
                using (Stream fileStream = await httpClient.GetStreamAsync(downloadUrl))
                {
                    using (FileStream localFile = new FileStream(temporaryFileName, FileMode.Create, FileAccess.Write))
                    {
                        await fileStream.CopyToAsync(localFile);
                    }
                }
                ZipFile.ExtractToDirectory(temporaryFileName, extractPath, overwriteFiles: true);
            }
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            if (this.cubeColor?.Count >= 2)
            {
                buttonLED1.BackColor = this.cubeColor[0].GetColor();
                buttonLED2.BackColor = this.cubeColor[1].GetColor();
            }

            if(!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dudeConfig.Programmer)))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dudeConfig.ExtractPath));

                    await DownloadZipAndExtractAsync(dudeConfig.DownloadUrl, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dudeConfig.ExtractPath));
                    MessageBox.Show(StringResource.StatusProgrammerDownloadComplete, StringResource.MessageBoxInformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{StringResource.ErrorProgrammerDownloadFailed} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }
            }

            Directory.CreateDirectory(tempPath);

            try
            {
                await DownloadZipAndExtractAsync(firmware.DownloadUrl, tempPath);
                MessageBox.Show(StringResource.StatusFirmwareDownloadComplete, StringResource.MessageBoxInformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{StringResource.ErrorFirmwareDownloadFailed} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
            this.Enabled = true;
        }

        private void trackBarIntensity_Scroll(object sender, EventArgs e)
        {
            Color color_button1 = Color.FromArgb(
                    trackBarIntensity.Value * 255 / 15,
                    buttonLED1.BackColor.R,
                    buttonLED1.BackColor.G,
                    buttonLED1.BackColor.B);

            Color color_button2 = Color.FromArgb(
                    trackBarIntensity.Value * 255 / 15,
                    buttonLED2.BackColor.R,
                    buttonLED2.BackColor.G,
                    buttonLED2.BackColor.B);

            buttonLED1.BackColor = color_button1;
            buttonLED2.BackColor = color_button2;
        }

        private void buttonLED_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (colorDialogLED.ShowDialog() == DialogResult.OK)
            {
                Color color = Color.FromArgb(
                    trackBarIntensity.Value * 255 / 15,
                    colorDialogLED.Color.R,
                    colorDialogLED.Color.G,
                    colorDialogLED.Color.B);

                button.BackColor = color;
            }
        }

        private async Task RunAvrdudeAsync(string extractPath, string port)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.dudeConfig.Programmer),
                Arguments = $"{(this.dudeConfig.Verbose ? "-v" : string.Empty)} -p {this.firmware.MCU} -c {this.dudeConfig.Mode} -P {port} -b {this.dudeConfig.Baudrate} -U flash:w:{Path.Combine(extractPath, this.firmware.FlashFile)}:i -U eeprom:w:{Path.Combine(extractPath, this.firmware.EEPROMFile)}:i",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = psi;
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    Debug.WriteLine("avrdude output:\n" + output);
                    Debug.WriteLine("avrdude error:\n" + error);

                    await Task.Run(() => process.WaitForExit());

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show(StringResource.StatusProgrammingCompleted, StringResource.MessageBoxInformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{StringResource.ErrorProgrammingFailed} {error}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{StringResource.ErrorStartingAVRDude} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void buttonProgram_Click(object sender, EventArgs e)
        {
            if (comboBoxPort.SelectedItem is null || comboBoxPort.SelectedIndex == -1)
            {
                MessageBox.Show(StringResource.ErrorLoadingSerialPorts, StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Enabled = false;
            this.progressBarStatus.Value = 10;

            string[] lines = await File.ReadAllLinesAsync(Path.Combine(tempPath, firmware.EEPROMFile));

            this.progressBarStatus.Value = 20;

            IntelHex hexLine;

            try
            {
                hexLine = lines[0].FromString();
                hexLine.ModifyByte(0, Convert.ToByte(trackBarIntensity.Value));
                hexLine.ModifyByte(1, buttonLED1.BackColor.R);
                hexLine.ModifyByte(2, buttonLED1.BackColor.G);
                hexLine.ModifyByte(3, buttonLED1.BackColor.B);

                hexLine.ModifyByte(4, Convert.ToByte(trackBarIntensity.Value));
                hexLine.ModifyByte(5, buttonLED1.BackColor.R);
                hexLine.ModifyByte(6, buttonLED1.BackColor.G);
                hexLine.ModifyByte(7, buttonLED1.BackColor.B);

                hexLine.Checksum = hexLine.CalculateChecksum();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{StringResource.ErrorDataModificationFailed} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            this.progressBarStatus.Value = 50;

            lines[0] = hexLine.ToString();

            Debug.WriteLine(lines[0]);

            await File.WriteAllLinesAsync(Path.Combine(tempPath, $"{firmware.EEPROMFile}_modified"), lines);
            this.progressBarStatus.Value = 60;

            await RunAvrdudeAsync(extractPath: tempPath, port: comboBoxPort.SelectedItem.ToString());

            this.progressBarStatus.Value = 100;
            this.Enabled = true;
        }

        public static byte CalculateIntelHexChecksum(byte[] bytes)
        {
            int sum = 0;
            foreach (byte b in bytes)
            {
                sum += b;
            }
            byte checksum = (byte)((256 - (sum & 0xFF)) & 0xFF);
            return checksum;
        }

        private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo($"https://{linkLabelGitHub.Text}")
                {
                    UseShellExecute = true
                });

            }
            catch { }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (File.Exists(tempZipPath))
                {
                    File.Delete(tempZipPath);
                }
            }
            catch { }

            try
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, recursive: true);
                }
            }
            catch { }
        }
    }
}
