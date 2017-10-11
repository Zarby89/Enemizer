using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class HelmasaurBoss : Boss
    {
        public HelmasaurBoss() : base(BossType.Helmasaur)
        {
            BossPointer = new byte[] { 0x49, 0xE0 };
            BossGraphics = 21;
            BossNode = "pod-helmasaur";
        }
    }
}
