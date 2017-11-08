using System;
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
            LogicalBossRoomId = "skull-mothula";
            DungeonRoomSpritePointerAddress = 0x04D680;
            BossDropItemAddress = 0x180155;

            // Kholdstare should work now
            //DisallowedBosses.Add(BossType.Kholdstare); // the moth room does really strange stuff
            DisallowedBosses.Add(BossType.Trinexx);
            DisallowedBosses.Add(BossType.Vitreous); // key drop in room above is busted with vitreous

            ShellX = 0x2B;
            ShellY = 0x28;
        }
    }
}
