using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class MoldormBoss : Boss
    {
        public MoldormBoss() : base(BossType.Moldorm)
        {
            BossPointer = new byte[] { 0xC3, 0xD9 };
            BossGraphics = 12;
            BossSpriteId = SpriteConstants.MoldormSprite;
            BossNode = "hera-moldorm";

            BossSpriteArray = new byte[]
            {
                0x09, 0x09, 0x09 // moldorm
            };
        }
    }
}
