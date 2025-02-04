using System;
using System.Windows.Forms;
using System.Drawing;

namespace RefineAI
{
    internal class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "RefineAI";
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            // Add a PictureBox for the image
            PictureBox logoPictureBox = new PictureBox
            {
                // Replace with your actual logo path
                Image = Image.FromFile("C:\\Users\\gaming\\Documents\\RefineAI\\RefineAI\\Images\\cfbef23306012464b7b00a6d980a922c.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(150, 150),
                Location = new Point(75, 20)
            };
            this.Controls.Add(logoPictureBox);

            Label titleLabel = new Label
            {
                Text = "RefineAI",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(100, 180)
            };
            this.Controls.Add(titleLabel);

            Label loadingLabel = new Label
            {
                Text = "Loading...",
                Font = new Font("Arial", 10),
                AutoSize = true,
                Location = new Point(110, 210)
            };
            this.Controls.Add(loadingLabel);

            Timer closeTimer = new Timer
            {
                Interval = 2000 // 2 seconds
            };
            closeTimer.Tick += (sender, e) =>
            {
                closeTimer.Stop();
                this.Close();
            };
            closeTimer.Start();
        }
    }
}