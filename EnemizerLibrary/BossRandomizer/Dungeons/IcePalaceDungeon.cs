using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class IcePalaceDungeon : Dungeon
    {
        public IcePalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Ice Palace";
            DungeonType = DungeonType.IcePalace;
            DungeonCrystalTypeAddress = CrystalConstants.IcePalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.IcePalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 222;
            DungeonRoomSpritePointerAddress = 0x04D7EA;
            BossDropItemAddress = 0x180157;

            ShellX = 0x2B;
            ShellY = 0x08;
            ClearLayer2 = true;
        }
    }
}
