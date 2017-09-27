using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class ThievesTownDungeon : Dungeon
    {
        public ThievesTownDungeon(int priority = 255) : base(priority)
        {
            Name = "Thieves' Town";
            DungeonType = DungeonType.ThievesTown;
            DungeonCrystalTypeAddress = CrystalConstants.ThievesTownCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.ThievesTownCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 172;
            BossAddress = 0x04D786;
            BossDropItemAddress = 0x180156;
        }
    }
}
