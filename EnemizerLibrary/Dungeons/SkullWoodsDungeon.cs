﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SkullWoodsDungeon : Dungeon
    {
        public SkullWoodsDungeon(int priority = 255) : base(priority)
        {
            Name = "Skull Woods";
            DungeonType = DungeonType.SkullWoods;
            DungeonCrystalTypeAddress = CrystalConstants.SkullWoodsCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.SkullWoodsCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 41;
            BossAddress = 0x04D680;
            BossDropItemAddress = 0x180155;
        }
    }
}