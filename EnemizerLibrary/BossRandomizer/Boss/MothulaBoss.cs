using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class MothulaBoss : Boss
    {
        public MothulaBoss() : base(BossType.Mothula)
        {
            BossPointer = new byte[] { 0x31, 0xDC };
            BossGraphics = 26;
        }
    }
}
