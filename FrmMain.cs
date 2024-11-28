using AdventureQuiz;
using System.Drawing.Design;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        private int level;
        private Scene currentScene;
        private List<Scene> sceneList = new List<Scene>();
        private string imageDir;

        Player player;
        public Form1()
        {

            InitializeComponent();

            InitGame();

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

        }

        private void InitGame()
        {

            level = -1;

            imageDir = "../../../Resources/";

            player = new Player("Player", 5, 1, $"{imageDir}player.png");

            FillScenes();

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

            sceneList.Add(new Scene($"{imageDir}jungle.jpg", "Jungle", new Position(390, 476), new Enemy("Mutated Crocodile", 2, 1, $"{imageDir}mutated_crocodile.png", new Position(900, 534))));

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

        }

    }
}
