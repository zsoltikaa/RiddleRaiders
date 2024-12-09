using AdventureQuiz;
using RiddleRaiders.Properties;
using Timer = System.Windows.Forms.Timer;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        private static Random rnd = new Random();

        private const int PLAYER_HEALTH = 5;

        private int level;
        private Scene currentScene;
        private List<Scene> sceneList = new List<Scene>();
        private string resourceDir;
        private Timer textTimer;
        private Timer questionTimer;
        private int currentCharIndex;
        private string text;
        private Player player;
        private List<Question> questionList = new List<Question>();
        private Question currentQuestion;
        private int originalTimerWidth;
        public Form1()
        {

            InitializeComponent();

            InitGame();

            originalTimerWidth = pnlTimer.Width;

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

            textTimer = new Timer();
            textTimer.Interval = 30;
            textTimer.Tick += TextTimerTick;

            questionTimer = new Timer();
            questionTimer.Interval = 5;
            questionTimer.Tick += QuestionTimerTick;

            btnAnswer1.Click += BtnAnswerClick;
            btnAnswer2.Click += BtnAnswerClick;
            btnAnswer3.Click += BtnAnswerClick;
            btnAnswer4.Click += BtnAnswerClick;

        }

        private async void BtnAnswerClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                btnAnswer1.Enabled = false;
                btnAnswer2.Enabled = false;
                btnAnswer3.Enabled = false;
                btnAnswer4.Enabled = false;
                questionTimer?.Stop();

                if (btn.Text == currentQuestion.right_answer)
                {
                    currentScene.enemy.TakeDamage(player.damage);
                    btn.BackColor = Color.Green;
                }
                else
                {
                    player.TakeDamage(currentScene.enemy.damage);
                    btn.BackColor = Color.Red;
                }

                lblPlayerHP.Text = $"HP: {player.health}";
                lblEnemyHP.Text = $"HP: {currentScene.enemy.health}";

                CheckPlayerDeath();
                if (currentScene.enemy.health <= 0)
                {
                    await Task.Delay(1000);
                    CheckEnemyDeath();
                }

                await Task.Delay(1000);
                GetRandomQuestion();

                btnAnswer1.Enabled = true;
                btnAnswer2.Enabled = true;
                btnAnswer3.Enabled = true;
                btnAnswer4.Enabled = true;
            }
        }

        private async void QuestionTimerTick(object sender, EventArgs e)
        {
            pnlTimer.Width -= 2;

            if (pnlTimer.Width <= 0)
            {
                questionTimer.Stop();
                player.TakeDamage(1);
                lblPlayerHP.Text = $"HP: {player.health}";
                CheckPlayerDeath();
                await Task.Delay(500);
                pnlTimer.Width = originalTimerWidth;
                GetRandomQuestion();
            }
        }

        private async void TextTimerTick(object? sender, EventArgs e)
        {
            if (currentCharIndex < text.Length)
            {
                rtbChat.AppendText(text[currentCharIndex].ToString());
                rtbChat.ScrollToCaret();
                currentCharIndex++;
            }
            else
            {
                textTimer.Stop();
                await Task.Delay(2000);
                rtbChat.Visible = false;
                rtbChat.Text = "";
                tblQuestionPanel.Visible = true;
                lblEnemyHP.Visible = true;
                lblPlayerHP.Visible = true;

                questionTimer.Start();

                GetRandomQuestion();
            }
        }

        private void InitGame()
        {
            level = -1;

            resourceDir = "../../../Resources/";

            player = new Player("Player", 1, 1, $"{resourceDir}player.png");

            FillScenes();

            FillQuestions();

            pbxPlayer.Visible = false;

            pbxEnemy.Visible = false;
        }

        private void BtnPlayClick(object? sender, EventArgs e)
        {
            player.health = PLAYER_HEALTH;

            btnPlay.Visible = false;
            btnExit.Visible = false;
            lblTitle.Visible = false;
            lblVersion.Visible = false;

            ChangeLevel();

            Image img = Image.FromFile(player.imagePath);

            pbxPlayer.Image = img;

            pbxPlayer.Width = img.Width;

            pbxPlayer.Height = img.Height;

            pbxPlayer.Visible = true;

            pbxEnemy.Visible = true;
        }

        private void BtnExitClick(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                caption: "Riddle Raiders ",
                text: "Do you want to exit the game? ",
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FillScenes()
        {
            sceneList.Add(new Scene($"{resourceDir}jungle.jpg", "Jungle", new Position(390, 476), new Enemy("Mutated Crocodile", 2, 1, $"{resourceDir}mutated_crocodile.png", new Position(900, 534)), "Looks like this is it...\nYou may be a mutated beast, but I won't let you stand in the way of my mission. \nPrepare yourself, creature!"));

            sceneList.Add(new Scene($"{resourceDir}ancient_building.jpg", "Ancient Building", new Position(390, 496), new Enemy("Black Guy", 3, 2, $"{resourceDir}black_guy.png", new Position(800, 425)), "That scythe looks heavy.\nBet you can't even swing it properly!\nThough, if you can, I'll be sure to dodge—it’d be a shame to ruin such dramatic fashion with my blood."));

            sceneList.Add(new Scene($"{resourceDir}mountain.jpg", "Mountain", new Position(350, 390), new Enemy("Long Arms", 4, 3, $"{resourceDir}long_arms.png", new Position(800, 350)), "Nice arms! \nDo they come with a user manual, or are you just winging it and hoping for the best? \nLet me guess—those things are for hugging... real aggressively, right?"));

            sceneList.Add(new Scene($"{resourceDir}cave.jpg", "Cave", new Position(380, 500), new Enemy("Long Sword", 5, 3, $"{resourceDir}long_sword.png", new Position(760, 445)), "Wow, nice sword!\n Compensating for something? Or is the dramatic lightning effect just to distract from the fact you haven’t smiled in centuries?"));

            sceneList.Add(new Scene($"{resourceDir}dungeon.jpg", "Dungeon", new Position(380, 500), new Enemy("Final Boss", 6, 4, $"{resourceDir}final_boss.png", new Position(760, 445)), "Whoa, looking nice!\n Is the sword for cutting down the rotted parts of your body? Or just to have fun in your last moments?"));
        }

        private void FillQuestions()
        {
            using (StreamReader sr = new StreamReader($"{resourceDir}questions.txt"))
            {
                while (!sr.EndOfStream)
                {
                    questionList.Add(new Question(sr.ReadLine()));
                }
            }
        }

        private void ChangeLevel()
        {
            level += 1;

            currentScene = sceneList[level];

            tblQuestionPanel.Visible = false;
            lblEnemyHP.Visible = false;
            lblPlayerHP.Visible = false;

            textTimer.Stop();
            questionTimer.Stop();

            UpdateScene();
        }

        private void UpdateScene()
        {
            this.BackgroundImage = Image.FromFile(currentScene.backgroundImage);

            Image img = Image.FromFile(currentScene.enemy.imagePath);

            pbxEnemy.Image = img;

            pbxEnemy.Width = img.Width;

            pbxEnemy.Height = img.Height;

            pbxPlayer.Location = new Point(currentScene.playerPosition.x, currentScene.playerPosition.y);

            pbxEnemy.Location = new Point(currentScene.enemy.position.x, currentScene.enemy.position.y);

            rtbChat.Visible = true;

            text = currentScene.chat;

            currentCharIndex = 0;

            lblPlayerHP.Text = $"HP: {player.health}";
            lblEnemyHP.Text = $"HP: {currentScene.enemy.health}";

            textTimer.Start();
        }

        private void UpdateQuestion()
        {
            lblQuestion.Text = currentQuestion.question;

            btnAnswer1.Text = currentQuestion.answers[0];

            btnAnswer2.Text = currentQuestion.answers[1];

            btnAnswer3.Text = currentQuestion.answers[2];

            btnAnswer4.Text = currentQuestion.answers[3];
        }

        private void GetRandomQuestion()
        {
            int questionIndex = rnd.Next(0, questionList.Count);
            currentQuestion = questionList[questionIndex];
            questionList.RemoveAt(questionIndex);
            pnlTimer.Width = originalTimerWidth;
            ResetButtonColor();
            UpdateQuestion();
            questionTimer.Start();
        }

        private void ShowMenu()
        {
            level = -1;

            lblTitle.Visible = true;

            this.BackgroundImage = Image.FromFile($"{resourceDir}menu_island.jpg");

            pbxEnemy.Visible = false;
            pbxPlayer.Visible = false;

            lblPlayerHP.Visible = false;
            lblEnemyHP.Visible = false;

            tblQuestionPanel.Visible = false;

            btnPlay.Visible = true;
            btnExit.Visible = true;
            lblVersion.Visible = true;
        }

        private void ResetButtonColor()
        {
            btnAnswer1.BackColor = Color.Gainsboro;
            btnAnswer2.BackColor = Color.Gainsboro;
            btnAnswer3.BackColor = Color.Gainsboro;
            btnAnswer4.BackColor = Color.Gainsboro;
        }

        private void CheckPlayerDeath()
        {
            if (player.health <= 0)
            {
                textTimer.Stop();
                questionTimer.Stop();
                DialogResult result = MessageBox.Show
                    (
                    caption: "GAME OVER!",
                    text: "You didn't manage to get through the challenges. \nYou'll be sent back to the menu. ",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Asterisk
                    );
                if (result == DialogResult.OK)
                {
                    ShowMenu();
                }
            }
        }

        private void CheckEnemyDeath()
        {
            if (level == sceneList.Count - 1)
            {
                this.Close();
            }
            else
            {
                ChangeLevel();
            }
        }
    }
}
