using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public static class Dungeons
    {
        public static Dictionary<string, string> DungeonKeys = new Dictionary<string, string>()
        {
          { HyruleCastle, "<Hyrule Key>" },
          { EasternPalace, "<Eastern Key>" },
          { DesertPalace, "<Desert Key>" },
          { TowerOfHera, "<Hera Key>" },
          { AgahnimsTower, "<Aga Key>" },
          { PalaceOfDarkness, "<PoD Key>" },
          { SwampPalace, "<Swamp Key>" },
          { SkullWoods, "<Skull Key>" },
          { ThievesTown, "<Thieves Key>" },
          { IcePalace, "<Ice Key>" },
          { MiseryMire, "<Mire Key>" },
          { TurtleRock, "<Turtle Key>" },
          { GanonsTower, "<GT Key>" },
        };

        public static Dictionary<string, string> DungeonBigKeys = new Dictionary<string, string>()
        {
          { HyruleCastle, "<Hyrule Big Key>" },
          { EasternPalace, "<Eastern Big Key>" },
          { DesertPalace, "<Desert Big Key>" },
          { TowerOfHera, "<Hera Big Key>" },
          { AgahnimsTower, "<Aga Big Key>" },
          { PalaceOfDarkness, "<PoD Big Key>" },
          { SwampPalace, "<Swamp Big Key>" },
          { SkullWoods, "<Skull Big Key>" },
          { ThievesTown, "<Thieves Big Key>" },
          { IcePalace, "<Ice Big Key>" },
          { MiseryMire, "<Mire Big Key>" },
          { TurtleRock, "<Turtle Big Key>" },
          { GanonsTower, "<GT Big Key>" },
        };

        public static string GetDungeonFromRoom(int roomId)
        {
            if(new int[] { 1, 2, 17, 18, 33, 34, 50, 65, 66, 80, 81, 82, 96, 97, 98, 112, 113, 114, 128, 129, 130 }.Contains(roomId))
            {
                return HyruleCastle;
            }
            if (new int[] { 137, 153, 168, 169, 170, 184, 185, 186, 200, 201, 216, 217, 218 }.Contains(roomId))
            {
                return EasternPalace;
            }
            if (new int[] { 51, 67, 83, 99, 115, 116, 117, 131, 132, 133 }.Contains(roomId))
            {
                return DesertPalace;
            }
            if (new int[] {
                            RoomIdConstants.R7_TowerofHera_Moldorm,
                            RoomIdConstants.R23_TowerofHera_MoldormFallRoom,
                            RoomIdConstants.R39_TowerofHera_BigChest,
                            RoomIdConstants.R49_TowerofHera_HardhatBeetlesRoom,
                            RoomIdConstants.R119_TowerofHera_EntranceRoom,
                            RoomIdConstants.R135_TowerofHera_TileRoom,
                            RoomIdConstants.R167_TowerofHera_FairyRoom
                            }.Contains(roomId))
            {
                return TowerOfHera;
            }
            if (new int[] { 32, 48, 64, 176, 192, 208, 224 }.Contains(roomId))
            {
                return AgahnimsTower;
            }
            if (new int[] { 9, 10, 11, 25, 26, 27, 42, 43, 58, 59, 74, 75, 90, 106 }.Contains(roomId))
            {
                return PalaceOfDarkness;
            }
            if (new int[] { 6, 22, 38, 40, 52, 53, 54, 55, 56, 70, 84, 102, 118 }.Contains(roomId))
            {
                return SwampPalace;
            }
            if (new int[] { 41, 57, 73, 86, 87, 88, 89, 103, 104 }.Contains(roomId))
            {
                return SkullWoods;
            }
            if (new int[] { 68, 69, 100, 101, 171, 172, 187, 188, 203, 204, 219, 220 }.Contains(roomId))
            {
                return ThievesTown;
            }
            if (new int[] { 14, 30, 31, 46, 62, 63, 78, 79, 94, 95, 110, 126, 127, 142, 158, 159, 174, 175, 190, 191, 206, 222 }.Contains(roomId))
            {
                return IcePalace;
            }
            if (new int[] { 144, 145, 146, 147, 151, 152, 160, 161, 162, 163, 177, 178, 179, 193, 194, 195, 209, 210 }.Contains(roomId))
            {
                return MiseryMire;
            }
            if (new int[] { 4, 19, 20, 21, 35, 36, 164, 180, 181, 182, 183, 196, 197, 198, 199, 213, 214 }.Contains(roomId))
            {
                return TurtleRock;
            }
            if (new int[] { 12, 13, 28, 29, 61, 76, 77, 91, 92, 93, 107, 108, 109, 123, 124, 125, 139, 140, 141, 149, 150, 155, 156, 157, 165, 166 }.Contains(roomId))
            {
                return GanonsTower;
            }

            throw new Exception("GetDungeonFromRoom - not a dungeon room!");
        }

        public const string
            HyruleCastle = "Hyrule Castle",
            EasternPalace = "Eastern Palace",
            DesertPalace = "Desert Palace",
            TowerOfHera = "Tower of Hera",
            AgahnimsTower = "Agahnim's Tower",
            PalaceOfDarkness = "Palace of Darkness",
            SwampPalace = "Swamp Palace",
            SkullWoods = "Skull Woods",
            ThievesTown = "Thieves' Town",
            IcePalace = "Ice Palace",
            MiseryMire = "Misery Mire",
            TurtleRock = "Turtle Rock",
            GanonsTower = "Ganon's Tower";
    }
}
