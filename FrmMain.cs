using AdventureQuiz;
using System.Runtime.InteropServices.Marshalling;
using System.Windows.Forms;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        private int level = 0;
        private Scene currentScene;
        private List<Scene> sceneList;
        private string imageDir = "../../../Resources/";

        Player player;
        public Form1()
        {

            InitializeComponent();

            player = new Player("Player", 5, 1, $"{imageDir}player.png");

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

        }

        private void BtnPlayClick(object? sender, EventArgs e)
        {
            
            btnPlay.Visible = false;
            btnExit.Visible = false;
            lblTitle.Visible = false;
            lblVersion.Visible = false;

            ChangeLevel();

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

            sceneList.Add(new Scene($"{imageDir}jungle.jpg", "Jungle", new Position(390, 476), new Position(900, 534)));

        }
        private void ChangeLevel()
        {

            level += 1;
            
            this.BackgroundImage = Image.FromFile($"../../../Resources/jungle.jpg");



        }

    }
}
