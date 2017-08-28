using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GT2Dungeon : Dungeon
    {
        public GT2Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 2";
            DungeonType = DungeonType.GanonsTower2;
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 108;
            BossAddress = 0x04D706;
            BossDropItemAddress = null;
        }
    }
}
