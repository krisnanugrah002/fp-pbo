using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryArena
{
    public class ProfileDeck : Panel
    {
        public Label UsernameLabel { get; private set; }
        public Label RankLabel { get; private set; }
        public Label PointsLabel { get; private set; }

        public ProfileDeck(Font ralewayFont)
        {
            this.Size = new Size(498, 212);
            this.Location = new Point(30, 70);
            this.BackColor = Color.Transparent;

            PictureBox profileDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile-deck.png")),
                Size = new Size(498, 212),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            this.Controls.Add(profileDeck);


            PictureBox profilePic = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile.png")),
                Size = new Size(100, 100),
                Location = new Point(40, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(profilePic);
            profilePic.BringToFront();

            UsernameLabel = new Label
            {
                Text = "krzzna",
                Font = ralewayFont,
                Location = new Point(160, 40),
                AutoSize = true,
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            this.Controls.Add(UsernameLabel);
            UsernameLabel.BringToFront();

            RankLabel = new Label
            {
                Text = "Rank: Bronze",
                Font = new Font("Arial", 15),
                Location = new Point(167, 95),
                AutoSize = true,
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            this.Controls.Add(RankLabel);
            RankLabel.BringToFront();

            PointsLabel = new Label
            {
                Text = "Points: 0",
                Font = new Font("Arial", 15),
                Location = new Point(167, 120),
                AutoSize = true,
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            this.Controls.Add(PointsLabel);
            PointsLabel.BringToFront();

            Button btnMore = new Button
            {
                BackgroundImage = Image.FromFile(Path.Combine("Assets", "more-profile.png")),
                BackgroundImageLayout = ImageLayout.Stretch,
                Size = new Size(28, 56),
                Location = new Point(440, 75),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };
            btnMore.FlatAppearance.BorderSize = 0;
            btnMore.Click += (s, e) => new ProfileForm().Show();
            this.Controls.Add(btnMore);
            btnMore.BringToFront();


        }
    }
}

