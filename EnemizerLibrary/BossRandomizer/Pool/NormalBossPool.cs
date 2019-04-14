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
            List<Boss> GTPool = new List<Boss>(pool);
            Boss newBoss = Boss.GetRandomBoss(rand, new GT1Dungeon()); // GT1
            GTPool.Add(newBoss);
            do {
                newBoss = Boss.GetRandomBoss(rand, new GT2Dungeon()); // GT2
            } while (GTPool.contains(newBoss));
            GTPool.Add(newBoss);
            do {
                newBoss = Boss.GetRandomBoss(rand, new GT3Dungeon()); // GT3
            } while (GTPool.contains(newBoss));
            GTPool.Add(newBoss);
            GTPool.ForEach(boss => pool.add(boss));
        }
    }
}
