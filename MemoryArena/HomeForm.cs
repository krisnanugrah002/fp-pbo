// HomeForm_Revised.cs - Koreksi Transparansi dan Layering + Warna Custom Button + Ukuran Custom More-Profile
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Text;

namespace MemoryArena
{
    public class HomeForm : Form
    {
        private bool hasResumeData = false;
        private int lives = 3;
        private Label lblUsername, lblRank, lblPoints;
        private FlowLayoutPanel livesPanel;
        private Timer cooldownTimer;
        private DateTime nextLifeTime;
        private Font ralewayFont;

        public HomeForm()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.BackgroundImage = Image.FromFile(Path.Combine("Assets", "background.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            ralewayFont = LoadRalewayBlack(30f);

            InitBaseLayers();
            InitProfile();
            InitButtons();
            InitLives();
        }

        private void InitBaseLayers()
        {
            PictureBox deck = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine("Assets", "button-deck.png")),
                Location = new Point(55, 300),
                Size = new Size(427, 460),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(deck);

            PictureBox settings = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine("Assets", "settings.png")),
                Location = new Point(20, 10),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            settings.Click += (s, e) => MessageBox.Show("Settings clicked");
            this.Controls.Add(settings);
            settings.BringToFront();
        }

        private void InitProfile()
        {
            PictureBox profileDeck = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile-deck.png")),
                Size = new Size(498, 212),
                Location = new Point(30, 70),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
            };
            this.Controls.Add(profileDeck);
            profileDeck.BringToFront();

            PictureBox profilePic = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile.png")),
                Size = new Size(100, 100),
                Location = new Point(70, 120),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White,
            };
            this.Controls.Add(profilePic);
            profilePic.BringToFront();

            lblUsername = new Label()
            {
                Text = "krzzna",
                Font = ralewayFont,
                ForeColor = Color.Black,
                Location = new Point(170, 110),
                AutoSize = true,
                BackColor = Color.White,
            };
            this.Controls.Add(lblUsername);
            lblUsername.BringToFront();

            lblRank = new Label()
            {
                Text = "Rank: Bronze",
                Font = new Font("Arial", 15),
                ForeColor = Color.Black,
                Location = new Point(177, 165),
                AutoSize = true,
                BackColor = Color.White,
            };
            this.Controls.Add(lblRank);
            lblRank.BringToFront();

            lblPoints = new Label()
            {
                Text = "Points: 0",
                Font = new Font("Arial", 15),
                ForeColor = Color.Black,
                Location = new Point(177, 190),
                AutoSize = true,
                BackColor = Color.White,
            };
            this.Controls.Add(lblPoints);
            lblPoints.BringToFront();

            Button btnMore = CreateImageButton("more-profile.png", new Point(420, 145), new Size(28, 56), () => MessageBox.Show("More Profile"));
            btnMore.BackColor = Color.White;
            this.Controls.Add(btnMore);
            btnMore.BringToFront();
        }

        private void InitButtons()
        {
            Button btnStart = CreateImageButton("button-newgame.png", new Point(115, 360), new Size(315, 100), () => new GameForm().Show());
            btnStart.BackColor = Color.White;
            this.Controls.Add(btnStart);
            btnStart.BringToFront();

            if (hasResumeData)
            {
                Button btnResume = CreateImageButton("button-resumegame.png", new Point(170, 260), new Size(315, 100), () => MessageBox.Show("Resume Game"));
                btnResume.BackColor = Color.LightGreen;
                this.Controls.Add(btnResume);
                btnResume.BringToFront();
            }

            Button btnBattle = CreateImageButton("button-endgame.png", new Point(115, 480), new Size(315, 100), () => MessageBox.Show("Battle Mode"));
            btnBattle.BackColor = Color.White;
            this.Controls.Add(btnBattle);
            btnBattle.BringToFront();
        }

        private void InitLives()
        {
            livesPanel = new FlowLayoutPanel()
            {
                Location = new Point(350, 10),
                Size = new Size(150, 50),
                BackColor = Color.Transparent
            };
            this.Controls.Add(livesPanel);
            livesPanel.BringToFront();
            DrawLives();
        }

        private void DrawLives()
        {
            livesPanel.Controls.Clear();
            for (int i = 0; i < lives; i++)
            {
                PictureBox liveIcon = new PictureBox()
                {
                    Image = Image.FromFile(Path.Combine("Assets", "live.png")),
                    Size = new Size(43, 43),
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                livesPanel.Controls.Add(liveIcon);
            }

            if (lives < 3)
            {
                StartLifeCooldown();
            }
        }

        private void StartLifeCooldown()
        {
            nextLifeTime = DateTime.Now.AddMinutes(5);
            cooldownTimer = new Timer { Interval = 1000 };
            cooldownTimer.Tick += (s, e) =>
            {
                if (DateTime.Now >= nextLifeTime)
                {
                    lives++;
                    cooldownTimer.Stop();
                    DrawLives();
                }
            };
            cooldownTimer.Start();
        }

        private Button CreateImageButton(string assetName, Point location, Size size, Action onClick)
        {
            var button = new Button
            {
                BackgroundImage = Image.FromFile(Path.Combine("Assets", assetName)),
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat,
                Size = size,
                Location = location,
                TabStop = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += (s, e) => onClick();
            return button;
        }

        private Font LoadRalewayBlack(float size)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine("Assets", "Raleway-Black.ttf"));
            return new Font(pfc.Families[0], size, FontStyle.Regular);
        }
    }
}
