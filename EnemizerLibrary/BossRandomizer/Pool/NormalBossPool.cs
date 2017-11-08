using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class NormalBossPool : BossPool
    {
        public NormalBossPool(Random rand)
            :base(rand)
        {

        }

        protected override void FillGTPool()
        {
            pool.Add(Boss.GetRandomBoss(rand, new GT1Dungeon())); // GT1
            pool.Add(Boss.GetRandomBoss(rand, new GT2Dungeon())); // GT2
            pool.Add(Boss.GetRandomBoss(rand, new GT3Dungeon())); // GT3
        }

    }
}
