using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class KholdstareBoss : Boss
    {
        public KholdstareBoss() : base(BossType.Kholdstare)
        {
            BossPointer = new byte[] { 0x01, 0xEA };
            BossGraphics = 22;
            BossNode = "ice-kholdstare";
        }
    }
}
