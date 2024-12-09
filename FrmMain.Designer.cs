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
            tblQuestionPanel = new TableLayoutPanel();
            tblPowerPanel = new TableLayoutPanel();
            tblAnswerPanel = new TableLayoutPanel();
            btnAnswer4 = new Button();
            btnAnswer3 = new Button();
            btnAnswer1 = new Button();
            btnAnswer2 = new Button();
            lblQuestion = new Label();
            pnlTimer = new Panel();
            lblPlayerHP = new Label();
            lblEnemyHP = new Label();
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).BeginInit();
            tblQuestionPanel.SuspendLayout();
            tblAnswerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Bell MT", 100.200005F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(315, 47);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(824, 153);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Riddle Raiders";
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnPlay.FlatStyle = FlatStyle.Popup;
            btnPlay.Font = new System.Drawing.Font("Viner Hand ITC", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            btnExit.Font = new System.Drawing.Font("Viner Hand ITC", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            lblVersion.Size = new Size(193, 61);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "v. 1.0.0.2";
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
            rtbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbChat.Location = new Point(504, 351);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(258, 152);
            rtbChat.TabIndex = 6;
            rtbChat.Text = "";
            rtbChat.Visible = false;
            // 
            // tblQuestionPanel
            // 
            tblQuestionPanel.ColumnCount = 1;
            tblQuestionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblQuestionPanel.Controls.Add(tblPowerPanel, 0, 1);
            tblQuestionPanel.Controls.Add(tblAnswerPanel, 0, 2);
            tblQuestionPanel.Controls.Add(lblQuestion, 0, 0);
            tblQuestionPanel.Controls.Add(pnlTimer, 0, 3);
            tblQuestionPanel.Location = new Point(261, 31);
            tblQuestionPanel.Name = "tblQuestionPanel";
            tblQuestionPanel.RowCount = 4;
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tblQuestionPanel.Size = new Size(901, 276);
            tblQuestionPanel.TabIndex = 7;
            tblQuestionPanel.Visible = false;
            // 
            // tblPowerPanel
            // 
            tblPowerPanel.ColumnCount = 3;
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.Location = new Point(3, 72);
            tblPowerPanel.Name = "tblPowerPanel";
            tblPowerPanel.RowCount = 1;
            tblPowerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPowerPanel.Size = new Size(895, 49);
            tblPowerPanel.TabIndex = 0;
            // 
            // tblAnswerPanel
            // 
            tblAnswerPanel.ColumnCount = 2;
            tblAnswerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblAnswerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblAnswerPanel.Controls.Add(btnAnswer4, 1, 1);
            tblAnswerPanel.Controls.Add(btnAnswer3, 0, 1);
            tblAnswerPanel.Controls.Add(btnAnswer1, 0, 0);
            tblAnswerPanel.Controls.Add(btnAnswer2, 1, 0);
            tblAnswerPanel.Location = new Point(3, 127);
            tblAnswerPanel.Name = "tblAnswerPanel";
            tblAnswerPanel.RowCount = 2;
            tblAnswerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnswerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnswerPanel.Size = new Size(895, 118);
            tblAnswerPanel.TabIndex = 1;
            // 
            // btnAnswer4
            // 
            btnAnswer4.BackColor = Color.Gainsboro;
            btnAnswer4.Location = new Point(450, 62);
            btnAnswer4.Name = "btnAnswer4";
            btnAnswer4.Size = new Size(441, 53);
            btnAnswer4.TabIndex = 3;
            btnAnswer4.Text = "Answer4";
            btnAnswer4.UseVisualStyleBackColor = false;
            // 
            // btnAnswer3
            // 
            btnAnswer3.BackColor = Color.Gainsboro;
            btnAnswer3.Location = new Point(3, 62);
            btnAnswer3.Name = "btnAnswer3";
            btnAnswer3.Size = new Size(441, 53);
            btnAnswer3.TabIndex = 2;
            btnAnswer3.Text = "Answer3";
            btnAnswer3.UseVisualStyleBackColor = false;
            // 
            // btnAnswer1
            // 
            btnAnswer1.BackColor = Color.Gainsboro;
            btnAnswer1.Location = new Point(3, 3);
            btnAnswer1.Name = "btnAnswer1";
            btnAnswer1.Size = new Size(441, 53);
            btnAnswer1.TabIndex = 0;
            btnAnswer1.Text = "Answer1";
            btnAnswer1.UseVisualStyleBackColor = false;
            // 
            // btnAnswer2
            // 
            btnAnswer2.BackColor = Color.Gainsboro;
            btnAnswer2.Location = new Point(450, 3);
            btnAnswer2.Name = "btnAnswer2";
            btnAnswer2.Size = new Size(441, 53);
            btnAnswer2.TabIndex = 1;
            btnAnswer2.Text = "Answer2";
            btnAnswer2.UseVisualStyleBackColor = false;
            // 
            // lblQuestion
            // 
            lblQuestion.Anchor = AnchorStyles.None;
            lblQuestion.AutoSize = true;
            lblQuestion.Font = new System.Drawing.Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblQuestion.Location = new Point(352, 19);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(197, 30);
            lblQuestion.TabIndex = 2;
            lblQuestion.Text = "This is the question.";
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTimer
            // 
            pnlTimer.BackColor = Color.DarkGray;
            pnlTimer.Location = new Point(3, 251);
            pnlTimer.Name = "pnlTimer";
            pnlTimer.Size = new Size(895, 22);
            pnlTimer.TabIndex = 3;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.BackColor = Color.FromArgb(200, 255, 255, 255);
            lblPlayerHP.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblPlayerHP.Location = new Point(-1, 722);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(168, 47);
            lblPlayerHP.TabIndex = 8;
            lblPlayerHP.Text = "player hp:";
            lblPlayerHP.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayerHP.Visible = false;
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.BackColor = Color.FromArgb(200, 255, 255, 255);
            lblEnemyHP.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblEnemyHP.Location = new Point(1198, 722);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(168, 47);
            lblEnemyHP.TabIndex = 9;
            lblEnemyHP.Text = "enemy hp:";
            lblEnemyHP.TextAlign = ContentAlignment.MiddleCenter;
            lblEnemyHP.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1366, 768);
            Controls.Add(lblEnemyHP);
            Controls.Add(lblPlayerHP);
            Controls.Add(tblQuestionPanel);
            Controls.Add(rtbChat);
            Controls.Add(pbxEnemy);
            Controls.Add(pbxPlayer);
            Controls.Add(lblVersion);
            Controls.Add(btnExit);
            Controls.Add(btnPlay);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "Riddle Raiders";
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).EndInit();
            tblQuestionPanel.ResumeLayout(false);
            tblQuestionPanel.PerformLayout();
            tblAnswerPanel.ResumeLayout(false);
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
        private TableLayoutPanel tblQuestionPanel;
        private TableLayoutPanel tblPowerPanel;
        private TableLayoutPanel tblAnswerPanel;
        private Label lblQuestion;
        private Button btnAnswer4;
        private Button btnAnswer3;
        private Button btnAnswer1;
        private Button btnAnswer2;
        private Panel pnlTimer;
        private Label lblPlayerHP;
        private Label lblEnemyHP;
    }
}
