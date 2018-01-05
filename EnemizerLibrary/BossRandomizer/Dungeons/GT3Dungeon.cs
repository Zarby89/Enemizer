using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GT3Dungeon : Dungeon
    {
        public GT3Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 3";
            DungeonType = DungeonType.GanonsTower3;
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 77;
            LogicalBossRoomId = "gt-moldorm";
            DungeonRoomSpritePointerAddress = 0x04D6C8;
            BossDropItemAddress = null;

            DisallowedBosses.Add(BossType.Armos);
            DisallowedBosses.Add(BossType.Arrghus);
            DisallowedBosses.Add(BossType.Blind);
            DisallowedBosses.Add(BossType.Lanmola); // still broken. will spawn on the top left after first cycle and you'll be stuck
            DisallowedBosses.Add(BossType.Trinexx);

            ShellX = 0x18;
            ShellY = 0x16;
        }
    }
}
