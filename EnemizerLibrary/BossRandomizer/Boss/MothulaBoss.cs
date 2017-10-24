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
            BossSpriteId = SpriteConstants.MothulaSprite;
            BossNode = "skull-mothula";

            BossSpriteArray = new byte[]
            {
                0x06, 0x08, 0x88 // mothula
                // 16 E7 07 // floor
            };
        }
    }
}
