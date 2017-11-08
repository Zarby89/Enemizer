using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class TurtleRockDungeon : Dungeon
    {
        public TurtleRockDungeon(int priority = 255) : base(priority)
        {
            Name = "Turtle Rock";
            DungeonType = DungeonType.TurtleRock;
            DungeonCrystalTypeAddress = CrystalConstants.TurtleRockCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.TurtleRockCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 164;
            LogicalBossRoomId = "turtle-trinexx";
            DungeonRoomSpritePointerAddress = 0x04D776;
            BossDropItemAddress = 0x180159;

            ShellX = 0x0B;
            ShellY = 0x28;
            ClearLayer2 = true;
        }
    }
}
