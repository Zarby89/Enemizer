using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class ChaosBossPool : BossPool
    {
        public ChaosBossPool(Random rand)
            :base(rand)
        {

        }

        public override void FillPool()
        {
            // do nothing, we will fake a pool
        }

        public override Boss GetRandomBoss(Dungeon dungeon, Graph graph)
        {
            return Boss.GetRandomBoss(rand, dungeon, dungeon.DisallowedBosses, graph);
        }

        public override void ReaddBoss(Boss boss)
        {
        }

        public override void RemoveBoss(Boss boss)
        {
        }
    }
}
