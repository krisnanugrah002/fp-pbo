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
    public class PF_SkinCollection : Panel
    {
        public PF_SkinCollection()
        {
            this.Size = new Size(498, 358);
            this.Location = new Point(22, 425);
            this.BackColor = Color.Transparent;

            PictureBox skinDeck = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "skin-deck.png")),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.FromArgb(87, 191, 237)
            };
            this.Controls.Add(skinDeck);
            skinDeck.SendToBack();

            Label lblSkin = new Label
            {
                Text = "Skin Collection",
                Font = new Font(LoadRalewayBlack().FontFamily, 35f),
                ForeColor = Color.Black,
                Location = new Point(68, 5),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblSkin);
            lblSkin.BringToFront();

            PictureBox defaultSkin = new PictureBox
            {
                Image = Image.FromFile(Path.Combine("Assets", "skin-default.png")),
                Size = new Size(86, 86),
                Location = new Point(35, 80),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            this.Controls.Add(defaultSkin);
            defaultSkin.BringToFront();

            Label lblDefaultSkin = new Label
            {
                Text = "Default",
                Font = new Font("Arial", 10),
                Location = new Point(52, 170),
                AutoSize = true,
                BackColor = Color.White
            };
            this.Controls.Add(lblDefaultSkin);
            lblDefaultSkin.BringToFront();
        }

        private Font LoadRalewayBlack()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine("Assets", "Raleway-Black.ttf"));
            return new Font(pfc.Families[0], 10f);
        }
    }
}
