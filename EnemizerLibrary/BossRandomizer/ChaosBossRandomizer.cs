using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class ChaosBossRandomizer : BossRandomizer
    {
        public ChaosBossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile)
            : base(rand, optionFlags, spoilerFile)
        {
        }
        protected override void FillBasePool()
        {
            // TODO: we need to ensure x number of bosses that can be defeated (can't have all trinexx)
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Armos
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Lanmolas
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Moldorm
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Helmasaur
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Arrghus
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Mothula
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Blind
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Kholdstare
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Vitreous
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // Trinexx
        }

        protected override void FillGTPool()
        {
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT1
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT2
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT3
        }
    }
}
