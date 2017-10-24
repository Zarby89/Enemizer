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
            BossSpriteId = SpriteConstants.BlindTheThiefSprite;
            BossNode = "thieves-blind";

            BossSpriteArray = new byte[]
            {
                0x05, 0x09, 0xCE // blind
            };
        }
    }
}
