using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldGlitchedGraphData : GraphData
    {
        public OverworldGlitchedGraphData(RomData romData, OptionFlags optionFlags, RomEntranceCollection romEntrances, RomExitCollection romExits, RomChestCollection romChests)
            :base(romData, optionFlags, romEntrances, romExits, romChests)
        {

        }
    }
}
