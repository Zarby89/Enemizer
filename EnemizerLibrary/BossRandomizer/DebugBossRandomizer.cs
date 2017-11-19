using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DebugBossRandomizer : BossRandomizer
    {
        public DebugBossRandomizer(Random rand, OptionFlags optionFlags, StringBuilder spoilerFile, Graph graph)
            : base(rand, optionFlags, spoilerFile, graph)
        {
            this.bossPool = new ChaosBossPool(rand);
        }

        protected override void GenerateRandomizedBosses()
        {
            foreach(var dungeon in this.DungeonPool)
            {
                switch(optionFlags.DebugForceBossId)
                {
                    case BossType.Armos:
                        dungeon.SelectedBoss = new ArmosBoss();
                        break;
                    case BossType.Arrghus:
                        dungeon.SelectedBoss = new ArrghusBoss();
                        break;
                    case BossType.Blind:
                        dungeon.SelectedBoss = new BlindBoss();
                        break;
                    case BossType.Helmasaur:
                        dungeon.SelectedBoss = new HelmasaurBoss();
                        break;
                    case BossType.Kholdstare:
                        dungeon.SelectedBoss = new KholdstareBoss();
                        break;
                    case BossType.Lanmola:
                        dungeon.SelectedBoss = new LanmolaBoss();
                        break;
                    case BossType.Moldorm:
                        dungeon.SelectedBoss = new MoldormBoss();
                        break;
                    case BossType.Mothula:
                        dungeon.SelectedBoss = new MothulaBoss();
                        break;
                    case BossType.Trinexx:
                        dungeon.SelectedBoss = new TrinexxBoss();
                        break;
                    case BossType.Vitreous:
                        dungeon.SelectedBoss = new VitreousBoss();
                        break;
                }
            }
        }

    }
}
