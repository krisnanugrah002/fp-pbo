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
                Location = new Point(200, 25),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            this.Controls.Add(header);

            lblBackgroundNoise = CreateLabel("Background Noise", new Point(50, 170));
            trackBackgroundNoise = CreateTrackBar(new Point(50, 210));

            lblGameplaySound = CreateLabel("Gameplay Sound", new Point(50, 280));
            trackGameplaySound = CreateTrackBar(new Point(50, 320));

            PictureBox picSound = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "sound.png")),
                Size = new Size(77, 77),
                Location = new Point(225, 75),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(picSound);
            picSound.BringToFront();

            PictureBox picInformation = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "information.png")),
                Size = new Size(77, 77),
                Location = new Point(225, 410),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(picInformation);
            picInformation.BringToFront();

            PictureBox picInformationDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "information-deck.png")),
                Size = new Size(430, 271),
                Location = new Point(50, 500),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(picInformationDeck);
            picInformationDeck.BringToFront();

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
    }
}
