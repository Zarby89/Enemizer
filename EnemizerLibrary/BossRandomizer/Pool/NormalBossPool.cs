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
            pool.Add(Boss.GetRandomBoss(rand)); // GT1
            pool.Add(Boss.GetRandomBoss(rand)); // GT2
            pool.Add(Boss.GetRandomBoss(rand)); // GT3
        }

    }
}
