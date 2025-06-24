using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MemoryArena
{
    public class SettingsForm : Form
    {
        private TrackBar trackBackgroundNoise, trackGameplaySound;
        private Label lblBackgroundNoise, lblGameplaySound;

        public SettingsForm()
        {
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            this.BackgroundImage = Image.FromFile(Path.Combine("Assets", "background.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            InitControls();
        }

        private void InitControls()
        {
            Label header = new Label()
            {
                Text = "Settings",
                Font = new Font("Arial", 24, FontStyle.Bold),
                Location = new Point(200, 50),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            this.Controls.Add(header);

            lblBackgroundNoise = CreateLabel("Background Noise", new Point(50, 150));
            trackBackgroundNoise = CreateTrackBar(new Point(50, 190));

            lblGameplaySound = CreateLabel("Gameplay Sound", new Point(50, 260));
            trackGameplaySound = CreateTrackBar(new Point(50, 300));

            Button infoButton = new Button()
            {
                Text = "Information",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Size = new Size(200, 50),
                Location = new Point(170, 400),
                BackColor = Color.White
            };
            infoButton.Click += (s, e) => ShowAppInfo();
            this.Controls.Add(infoButton);

            PictureBox btnBack = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine("Assets", "back-home.png")),
                Size = new Size(50, 50),
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);
        }

        private Label CreateLabel(string text, Point location)
        {
            var label = new Label
            {
                Text = text,
                Font = new Font("Arial", 14),
                Location = location,
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(label);
            return label;
        }

        private TrackBar CreateTrackBar(Point location)
        {
            var bar = new TrackBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = 50,
                TickStyle = TickStyle.None,
                Size = new Size(440, 45),
                Location = location
            };
            this.Controls.Add(bar);
            return bar;
        }

        private void ShowAppInfo()
        {
            MessageBox.Show(
                "Memory Arena adalah game edukasi berbasis puzzle memory.\n\n" +
                "Fitur:\n- Tanpa iklan\n- Leaderboard\n- Sistem poin dan nyawa\n" +
                "Cocok untuk semua usia, membantu meningkatkan daya ingat dan fokus!",
                "Tentang Aplikasi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
