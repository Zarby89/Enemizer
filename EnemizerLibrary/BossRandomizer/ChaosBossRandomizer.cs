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
        public ChaosBossRandomizer(Random rand, OptionFlags optionFlags, StringBuilder spoilerFile, Graph graph)
            : base(rand, optionFlags, spoilerFile, graph)
        {
            this.bossPool = new ChaosBossPool(rand);
        }

        protected override void GenerateRandomizedBosses()
        {
            foreach(var dungeon in this.DungeonPool)
            {
                if (optionFlags.DebugMode)
                {
                    dungeon.SelectedBoss = new KholdstareBoss();
                    //dungeon.SelectedBoss = new TrinexxBoss();
                    continue;
                }

                dungeon.SelectedBoss = bossPool.GetRandomBoss(dungeon, graph);
                graph.UpdateDungeonBoss(dungeon);
                /*
                var result = graph.FindPath("cave-links-house", "triforce-room");
                if (result.Success == false)
                {
                    // later placed boss must have made earlier placed boss unbeatable
                    foreach(var d in this.DungeonPool.Where(x => x.SelectedBoss != null).ToList())
                    {
                        if(!CanGetBossRoomAndDefeat(d, result))
                        {
                            d.SelectedBoss = null;
                            graph.UpdateDungeonBoss(d);
                        }
                    }
                }//*/
            }
        }

    }
}
