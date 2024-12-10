using AdventureQuiz;
using RiddleRaiders.Properties;
using Timer = System.Windows.Forms.Timer;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        private static Random rnd = new Random();

        private const int PLAYER_HEALTH = 10;

        private const int MAX_POWERUP_PER_LEVEL = 3;
        private const int POWERUP_DROP_CHANCE = 40;

        PowerUp pUpHalf = new PowerUp(0);
        PowerUp pUpStopTime = new PowerUp(0);
        PowerUp pUpHealth = new PowerUp(0);

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
            btnHalfPup.Click += BtnHalfPupClick;
            btnStopTimePup.Click += BtnStopTimePupClick;
            btnHealthPup.Click += BtnHealthPupClick;

        }

        private void BtnHealthPupClick(object sender, EventArgs e)
        {
            pUpHealth.owned--;
            RefreshPowerUps();
            if (player.health < PLAYER_HEALTH)
            {
                player.health = Math.Min(player.health + 2, PLAYER_HEALTH);
                lblPlayerHP.Text = $"HP: {player.health}";
            }
        }

        private void BtnStopTimePupClick(object sender, EventArgs e)
        {
            pUpStopTime.owned--;
            RefreshPowerUps();
            btnStopTimePup.Enabled = false;

            questionTimer.Stop();

            Task.Delay(5000).ContinueWith(t =>
            {

                Invoke(() => questionTimer.Start());

            });
        }

        private void BtnHalfPupClick(object sender, EventArgs e)
        {
            pUpHalf.owned--;
            RefreshPowerUps();
            btnHalfPup.Enabled = false;

            List<int> wrongAnswers = new List<int>();
            for (int i = 0; i < currentQuestion.answers.Length; i++)
            {
                if (currentQuestion.answers[i] != currentQuestion.right_answer)
                {
                    wrongAnswers.Add(i);
                }
            }

            Random rnd = new Random();
            int answerToRemove1 = wrongAnswers[rnd.Next(wrongAnswers.Count)];
            wrongAnswers.Remove(answerToRemove1);
            int answerToRemove2 = wrongAnswers[rnd.Next(wrongAnswers.Count)];

            if (answerToRemove1 == 0) btnAnswer1.Visible = false;
            if (answerToRemove1 == 1) btnAnswer2.Visible = false;
            if (answerToRemove1 == 2) btnAnswer3.Visible = false;
            if (answerToRemove1 == 3) btnAnswer4.Visible = false;

            if (answerToRemove2 == 0) btnAnswer1.Visible = false;
            if (answerToRemove2 == 1) btnAnswer2.Visible = false;
            if (answerToRemove2 == 2) btnAnswer3.Visible = false;
            if (answerToRemove2 == 3) btnAnswer4.Visible = false;
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

                questionTimer.Stop();

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
            else if (currentScene.enemy != null)
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
            sceneList.Add(new Scene($"{resourceDir}jungle.jpg", "Jungle", new Position(390, 476), new Enemy("Mutated Crocodile", 1, 1, $"{resourceDir}mutated_crocodile.png", new Position(900, 534)), "Looks like this is it...\nYou may be a mutated beast, but I won't let you stand in the way of my mission. \nPrepare yourself, creature!"));

            sceneList.Add(new Scene($"{resourceDir}ancient_building.jpg", "Ancient Building", new Position(390, 496), new Enemy("Black Guy", 1, 2, $"{resourceDir}black_guy.png", new Position(800, 425)), "That scythe looks heavy.\nBet you can't even swing it properly!\nThough, if you can, I'll be sure to dodge—it’d be a shame to ruin such dramatic fashion with my blood."));

            sceneList.Add(new Scene($"{resourceDir}mountain.jpg", "Mountain", new Position(350, 390), new Enemy("Long Arms", 1, 2, $"{resourceDir}long_arms.png", new Position(800, 350)), "Nice arms! \nDo they come with a user manual, or are you just winging it and hoping for the best? \nLet me guess—those things are for hugging... real aggressively, right?"));

            sceneList.Add(new Scene($"{resourceDir}cave.jpg", "Cave", new Position(380, 500), new Enemy("Long Sword", 1, 3, $"{resourceDir}long_sword.png", new Position(760, 445)), "Wow, nice sword!\nCompensating for something? Or is the dramatic lightning effect just to distract from the fact you haven’t smiled in centuries?"));

            sceneList.Add(new Scene($"{resourceDir}dungeon.jpg", "Dungeon", new Position(380, 500), new Enemy("Final Boss", 1, 5, $"{resourceDir}final_boss.png", new Position(760, 445)), "Whoa, looking nice!\nIs the sword for cutting down the rotted parts of your body? Or just to have fun in your last moments?"));

            sceneList.Add(new Scene($"{resourceDir}game_over.jpg", "Game Over", new Position(380, 500), null, "Congratulations!\nYou successfully defeated every opponent that came in your way. Lara had managed to obtain the almighty treasure."));
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

            Shuffle(questionList);
        }

        public static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
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

            GivePowerUps(MAX_POWERUP_PER_LEVEL, POWERUP_DROP_CHANCE);

            UpdateScene();
        }

        private void UpdateScene()
        {
            BackgroundImage = Image.FromFile(currentScene.backgroundImage);

            pbxPlayer.Location = new Point(currentScene.playerPosition.x, currentScene.playerPosition.y);

            rtbChat.Visible = true;

            text = currentScene.chat;

            currentCharIndex = 0;

            if (currentScene.enemy != null)
            {
                Image img = Image.FromFile(currentScene.enemy.imagePath);
                pbxEnemy.Location = new Point(currentScene.enemy.position.x, currentScene.enemy.position.y);
                pbxEnemy.Image = img;
                pbxEnemy.Width = img.Width;
                pbxEnemy.Height = img.Height;
                lblPlayerHP.Text = $"HP: {player.health}";
                lblEnemyHP.Text = $"HP: {currentScene.enemy.health}";
            }
            else
            {
                pbxEnemy.Visible = false;
                pbxPlayer.Visible = false;

            }

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
            Shuffle(questionList);
            pnlTimer.Width = originalTimerWidth;
            ResetAnswerButtons();
            UpdateQuestion();
            RefreshPowerUps();
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

        private void ResetAnswerButtons()
        {
            btnAnswer1.BackColor = Color.Gainsboro;
            btnAnswer2.BackColor = Color.Gainsboro;
            btnAnswer3.BackColor = Color.Gainsboro;
            btnAnswer4.BackColor = Color.Gainsboro;

            btnAnswer1.Visible = true;
            btnAnswer2.Visible = true;
            btnAnswer3.Visible = true;
            btnAnswer4.Visible = true;
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

        private void RefreshPowerUps()
        {
            btnHalfPup.Enabled = pUpHalf.owned > 0 ? true : false;
            btnStopTimePup.Enabled = pUpStopTime.owned > 0 ? true : false;
            btnHealthPup.Enabled = pUpHealth.owned > 0 ? true : false;

            RefreshPowerUpButtons();
        }

        private void GivePowerUps(int max, int chancePercentage)
        {
            Random rng = new Random();

            pUpHalf.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;
            pUpStopTime.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;
            pUpHealth.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;

            RefreshPowerUps();
        }

        private void RefreshPowerUpButtons()
        {
            btnHalfPup.Text = pUpHalf.owned > 0 ? $"Half Answers x{pUpHalf.owned}" : "Half Answers";
            btnStopTimePup.Text = pUpStopTime.owned > 0 ? $"Stop Time (5s) x{pUpStopTime.owned}" : "Stop Time (5s)";
            btnHealthPup.Text = pUpHealth.owned > 0 ? $" + 2 Health x{pUpHealth.owned}" : "+2 Health";
        }

    }
}
