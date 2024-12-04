using AdventureQuiz;
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
        public Form1()
        {

            InitializeComponent();

            InitGame();

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

            textTimer = new Timer();
            textTimer.Interval = 30; 
            textTimer.Tick += TextTimerTick;

            questionTimer = new Timer();
            questionTimer.Interval = 5;
            questionTimer.Tick += QuestionTimerTick;

        }

        private void QuestionTimerTick(object sender, EventArgs e)
        {
            pnlTimer.Width -= 2;

            if (pnlTimer.Width < 0)
            {
                player.health--;
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
                tblQuestionPanel.Visible = true;

                questionTimer.Start();
            }

        }

        private void InitGame()
        {

            level = -1;

            resourceDir = "../../../Resources/";

            player = new Player("Player", 5, 1, $"{resourceDir}player.png");

            FillScenes();

            FillQuestions();

            pbxPlayer.Visible = false;

            pbxEnemy.Visible = false;
        }

        private void EngageEnemy()
        {
            while(currentScene.enemy.health > 0)
            {
                
            }
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

        private void GenerateRandomQuestion()
        {
            int questionIndex = rnd.Next(0, questionList.Count);
            currentQuestion = questionList[questionIndex];
            questionList.RemoveAt(questionIndex);
        }
        
    }
}
