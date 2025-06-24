using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace MemoryArena
{
    public class ProfileForm : Form
    {
        private PF_ProfileInfo profileInfo;

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

            profileInfo = new PF_ProfileInfo();
            this.Controls.Add(profileInfo);

            PF_SkinCollection skinControl = new PF_SkinCollection();
            this.Controls.Add(skinControl);

            btnBack.BringToFront();
            profileInfo.BringToFront();
            skinControl.BringToFront();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            profileInfo.RefreshPoints();
        }

        private void InitBackground()
        {
            PictureBox backgroundLayer = new PictureBox
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
