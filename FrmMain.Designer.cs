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
            btnHealthPup = new Button();
            btnHalfPup = new Button();
            btnStopTimePup = new Button();
            tblAnswerPanel = new TableLayoutPanel();
            btnAnswer4 = new Button();
            btnAnswer3 = new Button();
            btnAnswer1 = new Button();
            btnAnswer2 = new Button();
            lblQuestion = new Label();
            pnlTimer = new Panel();
            lblPlayerHP = new Label();
            lblEnemyHP = new Label();
            btnMute = new Button();
            btnEN = new Button();
            btnHU = new Button();
            lblGameOver = new Label();
            btnBackToMenu = new Button();
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).BeginInit();
            tblQuestionPanel.SuspendLayout();
            tblPowerPanel.SuspendLayout();
            tblAnswerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Brush Script MT", 101.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.ButtonHighlight;
            lblTitle.Location = new Point(360, 63);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(952, 207);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Riddle Raiders";
            // 
            // btnPlay
            // 
            btnPlay.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnPlay.FlatStyle = FlatStyle.Popup;
            btnPlay.Font = new System.Drawing.Font("Brush Script MT", 48F, FontStyle.Italic);
            btnPlay.Location = new Point(605, 453);
            btnPlay.Margin = new Padding(3, 4, 3, 4);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(414, 127);
            btnPlay.TabIndex = 1;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Font = new System.Drawing.Font("Brush Script MT", 48F, FontStyle.Italic);
            btnExit.Location = new Point(605, 609);
            btnExit.Margin = new Padding(3, 4, 3, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(414, 127);
            btnExit.TabIndex = 2;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.Transparent;
            lblVersion.Font = new System.Drawing.Font("Bahnschrift", 36F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblVersion.ForeColor = Color.White;
            lblVersion.Location = new Point(14, 931);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(398, 81);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "v. 1.0.0.7";
            // 
            // pbxEnemy
            // 
            pbxEnemy.BackColor = Color.Transparent;
            pbxEnemy.Image = (System.Drawing.Image)resources.GetObject("pbxEnemy.Image");
            pbxEnemy.Location = new Point(1025, 712);
            pbxEnemy.Margin = new Padding(3, 4, 3, 4);
            pbxEnemy.Name = "pbxEnemy";
            pbxEnemy.Size = new Size(183, 247);
            pbxEnemy.TabIndex = 5;
            pbxEnemy.TabStop = false;
            // 
            // pbxPlayer
            // 
            pbxPlayer.BackColor = Color.Transparent;
            pbxPlayer.Image = (System.Drawing.Image)resources.GetObject("pbxPlayer.Image");
            pbxPlayer.Location = new Point(446, 635);
            pbxPlayer.Margin = new Padding(3, 4, 3, 4);
            pbxPlayer.Name = "pbxPlayer";
            pbxPlayer.Size = new Size(152, 324);
            pbxPlayer.TabIndex = 4;
            pbxPlayer.TabStop = false;
            // 
            // rtbChat
            // 
            rtbChat.BackColor = Color.White;
            rtbChat.Enabled = false;
            rtbChat.Font = new System.Drawing.Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            rtbChat.ForeColor = Color.Black;
            rtbChat.Location = new Point(576, 468);
            rtbChat.Margin = new Padding(3, 4, 3, 4);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(294, 201);
            rtbChat.TabIndex = 6;
            rtbChat.Text = "";
            rtbChat.Visible = false;
            // 
            // tblQuestionPanel
            // 
            tblQuestionPanel.BackColor = Color.FromArgb(171, 136, 109);
            tblQuestionPanel.ColumnCount = 1;
            tblQuestionPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblQuestionPanel.Controls.Add(tblPowerPanel, 0, 1);
            tblQuestionPanel.Controls.Add(tblAnswerPanel, 0, 2);
            tblQuestionPanel.Controls.Add(lblQuestion, 0, 0);
            tblQuestionPanel.Controls.Add(pnlTimer, 0, 3);
            tblQuestionPanel.Location = new Point(298, 41);
            tblQuestionPanel.Margin = new Padding(3, 4, 3, 4);
            tblQuestionPanel.Name = "tblQuestionPanel";
            tblQuestionPanel.RowCount = 4;
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tblQuestionPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tblQuestionPanel.Size = new Size(1030, 368);
            tblQuestionPanel.TabIndex = 7;
            tblQuestionPanel.Visible = false;
            // 
            // tblPowerPanel
            // 
            tblPowerPanel.ColumnCount = 3;
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tblPowerPanel.Controls.Add(btnHealthPup, 2, 0);
            tblPowerPanel.Controls.Add(btnHalfPup, 1, 0);
            tblPowerPanel.Controls.Add(btnStopTimePup, 0, 0);
            tblPowerPanel.Location = new Point(3, 96);
            tblPowerPanel.Margin = new Padding(3, 4, 3, 4);
            tblPowerPanel.Name = "tblPowerPanel";
            tblPowerPanel.RowCount = 1;
            tblPowerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPowerPanel.Size = new Size(1023, 65);
            tblPowerPanel.TabIndex = 0;
            // 
            // btnHealthPup
            // 
            btnHealthPup.BackColor = Color.FromArgb(214, 192, 179);
            btnHealthPup.FlatStyle = FlatStyle.Flat;
            btnHealthPup.Font = new System.Drawing.Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnHealthPup.ForeColor = Color.FromArgb(73, 54, 40);
            btnHealthPup.Location = new Point(685, 4);
            btnHealthPup.Margin = new Padding(3, 4, 3, 4);
            btnHealthPup.Name = "btnHealthPup";
            btnHealthPup.Size = new Size(334, 57);
            btnHealthPup.TabIndex = 2;
            btnHealthPup.Text = "+2 Health";
            btnHealthPup.UseVisualStyleBackColor = false;
            // 
            // btnHalfPup
            // 
            btnHalfPup.BackColor = Color.FromArgb(214, 192, 179);
            btnHalfPup.FlatStyle = FlatStyle.Flat;
            btnHalfPup.Font = new System.Drawing.Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnHalfPup.ForeColor = Color.FromArgb(73, 54, 40);
            btnHalfPup.Location = new Point(344, 4);
            btnHalfPup.Margin = new Padding(3, 4, 3, 4);
            btnHalfPup.Name = "btnHalfPup";
            btnHalfPup.Size = new Size(334, 57);
            btnHalfPup.TabIndex = 1;
            btnHalfPup.Text = "Half Answers";
            btnHalfPup.UseVisualStyleBackColor = false;
            // 
            // btnStopTimePup
            // 
            btnStopTimePup.BackColor = Color.FromArgb(214, 192, 179);
            btnStopTimePup.FlatStyle = FlatStyle.Flat;
            btnStopTimePup.Font = new System.Drawing.Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            btnStopTimePup.ForeColor = Color.FromArgb(73, 54, 40);
            btnStopTimePup.Location = new Point(3, 4);
            btnStopTimePup.Margin = new Padding(3, 4, 3, 4);
            btnStopTimePup.Name = "btnStopTimePup";
            btnStopTimePup.Size = new Size(334, 57);
            btnStopTimePup.TabIndex = 0;
            btnStopTimePup.Text = "Stop Time (5s)";
            btnStopTimePup.UseVisualStyleBackColor = false;
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
            tblAnswerPanel.Location = new Point(3, 169);
            tblAnswerPanel.Margin = new Padding(3, 4, 3, 4);
            tblAnswerPanel.Name = "tblAnswerPanel";
            tblAnswerPanel.RowCount = 2;
            tblAnswerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnswerPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnswerPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tblAnswerPanel.Size = new Size(1023, 157);
            tblAnswerPanel.TabIndex = 1;
            // 
            // btnAnswer4
            // 
            btnAnswer4.BackColor = Color.FromArgb(214, 192, 179);
            btnAnswer4.FlatStyle = FlatStyle.Flat;
            btnAnswer4.ForeColor = Color.FromArgb(73, 54, 40);
            btnAnswer4.Location = new Point(514, 82);
            btnAnswer4.Margin = new Padding(3, 4, 3, 4);
            btnAnswer4.Name = "btnAnswer4";
            btnAnswer4.Size = new Size(504, 71);
            btnAnswer4.TabIndex = 3;
            btnAnswer4.Text = "Answer4";
            btnAnswer4.UseVisualStyleBackColor = false;
            // 
            // btnAnswer3
            // 
            btnAnswer3.BackColor = Color.FromArgb(214, 192, 179);
            btnAnswer3.FlatStyle = FlatStyle.Flat;
            btnAnswer3.ForeColor = Color.FromArgb(73, 54, 40);
            btnAnswer3.Location = new Point(3, 82);
            btnAnswer3.Margin = new Padding(3, 4, 3, 4);
            btnAnswer3.Name = "btnAnswer3";
            btnAnswer3.Size = new Size(504, 71);
            btnAnswer3.TabIndex = 2;
            btnAnswer3.Text = "Answer3";
            btnAnswer3.UseVisualStyleBackColor = false;
            // 
            // btnAnswer1
            // 
            btnAnswer1.BackColor = Color.FromArgb(214, 192, 179);
            btnAnswer1.FlatStyle = FlatStyle.Flat;
            btnAnswer1.ForeColor = Color.FromArgb(73, 54, 40);
            btnAnswer1.Location = new Point(3, 4);
            btnAnswer1.Margin = new Padding(3, 4, 3, 4);
            btnAnswer1.Name = "btnAnswer1";
            btnAnswer1.Size = new Size(504, 69);
            btnAnswer1.TabIndex = 0;
            btnAnswer1.Text = "Answer1";
            btnAnswer1.UseVisualStyleBackColor = false;
            // 
            // btnAnswer2
            // 
            btnAnswer2.BackColor = Color.FromArgb(214, 192, 179);
            btnAnswer2.FlatStyle = FlatStyle.Flat;
            btnAnswer2.ForeColor = Color.FromArgb(73, 54, 40);
            btnAnswer2.Location = new Point(514, 4);
            btnAnswer2.Margin = new Padding(3, 4, 3, 4);
            btnAnswer2.Name = "btnAnswer2";
            btnAnswer2.Size = new Size(504, 69);
            btnAnswer2.TabIndex = 1;
            btnAnswer2.Text = "Answer2";
            btnAnswer2.UseVisualStyleBackColor = false;
            // 
            // lblQuestion
            // 
            lblQuestion.Anchor = AnchorStyles.None;
            lblQuestion.AutoSize = true;
            lblQuestion.Font = new System.Drawing.Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblQuestion.ForeColor = Color.FromArgb(73, 54, 40);
            lblQuestion.Location = new Point(390, 27);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(250, 37);
            lblQuestion.TabIndex = 2;
            lblQuestion.Text = "This is the question.";
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTimer
            // 
            pnlTimer.BackColor = Color.FromArgb(73, 54, 40);
            pnlTimer.Location = new Point(3, 334);
            pnlTimer.Margin = new Padding(3, 4, 3, 4);
            pnlTimer.Name = "pnlTimer";
            pnlTimer.Size = new Size(1023, 29);
            pnlTimer.TabIndex = 3;
            // 
            // lblPlayerHP
            // 
            lblPlayerHP.BackColor = Color.FromArgb(214, 192, 179);
            lblPlayerHP.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblPlayerHP.ForeColor = Color.FromArgb(73, 54, 40);
            lblPlayerHP.Location = new Point(-1, 963);
            lblPlayerHP.Name = "lblPlayerHP";
            lblPlayerHP.Size = new Size(192, 63);
            lblPlayerHP.TabIndex = 8;
            lblPlayerHP.Text = "player hp:";
            lblPlayerHP.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayerHP.Visible = false;
            // 
            // lblEnemyHP
            // 
            lblEnemyHP.BackColor = Color.FromArgb(214, 192, 179);
            lblEnemyHP.Font = new System.Drawing.Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblEnemyHP.ForeColor = Color.FromArgb(73, 54, 40);
            lblEnemyHP.Location = new Point(1369, 963);
            lblEnemyHP.Name = "lblEnemyHP";
            lblEnemyHP.Size = new Size(192, 63);
            lblEnemyHP.TabIndex = 9;
            lblEnemyHP.Text = "enemy hp:";
            lblEnemyHP.TextAlign = ContentAlignment.MiddleCenter;
            lblEnemyHP.Visible = false;
            // 
            // btnMute
            // 
            btnMute.BackColor = Color.White;
            btnMute.FlatStyle = FlatStyle.Popup;
            btnMute.Image = (System.Drawing.Image)resources.GetObject("btnMute.Image");
            btnMute.Location = new Point(1479, 928);
            btnMute.Margin = new Padding(3, 4, 3, 4);
            btnMute.Name = "btnMute";
            btnMute.Size = new Size(65, 65);
            btnMute.TabIndex = 10;
            btnMute.UseVisualStyleBackColor = false;
            // 
            // btnEN
            // 
            btnEN.Image = (System.Drawing.Image)resources.GetObject("btnEN.Image");
            btnEN.Location = new Point(1479, 856);
            btnEN.Name = "btnEN";
            btnEN.Size = new Size(65, 65);
            btnEN.TabIndex = 11;
            btnEN.UseVisualStyleBackColor = true;
            // 
            // btnHU
            // 
            btnHU.Image = (System.Drawing.Image)resources.GetObject("btnHU.Image");
            btnHU.Location = new Point(1479, 785);
            btnHU.Name = "btnHU";
            btnHU.Size = new Size(65, 65);
            btnHU.TabIndex = 12;
            btnHU.UseVisualStyleBackColor = true;
            // 
            // lblGameOver
            // 
            lblGameOver.BackColor = Color.Transparent;
            lblGameOver.Font = new System.Drawing.Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblGameOver.ForeColor = Color.FromArgb(106, 130, 102);
            lblGameOver.Location = new Point(11, 97);
            lblGameOver.Name = "lblGameOver";
            lblGameOver.Size = new Size(1537, 224);
            lblGameOver.TabIndex = 13;
            lblGameOver.Text = "game over text";
            lblGameOver.TextAlign = ContentAlignment.MiddleCenter;
            lblGameOver.Visible = false;
            // 
            // btnBackToMenu
            // 
            btnBackToMenu.BackColor = Color.FromArgb(190, 255, 255, 255);
            btnBackToMenu.FlatStyle = FlatStyle.Popup;
            btnBackToMenu.Font = new System.Drawing.Font("Brush Script MT", 31.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            btnBackToMenu.Location = new Point(1135, 885);
            btnBackToMenu.Margin = new Padding(3, 4, 3, 4);
            btnBackToMenu.Name = "btnBackToMenu";
            btnBackToMenu.Size = new Size(414, 127);
            btnBackToMenu.TabIndex = 14;
            btnBackToMenu.Text = "Back To Main Menu";
            btnBackToMenu.UseVisualStyleBackColor = false;
            btnBackToMenu.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1561, 1024);
            Controls.Add(btnBackToMenu);
            Controls.Add(lblGameOver);
            Controls.Add(btnHU);
            Controls.Add(btnEN);
            Controls.Add(btnMute);
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
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Riddle Raiders";
            ((System.ComponentModel.ISupportInitialize)pbxEnemy).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).EndInit();
            tblQuestionPanel.ResumeLayout(false);
            tblQuestionPanel.PerformLayout();
            tblPowerPanel.ResumeLayout(false);
            tblAnswerPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Button btnPlay;
        private Button btnExit;
        private Label lblVersion;
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
        private Button btnHealthPup;
        private Button btnHalfPup;
        private Button btnStopTimePup;
        private Button btnMute;
        private Button btnEN;
        private Button btnHU;
        private Label lblGameOver;
        private Button btnBackToMenu;
    }
}
