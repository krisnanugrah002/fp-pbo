// ProfileForm.cs
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MemoryArena
{
    public class ProfileForm : AnimatedForm
    {
        public ProfileForm()
        {
            try { this.BackgroundImage = Image.FromFile(Path.Combine("Assets", "ui-profile.png")); }
            catch { MessageBox.Show("Gagal memuat gambar ui-profile.png"); }

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(540, 960);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}