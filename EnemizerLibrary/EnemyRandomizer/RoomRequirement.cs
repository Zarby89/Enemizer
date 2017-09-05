using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RoomGroupRequirement
    {
        public List<int> Rooms { get; set; }
        public int? GroupId { get; set; }
        public int? Subgroup0 { get; set; }
        public int? Subgroup1 { get; set; }
        public int? Subgroup2 { get; set; }
        public int? Subgroup3 { get; set; }

        public RoomGroupRequirement(int? GroupId, int? Subgroup0, int? Subgroup1, int? Subgroup2, int? Subgroup3, params int[] Rooms)
        {
            if(GroupId == null && Subgroup0 == null && Subgroup1 == null && Subgroup2 == null && Subgroup3 == null)
            {
                throw new Exception("RoomGroupRequirement needs at least one non-null GroupId or Subgroup.");
            }

            this.Rooms = Rooms.ToList();
            this.GroupId = GroupId;
            this.Subgroup0 = Subgroup0;
            this.Subgroup1 = Subgroup1;
            this.Subgroup2 = Subgroup2;
            this.Subgroup3 = Subgroup3;
        }
    }

    public class RoomGroupRequirementCollection
    {
        public List<RoomGroupRequirement> RoomRequirements { get; set; }

        public RoomGroupRequirementCollection()
        {
            RoomRequirements = new List<RoomGroupRequirement>();

            FillRoomRequirements();
        }

        void FillRoomRequirements()
        {
            RoomRequirements.Add(new RoomGroupRequirement(1, 70, 73, 28, 82, 
                RoomIdConstants.R33_HyruleCastle_KeyRatRoom, 
                RoomIdConstants.R228_Cave_LostOldManFinalCave,
                RoomIdConstants.R240_Cave_LostOldManStartingCave,
                RoomIdConstants.R263_Library_BombFarmRoom));

            RoomRequirements.Add(new RoomGroupRequirement(5, 75, 77, 74, 90,
                RoomIdConstants.R243_House_OldWoman_SahasrahlasWifeMaybe,
                RoomIdConstants.R255_Cave0xFF,
                RoomIdConstants.R265_WitchHut,
                RoomIdConstants.R270_Cave0x10E,
                RoomIdConstants.R271_Shop0x10F,
                RoomIdConstants.R272_Shop0x110,
                RoomIdConstants.R273_ArcherGame,
                RoomIdConstants.R274_CaveShop0x112,
                RoomIdConstants.R282_Shabadoo,
                RoomIdConstants.R284_BombShop,
                RoomIdConstants.R287_Shop0x11F,
                RoomIdConstants.R289_SmithHouse,
                RoomIdConstants.R290_FortuneTellers));

            RoomRequirements.Add(new RoomGroupRequirement(7, 75, 77, 57, 54,
                RoomIdConstants.R8_Cave_HealingFairy,
                RoomIdConstants.R44_Cave0x2C,
                RoomIdConstants.R276_WishingWell_Cave0x114,
                RoomIdConstants.R277_WishingWell_BigFairy));

            RoomRequirements.Add(new RoomGroupRequirement(13, 81, 73, 19, 0,
                RoomIdConstants.R85_CastleSecretEntrance_UncleDeathRoom,
                RoomIdConstants.R258_SickKid,
                RoomIdConstants.R260_LinksHouse));

            RoomRequirements.Add(new RoomGroupRequirement(15, 79, 77, 74, 80,
                RoomIdConstants.R244_House_AngryBrothers,
                RoomIdConstants.R245_House_AngryBrothers2,
                RoomIdConstants.R257_ScaredLadyHouses,
                RoomIdConstants.R259_Inn_BushHouse,
                RoomIdConstants.R262_ChestGame_BombHouse,
                RoomIdConstants.R280_Shop0x118,
                RoomIdConstants.R281_BlindsHouse));

            RoomRequirements.Add(new RoomGroupRequirement(18, 85, 61, 66, 67,
                RoomIdConstants.R32_AgahnimsTower_Agahnim,
                RoomIdConstants.R48_AgahnimsTower_MaidenSacrificeChamber));

            RoomRequirements.Add(new RoomGroupRequirement(24, 85, 26, 66, 67, RoomIdConstants.R13_GanonsTower_Agahnim2));

            RoomRequirements.Add(new RoomGroupRequirement(34, 33, 65, 69, 51, RoomIdConstants.R0_Ganon));

            RoomRequirements.Add(new RoomGroupRequirement(40, 14, 30 /* TODO: ??? */, 74, 80,
                RoomIdConstants.R225_Cave_LostWoodsHP,
                RoomIdConstants.R256_Shop0x100,
                RoomIdConstants.R291_MiniMoldormCave,
                RoomIdConstants.R292_UnknownCave_BonkCave,
                RoomIdConstants.R293_Cave0x125,
                RoomIdConstants.R294_CheckerBoardCave));

            RoomRequirements.Add(new RoomGroupRequirement(23, 64, null, null, 63, RoomIdConstants.R164_TurtleRock_Trinexx));

            RoomRequirements.Add(new RoomGroupRequirement(9, null, null, null, 29,
                RoomIdConstants.R28_GanonsTower_IceArmos,
                RoomIdConstants.R200_EasternPalace_ArmosKnights,
                RoomIdConstants.R227_Cave_HalfMagic));

            RoomRequirements.Add(new RoomGroupRequirement(11, null, null, null, 49,
                RoomIdConstants.R144_MiseryMire_Vitreous));

            // combine??
            RoomRequirements.Add(new RoomGroupRequirement(22, null, null, null, 61,
                RoomIdConstants.R51_DesertPalace_Lanmolas,
                RoomIdConstants.R108_GanonsTower_LanmolasRoom));
            RoomRequirements.Add(new RoomGroupRequirement(22, null, null, 60, null,
                RoomIdConstants.R222_IcePalace_Kholdstare));

            RoomRequirements.Add(new RoomGroupRequirement(21, null, null, 58, 62,
                RoomIdConstants.R90_PalaceofDarkness_HelmasaurKing));

            RoomRequirements.Add(new RoomGroupRequirement(28, null, null, 38, null,
                RoomIdConstants.R14_IcePalace_EntranceRoom,
                RoomIdConstants.R126_IcePalace_HiddenChest_BombableFloorRoom,
                RoomIdConstants.R142_IcePalace0x8E,
                RoomIdConstants.R158_IcePalace_BigChestRoom));
            RoomRequirements.Add(new RoomGroupRequirement(41, null, null, 38, null,
                RoomIdConstants.R190_IcePalace_BlockPuzzleRoom));

            RoomRequirements.Add(new RoomGroupRequirement(12, null, null, 48, null,
                RoomIdConstants.R7_TowerofHera_Moldorm,
                RoomIdConstants.R77_GanonsTower_MoldormRoom));

            RoomRequirements.Add(new RoomGroupRequirement(26, null, null, 56, null,
                RoomIdConstants.R41_SkullWoods_Mothula));

            RoomRequirements.Add(new RoomGroupRequirement(20, null, null, 57, null,
                RoomIdConstants.R6_SwampPalace_Arrghus));

            RoomRequirements.Add(new RoomGroupRequirement(32, null, null, 59, null,
                RoomIdConstants.R172_ThievesTown_BlindTheThief));

        }
    }
}
