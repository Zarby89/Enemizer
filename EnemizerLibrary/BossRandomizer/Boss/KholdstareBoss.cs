using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class KholdstareBoss : Boss
    {
        public KholdstareBoss() : base(BossType.Kholdstare)
        {
            BossPointer = new byte[] { 0x01, 0xEA };
            BossGraphics = 22;
            BossNode = "ice-kholdstare";
        }

        protected new void FillRules()
        {
            Requirements = "Fire Rod;Bombos,L1 Sword";
            Rules.Add(this.CheckGTowerAndPedestalForItems);
            Rules.Add(this.CheckShabadooHasItem);
            Rules.Add(this.CheckFatFairyHasItem);
            Rules.Add(this.CheckBossDropHasImportantItem);
        }

        public override bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return base.CheckRules(dungeon, romData, ItemConstants.FireRod);
        }
    }
}
