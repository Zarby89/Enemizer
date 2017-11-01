using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GTLanmolaBoss : Boss
    {
        public GTLanmolaBoss() : base(BossType.Lanmola)
        {
            BossPointer = new byte[] { 0xBE, 0xE1 };
            BossGraphics = 35;
            BossSpriteId = SpriteConstants.LanmolasSprite;
            BossNode = "gt-lanmolas";

            BossSpriteArray = new byte[]
            {
                0x07, 0x06, 0x54, // lanmolas
                0x07, 0x09, 0x54, // lanmolas
                0x09, 0x07, 0x54, // lanmolas
                0x18, 0x17, 0xD1, // bunny beam
                0x1C, 0x03, 0xC5, // medusa
            };
        }
    }
}
