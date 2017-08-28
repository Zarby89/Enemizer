using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class BlindBoss : Boss
    {
        public BlindBoss() : base(BossType.Blind)
        {
            BossPointer = new byte[] { 0x54, 0xE6 };
            BossGraphics = 32;
        }
    }
}
