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
        public NormalBossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile, Graph graph)
            :base(rand, optionFlags, spoilerFile, graph)
        {
            this.bossPool = new NormalBossPool(rand);
        }
    }
}
