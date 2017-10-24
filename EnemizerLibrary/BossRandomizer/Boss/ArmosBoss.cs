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
            BossSpriteId = SpriteConstants.ArmosKnightsSprite;
            BossNode = "eastern-armos";

            BossSpriteArray = new byte[] 
            {
                0x05, 0x04, 0x53, // armos
                0x05, 0x07, 0x53, // armos
                0x05, 0x0A, 0x53, // armos
                0x08, 0x0A, 0x53, // armos
                0x08, 0x07, 0x53, // armos
                0x08, 0x04, 0x53, // armos
                0x08, 0xE7, 0x19  // armos trigger OL
            };
        }
    }
}
