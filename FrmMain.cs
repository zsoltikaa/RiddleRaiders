namespace RiddleRaiders
{
    using AdventureQuiz;
    using NAudio.Wave;
    using Timer = System.Windows.Forms.Timer;

    public partial class Form1 : Form
    {
        private static Random rnd = new Random();

        private WaveOutEvent musicPlayer;
        private AudioFileReader audioFileReader;

        private string menuMusicPath = "../../../Resources/menu_music.mp3";

        private string combatMusicPath = "../../../Resources/combat_music.mp3";

        private const int PLAYER_HEALTH = 10;

        private const int MAX_POWERUP_PER_LEVEL = 3;
        private const int POWERUP_DROP_CHANCE = 20;

        private bool wasWrong = false;
        private bool isTimeStopped = false;

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
        private bool isMuted = false;
        private string currentLanguage = "EN";

        public Form1()
        {

            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            InitGame();

            originalTimerWidth = pnlTimer.Width;

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

            textTimer = new Timer();
            textTimer.Interval = 30;
            textTimer.Tick += TextTimerTick;

            questionTimer = new Timer();
            questionTimer.Interval = 7;
            questionTimer.Tick += QuestionTimerTick;

            btnAnswer1.Click += BtnAnswerClick;
            btnAnswer2.Click += BtnAnswerClick;
            btnAnswer3.Click += BtnAnswerClick;
            btnAnswer4.Click += BtnAnswerClick;
            btnHalfPup.Click += BtnHalfPupClick;
            btnStopTimePup.Click += BtnStopTimePupClick;
            btnHealthPup.Click += BtnHealthPupClick;
            btnMute.Click += BtnMuteClick;
            btnEN.Click += BtnENClick;
            btnHU.Click += BtnHUClick;

        }

        private void BtnENClick(object sender, EventArgs e)
        {
            SetLanguage("EN");
            FillScenes();
        }

        private void BtnHUClick(object sender, EventArgs e)
        {
            SetLanguage("HU");
            FillScenes();
        }

        private void SetLanguage(string language)
        {
            currentLanguage = language;

            questionList.Clear();

            string questionFilePath = $"{resourceDir}questions{currentLanguage}.txt";

            if (File.Exists(questionFilePath))
            {
                using (StreamReader sr = new StreamReader(questionFilePath))
                {
                    while (!sr.EndOfStream)
                    {
                        questionList.Add(new Question(sr.ReadLine()));
                    }
                }
                Shuffle(questionList);
            }
            else
            {
                MessageBox.Show($"The questions file for {currentLanguage} is missing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateUIText();

        }

        private void UpdateUIText()
        {
            if (currentLanguage == "EN")
            {
                lblTitle.Text = "Riddle Raiders";
                lblVersion.Text = "v. 1.0.0.7";
                btnPlay.Text = "Play";
                btnExit.Text = "Exit";
            }
            else if (currentLanguage == "HU")
            {
                lblTitle.Text = "Riddle Raiders";
                lblVersion.Text = "v. 1.0.0.7";
                btnPlay.Text = "Játék";
                btnExit.Text = "Kilépés";
            }
        }


        private void BtnMuteClick(object sender, EventArgs e)
        {

            if (isMuted)
            {
                if (currentScene != null && currentScene.enemy != null)
                {
                    PlayMusic(combatMusicPath, 0.02f);
                }
                else
                {
                    PlayMusic(menuMusicPath, 0.5f);
                }
            }
            else
            {
                PlayMusic(menuMusicPath, 0f);
            }

            isMuted = !isMuted;

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
            isTimeStopped = true;

            Task.Delay(5000).ContinueWith(t =>
            {

                if (!isTimeStopped)
                {
                    Invoke(() => questionTimer.Start());
                    isTimeStopped = false;
                }
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

            questionTimer.Stop();
            Button btn = sender as Button;

            if (btn != null)
            {
                btnAnswer1.Enabled = false;
                btnAnswer2.Enabled = false;
                btnAnswer3.Enabled = false;
                btnAnswer4.Enabled = false;

                if (btn.Text == currentQuestion.right_answer)
                {
                    currentScene.enemy.TakeDamage(player.damage);
                    btn.BackColor = Color.Green;
                }
                else
                {
                    player.TakeDamage(currentScene.enemy.damage);
                    if (!wasWrong) wasWrong = true;
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

                if (level == sceneList.Count - 1)
                {
                    lblGameOver.Text += text[currentCharIndex];
                }
                else
                {
                    rtbChat.AppendText(text[currentCharIndex].ToString());
                    rtbChat.ScrollToCaret();
                }
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
                isTimeStopped = false;

                GetRandomQuestion();
            }

        }

        private void InitGame()
        {

            level = -1;

            resourceDir = "../../../Resources/";

            player = new Player("Player", 1, 1, $"{resourceDir}player.png");

            FillScenes();

            SetLanguage("EN");

            pbxPlayer.Visible = false;

            pbxEnemy.Visible = false;

            PlayMusic(menuMusicPath, 0.8f);

        }

        private void BtnPlayClick(object? sender, EventArgs e)
        {

            StopMusic();

            player.health = PLAYER_HEALTH;

            btnPlay.Visible = false;
            btnExit.Visible = false;
            lblTitle.Visible = false;
            lblVersion.Visible = false;

            ChangeLevel();

            PlayMusic(combatMusicPath, 0.02f);

            Image img = Image.FromFile(player.imagePath);

            pbxPlayer.Image = img;

            pbxPlayer.Width = img.Width;

            pbxPlayer.Height = img.Height;

            pbxPlayer.Visible = true;

            pbxEnemy.Visible = true;

            btnMute.Visible = false;

            btnEN.Visible = false;

            btnHU.Visible = false;

        }

        private void BtnExitClick(object? sender, EventArgs e)
        {
            string caption, text;

            if (currentLanguage == "EN")
            {
                caption = "Riddle Raiders";
                text = "Do you want to exit the game?";
            }
            else if (currentLanguage == "HU")
            {
                caption = "Riddle Raiders";
                text = "Ki szeretnél lépni a játékból?";
            }
            else
            {
                caption = "Riddle Raiders";
                text = "Do you want to exit the game?";
            }

            DialogResult result = MessageBox.Show(
                caption: caption,
                text: text,
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
            sceneList.Clear();
            sceneList.Add(new Scene($"{resourceDir}jungle.jpg", "Jungle", new Position(390, 476), new Enemy("Mutated Crocodile", 2, 1, $"{resourceDir}mutated_crocodile.png", new Position(900, 534)), currentLanguage == "HU"? "Úgy tûnik, ennyi volt...\nLehet, hogy mutáns szörnyeteg vagy, de nem hagyom, hogy az utamba állj a küldetésemben.\nKészülj, teremtmény!" : "Looks like this is it...\nYou may be a mutated beast, but I won't let you stand in the way of my mission.\nPrepare yourself, creature!"));

            sceneList.Add(new Scene($"{resourceDir}ancient_building.jpg", "Ancient Building", new Position(390, 496), new Enemy("Black Guy", 3, 2, $"{resourceDir}black_guy.png", new Position(800, 425)), currentLanguage == "HU" ? "Az a kasza elég nehéznek tûnik.\nFogadok, hogy még meglengetni sem tudod rendesen!\nDe ha mégis, biztos kitérek—kár lenne elrontani egy ilyen drámai divatot a véremmel." : "That scythe looks heavy.\nBet you can't even swing it properly!\nThough, if you can, I'll be sure to dodge—it’d be a shame to ruin such dramatic fashion with my blood"));

            sceneList.Add(new Scene($"{resourceDir}mountain.jpg", "Mountain", new Position(350, 390), new Enemy("Long Arms", 4, 2, $"{resourceDir}long_arms.png", new Position(800, 350)), currentLanguage == "HU" ? "Szép karok!\nVan hozzájuk használati útmutató, vagy csak improvizálsz és reménykedsz a legjobbakban?\nHadd találjam ki—azok a dolgok ölelésre valók... nagyon agresszívan, igaz?" : "Nice arms! \nDo they come with a user manual, or are you just winging it and hoping for the best? \nLet me guess—those things are for hugging... real aggressively, right?"));

            sceneList.Add(new Scene($"{resourceDir}cave.jpg", "Cave", new Position(380, 500), new Enemy("Long Sword", 5, 3, $"{resourceDir}long_sword.png", new Position(760, 445)), currentLanguage == "HU" ? "Hû, szép kard!\nValamit kompenzálsz vele? Vagy a drámai villámhatás csak arra szolgál, hogy elterelje a figyelmet arról, hogy évszázadok óta nem mosolyogtál?" : "Wow, nice sword!\nCompensating for something? Or is the dramatic lightning effect just to distract from the fact you haven’t smiled in centuries?"));

            sceneList.Add(new Scene($"{resourceDir}dungeon.jpg", "Dungeon", new Position(380, 500), new Enemy("Final Boss", 6, 5, $"{resourceDir}final_boss.png", new Position(760, 445)), currentLanguage == "HU" ? "Hûha, jól nézel ki!\nA kard arra van, hogy levágd a tested elrohadt részeit? Vagy csak arra, hogy szórakozz az utolsó pillanataidban?" : "Whoa, looking nice!\nIs the sword for cutting down the rotted parts of your body? Or just to have fun in your last moments?"));

            sceneList.Add(new Scene($"{resourceDir}game_over.jpg", "Game Over", new Position(581, 462), null, currentLanguage == "HU" ? "Gratulálok!\nSikeresen legyõztél minden ellenfelet, aki az utadba került.\nLara megszerezte a mindenható kincset." : "Congratulations!\nYou successfully defeated every opponent that came in your way. \nLara had managed to obtain the almighty treasure."));

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

                if (rng.NextDouble() > 0.5)
                {
                    int m = rng.Next(n);
                    T temp = list[m];
                    list[m] = list[k];
                    list[k] = temp;
                }
            }

            int extraShuffles = rng.Next(3, 6);

            for (int i = 0; i < extraShuffles; i++)

            {
                int index1 = rng.Next(list.Count);
                int index2 = rng.Next(list.Count);
                T temp2 = list[index1];
                list[index1] = list[index2];
                list[index2] = temp2;
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
                StopMusic();
                pbxEnemy.Visible = false;
                if (!wasWrong)
                {
                    Image notsafe4work = Image.FromFile($"{resourceDir}lara_croft_nsfw.png");
                    pbxPlayer.Image = notsafe4work;
                    pbxPlayer.Width = notsafe4work.Width;
                    pbxPlayer.Height = notsafe4work.Height;
                }
                pbxPlayer.Visible = !wasWrong;
                rtbChat.Visible = false;
                lblGameOver.Text = String.Empty;
                lblGameOver.Visible = true;
                rtbChat.Text = String.Empty;
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
            isTimeStopped = false;

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

            PlayMusic(menuMusicPath, 0.8f);

        }

        private void ResetAnswerButtons()
        {

            btnAnswer1.BackColor = Color.FromArgb(255, 214, 192, 179);
            btnAnswer2.BackColor = Color.FromArgb(255, 214, 192, 179);
            btnAnswer3.BackColor = Color.FromArgb(255, 214, 192, 179);
            btnAnswer4.BackColor = Color.FromArgb(255, 214, 192, 179);

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

                string caption, text;

                if (currentLanguage == "EN")
                {
                    caption = "GAME OVER!";
                    text = "You didn't manage to get through the challenges. \nYou'll be sent back to the menu.";
                }
                else if (currentLanguage == "HU")
                {
                    caption = "JÁTÉK VÉGE!";
                    text = "Nem sikerült átverekedned magad a kihívásokon. \nVisszakerülsz a menübe.";
                }
                else
                {
                    caption = "GAME OVER!";
                    text = "You didn't manage to get through the challenges. \nYou'll be sent back to the menu.";
                }

                DialogResult result = MessageBox.Show
                    (
                    caption: caption,
                    text: text,
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
                StopMusic();
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

            if (currentLanguage == "EN")
            {
                btnHalfPup.Text = pUpHalf.owned > 0 ? $"Half Answers x{pUpHalf.owned}" : "Half Answers";
                btnStopTimePup.Text = pUpStopTime.owned > 0 ? $"Stop Time (5s) x{pUpStopTime.owned}" : "Stop Time (5s)";
                btnHealthPup.Text = pUpHealth.owned > 0 ? $"+2 Health x{pUpHealth.owned}" : "+2 Health";
            }
            else if (currentLanguage == "HU")
            {
                btnHalfPup.Text = pUpHalf.owned > 0 ? $"Felezõ x{pUpHalf.owned}" : "Felezõ";
                btnStopTimePup.Text = pUpStopTime.owned > 0 ? $"Idõmegállítás (5s) x{pUpStopTime.owned}" : "Idõmegállítás (5s)";
                btnHealthPup.Text = pUpHealth.owned > 0 ? $"+2 Élet x{pUpHealth.owned}" : "+2 Élet";
            }

        }

        private void PlayMusic(string musicPath, float volume)
        {

            StopMusic();

            if (isMuted) volume = 0f;

            audioFileReader = new AudioFileReader(musicPath)
            {
                Volume = volume
            };

            musicPlayer = new WaveOutEvent();

            musicPlayer.Init(audioFileReader);
            musicPlayer.PlaybackStopped += (sender, args) =>
            {
                if (audioFileReader != null)
                {
                    audioFileReader.Position = 0;
                    musicPlayer.Play();
                }
            };
            musicPlayer.Play();

        }

        private void StopMusic()
        {

            if (musicPlayer != null)
            {
                musicPlayer.Stop();
                musicPlayer.Dispose();
                musicPlayer = null;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader = null;
            }
        }

    }
}