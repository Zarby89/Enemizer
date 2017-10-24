using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    // TODO: replace with enums that which can be replaced with enums
    public class AddressConstants
    {
        // enemizer header patched rom
        public static int dungeonHeaderBaseAddress = XkasSymbols.Instance.Symbols["room_header_table"]; //0x120090;

        public const int RoomHeaderBankLocation = 0x0B5E7;

        public const int dungeonHeaderPointerTableBaseAddress = 0x271E2;
        public const int dungeonSpritePointerTableBaseAddress = 0x4D62E;

        public static int movedRoomObjectBaseAddress = XkasSymbols.Instance.Symbols["modified_room_object_table"]; // 0x122000;
        public const int ObjectDataPointerTableAddress = 0xF8000;

        public const int OverworldAreaGraphicsBlockBaseAddress = 0x007A81;
        public const int OverworldSpritePointerTableBaseAddress = 0x04C901;

        public const int MoldormEyeCountAddressVanilla = 0x0EDBB3;
        public const int MoldormEyeCountAddressEnemizer = 0x200102;

        public const int NewBossGraphicsBaseAddress = 0x2F8000;

        public const int RandomSpriteGraphicsBaseAddress = 0x300000;

        public const int EnemizerFileLength = 0x400000;

        public const int HiddenEnemyChancePoolBaseAddress = 0xD7BBB;

        //public const byte MovedRoomBank = 0x24;
    }

    public class ItemConstants
    {
        public static readonly byte Nothing = 0xFF,
            Arrow_1 = 0x43,
            Rupee_1 = 0x34,
            Bombs_3 = 0x28,
            Rupee_5 = 0x35,
            Arrow_10 = 0x44,
            Bomb_10 = 0x31,
            Rupee_20 = 0x36,
            Rupee_20_2 = 0x47,
            Rupee_50 = 0x41,
            Rupee_100 = 0x40,
            Rupee_300 = 0x46,
            Bee_NoBottle = 0x0E,
            BigKey = 0x32,
            BlueMail = 0x22,
            FighterShield = 0x04,
            BluePotion_NoBottle = 0x30,
            BookOfMudora = 0x1D,
            BlueBoomerang = 0x0C,
            Bombos = 0x0F,
            Bomb = 0x27,
            Bottle = 0x16,
            Bottle_RedPotion = 0x2B,
            Bottle_GreenPotion = 0x2C,
            Bottle_BluePotion = 0x2D,
            Bottle_Bee = 0x3C,
            Bottle_Fairy = 0x3D,
            Bottle_GoldBee = 0x48,
            BowAndArrows = 0x3A,
            BowAndSilverArrows = 0x3B,
            Bow = 0x0B,
            BugNet = 0x21,
            CaneOfSomaria = 0x15,
            CaneOfByrna = 0x18,
            Compass = 0x25,
            Crystal_WILL_CRASH_GAME = 0x20,
            Ether = 0x10,
            Flippers = 0x1E,
            GreenPotion_NoBottle = 0x2F,
            Hammer = 0x09,
            HeartContainer_NoDialog = 0x26,
            SanctuaryHeart = 0x3F,
            Heart = 0x42,
            BossHeart = 0x3E,
            Hookshot = 0x0A,
            FireRod = 0x07,
            IceRod = 0x08,
            Key = 0x24,
            L1SwordAndShield = 0x00,
            L1Sword = 0x49,
            L2Sword = 0x50,
            L3Sword = 0x02,
            L4Sword = 0x03,
            Lamp = 0x12,
            MagicCape = 0x19,
            MagicMirror = 0x1A,
            Map = 0x33,
            MirrorShield = 0x06,
            Mushroom = 0x29,
            MoonPearl = 0x1F,
            OcarinaActive = 0x4A,
            OcarinaInactive = 0x14,
            PegasusBoots = 0x4B,
            PieceOfHeart = 0x17,
            PendantOfCourage = 0x37,
            PendantOfWisdom = 0x38,
            PendantOfPower = 0x39,
            Powder = 0x0D,
            PowerGloves = 0x1B,
            Quake = 0x11,
            FireShield = 0x05,
            RedBoomerang = 0x2A,
            RedMail = 0x23,
            RedPotion_NoBottle = 0x2E,
            Shovel = 0x13,
            SmallMagic = 0x45,
            TitansMitt = 0x1C,
            MaxBombs = 0x4C,
            MaxArrows = 0x4D,
            HalfMagic = 0x4E,
            QuarterMagic = 0x4F,
            MaxBombs_5 = 0x51,
            MaxBombs_10 = 0x52,
            MaxArrows_5 = 0x53,
            MaxArrows_10 = 0x54,
            Trap1 = 0x55,
            Trap2 = 0x56,
            Trap3 = 0x57,
            SilverArrows = 0x58,
            Rupoor = 0x59,
            NullItem = 0x5A, // ??
            RedClock = 0x5B,
            BlueClock = 0x5C,
            GreenClock = 0x5D,
            ProgressiveSword = 0x5E,
            ProgressiveShield = 0x5F,
            ProgressiveArmor = 0x60,
            ProgressiveLiftingGlove = 0x61,
            RNGPoolItem_Single = 0x62,
            RNGPoolItem_Multi = 0x63,
            GoalItem_Single_Triforce = 0x6A,
            GoalItem_Multi_PowerStar = 0x6B,
            Maps = 0x70,
            Compasses = 0x80,
            BigKeys = 0x90, // not sure what this is compared to big key
            SmallKeys = 0xA0;

        public static readonly IEnumerable<byte> ImportantItems = new byte[]
        {
            ItemConstants.BigKey,
            ItemConstants.BookOfMudora,
            ItemConstants.Bottle,
            ItemConstants.Bottle_RedPotion,
            ItemConstants.Bottle_GreenPotion,
            ItemConstants.Bottle_BluePotion,
            ItemConstants.Bottle_Bee,
            ItemConstants.Bottle_Fairy,
            ItemConstants.Bottle_GoldBee,
            ItemConstants.BowAndArrows,
            ItemConstants.BowAndSilverArrows,
            ItemConstants.Bow,
            ItemConstants.CaneOfSomaria,
            ItemConstants.CaneOfByrna,
            ItemConstants.Bombos,
            ItemConstants.Ether,
            ItemConstants.Quake,
            ItemConstants.FireRod,
            ItemConstants.IceRod,
            ItemConstants.Flippers,
            ItemConstants.Hammer,
            ItemConstants.Hookshot,
            ItemConstants.Key,
            ItemConstants.Lamp,
            ItemConstants.MagicCape,
            ItemConstants.MagicMirror,
            ItemConstants.Mushroom,
            ItemConstants.MoonPearl,
            ItemConstants.OcarinaInactive,
            ItemConstants.PegasusBoots,
            ItemConstants.Powder,
            ItemConstants.PowerGloves,
            ItemConstants.Shovel,
            ItemConstants.TitansMitt,
            ItemConstants.ProgressiveSword,
            ItemConstants.ProgressiveLiftingGlove
        };

        public static readonly int
            MasterSwordPedestalAddress = 0x289B0,
            SahasrahlaItemAddress = 0x2F1FC,
            CrystalTypePendant = 0x00,
            CrystalGreenPendant = 0x04,
            CrystalTypeCrystal = 0x40,
            Crystal5 = 0x04,
            Crystal6 = 0x01,
            FatFairyItem1Address = 0xE980,
            FatFairyItem2Address = 0xE983;
    }

    public class BossConstants
    {
        public static readonly byte
            KholdstareGraphics = 22,
            MoldormGraphics = 12,
            MothulaGraphics = 26,
            VitreousGraphics = 22,
            HelmasaurGraphics = 21,
            ArmosGraphics = 9,
            LanmolaGraphics = 11,
            BlindGraphics = 32,
            ArrghusGraphics = 20,
            TrinexxGraphics = 23;

        public static readonly byte[] BossGraphics = 
        {
            KholdstareGraphics, MoldormGraphics, MothulaGraphics, VitreousGraphics,
            HelmasaurGraphics, ArmosGraphics, LanmolaGraphics, BlindGraphics, ArmosGraphics, TrinexxGraphics
        };

        public static readonly string
            KholdstareName = "Kholdstare",
            MoldormName = "Moldorm",
            MothulaName = "Mothula",
            VitreousName = "Vitreous",
            HelmasaurName = "Helmasaur King",
            ArmosName = "Armos Knights",
            LanmolaName = "Lanmola",
            BlindName = "Blind",
            ArrghusName = "Arrghus",
            TrinexxName = "Trinexx";

        public static readonly string[] BossNames =
        {
            KholdstareName, MoldormName, MothulaName, VitreousName, HelmasaurName,
            ArmosName, LanmolaName, BlindName, ArrghusName, TrinexxName
        };

        public static readonly int
            TowerOfHeraBossDropItemAddress = 0x180152,
            EasternPalaceBossDropItemAddress = 0x180150,
            SkullWoodsBossDropItemAddress = 0x180155,
            DesertPalaceBossDropItemAddress = 0x180151,
            PalaceOfDarknessBossDropItemAddress = 0x180153,
            MiseryMireBossDropItemAddress = 0x180158,
            ThievesTownBossDropItemAddress = 0x180156,
            SwampPalaceBossDropItemAddress = 0x180154,
            IcePalaceBossDropItemAddress = 0x180157,
            TurtleRockBossDropItemAddress = 0x180159,
            GanonTower1BossDropItemAddress = 0x0,
            GanonTower2BossDropItemAddress = 0x0,
            GanonTower3BossDropItemAddress = 0x0;

        public static readonly int[] BossDropItemAddresses =  
        {
            TowerOfHeraBossDropItemAddress,
            EasternPalaceBossDropItemAddress,
            SkullWoodsBossDropItemAddress,
            DesertPalaceBossDropItemAddress,
            PalaceOfDarknessBossDropItemAddress,
            MiseryMireBossDropItemAddress,
            ThievesTownBossDropItemAddress,
            SwampPalaceBossDropItemAddress,
            IcePalaceBossDropItemAddress,
            TurtleRockBossDropItemAddress,
            GanonTower1BossDropItemAddress,
            GanonTower2BossDropItemAddress,
            GanonTower3BossDropItemAddress
        };

        public static readonly byte[] 
            KholdstarePointer = { 0x01, 0xEA },
            MoldormPointer = { 0xC3, 0xD9 },
            MothulaPointer = { 0x31, 0xDC },
            VitreousPointer = { 0x57, 0xE4 },
            HelmasaurPointer = { 0x49, 0xE0 },
            ArmosPointer = { 0x87, 0xE8 },
            LanmolaPointer = { 0xCB, 0xDC },
            BlindPointer = { 0x54, 0xE6 },
            ArrghusPointer = { 0x97, 0xD9 },
            TrinexxPointer = { 0xBA, 0xE5 };

        public static readonly byte[][] BossPointers =
        {
            KholdstarePointer,
            MoldormPointer,
            MothulaPointer,
            VitreousPointer,
            HelmasaurPointer,
            ArmosPointer,
            LanmolaPointer,
            BlindPointer,
            ArrghusPointer,
            TrinexxPointer
        };

        public static readonly byte
            KholdstareBossId = 0,
            MoldormBossId = 1,
            MothulaBossId = 2,
            VitreousBossId = 3,
            HelmasaurBossId = 4,
            ArmosBossId = 5,
            LanmolaBossId = 6,
            BlindBossId = 7,
            ArrghusBossId = 8,
            TrixnessBossId = 9,
            NoBossSetId = 255;
    }

    public class CrystalConstants
    {
        public static readonly int CrystalTypeBaseAddress = 0x180052;
        public static readonly int
            TowerOfHeraOffset = 8,
            EasternPalaceOffset = 0,
            SkullWoodsOffset = 6,
            DesertPalaceOffset = 1,
            PalaceOfDarknessOffset = 4,
            MiseryMireOffset = 5,
            ThievesTownOffset = 9,
            SwampPalaceOffset = 3,
            IcePalaceOffset = 7,
            TurtleRockOffset = 10;

        public static readonly int[] CrystalTypeAddresses =
        {
            CrystalTypeBaseAddress + TowerOfHeraOffset,
            CrystalTypeBaseAddress + EasternPalaceOffset,
            CrystalTypeBaseAddress + SkullWoodsOffset,
            CrystalTypeBaseAddress + DesertPalaceOffset,
            CrystalTypeBaseAddress + PalaceOfDarknessOffset,
            CrystalTypeBaseAddress + MiseryMireOffset,
            CrystalTypeBaseAddress + ThievesTownOffset,
            CrystalTypeBaseAddress + SwampPalaceOffset,
            CrystalTypeBaseAddress + IcePalaceOffset,
            CrystalTypeBaseAddress + TurtleRockOffset,
            0, 0, 0 // Ganon Tower
        };

        public static readonly int
            TowerOfHeraCrystalTypeAddress = CrystalTypeBaseAddress + TowerOfHeraOffset,
            EasternPalaceCrystalTypeAddress = CrystalTypeBaseAddress + EasternPalaceOffset,
            SkullWoodsCrystalTypeAddress = CrystalTypeBaseAddress + SkullWoodsOffset,
            DesertPalaceCrystalTypeAddress = CrystalTypeBaseAddress + DesertPalaceOffset,
            PalaceOfDarknessCrystalTypeAddress = CrystalTypeBaseAddress + PalaceOfDarknessOffset,
            MiseryMireCrystalTypeAddress = CrystalTypeBaseAddress + MiseryMireOffset,
            ThievesTownCrystalTypeAddress = CrystalTypeBaseAddress + ThievesTownOffset,
            SwampPalaceCrystalTypeAddress = CrystalTypeBaseAddress + SwampPalaceOffset,
            IcePalaceCrystalTypeAddress = CrystalTypeBaseAddress + IcePalaceOffset,
            TurtleRockCrystalTypeAddress = CrystalTypeBaseAddress + TurtleRockOffset;



        public static readonly int CrystalBaseAddress = 0x1209D;
        public static readonly int[] CrystalAddresses = 
        {
            CrystalBaseAddress + TowerOfHeraOffset,
            CrystalBaseAddress + EasternPalaceOffset,
            CrystalBaseAddress + SkullWoodsOffset,
            CrystalBaseAddress + DesertPalaceOffset,
            CrystalBaseAddress + PalaceOfDarknessOffset,
            CrystalBaseAddress + MiseryMireOffset,
            CrystalBaseAddress + ThievesTownOffset,
            CrystalBaseAddress + SwampPalaceOffset,
            CrystalBaseAddress + IcePalaceOffset,
            CrystalBaseAddress + TurtleRockOffset,
            0, 0, 0 // Ganon Tower
        };

        public static readonly int
            TowerOfHeraCrystalAddress = CrystalBaseAddress + TowerOfHeraOffset,
            EasternPalaceCrystalAddress = CrystalBaseAddress + EasternPalaceOffset,
            SkullWoodsCrystalAddress = CrystalBaseAddress + SkullWoodsOffset,
            DesertPalaceCrystalAddress = CrystalBaseAddress + DesertPalaceOffset,
            PalaceOfDarknessCrystalAddress = CrystalBaseAddress + PalaceOfDarknessOffset,
            MiseryMireCrystalAddress = CrystalBaseAddress + MiseryMireOffset,
            ThievesTownCrystalAddress = CrystalBaseAddress + ThievesTownOffset,
            SwampPalaceCrystalAddress = CrystalBaseAddress + SwampPalaceOffset,
            IcePalaceCrystalAddress = CrystalBaseAddress + IcePalaceOffset,
            TurtleRockCrystalAddress = CrystalBaseAddress + TurtleRockOffset;
    }

    public class DungeonConstants
    {
        public static readonly int
            TowerOfHeraDungeonId = 0,
            SwampPalaceDungeonId = 7,
            IcePalaceDungeonId = 8,
            TurtleRockDungeonId = 9,
            GTower3DungeonId = 12;

        //0x2E, 0xA0, 0xFF //trinexx // FF2 in HM
        //0x2D, 0xA1, 0xF9 //kholdstare // F95 in HM
        // Room 6 = Arrghus
        public const int room_6_shell_index = 10;
        public static readonly byte[] room_6_shell =
        {
            // header
            0xE1, 0x00,
            // bg1
            0x0C, 0xA5, 0x7F,
            0x6C, 0xA5, 0x80,
            0xFF, 0xFF,
            // bg2
            0x2D, 0xA1, 0xF9, // byte 10
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x61, 0x18,
            0xFF, 0xFF
        };

        // Room 7 = ToH Moldorm
        public const int room_7_shell_index = 307;
        public static readonly byte[] room_7_shell =
        {
            // header
            0x81, 0x1C,
            // bg1
            0x0A, 0x4E, 0x0D,   0x0A, 0xAA, 0x0E,   0x0B, 0x51, 0x61,   0xC0, 0x2C, 0xA2,   0xB0, 0x20, 0x0F,
            0xB0, 0x22, 0x62,   0xFE, 0xC1, 0x02,   0xC9, 0x38, 0x01,   0xFF, 0xA3, 0x82,   0xBA, 0xE6, 0x10,
            0xE8, 0xAA, 0x62,   0xFF, 0x43, 0xB9,   0x53, 0x53, 0xE0,   0x91, 0x53, 0xE0,   0x53, 0x91, 0xE0,
            0x91, 0x91, 0xE0,   0x3C, 0x6B, 0xC2,   0x3D, 0x9B, 0xC3,   0x54, 0xA6, 0xC3,   0x5C, 0xAA, 0xC3,
            0x68, 0xB1, 0xC3,   0x75, 0xB0, 0xC3,   0x8F, 0xB1, 0xC3,   0x9B, 0xAA, 0xC3,   0xA6, 0xA0, 0xC3,
            0xAD, 0x98, 0xC3,   0xB4, 0x6A, 0xC2,   0x51, 0x3D, 0xC3,   0x45, 0x49, 0xC3,   0x3D, 0x51, 0xC3,
            0x9C, 0x39, 0xC2,   0xA1, 0x49, 0xC3,   0xAD, 0x51, 0xC3,   0x3A, 0x50, 0x8A,   0x38, 0x50, 0x22,
            0x44, 0x44, 0x69,   0x44, 0x44, 0x22,   0x58, 0x13, 0x05,   0x60, 0x15, 0x55,   0x78, 0x10, 0x3A,
            0x08, 0x5B, 0x65,   0x0C, 0x61, 0x7F,   0xC8, 0x39, 0x05,   0xE8, 0x5B, 0x66,   0xEC, 0x4A, 0x80,
            0x58, 0xEB, 0x06,   0x60, 0xED, 0x56,   0x78, 0xEC, 0x3B,   0x50, 0x38, 0x69,   0x50, 0x38, 0x5F,
            0xA8, 0x38, 0x69,   0xA8, 0x44, 0x22,   0xB4, 0x44, 0x69,   0xB4, 0x51, 0x22,   0xC6, 0x50, 0x8A,
            0x3B, 0xC8, 0x22,   0x8B, 0xC8, 0x22,   0x74, 0xBC, 0x69,   0x88, 0xBC, 0x69,   0x63, 0x3C, 0xC2,
            0x66, 0x4F, 0x29,   0x64, 0x50, 0x6B,   0x5C, 0x54, 0x2B,   0x5C, 0x58, 0x6B,   0x54, 0x5C, 0x2B,
            0x54, 0x60, 0x6B,   0x4C, 0x64, 0x2B,   0x4E, 0x6B, 0x6B,   0x4C, 0x98, 0x2D,   0x54, 0x9C, 0x6B,
            0x54, 0xA0, 0x2D,   0x5C, 0xA4, 0x6B,   0x5C, 0xA8, 0x2D,   0x64, 0xAC, 0x6B,   0x66, 0xB3, 0x2A,
            0x98, 0xAC, 0x6A,   0x98, 0xA8, 0x2E,   0xA0, 0xA4, 0x6A,   0xA0, 0xA0, 0x2E,   0xA8, 0x9C, 0x6A,
            0xA8, 0x98, 0x2E,   0xB2, 0x6B, 0x6A,   0xA8, 0x64, 0x2C,   0xA8, 0x60, 0x6A,   0xA0, 0x5C, 0x2C,
            0xA0, 0x58, 0x6A,   0x98, 0x54, 0x2C,   0x98, 0x50, 0x6A,   0x68, 0x74, 0xC2,   0x68, 0x71, 0x27,
            0x68, 0x77, 0x6A,   0x74, 0x77, 0x6B,   0x68, 0x85, 0x28,   0xFC, 0x31, 0x72,   0x74, 0xAE, 0x04,
            0x71, 0xA0, 0xE0,   0x0A, 0x13, 0xA0,   0x0A, 0xBF, 0xA1,   0xBE, 0xF7, 0xA3,   0xC3, 0x11, 0xC0,
            0xD1, 0x31, 0x00,
            0xFF, 0xFF,
            // bg2
            0x61, 0x51, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0xFF, 0xFF

        };


        //0x2E, 0xA0, 0xFF //trinexx
        //0x2D, 0xA1, 0xF9 //kholdstare
        // Room 200 = Eastern Armos
        public const int room_200_shell_index = 13;
        public static readonly byte[] room_200_shell =
        {
            // type 1
            0xE1, 0x00,
            0x98, 0x92, 0x3A,
            0x88, 0xAA, 0x65,
            0xE8, 0xAA, 0x66,
            0xFF, 0xFF,
            0xAD, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x81, 0x18,
            0xFF, 0xFF,
        };

        // Room 41 = Mothula
        public const int room_41_shell_index = 0;
        public static readonly byte[] room_41_shell =
        {
            // type 1
            0xE5, 0x00,
            0x97, 0x9C, 0xDE,   0xB7, 0x9C, 0xDE,   0xD6, 0x9C, 0xDE,   0x97, 0xE4, 0xDE,   0xB7, 0xE4, 0xDE,
            0xD6, 0xE4, 0xDE,   0x94, 0xA7, 0xDE,   0x94, 0xC7, 0xDE,   0xE4, 0xA7, 0xDE,   0xE4, 0xC7, 0xDE,
            0xFF, 0xFF,
            0x03, 0x03, 0xCA,   0x43, 0x03, 0xCA,   0x83, 0x03, 0xCA,   0xC3, 0x03, 0xCA,   0x03, 0x43, 0xCA,
            0x43, 0x43, 0xCA,   0x83, 0x43, 0xCA,   0xC3, 0x43, 0xCA,   0x03, 0x83, 0xCA,   0x43, 0x83, 0xCA,
            0x83, 0x83, 0xCA,   0xC3, 0x83, 0xCA,   0x03, 0xC3, 0xCA,   0x43, 0xC3, 0xCA,   0x83, 0xC3, 0xCA,
            0xC3, 0xC3, 0xCA,
            0xFF, 0xFF,
            0x9F, 0xA7, 0xC6,   0xD4, 0xA7, 0xC6,   0xFE, 0xF9, 0xF4,   0xFF, 0x1E, 0x74,   0xFE, 0x5C, 0x74,
            0xFF, 0x9C, 0x74,
            //AD A9 F9 // kholdstare
            //AE A8 FF // trinexx
            // type 2
            0xF0, 0xFF,
            0xFF, 0xFF,
        };

        // Room 51 = Desert Lanmolas
        public const int room_51_shell_index = 4;
        public static readonly byte[] room_51_shell =
        {
            // type 1
            0xE9, 0x00,
            0xFF, 0xFF,
            0x2D, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x61, 0x18,
            0xFF, 0xFF,
        };

        // Room 90 = Helmasaur
        public const int room_90_shell_index = 22;
        public static readonly byte[] room_90_shell =
        {
            // type 1
            0xE9, 0x00,
            0xA8, 0xA8, 0xDE,   0xB0, 0xA0, 0xDE,   0xB8, 0xA8, 0xDE,   0xC0, 0xA0, 0xDE,   0xC8, 0xA8, 0xDE,
            0xD0, 0xA0, 0xDE,
            0xFF, 0xFF,
            0xAD, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x81, 0x18,
            0xFF, 0xFF,
        };

        // Room 144 = Vitreous
        public const int room_144_shell_index = 16;
        public static readonly byte[] room_144_shell =
        {
            // type 1
            0xE1, 0x00,
            0x28, 0xEC, 0x56,   0x48, 0xEC, 0x56,   0x1B, 0xA2, 0xFF,
            0xFF, 0xFF,
            0x16, 0x9C, 0xFE,   0x2D, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x61, 0x18,
            0xFF, 0xFF,
        };

        // Room 172 = Blind
        public const int room_172_shell_index = 34;
        public static readonly byte[] room_172_blind_room_shell =
        {
            0xE9, 0x00,
            0x88, 0xA4, 0x0D,   0x88, 0xD0, 0x0E,   0xE0, 0x90, 0x0F,   0xE0, 0xE4, 0x10,   0x89, 0xAB, 0x61,
            0xE9, 0xAB, 0x62,   0x88, 0x91, 0xA0,   0x88, 0xE5, 0xA1,   0xE4, 0x91, 0xA2,   0xE4, 0xF5, 0xA3,
            0xFF, 0xFF,
            0xAD, 0xA1, 0xF9,   0xB1, 0xA8, 0xFF,
            0xFF, 0xFF,
            0xF0, 0xFF,
            0x81, 0x18,
            0xFF, 0xFF,
            // TODO: remove blinds light
        };

        // Room 222 = Kholdstare
        public const int room_222_shell_index = 4;
        public static readonly byte[] room_222_shell =
        {
            // type 1
            0xE4, 0x00,
            0xFF, 0xFF,
            0xAD, 0x21, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0xFF, 0xFF,
        };

        // Room 164 = Trinexx
        public const int room_164_shell_index = 58;
        public static readonly byte[] room_164_shell =
        {
            // 0x2E, 0x98, 0xFF
            // type 2
            0xE1, 0x00,
            0xFC, 0x08, 0x00,   0x13, 0x80, 0x01,   0xFD, 0xC8, 0x02,   0x02, 0x93, 0x61,
            0xFC, 0x0E, 0x81,   0x13, 0xE8, 0x02,   0xFD, 0xCE, 0x83,   0x72, 0x93, 0x62,
            0x13, 0x93, 0xC4,   0x51, 0x93, 0xC4,   0x51, 0xC9, 0xC4,   0x10, 0xC9, 0xC4,
            0x0E, 0x8D, 0xDE,   0x0D, 0x9C, 0xDE,   0x0C, 0xA5, 0xDE,   0x5E, 0x8C, 0xDE,
            0x65, 0x94, 0xDE,   0x6C, 0x9C, 0xDE,
            0xFF, 0xFF,
            0x2D, 0xA1, 0xF9,
            //2E 98 FF
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x61, 0x18,
            0xFF, 0xFF,
        };

        // Room 28 = GT Armos
        public const int room_28_shell_index = 55;
        public static readonly byte[] room_28_shell =
        {
            // type 1
            0xE1, 0x00,
            0x2D, 0x32, 0xA4,   0xA9, 0x1E, 0xDC,   0xA8, 0x91, 0x3A,   0x88, 0xAD, 0x76,   0xEC, 0xAD, 0x77,
            0xA8, 0x50, 0x3D,   0xD0, 0x50, 0x3D,   0x30, 0xA9, 0x3D,   0x30, 0xC1, 0x3D,   0xFC, 0x69, 0x38,
            0x97, 0x9F, 0xD1,   0xCD, 0x9F, 0xD1,   0x97, 0xDC, 0xD1,   0xCD, 0xDC, 0xD1,   0xBD, 0x32, 0xF9,
            0xB1, 0x22, 0xF9,   0xC9, 0x22, 0xF9,
            0xFF, 0xFF,
            0xAD, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x80, 0x36, 0x82, 0x18, 0x60, 0x28,
            0xFF, 0xFF,
        };

        // Room 108 = GT Lanmolas
        public const int room_108_shell_index = 70;
        public static readonly byte[] room_108_shell =
        {
            // type 1
            0xE2, 0x00,
            0x17, 0x9F, 0xE8,   0x4D, 0x9F, 0xE8,   0x17, 0xDC, 0xE8,   0x4D, 0xDC, 0xE8,   0x18, 0xE1, 0xFE,
            0x88, 0xAD, 0x76,   0x99, 0xBC, 0x33,   0x9B, 0xBB, 0x34,   0x9B, 0xCF, 0x34,   0xD8, 0xB8, 0x34,
            0xD8, 0xCC, 0x34,   0xAF, 0xAA, 0xFE,   0xC7, 0xAA, 0xFE,   0xAF, 0xD2, 0xFE,   0xC7, 0xD2, 0xFE,
            0x28, 0x11, 0x3A,   0x28, 0x91, 0x3A,   0xFC, 0xE1, 0x38,   0x2B, 0x33, 0xFA,   0x53, 0x33, 0xFA,
            0x2B, 0x53, 0xFA,   0x53, 0x53, 0xFA,
            0xFF, 0xFF,
            0x2D, 0xA1, 0xF9,
            0xFF, 0xFF,
            // type 2
            0xF0, 0xFF,
            0x82, 0x38, 0x60, 0x18, 0x83, 0x00,
            0xFF, 0xFF,
        };

        // Room 77 = GT Moldorm
        public const int room_77_shell_index = 244;
        public static readonly byte[] room_77_shell =
        {
            // type 1
            0x82, 0x1C,
            0x09, 0x34, 0x0D,   0x08, 0x3A, 0x61,   0x09, 0xC0, 0x0E,   0x08, 0xC2, 0x61,   0xD1, 0x10, 0x0F,
            0xE8, 0x3A, 0x62,   0x5E, 0x1C, 0x03,   0x17, 0x49, 0x63,   0xDF, 0x4B, 0x64,   0xDC, 0xCA, 0x64,
            0xFF, 0x7D, 0xCB,   0x9D, 0xDF, 0x04,   0x3B, 0x5B, 0xE0,   0x7B, 0x5B, 0xE0,   0xB8, 0x5B, 0xE0,
            0x6A, 0xB1, 0xE0,   0x78, 0x54, 0xC2,   0x5B, 0x2A, 0xC2,   0x98, 0x2A, 0xC2,   0x21, 0x4B, 0xC3,
            0x21, 0x7B, 0xC3,   0x21, 0xA1, 0xC3,   0x38, 0x7B, 0xC2,   0x48, 0x8A, 0xC2,   0x3A, 0xAA, 0xC2,
            0x5B, 0x9C, 0xC2,   0xC9, 0x4B, 0xC3,   0xC9, 0x7B, 0xC3,   0xB8, 0x79, 0xC2,   0xA8, 0x88, 0xC2,
            0x9B, 0x9B, 0xC2,   0x9B, 0xD0, 0xC2,   0xD0, 0xA3, 0xC2,   0x78, 0x8C, 0xC2,   0x15, 0x45, 0x22,
            0x59, 0x1F, 0x69,   0xA5, 0x1F, 0x69,   0xC9, 0x45, 0x22,   0x68, 0xE4, 0x5E,   0x15, 0xB9, 0x22,
            0x35, 0xB9, 0x69,   0x37, 0xD9, 0x22,   0x88, 0xD9, 0x22,   0x98, 0xD9, 0x69,   0x66, 0xCB, 0x2A,
            /*                                      validation chest                                        */ 
            0x69, 0xC9, 0x04,   0x79, 0xCB, 0xF9,   0x8D, 0xBA, 0xF9,   0x37, 0x57, 0x29,   0x87, 0x57, 0x29,

            0x78, 0x5A, 0x6A,   0x84, 0x5A, 0x6B,   0x78, 0x65, 0x28,   0x35, 0x5B, 0x6B,   0x34, 0x7A, 0x2D,
            0x44, 0x7E, 0x6B,   0x44, 0x8A, 0x2D,   0x54, 0x8E, 0x6B,   0x55, 0x9B, 0x2A,   0x78, 0x8E, 0x6A,
            0x78, 0x89, 0x27,   0x84, 0x8E, 0x6B,   0x85, 0x9B, 0x2A,   0xA8, 0x8E, 0x6A,   0xA8, 0x8A, 0x2E,
            0xB8, 0x7E, 0x6A,   0xB8, 0x7A, 0x2E,   0xC9, 0x5B, 0x6A,   0x66, 0xAF, 0x29,   0x65, 0xB1, 0x6B,
            0x99, 0xB1, 0x6A,   0x38, 0x4B, 0x03,   0xA8, 0x4B, 0x03,   0x48, 0x13, 0x3A,   0x0C, 0x4A, 0x7F,
            0xEC, 0x4A, 0x80,   0xFE, 0xE1, 0x39,   0x09, 0x11, 0xA0,   0xD5, 0x11, 0xA2,   0x09, 0xD5, 0xA1,
            0xFF, 0xFF,
            0x61, 0x51, 0xFF,
            0x5B, 0x19, 0xDB,   0x98, 0x19, 0xDB,   0x11, 0x4B, 0xDB,   0x11, 0x8A, 0xDB,
            0x39, 0x48, 0xDB,   0xA9, 0x48, 0xDB,   0xD9, 0x4B, 0xDB,   0xD9, 0x8B, 0xDB,   0x6A, 0xC8, 0xDB,
            0x9B, 0xCA, 0xDB,   0xD9, 0xCA, 0xDB,
            0xFF, 0xFF,
            // 69 61 F9 // kholdstare
            // type 2
            0xF0, 0xFF,
            0x00, 0x1C,     0x22, 0x00,
            0xFF, 0xFF,
        };

    }

    public class AssemblyConstants
    {
        public static readonly byte
            NoOp = 0xEA;
    }
}
