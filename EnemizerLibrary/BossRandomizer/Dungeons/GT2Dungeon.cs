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
            LogicalBossRoomId = "gt-lanmolas";
            DungeonRoomSpritePointerAddress = 0x04D706;
            BossDropItemAddress = null;

            // TODO: stop blind from spawning in GT2?
            //DisallowedBosses.Add(BossType.Blind);

            ShellX = 0x0B;
            ShellY = 0x28;

            ExtraSprites = new byte[]
            {
                0x18, 0x17, 0xD1, // bunny beam
                0x1C, 0x03, 0xC5  // medusa
            };
        }
    }
}
