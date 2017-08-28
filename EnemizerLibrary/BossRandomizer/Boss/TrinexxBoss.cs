using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class TrinexxBoss : Boss
    {
        public TrinexxBoss() : base(BossType.Trixnexx)
        {
            BossPointer = new byte[] { 0xBA, 0xE5 };
            BossGraphics = 23;
        }

        protected new void FillRules()
        {
            Rules.Add(this.CheckGTowerAndPedestalForItems);
            Rules.Add(this.CheckShabadooHasItem);
            Rules.Add(this.CheckFatFairyHasItem);
            Rules.Add(this.CheckBossDropHasImportantItem);
        }

        public override bool CheckRules(Dungeon dungeon, RomData romData)
        {
            return base.CheckRules(dungeon, romData, ItemConstants.FireRod, ItemConstants.IceRod);
        }
    }
}
