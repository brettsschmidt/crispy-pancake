namespace FoamingOJ
{
    partial class Snatch
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StoryBox = new System.Windows.Forms.TextBox();
            this.OptionOne_BTN = new System.Windows.Forms.Button();
            this.OptionTwo_BTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 27);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(665, 389);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.skinToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1161, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // skinToolStripMenuItem
            // 
            this.skinToolStripMenuItem.Name = "skinToolStripMenuItem";
            this.skinToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.skinToolStripMenuItem.Text = "Skin";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // StoryBox
            // 
            this.StoryBox.Location = new System.Drawing.Point(683, 27);
            this.StoryBox.Multiline = true;
            this.StoryBox.Name = "StoryBox";
            this.StoryBox.Size = new System.Drawing.Size(466, 389);
            this.StoryBox.TabIndex = 2;
            this.StoryBox.TextChanged += new System.EventHandler(this.StoryBox_TextChanged);
            // 
            // OptionOne_BTN
            // 
            this.OptionOne_BTN.Location = new System.Drawing.Point(329, 453);
            this.OptionOne_BTN.Name = "OptionOne_BTN";
            this.OptionOne_BTN.Size = new System.Drawing.Size(277, 89);
            this.OptionOne_BTN.TabIndex = 3;
            this.OptionOne_BTN.Text = "Option One";
            this.OptionOne_BTN.UseVisualStyleBackColor = true;
            this.OptionOne_BTN.Click += new System.EventHandler(this.OptionOne_BTN_Click);
            // 
            // OptionTwo_BTN
            // 
            this.OptionTwo_BTN.Location = new System.Drawing.Point(624, 453);
            this.OptionTwo_BTN.Name = "OptionTwo_BTN";
            this.OptionTwo_BTN.Size = new System.Drawing.Size(277, 89);
            this.OptionTwo_BTN.TabIndex = 4;
            this.OptionTwo_BTN.Text = "Option Two";
            this.OptionTwo_BTN.UseVisualStyleBackColor = true;
            this.OptionTwo_BTN.Click += new System.EventHandler(this.OptionTwo_BTN_Click);
            // 
            // Snatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 580);
            this.Controls.Add(this.OptionTwo_BTN);
            this.Controls.Add(this.OptionOne_BTN);
            this.Controls.Add(this.StoryBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Snatch";
            this.Text = "Option One";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TextBox StoryBox;
        private System.Windows.Forms.Button OptionOne_BTN;
        private System.Windows.Forms.Button OptionTwo_BTN;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    }
}

