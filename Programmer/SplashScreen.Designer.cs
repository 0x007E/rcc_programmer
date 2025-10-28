namespace RaGae.Projects.RCC
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labelTitle = new Label();
            progressBarStatus = new ProgressBar();
            checkBoxAVRDude = new CheckBox();
            timerLoad = new System.Windows.Forms.Timer(components);
            checkBoxFirmware = new CheckBox();
            labelStatus = new Label();
            labelStatusText = new Label();
            labelVersion = new Label();
            linkLabelGitHub = new LinkLabel();
            pictureBoxProgrammer = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProgrammer).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(41, 29);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(274, 25);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "RCC - Programming Interface";
            // 
            // progressBarStatus
            // 
            progressBarStatus.Location = new Point(41, 187);
            progressBarStatus.Name = "progressBarStatus";
            progressBarStatus.Size = new Size(300, 30);
            progressBarStatus.TabIndex = 1;
            // 
            // checkBoxAVRDude
            // 
            checkBoxAVRDude.AutoCheck = false;
            checkBoxAVRDude.AutoSize = true;
            checkBoxAVRDude.Location = new Point(50, 71);
            checkBoxAVRDude.Name = "checkBoxAVRDude";
            checkBoxAVRDude.Size = new Size(80, 19);
            checkBoxAVRDude.TabIndex = 2;
            checkBoxAVRDude.Text = "AVR-Dude";
            checkBoxAVRDude.UseVisualStyleBackColor = true;
            // 
            // timerLoad
            // 
            timerLoad.Tick += timerLoad_Tick;
            // 
            // checkBoxFirmware
            // 
            checkBoxFirmware.AutoCheck = false;
            checkBoxFirmware.AutoSize = true;
            checkBoxFirmware.Location = new Point(50, 96);
            checkBoxFirmware.Name = "checkBoxFirmware";
            checkBoxFirmware.Size = new Size(103, 19);
            checkBoxFirmware.TabIndex = 3;
            checkBoxFirmware.Text = "RCC-Firmware";
            checkBoxFirmware.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(50, 158);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(42, 15);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Status:";
            // 
            // labelStatusText
            // 
            labelStatusText.AutoSize = true;
            labelStatusText.Location = new Point(98, 158);
            labelStatusText.Name = "labelStatusText";
            labelStatusText.Size = new Size(19, 15);
            labelStatusText.TabIndex = 5;
            labelStatusText.Text = "    ";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.ImeMode = ImeMode.NoControl;
            labelVersion.Location = new Point(12, 226);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(66, 15);
            labelVersion.TabIndex = 9;
            labelVersion.Text = "Version: 1.0";
            // 
            // linkLabelGitHub
            // 
            linkLabelGitHub.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            linkLabelGitHub.AutoSize = true;
            linkLabelGitHub.ImeMode = ImeMode.NoControl;
            linkLabelGitHub.Location = new Point(188, 226);
            linkLabelGitHub.Margin = new Padding(1, 0, 1, 0);
            linkLabelGitHub.Name = "linkLabelGitHub";
            linkLabelGitHub.Size = new Size(202, 15);
            linkLabelGitHub.TabIndex = 8;
            linkLabelGitHub.TabStop = true;
            linkLabelGitHub.Text = "github.com/0x007e/rcc_programmer";
            linkLabelGitHub.LinkClicked += linkLabelGitHub_LinkClicked;
            // 
            // pictureBoxProgrammer
            // 
            pictureBoxProgrammer.BackgroundImage = Resources.ImageAssembled;
            pictureBoxProgrammer.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBoxProgrammer.Location = new Point(241, 57);
            pictureBoxProgrammer.Name = "pictureBoxProgrammer";
            pictureBoxProgrammer.Size = new Size(100, 124);
            pictureBoxProgrammer.TabIndex = 10;
            pictureBoxProgrammer.TabStop = false;
            // 
            // SplashScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 250);
            ControlBox = false;
            Controls.Add(labelVersion);
            Controls.Add(linkLabelGitHub);
            Controls.Add(labelStatusText);
            Controls.Add(labelStatus);
            Controls.Add(checkBoxFirmware);
            Controls.Add(checkBoxAVRDude);
            Controls.Add(progressBarStatus);
            Controls.Add(labelTitle);
            Controls.Add(pictureBoxProgrammer);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(400, 250);
            MinimizeBox = false;
            MinimumSize = new Size(400, 250);
            Name = "SplashScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SplashScreen";
            Load += SplashScreen_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxProgrammer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitle;
        private ProgressBar progressBarStatus;
        private CheckBox checkBoxAVRDude;
        private System.Windows.Forms.Timer timerLoad;
        private CheckBox checkBoxFirmware;
        private Label labelStatus;
        private Label labelStatusText;
        private Label labelVersion;
        private LinkLabel linkLabelGitHub;
        private PictureBox pictureBoxProgrammer;
    }
}