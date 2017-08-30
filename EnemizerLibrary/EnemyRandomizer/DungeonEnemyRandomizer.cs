using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonEnemyRandomizer
    {
        Random rand { get; set; }
        RomData romData { get; set; }

        public DungeonEnemyRandomizer(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
        }

        public void RandomizeDungeonEnemies()
        {

        }
    }
}
