using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryArena
{
    public static class PlayerData
    {
        public static int Score { get; set; } = 0;
        public static int Lives { get; set; } = 3;
        public static DateTime? NextLifeTime { get; set; } = null;
    }
}
