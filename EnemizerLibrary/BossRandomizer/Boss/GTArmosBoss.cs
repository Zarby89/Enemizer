using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    // this should only be used for resetting a rom back to vanilla
    public class GTArmosBoss : Boss
    {
        public GTArmosBoss() : base(BossType.Armos)
        {
            BossPointer = new byte[] { 0x23, 0xDB };
            BossGraphics = 9;
            BossSpriteId = SpriteConstants.ArmosKnightsSprite;
            BossNode = "gt-armos";

            BossSpriteArray = new byte[] 
            {
                0x05, 0x04, 0x53, // armos
                0x05, 0x07, 0x53, // armos
                0x05, 0x0A, 0x53, // armos
                0x08, 0x0A, 0x53, // armos
                0x08, 0x07, 0x53, // armos
                0x08, 0x04, 0x53, // armos
                0x08, 0xE7, 0x19, // armos trigger OL
                0x07, 0x07, 0xE3, // fairy
                0x07, 0x07, 0xE3, // fairy
                0x07, 0x07, 0xE3, // fairy
                0x07, 0x07, 0xE3, // fairy
            };
        }
    }
}
