using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class NormalBossRandomizer : BossRandomizer
    {
        public NormalBossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile)
            :base(rand, optionFlags, spoilerFile)
        {
        }

        protected override void FillGTPool()
        {
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT1
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT2
            PossibleBossesPool.Add(Boss.GetRandomBoss(rand)); // GT3
        }
    }
}
