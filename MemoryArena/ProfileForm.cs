using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace MemoryArena
{
    public class ProfileForm : Form
    {
        private PictureBox backgroundLayer;

        private Font LoadRalewayBlack()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine("Assets", "Raleway-Black.ttf"));
            return new Font(pfc.Families[0], 10f); // Ukuran bebas, kamu set ulang di atas
        }


        public ProfileForm()
        {
            this.DoubleBuffered = true;
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitBackground();

            PictureBox btnBack = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "back-home.png")),
                Size = new Size(50, 50),
                Location = new Point(15, 15),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);
            btnBack.BringToFront();

            PictureBox profileDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile-detail-deck.png")),
                Location = new Point(22, 70),
                Size = new Size(497, 332),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)

            };
            this.Controls.Add(profileDeck);
            profileDeck.BringToFront();

            PictureBox profilePic = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile.png")),
                Size = new Size(100, 100),
                Location = new Point(50, 101),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(profilePic);
            profilePic.BringToFront();


            Label lblUsername = new Label
            {
                Text = "krzzna",
                Font = new Font(LoadRalewayBlack().FontFamily, 50f),
                ForeColor = Color.Black,
                Location = new Point(155, 90),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblUsername);
            lblUsername.BringToFront();


            Label lblPoints = new Label()
            {
                Text = $"Points: {PlayerData.Score}",
                Font = new Font("Arial", 15),
                ForeColor = Color.Black,
                Location = new Point(177, 190),
                AutoSize = true,
                BackColor = Color.White,
            };



            Label lblCurrentTitle = new Label
            {
                Text = "Current Rank:",
                Font = new Font("Arial", 10),
                Location = new Point(121, 229),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblCurrentTitle);
            lblCurrentTitle.BringToFront();


            PictureBox currentRank = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "rank-bronze.png")),
                Size = new Size(106, 106),
                Location = new Point(111, 247),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(currentRank);
            currentRank.BringToFront();


            Label lblCurrent = new Label
            {
                Text = "Bronze",
                Font = new Font("Arial", 10),
                Location = new Point(139, 358),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblCurrent);
            lblCurrent.BringToFront();


            Label lblHighestTitle = new Label
            {
                Text = "Highest Rank:",
                Font = new Font("Arial", 10),
                Location = new Point(331, 229),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblHighestTitle);
            lblHighestTitle.BringToFront();


            PictureBox highestRank = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "rank-diamond.png")),
                Size = new Size(106, 106),
                Location = new Point(323, 247),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(highestRank);
            highestRank.BringToFront();


            Label lblHighest = new Label
            {
                Text = "Diamond",
                Font = new Font("Arial", 10),
                Location = new Point(341, 353),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblHighest);
            lblHighest.BringToFront();

            PictureBox SkinDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "skin-deck.png")),
                Size = new Size(498, 358),
                Location = new Point(22, 425),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(SkinDeck);
            SkinDeck.BringToFront();

            Label lblSkin = new Label
            {
                Text = "Skin Collection",
                Font = new Font(LoadRalewayBlack().FontFamily, 35f),
                ForeColor = Color.Black,
                Location = new Point(90, 430),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblSkin);
            lblSkin.BringToFront();

            PictureBox DefaultSkin = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "skin-default.png")),
                Size = new Size(86, 86),
                Location = new Point(57, 505),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(DefaultSkin);
            DefaultSkin.BringToFront();

            Label lblDefaultSkin = new Label
            {
                Text = "Default",
                Font = new Font("Arial", 10),
                Location = new Point(74, 595),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblDefaultSkin);
            lblDefaultSkin.BringToFront();

        }



        private void InitBackground()
        {
            backgroundLayer = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "background.png")),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(backgroundLayer);
            backgroundLayer.SendToBack();

        }
    }
}
