using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class TrinexxBoss : Boss
    {
        public TrinexxBoss() : base(BossType.Trinexx)
        {
            BossPointer = new byte[] { 0xBA, 0xE5 };
            BossGraphics = 23;
            BossNode = "turtle-trinexx";
        }
    }
}
