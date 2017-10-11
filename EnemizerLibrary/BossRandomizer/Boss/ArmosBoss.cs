using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class ArmosBoss : Boss
    {
        public ArmosBoss() : base(BossType.Armos)
        {
            BossPointer = new byte[] { 0x87, 0xE8 };
            BossGraphics = 9;
            BossNode = "eastern-armos";
        }
    }
}
