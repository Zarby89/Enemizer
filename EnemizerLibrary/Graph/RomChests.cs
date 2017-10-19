using EnemizerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RomChest
    {
        public int Address { get; set; }
        public int ItemId { get; set; }
        public int RoomId { get; set; }
        public string LogicalId
        {
            get
            {
                return Data.GameItems.Items.Values.Where(x => x.Id == ItemId).Select(x => x.LogicalId).FirstOrDefault();
            }
        }

        RomData romData;

        public RomChest(RomData romData, int address)
        {
            this.romData = romData;

            Address = address;
            ItemId = romData[Address];

            if (CrystalAddresses.Contains(Address))
            {
                // crystal/pendant boss drop
                var crystalName = GetCrystalString(Address);
                ItemId = Data.GameItems.Items.Values.Where(x => x.LogicalId == crystalName).Select(x => x.Id).FirstOrDefault();
            }

            if(Address == MiseryMireMedallionAddress || Address == TurtleRockMedallionAddress)
            {
                var medallionName = RomItemConstants.GetEntranceMedallion(ItemId);
                ItemId = Data.GameItems.Items.Values.Where(x => x.LogicalId == medallionName).Select(x => x.Id).FirstOrDefault();
            }

            byte low = romData[Address - 2];
            byte high = romData[Address - 1];

            if (Address >= 0xE96E && Address <= 0xEB65)
            {
                if (high == 0x80)
                {
                    RoomId = low;
                }
                else
                {
                    RoomId = (high << 8) + low;
                }
            }
            else
            {
                RoomId = GetRoomIdFromAddress(Address);
            }
        }


        string GetCrystalString(int address)
        {
            var numberAddress = 0;
            var pendantCrystalType = RomItemConstants.GetPendantCrystalType(romData[address]);

            crystalMap.TryGetValue(address, out numberAddress);

            var number = romData[numberAddress];

            if (pendantCrystalType == "Pendant")
            {
                return $"<{pendantCrystalType} {RomItemConstants.GetPendantType(number)}>";
            }
            else
            {
                return $"<{pendantCrystalType} {RomItemConstants.GetCrystalType(number)}>";
            }
        }

        private int GetRoomIdFromAddress(int address)
        {
            // overworld areas will be returned with 0x80xx where xx is the aread id

            switch (address)
            {
                // Eastern Palace
                case 0x00C6FE:
                case 0x01209D:
                case 0x180150:
                    return RoomIdConstants.R200_EasternPalace_ArmosKnights; // 0x00C8; // 200

                // Desert Palace
                case 0x00C6FF:
                case 0x01209E:
                case 0x180151:
                    return RoomIdConstants.R51_DesertPalace_Lanmolas; // 0x0033; // 51

                // Swamp Palace
                case 0x00C701:
                case 0x0120A0:
                case 0x180154:
                    return RoomIdConstants.R6_SwampPalace_Arrghus; // 0x0006; // 6

                // Palace of Darkness
                case 0x00C702:
                case 0x0120A1:
                case 0x180153:
                    return RoomIdConstants.R90_PalaceofDarkness_HelmasaurKing; // 0x005A; // 90

                // Misery Mire
                case 0x00C703:
                case 0x0120A2:
                case 0x180158:
                    return RoomIdConstants.R144_MiseryMire_Vitreous; // 0x0090; // 144

                // Skull Woods
                case 0x00C704:
                case 0x0120A3:
                case 0x180155:
                    return RoomIdConstants.R41_SkullWoods_Mothula; // 0x0029; // 41

                // Ice Palace
                case 0x00C705:
                case 0x0120A4:
                case 0x180157:
                    return RoomIdConstants.R222_IcePalace_Kholdstare; // 0x00DE; // 222

                // Tower of Hera
                case 0x00C706:
                case 0x0120A5:
                case 0x180152:
                    return RoomIdConstants.R7_TowerofHera_Moldorm; // 0x0007; // 7

                // Thieves Town
                case 0x00C707:
                case 0x0120A6:
                case 0x180156:
                    return RoomIdConstants.R172_ThievesTown_BlindTheThief; // 0x00AC; // 172

                // Turtle Rock
                case 0x00C708:
                case 0x0120A7:
                case 0x180159:
                    return RoomIdConstants.R164_TurtleRock_Trinexx; // 0x00A4; // 164

                case 0x00EDA8: // Piece of Heart (Treasure Chest Game)	
                    return RoomIdConstants.R262_ChestGame_BombHouse; // 0x0106;
                case 0x0289B0: // Altar	
                    return 0x8080;
                case 0x02DF45: // Uncle	
                    return RoomIdConstants.R85_CastleSecretEntrance_UncleDeathRoom; // 0x0055;
                case 0x02EB18: // Bottle Vendor	
                    return 0x8018;
                case 0x02F1FC: // Sahasrahla	
                    return RoomIdConstants.R261_ShabadooHouse; // 0x0105;
                case 0x0330C7: // Flute Boy	
                    return 0x806A;
                case 0x03355C: // Blacksmiths2	
                    return RoomIdConstants.R289_SmithHouse; // 0x0121;
                case 0x0339CF: // Sick Kid	
                    return RoomIdConstants.R258_SickKid; // 0x0102;
                case 0x033D68: // Purple Chest	
                    return 0x803A;
                case 0x033E7D: // Hobo	
                    return 0x8080;
                case 0x0348FF: // Waterfall Bottle	
                    return RoomIdConstants.R276_WishingWell_Cave0x114; // 0x0114;
                case 0x034914: // Pyramid - Bow	
                    return RoomIdConstants.R278_FatFairy; // 0x0116;
                case 0x03493B: // Pyramid Bottle	
                    return RoomIdConstants.R278_FatFairy; // 0x0116;
                case 0x0EE185: // Catfish	
                    return 0x804F;
                case 0x0EE1C3: // King Zora	
                    return 0x8081;
                case 0x0F69FA: // Old Mountain Man	
                    return 0x8003;
                case 0x180000: // Piece of Heart (Thieves' Forest Hideout)	
                    return RoomIdConstants.R225_Cave_LostWoodsHP; // 0x00E1;
                case 0x180001: // Piece of Heart (Lumberjack Tree)	
                    return RoomIdConstants.R226_Cave_LumberjacksTreeHP; // 0x00E2;
                case 0x180002: // Piece of Heart (Spectacle Rock Cave)	
                    return RoomIdConstants.R234_Cave_SpectacleRockHP; // 0x00EA;
                case 0x180003: // Piece of Heart (south of Haunted Grove)	
                    return RoomIdConstants.R283_MirrorCaveGroveAndTomb; // 0x011B;
                case 0x180004: // Piece of Heart (Graveyard)	
                    return RoomIdConstants.R283_MirrorCaveGroveAndTomb; // 0x011B;
                case 0x180005: // Piece of Heart (Desert - northeast corner)	
                    return RoomIdConstants.R294_CheckerBoardCave; // 0x0126;
                case 0x180006: // Piece of Heart (Dark World blacksmith pegs)	
                    return RoomIdConstants.R295_HammerPegCave; // 0x0127;
                case 0x180010: // [cave-050] cave southwest of Lake Hylia - generous guy	
                    return RoomIdConstants.R291_MiniMoldormCave; // 0x0123;
                case 0x180011: // [cave-073] cave northeast of swamp palace - generous guy	
                    return RoomIdConstants.R286_HypeCave; // 0x011E;
                case 0x180012: // Library	
                    return RoomIdConstants.R263_Library_BombFarmRoom; // 0x0107;
                case 0x180013: // Mushroom	
                    return 0x8000;
                case 0x180014: // Witch	
                    return RoomIdConstants.R265_WitchHut; // 0x0109;
                case 0x180015: // Magic Bat	
                    return RoomIdConstants.R227_Cave_HalfMagic; // 0x00E3;
                case 0x180016: // Ether Tablet	
                    return 0x8003;
                case 0x180017: // Bombos Tablet	
                    return 0x8030;
                case 0x180022: // Misery Mire Medallion	
                    return 0x8070;
                case 0x180023: // Turtle Rock Medallion	
                    return 0x8047;
                case 0x180028: // Pyramid - Sword	
                    return RoomIdConstants.R278_FatFairy; // 0x0116;
                case 0x18002A: // Blacksmiths	
                    return RoomIdConstants.R289_SmithHouse; // 0x0121;
                case 0x180140: // Piece of Heart (Spectacle Rock)	
                    return 0x8003;
                case 0x180141: // Piece of Heart (Death Mountain - floating island)	
                    return 0x8005;
                case 0x180142: // Piece of Heart (Maze Race)	
                    return 0x8028;
                case 0x180143: // Piece of Heart (Desert - west side)	
                    return 0x8030;
                case 0x180144: // Piece of Heart (Lake Hylia)	
                    return 0x8035;
                case 0x180145: // Piece of Heart (Dam)	
                    return 0x803B;
                case 0x180146: // Piece of Heart (Dark World - bumper cave)	
                    return 0x804A;
                case 0x180147: // Piece of Heart (Pyramid)	
                    return 0x805B;
                case 0x180148: // Piece of Heart (Digging Game)	
                    return 0x8068;
                case 0x180149: // Piece of Heart (Zora's River)	
                    return 0x8081;
                case 0x18014A: // Haunted Grove item	
                    return 0x802A;
                case 0x180160: // [dungeon-L2-B1] Desert Palace - Small key room	
                    return 0x0073;
                case 0x180161: // [dungeon-A2-1F] Ganon's Tower - down left staircase from entrance	
                    return 0x008C;
                case 0x180162: // [dungeon-L3-1F] Tower of Hera - freestanding key	
                    return 0x0087;

                default:
                    throw new Exception("Invalid Address");
            }
        }

        readonly List<int> CrystalAddresses = new List<int>()
        {
            0xC705, 0xC703, 0xC702, 0xC704, 0xC701, 0xC707, 0xC708, 0xC6FF, 0xC6FE, 0xC706
        };

        readonly Dictionary<int, int> crystalMap = new Dictionary<int, int>()
        {
            { 0xC705, 0x120A4 },
            { 0xC703, 0x120A2 },
            { 0xC702, 0x120A1 },
            { 0xC704, 0x120A3 },
            { 0xC701, 0x120A0 },
            { 0xC707, 0x120A6 },
            { 0xC708, 0x120A7 },
            { 0xC6FF, 0x1209E },
            { 0xC6FE, 0x1209D },
            { 0xC706, 0x120A5 },
        };

        public const int MiseryMireMedallionAddress = 0x180022;
        public const int TurtleRockMedallionAddress = 0x180023;
    }

    public class RomChestCollection
    {
        public List<RomChest> Chests { get; set; }
        RomData romData;

        public RomChestCollection(RomData romData)
        {
            this.romData = romData;
            Chests = new List<RomChest>();
        }

        public void LoadChests(RawItemLocationCollection rawItemLocations)
        {
            foreach (var locations in rawItemLocations.RawItemLocations.Values
                                                    .Where(x => x.LocationAddress > 2) // skip any fake items like the frog or purple chest
                                                    .Select(x => x.LocationAddress)
                    )
            {
                Chests.Add(new RomChest(romData, locations));
            }
        }
    }

    public class RomItemConstants
    {
        public static string GetPendantType(int itemId)
        {
            string itemname;
            if (pendantType.TryGetValue(itemId, out itemname))
            {
                return itemname;
            }
            return null;
        }
        public readonly static Dictionary<int, string> pendantType = new Dictionary<int, string>()
        {
            { 1, "Red" },
            { 2, "Blue" },
            { 4, "Green" },
        };

        public static string GetCrystalType(int itemId)
        {
            string itemname;
            if (crystalType.TryGetValue(itemId, out itemname))
            {
                return itemname;
            }
            return null;
        }
        public readonly static Dictionary<int, string> crystalType = new Dictionary<int, string>()
        {
            { 1, "6" },
            { 2, "1" },
            { 4, "5" },
            { 8, "7" },
            { 16, "2" },
            { 32, "4" },
            { 64, "3" },
        };

        public static string GetPendantCrystalType(int itemId)
        {
            string itemname;
            if (crystalType6.TryGetValue(itemId, out itemname))
            {
                return itemname;
            }
            return null;
        }
        public readonly static Dictionary<int, string> crystalType6 = new Dictionary<int, string>()
        {
            { 1, "Pendant" },
            { 2, "Pendant" },
            { 3, "Pendant" },
            { 6, "Crystal" },
        };

        public static string GetEntranceMedallion(int itemId)
        {
            string itemname;
            if (entranceMedallion.TryGetValue(itemId, out itemname))
            {
                return itemname;
            }
            return null;
        }
        public readonly static Dictionary<int, string> entranceMedallion = new Dictionary<int, string>()
        {
            { 0, "Bombos" },
            { 1, "Ether" },
            { 2, "Quake" }
        };
    }
}
