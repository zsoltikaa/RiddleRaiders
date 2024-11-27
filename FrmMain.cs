using System.Windows.Forms;

namespace RiddleRaiders
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            btnExit.Click += BtnExitClick;

            btnPlay.Click += BtnPlayClick;

        }

        private void BtnPlayClick(object? sender, EventArgs e)
        {
            
            btnPlay.Visible = false;
            btnExit.Visible = false;
            lblTitle.Visible = false;
            lblVersion.Visible = false;

            this.BackgroundImage = Image.FromFile("assets/img/level_01.jpg");



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



    }
}
