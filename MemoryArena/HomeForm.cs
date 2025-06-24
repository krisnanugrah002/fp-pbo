using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Media;

namespace MemoryArena
{
    public class HomeForm : Form
    {
        private ProfileDeck profilePanel;

        public HomeForm()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.BackgroundImage = Image.FromFile(Path.Combine("Assets", "background.png"));
            this.BackgroundImageLayout = ImageLayout.Stretch;

            InitUI();
        }

        private void InitUI()
        {
            Font ralewayFont = FontLoader.LoadRalewayBlack(30f);

            InitBaseLayers();
            InitProfile(ralewayFont);
            InitButtons();
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
            settings.Click += (s, e) => new SettingsForm().ShowDialog();
            this.Controls.Add(settings);
            settings.BringToFront();
        }

        private void InitProfile(Font ralewayFont)
        {
            profilePanel = new ProfileDeck(ralewayFont);
            this.Controls.Add(profilePanel);
            profilePanel.BringToFront();
        }

        private void InitButtons()
        {
            Button btnStart = CreateImageButton("button-newgame.png", new Point(115, 360), new Size(315, 100), () => new GameForm().Show());
            btnStart.BackColor = Color.White;
            this.Controls.Add(btnStart);
            btnStart.BringToFront();

            Button btnBattle = CreateImageButton("button-endgame.png", new Point(115, 480), new Size(315, 100), () => MessageBox.Show("Battle Mode"));
            btnBattle.BackColor = Color.White;
            this.Controls.Add(btnBattle);
            btnBattle.BringToFront();
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

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            profilePanel.PointsLabel.Text = $"Points: {PlayerData.Score}";

        }
    }
}
