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
            pbxEnemy = new PictureBox();
            pbxPlayer = new PictureBox();
            rtbChat = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).BeginInit();
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
            // pbxEnemy
            // 
            pbxEnemy.BackColor = Color.Transparent;
            pbxEnemy.Image = (System.Drawing.Image)resources.GetObject("pbxEnemy.Image");
            pbxEnemy.Location = new Point(897, 534);
            pbxEnemy.Name = "pbxEnemy";
            pbxEnemy.Size = new Size(160, 185);
            pbxEnemy.TabIndex = 5;
            pbxEnemy.TabStop = false;
            // 
            // pbxPlayer
            // 
            pbxPlayer.BackColor = Color.Transparent;
            pbxPlayer.Image = (System.Drawing.Image)resources.GetObject("pbxPlayer.Image");
            pbxPlayer.Location = new Point(390, 476);
            pbxPlayer.Name = "pbxPlayer";
            pbxPlayer.Size = new Size(133, 243);
            pbxPlayer.TabIndex = 4;
            pbxPlayer.TabStop = false;
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.White;
            rtbChat.BorderStyle = BorderStyle.FixedSingle;
            rtbChat.Enabled = false;
            rtbChat.Font = new System.Drawing.Font("Monocraft", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbChat.Location = new Point(504, 351);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(258, 152);
            rtbChat.TabIndex = 6;
            rtbChat.Text = "";
            rtbChat.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1366, 768);
            Controls.Add(rtbChat);
            Controls.Add(pbxEnemy);
            Controls.Add(pbxPlayer);
            Controls.Add(lblVersion);
            Controls.Add(btnExit);
            Controls.Add(btnPlay);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Riddle Raiders";
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnPlay;
        private Button btnExit;
        private Label lblVersion;
        private PictureBox pictureBox1;
        private PictureBox pbxEnemy;
        private PictureBox pbxPlayer;
        private RichTextBox rtbChat;
    }
}
