using Microsoft.Extensions.Configuration;
using RaGae.Projects.RCC.Core;
using RaGae.Projects.RCC.Programmer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaGae.Projects.RCC
{
    public enum SplashScreenLoadingStatus
    {
        None,
        Config,
        AVRDude,
        Firmware,
        End
    }

    public partial class SplashScreen : Form
    {
        private SplashScreenLoadingStatus splashScreenStatus;

        public SplashScreen()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Abort;
            this.timerLoad.Interval = 25;
            this.timerLoad.Start();
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

        private async void SplashScreen_Load(object sender, EventArgs e)
        {
            this.splashScreenStatus = SplashScreenLoadingStatus.Config;
            this.labelStatusText.Text = StringResource.MessageLoadingConfiguration;

            try
            {
                Program.Firmware = Program.Configuration.GetSection(nameof(Firmware)).Get<Firmware>();
                Program.DudeConfig = Program.Configuration.GetSection(nameof(DudeConfig)).Get<DudeConfig>();
                Program.CubeColor = Program.Configuration.GetSection(nameof(CubeColor)).Get<List<CubeColor>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            this.splashScreenStatus = SplashScreenLoadingStatus.AVRDude;
            this.labelStatusText.Text = StringResource.MessageLoadingAVRDude;

            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.DudeConfig.Programmer)))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.DudeConfig.ExtractPath));

                    await DownloadZipAndExtractAsync(Program.DudeConfig.DownloadUrl, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Program.DudeConfig.ExtractPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{StringResource.ErrorProgrammerDownloadFailed} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }
            }

            this.checkBoxAVRDude.Checked = true;
            this.splashScreenStatus = SplashScreenLoadingStatus.Firmware;
            this.labelStatusText.Text = StringResource.MessageLoadingFirmware;

            Directory.CreateDirectory(Program.TempPath);

            try
            {
                await DownloadZipAndExtractAsync(Program.Firmware.DownloadUrl, Program.TempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{StringResource.ErrorFirmwareDownloadFailed} {ex.Message}", StringResource.MessageBoxErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
            this.checkBoxFirmware.Checked = true;

            this.splashScreenStatus = SplashScreenLoadingStatus.End;
            this.labelStatusText.Text = StringResource.MessageStartingApplication;
            this.DialogResult = DialogResult.OK;
        }

        private void timerLoad_Tick(object sender, EventArgs e) => this.incrementProgressBar(1);

        private void incrementProgressBar(int value)
        {
            switch (this.splashScreenStatus)
            {
                case SplashScreenLoadingStatus.Config:
                    if (this.progressBarStatus.Value >= 20)
                    {
                        return;
                    }
                    break;
                case SplashScreenLoadingStatus.AVRDude:
                    if (this.progressBarStatus.Value >= 40)
                    {
                        return;
                    }
                    break;
                case SplashScreenLoadingStatus.Firmware:
                    if (this.progressBarStatus.Value >= 80)
                    {
                        return;
                    }
                    break;
                case SplashScreenLoadingStatus.End:
                    if (this.progressBarStatus.Value >= 100)
                    {
                        this.timerLoad.Stop();
                        this.Close();
                    }
                    break;
                default:
                    return;
            }

            if (this.progressBarStatus.Value + value <= this.progressBarStatus.Maximum)
            {
                this.progressBarStatus.Increment(value);
            }
            else
            {
                this.progressBarStatus.Value = this.progressBarStatus.Maximum;
            }
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
    }
}
