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
        public ChaosBossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile, Graph graph)
            : base(rand, optionFlags, spoilerFile, graph)
        {
            this.bossPool = new ChaosBossPool(rand);
        }

        protected override void GenerateRandomizedBosses()
        {
            foreach(var dungeon in this.DungeonPool)
            {
                dungeon.SelectedBoss = bossPool.GetRandomBoss(dungeon.DisallowedBosses, graph);
            }
        }
    }
}
