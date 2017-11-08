using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class PalaceOfDarknessDungeon : Dungeon
    {
        public PalaceOfDarknessDungeon(int priority = 255) : base(priority)
        {
            Name = "Palace of Darkness";
            DungeonType = DungeonType.PalaceOfDarkness;
            DungeonCrystalTypeAddress = CrystalConstants.PalaceOfDarknessCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.PalaceOfDarknessCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 90;
            LogicalBossRoomId = "pod-helmasaur";
            DungeonRoomSpritePointerAddress = 0x04D6E2;
            BossDropItemAddress = 0x180153;

            ShellX = 0x2B;
            ShellY = 0x28;
        }
    }
}
