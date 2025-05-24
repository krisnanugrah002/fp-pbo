// AnimatedForm.cs
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryArena
{
    public class AnimatedForm : Form
    {
        public async void AnimateFadeIn()
        {
            this.Opacity = 0;
            Show();
            for (double d = 0.0; d <= 1.0; d += 0.05)
            {
                await Task.Delay(10);
                this.Opacity = d;
            }
        }
    }
}