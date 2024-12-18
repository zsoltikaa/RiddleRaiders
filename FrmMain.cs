namespace RiddleRaiders
{
    using AdventureQuiz;
    using NAudio.Wave;
    using Timer = System.Windows.Forms.Timer;

    public partial class FrmMain : Form
    {
        // create a static random object to generate random numbers
        private static readonly Random rnd = new();
        
        // declare the music player and audio file reader objects for playing sound
        private WaveOutEvent musicPlayer;
        private AudioFileReader audioFileReader;
        
        // define paths to different music files for various scenes
        private readonly string menuMusicPath = "../../../Resources/menu_music.mp3";
        private readonly string combatMusicPath = "../../../Resources/combat_music.mp3";
        private readonly string gameOverMusicPath = "../../../Resources/ending_music.mp3";
        
        // define a constant for player health
        private const int PLAYER_HEALTH = 10;
        
        // define constants for power-up settings
        private const int MAX_POWERUP_PER_LEVEL = 3; 
        private const int POWERUP_DROP_CHANCE = 100;
        
        // declare boolean flags for various game states
        private bool wasWrong = false; 
        private bool isTimeStopped = false;

        // instantiate power-ups with default values
        readonly PowerUp pUpHalf = new(0);
        readonly PowerUp pUpStopTime = new(0);
        readonly PowerUp pUpHealth = new(0);
        
        // declare variables for level, scene, and other game elements
        private int level;
        private Scene currentScene;
        private readonly List<Scene> sceneList = []; 
        private string resourceDir;
        
        // declare timers for text and question timings
        private readonly Timer textTimer;
        private readonly Timer questionTimer;
        
        // variable to track the current character index in the text being displayed
        private int currentCharIndex;
        
        // string to hold the current text being displayed
        private string text;
        
        // player object and list of questions
        private Player player;
        private readonly List<Question> questionList = [];
        private Question currentQuestion;
        
        // store the original width of the timer
        private readonly int originalTimerWidth;
        
        // declare a flag for muting the game sounds
        private bool isMuted = false;
        
        // store the current language setting
        private string currentLanguage = "EN"; 

        public FrmMain()
        {

            // set the form's start position to center of the screen
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // initialize the form components
            InitializeComponent();
            
            // initialize the game
            InitGame();
            
            // store the original width of the timer panel
            originalTimerWidth = pnlTimer.Width;
            
            // add event handler for the exit button click
            btnExit.Click += BtnExitClick;
            
            // add event handler for the play button click
            btnPlay.Click += BtnPlayClick;

            // initialize the text timer with an interval of 30 milliseconds
            textTimer = new Timer
            {
                Interval = 30
            };
            textTimer.Tick += TextTimerTick;

            // initialize the question timer with an interval of 7 milliseconds
            questionTimer = new Timer
            {
                Interval = 7
            };
            questionTimer.Tick += QuestionTimerTick;
            
            // add event handlers for the answer buttons' click events
            btnAnswer1.Click += BtnAnswerClick;
            btnAnswer2.Click += BtnAnswerClick;
            btnAnswer3.Click += BtnAnswerClick;
            btnAnswer4.Click += BtnAnswerClick;
            
            // add event handlers for the power-up buttons' click events
            btnHalfPup.Click += BtnHalfPupClick;
            btnStopTimePup.Click += BtnStopTimePupClick;
            btnHealthPup.Click += BtnHealthPupClick;
            
            // add event handler for the mute button click
            btnMute.Click += BtnMuteClick;
            
            // add event handler for the English language button click
            btnEN.Click += BtnENClick;
            
            // add event handler for the Hungarian language button click
            btnHU.Click += BtnHUClick;
            
            // add event handler for the back-to-menu button click
            btnBackToMenu.Click += BtnBackToMenuClick;

        }

        // stops the text timer and shows the menu when the back-to-menu button is clicked
        private void BtnBackToMenuClick(object sender, EventArgs e)
        {
            textTimer.Stop();
            ShowMenu();
        }
        
        // sets the language to English and updates the scenes
        private void BtnENClick(object sender, EventArgs e)
        {
            SetLanguage("EN");
            FillScenes();
        }
        
        // sets the language to Hungarian and updates the scenes
        private void BtnHUClick(object sender, EventArgs e)
        {
            SetLanguage("HU");
            FillScenes();
        }
        
        // sets the current language and loads the respective question file
        private void SetLanguage(string language)
        {
            currentLanguage = language;
        
            questionList.Clear();
        
            string questionFilePath = $"{resourceDir}questions{currentLanguage}.txt";
        
            if (File.Exists(questionFilePath))
            {
                using (StreamReader sr = new(questionFilePath))
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
        
        // updates the UI text based on the selected language
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

        // toggles the mute state and adjusts the music volume accordingly
        private void BtnMuteClick(object sender, EventArgs e)
        {
            ToggleMute();
        }
        
        // changes the mute state and adjusts the music player's volume
        private void ToggleMute()
        {
            isMuted = !isMuted; 
        
            if (isMuted)
            {
                musicPlayer.Volume = 0f;
            }
            else
            {
                musicPlayer.Volume = 0.8f; 
            }
        }
        
        // uses a health power-up to increase the player's health
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

        // uses the stop time power-up to pause the question timer
        private void BtnStopTimePupClick(object sender, EventArgs e)
        {
            pUpStopTime.owned--;
            RefreshPowerUps();
            btnStopTimePup.Enabled = false; 

            questionTimer.Stop(); 
            isTimeStopped = true; 

            Task.Delay(5000).ContinueWith(t =>
            {
                if (isTimeStopped) 
                {
                    Invoke(() =>
                    {
                        questionTimer.Start();
                        isTimeStopped = false; 
                    });
                }
            });
        }


        // uses the half-pick power-up to remove two wrong answers from the choices
        private void BtnHalfPupClick(object sender, EventArgs e)
        {
            pUpHalf.owned--;
            RefreshPowerUps();
            btnHalfPup.Enabled = false;
        
            List<int> wrongAnswers = [];
            for (int i = 0; i < currentQuestion.answers.Length; i++)
            {
                if (currentQuestion.answers[i] != currentQuestion.right_answer)
                {
                    wrongAnswers.Add(i);
                }
            }
        
            Random rnd = new();
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

        // handles the answer button click event, checks if the answer is correct, and updates health
        private async void BtnAnswerClick(object sender, EventArgs e)
        {
            questionTimer.Stop();

            if (sender is Button btn)
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
                    ChangeLevel();
                }

                await Task.Delay(1000);
                GetRandomQuestion();

                btnAnswer1.Enabled = true;
                btnAnswer2.Enabled = true;
                btnAnswer3.Enabled = true;
                btnAnswer4.Enabled = true;

            }
        }
        
        // handles the question timer tick, reduces the timer width, and damages the player if time runs out
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
        
        // this method handles the timer tick event, updating the displayed text and progressing through the game scenes.
        private async void TextTimerTick(object sender, EventArgs e)
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
            else
            {
                btnBackToMenu.Visible = true;
            }
        }
        
        // initializes the game, setting up the player, scenes, and starting music
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

        // handles the Play button click event, starts the game, and plays combat music
        private void BtnPlayClick(object sender, EventArgs e)
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
        
        // handles the Exit button click event, asks the user if they want to exit the game
        private void BtnExitClick(object sender, EventArgs e)
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
        
        // fills the scene list with different scenes, enemies, and descriptions based on the current language
        private void FillScenes()
        {
            sceneList.Clear();
        
            sceneList.Add(new Scene($"{resourceDir}jungle.jpg", "Jungle", new Position(390, 476), new Enemy("Mutated Crocodile", 2, 1, $"{resourceDir}mutated_crocodile.png", new Position(900, 534)), currentLanguage == "HU"? "Úgy tűnik, ennyi volt...\nLehet, hogy mutáns szörnyeteg vagy, de nem hagyom, hogy az utamba állj a küldetésemben.\nKészülj, teremtmény!" : "Looks like this is it...\nYou may be a mutated beast, but I won't let you stand in the way of my mission.\nPrepare yourself, creature!"));
        
            sceneList.Add(new Scene($"{resourceDir}ancient_building.jpg", "Ancient Building", new Position(390, 496), new Enemy("Black Guy", 3, 2, $"{resourceDir}black_guy.png", new Position(800, 425)), currentLanguage == "HU" ? "Az a kasza elég nehéznek tűnik.\nFogadok, hogy még meglengetni sem tudod rendesen!\nDe ha mégis, biztos kitérekkár lenne elrontani egy ilyen drámai divatot a véremmel." : "That scythe looks heavy.\nBet you can't even swing it properly!\nThough, if you can, I'll be sure to dodgeit’d be a shame to ruin such dramatic fashion with my blood"));
        
            sceneList.Add(new Scene($"{resourceDir}mountain.jpg", "Mountain", new Position(350, 390), new Enemy("Long Arms", 4, 2, $"{resourceDir}long_arms.png", new Position(800, 350)), currentLanguage == "HU" ? "Szép karok!\nVan hozzájuk használati útmutató, vagy csak improvizálsz és reménykedsz a legjobbakban?\nHadd találjam kiazok a dolgok ölelésre valók... nagyon agresszívan, igaz?" : "Nice arms! \nDo they come with a user manual, or are you just winging it and hoping for the best? \nLet me guessthose things are for hugging... real aggressively, right?"));
        
            sceneList.Add(new Scene($"{resourceDir}cave.jpg", "Cave", new Position(380, 500), new Enemy("Long Sword", 5, 3, $"{resourceDir}long_sword.png", new Position(760, 445)), currentLanguage == "HU" ? "Hû, szép kard!\nValamit kompenzálsz vele? Vagy a drámai villámhatás csak arra szolgál, hogy elterelje a figyelmet arról, hogy évszázadok óta nem mosolyogtál?" : "Wow, nice sword!\nCompensating for something? Or is the dramatic lightning effect just to distract from the fact you haven’t smiled in centuries?"));
        
            sceneList.Add(new Scene($"{resourceDir}dungeon.jpg", "Dungeon", new Position(380, 500), new Enemy("Final Boss", 6, 5, $"{resourceDir}final_boss.png", new Position(760, 445)), currentLanguage == "HU" ? "Hûha, jól nézel ki!\nA kard arra van, hogy levágd a tested elrohadt részeit? Vagy csak arra, hogy szórakozz az utolsó pillanataidban?" : "Whoa, looking nice!\nIs the sword for cutting down the rotted parts of your body? Or just to have fun in your last moments?"));
        
            sceneList.Add(new Scene($"{resourceDir}game_over.jpg", "Game Over", new Position(581, 462), null, currentLanguage == "HU" ? "Gratulálok!\nSikeresen legyõztél minden ellenfelet, aki az utadba került.\nLara megszerezte a mindenható kincset." : "Congratulations!\nYou successfully defeated every opponent that came in your way. \nLara had managed to obtain the almighty treasure."));
        }

        // shuffles a list of items randomly. It performs a basic shuffle and then adds extra shuffles for additional randomness.
        public static void Shuffle<T>(List<T> list)
        {

            for (int i = 0; i < list.Count/2; i++)
            {
                int ri = rnd.Next(list.Count);
                int rj = rnd.Next(list.Count);

                (list[ri], list[rj]) = (list[rj], list[ri]);
            }

            }
        
        // changes the level by incrementing the level counter and updating the scene and power-ups.
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
        
        // updates the visual elements for the current scene (background, player, enemy, etc.) and starts text display.
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
                PlayMusic(gameOverMusicPath, 0.1f);
                musicPlayer.PlaybackStopped += (sender, args) =>
                {
                    ShowMenu();
                };
        
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

        // updates the question and answers displayed on the UI.
        private void UpdateQuestion()
        {

            lblQuestion.Text = currentQuestion.question;

            btnAnswer1.Text = currentQuestion.answers[0];

            btnAnswer2.Text = currentQuestion.answers[1];

            btnAnswer3.Text = currentQuestion.answers[2];

            btnAnswer4.Text = currentQuestion.answers[3];

        }

        // selects a random question, removes it from the list, and updates the UI.
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
        
        // displays the main menu, resetting the game state and showing menu elements.
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
            btnHU.Visible = true;
            btnEN.Visible = true;
            btnMute.Visible = true;

            btnBackToMenu.Visible = false;

            lblGameOver.Visible = false;

            PlayMusic(menuMusicPath, 0.8f);

            FillScenes();

        }

        // resets the appearance of the answer buttons to their default state.
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

        // checks if the player's health has reached zero, triggering the game over process.
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

        // updates the status of power-ups buttons based on their availability.
        private void RefreshPowerUps()
        {

            btnHalfPup.Enabled = pUpHalf.owned > 0;
            btnStopTimePup.Enabled = pUpStopTime.owned > 0;
            btnHealthPup.Enabled = pUpHealth.owned > 0;

            RefreshPowerUpButtons();

        }

        // grants power-ups to the player based on a chance percentage.
        private void GivePowerUps(int max, int chancePercentage)
        {

            Random rng = new();

            pUpHalf.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;
            pUpStopTime.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;
            pUpHealth.owned += rng.Next(1, 100) <= chancePercentage ? rng.Next(1, max) : 0;

            RefreshPowerUps();

        }

        // updates the text of power-up buttons based on the number of owned power-ups.
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

        // plays music from the specified path with the given volume, and loops it.
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

        // stops the currently playing music and disposes of the audio resources.
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
