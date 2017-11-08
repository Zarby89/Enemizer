using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class EasternPalaceDungeon : Dungeon
    {
        public EasternPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Eastern Palace";
            DungeonType = DungeonType.EasternPalace;
            DungeonCrystalTypeAddress = CrystalConstants.EasternPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.EasternPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 200;
            LogicalBossRoomId = "eastern-armos";
            DungeonRoomSpritePointerAddress = 0x04D7BE;
            BossDropItemAddress = 0x180150;

            ShellX = 0x2B;
            ShellY = 0x28;
        }
    }
}
