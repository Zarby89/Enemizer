using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SwampPalaceDungeon : Dungeon
    {
        public SwampPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Swamp Palace";
            DungeonType = DungeonType.SwampPalace;
            DungeonCrystalTypeAddress = CrystalConstants.SwampPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.SwampPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 6;
            LogicalBossRoomId = "swamp-arrghus";
            DungeonRoomSpritePointerAddress = 0x04D63A;
            BossDropItemAddress = 0x180154;

            ShellX = 0x0B;
            ShellY = 0x28;
            //ClearLayer2 = true;
        }
    }
}
