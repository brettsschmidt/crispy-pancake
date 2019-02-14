namespace FoamingOJ
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.About = new System.Windows.Forms.Label();
            this.HelpLink = new System.Windows.Forms.Label();
            this.Version = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.AboutTextBox = new System.Windows.Forms.TextBox();
            this.EXT_BTN = new System.Windows.Forms.Button();
            this.HelpLinkString = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, -181);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(764, 906);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // About
            // 
            this.About.AutoSize = true;
            this.About.Location = new System.Drawing.Point(763, 254);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(35, 13);
            this.About.TabIndex = 4;
            this.About.Text = "About";
            // 
            // HelpLink
            // 
            this.HelpLink.AutoSize = true;
            this.HelpLink.Location = new System.Drawing.Point(760, 226);
            this.HelpLink.Name = "HelpLink";
            this.HelpLink.Size = new System.Drawing.Size(35, 13);
            this.HelpLink.TabIndex = 5;
            this.HelpLink.Text = "Help :";
            // 
            // Version
            // 
            this.Version.AutoSize = true;
            this.Version.Location = new System.Drawing.Point(760, 203);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(45, 13);
            this.Version.TabIndex = 6;
            this.Version.Text = "Version:";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(801, 203);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(56, 13);
            this.VersionLabel.TabIndex = 9;
            this.VersionLabel.Text = "0.00.001A";
            // 
            // AboutTextBox
            // 
            this.AboutTextBox.Location = new System.Drawing.Point(765, 270);
            this.AboutTextBox.Multiline = true;
            this.AboutTextBox.Name = "AboutTextBox";
            this.AboutTextBox.Size = new System.Drawing.Size(428, 337);
            this.AboutTextBox.TabIndex = 10;
            // 
            // EXT_BTN
            // 
            this.EXT_BTN.Location = new System.Drawing.Point(1118, 613);
            this.EXT_BTN.Name = "EXT_BTN";
            this.EXT_BTN.Size = new System.Drawing.Size(75, 23);
            this.EXT_BTN.TabIndex = 11;
            this.EXT_BTN.Text = "Exit";
            this.EXT_BTN.UseVisualStyleBackColor = true;
            this.EXT_BTN.Click += new System.EventHandler(this.EXT_BTN_Click);
            // 
            // HelpLinkString
            // 
            this.HelpLinkString.AutoSize = true;
            this.HelpLinkString.Location = new System.Drawing.Point(801, 226);
            this.HelpLinkString.Name = "HelpLinkString";
            this.HelpLinkString.Size = new System.Drawing.Size(273, 13);
            this.HelpLinkString.TabIndex = 12;
            this.HelpLinkString.TabStop = true;
            this.HelpLinkString.Text = "https://github.com/brettsschmidt/crispy-pancake/issues";
            this.HelpLinkString.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 641);
            this.Controls.Add(this.HelpLinkString);
            this.Controls.Add(this.EXT_BTN);
            this.Controls.Add(this.AboutTextBox);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.HelpLink);
            this.Controls.Add(this.About);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Help";
            this.Text = "Help";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label About;
        private System.Windows.Forms.Label HelpLink;
        private System.Windows.Forms.Label Version;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.TextBox AboutTextBox;
        private System.Windows.Forms.Button EXT_BTN;
        private System.Windows.Forms.LinkLabel HelpLinkString;
    }
}