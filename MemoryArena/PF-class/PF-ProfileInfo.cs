using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryArena
{
    public class PF_ProfileInfo : Panel
    {
        private Label lblPoints;

        public PF_ProfileInfo()
        {
            this.Size = new Size(497, 332);
            this.Location = new Point(22, 70);
            this.BackColor = Color.Transparent;

            PictureBox profileDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile-detail-deck.png")),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(profileDeck);
            profileDeck.SendToBack();

            PictureBox profilePic = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "profile.png")),
                Size = new Size(100, 100),
                Location = new Point(28, 31),
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
                Location = new Point(133, 20),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblUsername);
            lblUsername.BringToFront();

            lblPoints = new Label
            {
                Text = $"Points: {PlayerData.Score}",
                Font = new Font("Arial", 15),
                ForeColor = Color.Black,
                Location = new Point(145, 105),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblPoints);
            lblPoints.BringToFront();
            AddRankInfo();
        }

        private void AddRankInfo()
        {
            Label lblCurrentTitle = new Label 
            { 
                Text = "Current Rank:", 
                Font = new Font("Arial", 10), 
                Location = new Point(99, 159), 
                AutoSize = true, 
                BackColor = Color.White 
            };
            this.Controls.Add(lblCurrentTitle);
            lblCurrentTitle.BringToFront();

            PictureBox currentRank = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "rank-bronze.png")),
                Size = new Size(106, 106),
                Location = new Point(89, 177),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(currentRank);
            currentRank.BringToFront();

            Label lblCurrent = new Label 
            { 
                Text = "Bronze", 
                Font = new Font("Arial", 10), 
                Location = new Point(117, 288), 
                AutoSize = true, 
                BackColor = Color.White 
            };
            this.Controls.Add(lblCurrent);
            lblCurrent.BringToFront();


            Label lblHighestTitle = new Label 
            { 
                Text = "Highest Rank:", 
                Font = new Font("Arial", 10), 
                Location = new Point(299, 159), 
                AutoSize = true, 
                BackColor = Color.White 
            };
            this.Controls.Add(lblHighestTitle);
            lblHighestTitle.BringToFront();

            PictureBox highestRank = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "rank-diamond.png")),
                Size = new Size(106, 106),
                Location = new Point(291, 177),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(highestRank);
            highestRank.BringToFront();

            Label lblHighest = new Label 
            { 
                Text = "Diamond", 
                Font = new Font("Arial", 10), 
                Location = new Point(309, 283), 
                AutoSize = true, 
                BackColor = Color.White 
            };
            this.Controls.Add(lblHighest);
            lblHighest.BringToFront();
        }

        private Font LoadRalewayBlack()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine("Assets", "Raleway-Black.ttf"));
            return new Font(pfc.Families[0], 10f);
        }

        public void RefreshPoints()
        {
            lblPoints.Text = $"Points: {PlayerData.Score}";
        }
    }
}
