using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class LanmolaBoss : Boss
    {
        public LanmolaBoss() : base(BossType.Lanmola)
        {
            BossPointer = new byte[] { 0xCB, 0xDC };
            BossGraphics = 11;
            BossSpriteId = SpriteConstants.LanmolasSprite;
            BossNode = "desert-lanmolas";

            BossSpriteArray = new byte[]
            {
                0x07, 0x06, 0x54, // lanmolas
                0x07, 0x09, 0x54, // lanmolas
                0x09, 0x07, 0x54  // lanmolas
            };
        }
    }
}
