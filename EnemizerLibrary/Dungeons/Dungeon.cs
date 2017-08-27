using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public abstract class Dungeon
    {
        public string Name { get; set; }
        public int Priority { get; set; } = 255;
        public int? DungeonCrystalTypeAddress { get; set; }
        public int? DungeonCrystalAddress { get; set; }
        public BossType BossType { get; set; } // TODO: need?
        public Boss SelectedBoss { get; set; }
        public int BossRoomId { get; set; }
        public int BossAddress { get; set; }

        List<BossType> DisallowedBosses { get; set; } = new List<BossType>();

        public Dungeon(int priority)
        {
            Priority = priority;
        }
    }

    public class EasternPalaceDungeon : Dungeon
    {
        public EasternPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Eastern Palace";
            DungeonCrystalTypeAddress = CrystalConstants.EasternPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.EasternPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 200;
            BossAddress = 0x04D7BE;
        }
    }

    public class DesertPalaceDungeon : Dungeon
    {
        public DesertPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Desert Palace";
            DungeonCrystalTypeAddress = CrystalConstants.DesertPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.DesertPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 51;
            BossAddress = 0x04D694;
        }
    }

    public class TowerOfHeraDungeon : Dungeon
    {
        public TowerOfHeraDungeon(int priority = 255) : base(priority)
        {
            Name = "Tower of Hera";
            DungeonCrystalTypeAddress = CrystalConstants.TowerOfHeraCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.TowerOfHeraCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 7;
            BossAddress = 0x04D63C;
        }
    }

    public class PalaceOfDarknessDungeon : Dungeon
    {
        public PalaceOfDarknessDungeon(int priority = 255) : base(priority)
        {
            Name = "Palace of Darkness";
            DungeonCrystalTypeAddress = CrystalConstants.PalaceOfDarknessCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.PalaceOfDarknessCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 90;
            BossAddress = 0x04D6E2;
        }
    }

    public class SwampPalaceDungeon : Dungeon
    {
        public SwampPalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Swamp Palace";
            DungeonCrystalTypeAddress = CrystalConstants.SwampPalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.SwampPalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 6;
            BossAddress = 0x04D63A;
        }
    }

    public class SkullWoodsDungeon : Dungeon
    {
        public SkullWoodsDungeon(int priority = 255) : base(priority)
        {
            Name = "Skull Woods";
            DungeonCrystalTypeAddress = CrystalConstants.SkullWoodsCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.SkullWoodsCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 41;
            BossAddress = 0x04D680;
        }
    }

    public class ThievesTownDungeon : Dungeon
    {
        public ThievesTownDungeon(int priority = 255) : base(priority)
        {
            Name = "Thieves' Town";
            DungeonCrystalTypeAddress = CrystalConstants.ThievesTownCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.ThievesTownCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 172;
            BossAddress = 0x04D786;
        }
    }

    public class IcePalaceDungeon : Dungeon
    {
        public IcePalaceDungeon(int priority = 255) : base(priority)
        {
            Name = "Ice Palace";
            DungeonCrystalTypeAddress = CrystalConstants.IcePalaceCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.IcePalaceCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 222;
            BossAddress = 0x04D7EA;
        }
    }

    public class MiseryMireDungeon : Dungeon
    {
        public MiseryMireDungeon(int priority = 255) : base(priority)
        {
            Name = "Misery Mire";
            DungeonCrystalTypeAddress = CrystalConstants.MiseryMireCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.MiseryMireCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 144;
            BossAddress = 0x04D74E;
        }
    }

    public class TurtleRockDungeon : Dungeon
    {
        public TurtleRockDungeon(int priority = 255) : base(priority)
        {
            Name = "Turtle Rock";
            DungeonCrystalTypeAddress = CrystalConstants.TurtleRockCrystalTypeAddress;
            DungeonCrystalAddress = CrystalConstants.TurtleRockCrystalAddress;
            SelectedBoss = null;
            BossRoomId = 164;
            BossAddress = 0x04D776;
        }
    }

    public class GT1Dungeon : Dungeon
    {
        public GT1Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 1";
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 28;
            BossAddress = 0x04D666;
        }
    }

    public class GT2Dungeon : Dungeon
    {
        public GT2Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 2";
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 108;
            BossAddress = 0x04D706;
        }
    }

    public class GT3Dungeon : Dungeon
    {
        public GT3Dungeon(int priority = 255) : base(priority)
        {
            Name = "Ganon's Tower 3";
            DungeonCrystalTypeAddress = null;
            DungeonCrystalAddress = null;
            SelectedBoss = null;
            BossRoomId = 77;
            BossAddress = 0x04D6C8;
        }
    }
}
