using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GTMoldormBoss : Boss
    {
        public GTMoldormBoss() : base(BossType.Moldorm)
        {
            BossPointer = new byte[] { 0x1E, 0xDF };
            BossGraphics = 12;
            BossSpriteId = SpriteConstants.MoldormSprite;
            BossNode = "gt-moldorm";

            BossSpriteArray = new byte[]
            {
                0x09, 0x09, 0x09 // moldorm
            };
        }
    }
}
