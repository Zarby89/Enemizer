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
        }

    }
}
