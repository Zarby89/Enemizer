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
            BossAddress = 0x04D6C8;
            BossDropItemAddress = null;

            DisallowedBosses.Add(BossType.Armos);
            DisallowedBosses.Add(BossType.Arrghus);
            DisallowedBosses.Add(BossType.Blind);
            DisallowedBosses.Add(BossType.Lanmola);
            DisallowedBosses.Add(BossType.Trinexx);
        }
    }
}
