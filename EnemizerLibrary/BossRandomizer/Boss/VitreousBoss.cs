using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class VitreousBoss : Boss
    {
        public VitreousBoss() : base(BossType.Vitreous)
        {
            BossPointer = new byte[] { 0x57, 0xE4 };
            BossGraphics = 22; // TODO: really?
            BossSpriteId = SpriteConstants.Vitreous_LargeEyeballSprite;
            BossNode = "mire-vitreous";
        }
    }
}
