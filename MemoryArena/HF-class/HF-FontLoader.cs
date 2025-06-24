using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryArena
{
    public static class FontLoader
    {
        public static Font LoadRalewayBlack(float size)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine("Assets", "Raleway-Black.ttf"));
            return new Font(pfc.Families[0], size);
        }
    }
}
