using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class ArrghusBoss : Boss
    {
        public ArrghusBoss() : base(BossType.Arrghus)
        {
            BossPointer = new byte[] { 0x97, 0xD9 };
            BossGraphics = 20;
            BossNode = "swamp-arrghus";
        }
    }
}
