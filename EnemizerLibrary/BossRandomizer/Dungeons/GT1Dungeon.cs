using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GT1Dungeon : Dungeon
    {
        public GT1Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 1";
            DungeonType = DungeonType.GanonsTower1;
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 28;
            LogicalBossRoomId = "gt-armos";
            DungeonRoomSpritePointerAddress = 0x04D666;
            BossDropItemAddress = null;

            // TODO: stop kholdstare from spawning in GT1?
            //DisallowedBosses.Add(BossType.Kholdstare);

            ShellX = 0x2B;
            ShellY = 0x28;

            ExtraSprites = new byte[]
            {
                0x07, 0x07, 0xE3, // fairy
                0x07, 0x08, 0xE3, // fairy
                0x08, 0x07, 0xE3, // fairy
                0x08, 0x08, 0xE3  // fairy
            };
        }
    }
}
