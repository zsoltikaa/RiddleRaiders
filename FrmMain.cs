using AdventureQuiz;
using RiddleRaiders.Properties;
using Timer = System.Windows.Forms.Timer;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        private static Random rnd = new Random();

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

        private void BtnAnswerClick(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;

            if (btn != null)
            {
                if (btn.Text == currentQuestion.right_answer)
                {
                    currentScene.enemy.TakeDamage(1);
                    pnlTimer.Width = originalTimerWidth;
                    GetRandomQuestion();
                    btn.BackColor = Color.Green;
                }
                else
                {
                    player.TakeDamage(1);
                    pnlTimer.Width = originalTimerWidth;
                    GetRandomQuestion();
                    btn.BackColor = Color.Red;
                }
            }

            lblPlayerHP.Text = $"HP: {player.health}";
            lblEnemyHP.Text = $"HP: {currentScene.enemy.health}";

        }

        private void QuestionTimerTick(object sender, EventArgs e)
        {
            pnlTimer.Width -= 2;

            if (pnlTimer.Width <= 0)
            {
                player.TakeDamage(1);
                lblPlayerHP.Text = $"HP: {player.health}";
                if (player.health <= 0)
                {
                    textTimer.Stop();
                    questionTimer.Stop();
                    DialogResult result = MessageBox.Show
                        (
                        caption: "GAME OVER!",
                        text: "You didn't manage to get through the challanges. \nYou'll be sent back to the menu. ",
                        buttons: MessageBoxButtons.OK
                        );
                    if (result == DialogResult.OK)
                    {
                        ShowMenu();
                    }
                }
                Thread.Sleep(500);
                pnlTimer.Width = originalTimerWidth;
                GetRandomQuestion();
            }
        }

        private void TextTimerTick(object? sender, EventArgs e)
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
                Thread.Sleep(2000);
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
                caption: "RiddleRaiders ",
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

            lblPlayerHP.Text = $"HP: {player.health}";
            lblEnemyHP.Text = $"HP: {currentScene.enemy.health}";

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
            ResetButtonColor();
            UpdateQuestion();            
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

    }
}
