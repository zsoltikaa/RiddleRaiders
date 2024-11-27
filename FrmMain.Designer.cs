using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace RiddleRaiders
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblTitle = new Label();
            btnPlay = new Button();
            btnExit = new Button();
            lblVersion = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Rage Italic", 99.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(315, 47);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(805, 168);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Riddle Raiders";
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnPlay.FlatStyle = FlatStyle.Popup;
            btnPlay.Font = new System.Drawing.Font("Harrington", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlay.Location = new Point(529, 340);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(362, 95);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Font = new System.Drawing.Font("Harrington", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(529, 457);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(362, 95);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new System.Drawing.Font("Bahnschrift", 36F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(12, 698);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(184, 61);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "v. 1.0.0.1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1366, 768);
            Controls.Add(lblVersion);
            Controls.Add(btnExit);
            Controls.Add(btnPlay);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Riddle Raiders";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnPlay;
        private Button btnExit;
        private Label lblVersion;
    }
}
