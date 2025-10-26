using RaGae.Projects.RCC.Core;
using RaGae.Projects.RCC.Domain.Extensions;
using System.Diagnostics;

namespace RaGae.Projects.RCC.Programmer
{
    public partial class FormMain : Form
    {
        private readonly string[] ports;

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

        private async void FormMain_Load(object sender, EventArgs e)
        {
            if (Program.CubeColor?.Count >= 2)
            {
                buttonLED1.BackColor = Program.CubeColor[0].GetColor();
                buttonLED2.BackColor = Program.CubeColor[1].GetColor();
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
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.DudeConfig.Programmer),
                        Arguments = $"{(Program.DudeConfig.Verbose ? "-v" : string.Empty)} -p {Program.Firmware.MCU} -c {Program.DudeConfig.Mode} -P {port} -b {Program.DudeConfig.Baudrate} -U flash:w:{Path.Combine(extractPath, Program.Firmware.FlashFile)}:i -U eeprom:w:{Path.Combine(extractPath, Program.Firmware.EEPROMFile)}:i",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "avrdude.out"), output);
                    await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "avrdude.err"), error);

                    await process.WaitForExitAsync();

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

            this.groupBoxIntensity.Enabled = false;
            this.groupBoxProgram.Enabled = false;
            this.groupBoxRCC.Enabled = false;

            this.progressBarStatus.Value = 10;

            string[] lines = await File.ReadAllLinesAsync(Path.Combine(Program.TempPath, Program.Firmware.EEPROMFile));

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

            await File.WriteAllLinesAsync(Path.Combine(Program.TempPath, $"{Program.Firmware.EEPROMFile}_modified"), lines);
            this.progressBarStatus.Value = 60;

            await RunAvrdudeAsync(extractPath: Program.TempPath, port: comboBoxPort.SelectedItem.ToString());

            this.progressBarStatus.Value = 100;

            this.groupBoxIntensity.Enabled = true;
            this.groupBoxProgram.Enabled = true;
            this.groupBoxRCC.Enabled = true;
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
                if (File.Exists(Program.TempZIPPath))
                {
                    File.Delete(Program.TempZIPPath);
                }
            }
            catch { }

            try
            {
                if (Directory.Exists(Program.TempPath))
                {
                    Directory.Delete(Program.TempPath, recursive: true);
                }
            }
            catch { }
        }
    }
}
