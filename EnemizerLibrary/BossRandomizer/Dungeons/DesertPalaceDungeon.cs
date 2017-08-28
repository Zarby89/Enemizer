using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DesertPalaceDungeon : Dungeon
    {
        public DesertPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Desert Palace";
            DungeonType = DungeonType.DesertPalace;
            DungeonCrystalTypeAddress = CrystalConstants.DesertPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.DesertPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 51;
            BossAddress = 0x04D694;
            BossDropItemAddress = 0x180151;
        }
    }
}
