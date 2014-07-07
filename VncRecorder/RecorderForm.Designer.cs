namespace VncRecorder
{
    partial class RecorderForm
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
            this.components = new System.ComponentModel.Container();
            this.textBoxVncIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxStudentId = new System.Windows.Forms.TextBox();
            this.buttonStartRecord = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.rd = new VncSharp.RemoteDesktop();
            this.timerCapture = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownCapturePeriod = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.timerUpload = new System.Windows.Forms.Timer(this.components);
            this.labelFtpIp = new System.Windows.Forms.Label();
            this.textBoxFtpIp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonDownloadTestMaterial = new System.Windows.Forms.Button();
            this.buttonUploadYourTestWork = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCapturePeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxVncIp
            // 
            this.textBoxVncIp.Location = new System.Drawing.Point(77, 552);
            this.textBoxVncIp.Name = "textBoxVncIp";
            this.textBoxVncIp.Size = new System.Drawing.Size(152, 20);
            this.textBoxVncIp.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 552);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(531, 551);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "1234";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 554);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 555);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Student ID";
            // 
            // textBoxStudentId
            // 
            this.textBoxStudentId.Location = new System.Drawing.Point(299, 552);
            this.textBoxStudentId.Name = "textBoxStudentId";
            this.textBoxStudentId.Size = new System.Drawing.Size(152, 20);
            this.textBoxStudentId.TabIndex = 6;
            // 
            // buttonStartRecord
            // 
            this.buttonStartRecord.Location = new System.Drawing.Point(644, 550);
            this.buttonStartRecord.Name = "buttonStartRecord";
            this.buttonStartRecord.Size = new System.Drawing.Size(75, 23);
            this.buttonStartRecord.TabIndex = 7;
            this.buttonStartRecord.Text = "Start Record";
            this.buttonStartRecord.UseVisualStyleBackColor = true;
            this.buttonStartRecord.Click += new System.EventHandler(this.buttonStartRecord_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(644, 581);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 8;
            this.buttonStop.Text = "Stop Record";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // rd
            // 
            this.rd.AutoScroll = true;
            this.rd.AutoScrollMinSize = new System.Drawing.Size(608, 427);
            this.rd.Location = new System.Drawing.Point(42, 5);
            this.rd.Name = "rd";
            this.rd.Size = new System.Drawing.Size(700, 525);
            this.rd.TabIndex = 0;
            // 
            // timerCapture
            // 
            this.timerCapture.Tick += new System.EventHandler(this.timerCapture_Tick);
            // 
            // numericUpDownCapturePeriod
            // 
            this.numericUpDownCapturePeriod.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCapturePeriod.Location = new System.Drawing.Point(125, 614);
            this.numericUpDownCapturePeriod.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCapturePeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCapturePeriod.Name = "numericUpDownCapturePeriod";
            this.numericUpDownCapturePeriod.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCapturePeriod.TabIndex = 9;
            this.numericUpDownCapturePeriod.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 616);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Capture Time in Sec";
            // 
            // timerUpload
            // 
            this.timerUpload.Interval = 1000;
            this.timerUpload.Tick += new System.EventHandler(this.timerUpload_Tick);
            // 
            // labelFtpIp
            // 
            this.labelFtpIp.AutoSize = true;
            this.labelFtpIp.Location = new System.Drawing.Point(16, 586);
            this.labelFtpIp.Name = "labelFtpIp";
            this.labelFtpIp.Size = new System.Drawing.Size(40, 13);
            this.labelFtpIp.TabIndex = 11;
            this.labelFtpIp.Text = "FTP IP";
            // 
            // textBoxFtpIp
            // 
            this.textBoxFtpIp.Location = new System.Drawing.Point(77, 586);
            this.textBoxFtpIp.Name = "textBoxFtpIp";
            this.textBoxFtpIp.Size = new System.Drawing.Size(152, 20);
            this.textBoxFtpIp.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 589);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Username: anonymous without password!";
            // 
            // buttonDownloadTestMaterial
            // 
            this.buttonDownloadTestMaterial.Location = new System.Drawing.Point(299, 611);
            this.buttonDownloadTestMaterial.Name = "buttonDownloadTestMaterial";
            this.buttonDownloadTestMaterial.Size = new System.Drawing.Size(152, 23);
            this.buttonDownloadTestMaterial.TabIndex = 14;
            this.buttonDownloadTestMaterial.Text = "Download Test Material";
            this.buttonDownloadTestMaterial.UseVisualStyleBackColor = true;
            this.buttonDownloadTestMaterial.Click += new System.EventHandler(this.buttonDownloadTestMaterial_Click);
            // 
            // buttonUploadYourTestWork
            // 
            this.buttonUploadYourTestWork.Location = new System.Drawing.Point(475, 611);
            this.buttonUploadYourTestWork.Name = "buttonUploadYourTestWork";
            this.buttonUploadYourTestWork.Size = new System.Drawing.Size(156, 23);
            this.buttonUploadYourTestWork.TabIndex = 15;
            this.buttonUploadYourTestWork.Text = "Upload Your Test Work";
            this.buttonUploadYourTestWork.UseVisualStyleBackColor = true;
            this.buttonUploadYourTestWork.Click += new System.EventHandler(this.buttonUploadYourTestWork_Click);
            // 
            // RecorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.buttonUploadYourTestWork);
            this.Controls.Add(this.buttonDownloadTestMaterial);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFtpIp);
            this.Controls.Add(this.labelFtpIp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownCapturePeriod);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStartRecord);
            this.Controls.Add(this.textBoxStudentId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxVncIp);
            this.Controls.Add(this.rd);
            this.Name = "RecorderForm";
            this.Text = "VNC recorder";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCapturePeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VncSharp.RemoteDesktop rd;
        private System.Windows.Forms.TextBox textBoxVncIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxStudentId;
        private System.Windows.Forms.Button buttonStartRecord;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timerCapture;
        private System.Windows.Forms.NumericUpDown numericUpDownCapturePeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timerUpload;
        private System.Windows.Forms.Label labelFtpIp;
        private System.Windows.Forms.TextBox textBoxFtpIp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonDownloadTestMaterial;
        private System.Windows.Forms.Button buttonUploadYourTestWork;
    }
}

