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
        }
    }
}
