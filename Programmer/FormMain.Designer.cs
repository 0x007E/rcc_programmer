namespace RaGae.Projects.RCC.Programmer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            linkLabelGitHub = new LinkLabel();
            pictureBoxRCC = new PictureBox();
            groupBoxRCC = new GroupBox();
            buttonLED2 = new Button();
            buttonLED1 = new Button();
            groupBoxInfo = new GroupBox();
            labelClick = new Label();
            groupBoxIntensity = new GroupBox();
            trackBarIntensity = new TrackBar();
            groupBoxProgram = new GroupBox();
            progressBarStatus = new ProgressBar();
            buttonProgram = new Button();
            comboBoxPort = new ComboBox();
            labelVersion = new Label();
            colorDialogLED = new ColorDialog();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRCC).BeginInit();
            groupBoxRCC.SuspendLayout();
            groupBoxInfo.SuspendLayout();
            groupBoxIntensity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensity).BeginInit();
            groupBoxProgram.SuspendLayout();
            SuspendLayout();
            // 
            // linkLabelGitHub
            // 
            resources.ApplyResources(linkLabelGitHub, "linkLabelGitHub");
            linkLabelGitHub.Name = "linkLabelGitHub";
            linkLabelGitHub.TabStop = true;
            linkLabelGitHub.LinkClicked += linkLabelGitHub_LinkClicked;
            // 
            // pictureBoxRCC
            // 
            resources.ApplyResources(pictureBoxRCC, "pictureBoxRCC");
            pictureBoxRCC.BackgroundImage = Resources.ImagePCB;
            pictureBoxRCC.Name = "pictureBoxRCC";
            pictureBoxRCC.TabStop = false;
            // 
            // groupBoxRCC
            // 
            groupBoxRCC.Controls.Add(buttonLED2);
            groupBoxRCC.Controls.Add(buttonLED1);
            groupBoxRCC.Controls.Add(pictureBoxRCC);
            resources.ApplyResources(groupBoxRCC, "groupBoxRCC");
            groupBoxRCC.Name = "groupBoxRCC";
            groupBoxRCC.TabStop = false;
            // 
            // buttonLED2
            // 
            buttonLED2.BackColor = Color.White;
            buttonLED2.ForeColor = Color.Transparent;
            resources.ApplyResources(buttonLED2, "buttonLED2");
            buttonLED2.Name = "buttonLED2";
            buttonLED2.UseVisualStyleBackColor = false;
            buttonLED2.Click += buttonLED_Click;
            // 
            // buttonLED1
            // 
            buttonLED1.BackColor = Color.White;
            buttonLED1.ForeColor = Color.Transparent;
            resources.ApplyResources(buttonLED1, "buttonLED1");
            buttonLED1.Name = "buttonLED1";
            buttonLED1.UseVisualStyleBackColor = false;
            buttonLED1.Click += buttonLED_Click;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(labelClick);
            resources.ApplyResources(groupBoxInfo, "groupBoxInfo");
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.TabStop = false;
            // 
            // labelClick
            // 
            resources.ApplyResources(labelClick, "labelClick");
            labelClick.Name = "labelClick";
            // 
            // groupBoxIntensity
            // 
            groupBoxIntensity.Controls.Add(trackBarIntensity);
            resources.ApplyResources(groupBoxIntensity, "groupBoxIntensity");
            groupBoxIntensity.Name = "groupBoxIntensity";
            groupBoxIntensity.TabStop = false;
            // 
            // trackBarIntensity
            // 
            resources.ApplyResources(trackBarIntensity, "trackBarIntensity");
            trackBarIntensity.Maximum = 14;
            trackBarIntensity.Minimum = 1;
            trackBarIntensity.Name = "trackBarIntensity";
            trackBarIntensity.TickStyle = TickStyle.Both;
            trackBarIntensity.Value = 10;
            trackBarIntensity.Scroll += trackBarIntensity_Scroll;
            // 
            // groupBoxProgram
            // 
            groupBoxProgram.Controls.Add(progressBarStatus);
            groupBoxProgram.Controls.Add(buttonProgram);
            groupBoxProgram.Controls.Add(comboBoxPort);
            resources.ApplyResources(groupBoxProgram, "groupBoxProgram");
            groupBoxProgram.Name = "groupBoxProgram";
            groupBoxProgram.TabStop = false;
            // 
            // progressBarStatus
            // 
            resources.ApplyResources(progressBarStatus, "progressBarStatus");
            progressBarStatus.Name = "progressBarStatus";
            // 
            // buttonProgram
            // 
            resources.ApplyResources(buttonProgram, "buttonProgram");
            buttonProgram.Name = "buttonProgram";
            buttonProgram.UseVisualStyleBackColor = true;
            buttonProgram.Click += buttonProgram_Click;
            // 
            // comboBoxPort
            // 
            comboBoxPort.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPort.FormattingEnabled = true;
            resources.ApplyResources(comboBoxPort, "comboBoxPort");
            comboBoxPort.Name = "comboBoxPort";
            // 
            // labelVersion
            // 
            resources.ApplyResources(labelVersion, "labelVersion");
            labelVersion.Name = "labelVersion";
            // 
            // colorDialogLED
            // 
            colorDialogLED.Color = Color.Yellow;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelVersion);
            Controls.Add(groupBoxProgram);
            Controls.Add(groupBoxIntensity);
            Controls.Add(groupBoxInfo);
            Controls.Add(linkLabelGitHub);
            Controls.Add(groupBoxRCC);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxRCC).EndInit();
            groupBoxRCC.ResumeLayout(false);
            groupBoxInfo.ResumeLayout(false);
            groupBoxIntensity.ResumeLayout(false);
            groupBoxIntensity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensity).EndInit();
            groupBoxProgram.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private LinkLabel linkLabelGitHub;
        private PictureBox pictureBoxRCC;
        private GroupBox groupBoxRCC;
        private GroupBox groupBoxInfo;
        private Label labelClick;
        private Button buttonLED1;
        private GroupBox groupBoxIntensity;
        private GroupBox groupBoxProgram;
        private ComboBox comboBoxPort;
        private Button buttonProgram;
        private ProgressBar progressBarStatus;
        private TrackBar trackBarIntensity;
        private Button buttonLED2;
        private Label labelVersion;
        private ColorDialog colorDialogLED;
    }
}
