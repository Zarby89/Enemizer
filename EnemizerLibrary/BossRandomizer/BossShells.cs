using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonShell
    {
        public DungeonType DungeonType { get; set; }
        public int RomPointer { get; set; }
        public byte[] Pointer { get; set; }
        public byte[] ShellData { get; set; }
    }

    public class DungeonShells
    {
        public List<DungeonShell> Shells { get; set; } = new List<DungeonShell>();

        public DungeonShells()
        {
            FillShells();
        }

        public void FillShells()
        {
            int offset = 0;

            AddShell(DungeonType.SkullWoods, offset, null); // reuse ToH because skull woods is empty????

            offset += AddShell(DungeonType.TowerOfHera, offset, DungeonConstants.room_7_shell);

            offset += AddShell(DungeonType.EasternPalace, offset, DungeonConstants.room_200_shell);

            offset += AddShell(DungeonType.DesertPalace, offset, DungeonConstants.room_51_shell);

            offset += AddShell(DungeonType.PalaceOfDarkness, offset, DungeonConstants.room_90_shell);

            offset += AddShell(DungeonType.MiseryMire, offset, DungeonConstants.room_144_shell);

            offset += AddShell(DungeonType.ThievesTown, offset, DungeonConstants.room_172_blind_room_shell);

            offset += AddShell(DungeonType.SwampPalace, offset, DungeonConstants.room_6_shell);

            offset += AddShell(DungeonType.IcePalace, offset, DungeonConstants.room_222_shell);

            offset += AddShell(DungeonType.TurtleRock, offset, DungeonConstants.room_164_shell);

            offset += AddShell(DungeonType.GanonsTower1, offset, DungeonConstants.room_28_shell);

            offset += AddShell(DungeonType.GanonsTower2, offset, DungeonConstants.room_108_shell);

            offset += AddShell(DungeonType.GanonsTower3, offset, DungeonConstants.room_77_shell);
        }

        private int AddShell(DungeonType dungeonType, int offset, byte[] shellData)
        {
            DungeonShell shell = new DungeonShell();
            shell.DungeonType = dungeonType;
            shell.RomPointer = 0x122000 + offset;
            shell.Pointer = Utilities.PCAddressToSnesByteArray(0x122000 + offset);
            shell.ShellData = shellData;
            Shells.Add(shell);

            if(shellData != null)
            {
                return offset + shellData.Length;
            }

            return offset;
        }

        public void WriteShellsToRom(RomData romData)
        {
            foreach(var shell in Shells.Where(x => x.ShellData != null))
            {
                for (int i = 0; i < shell.ShellData.Length; i++)
                {
                    romData[shell.RomPointer + i] = shell.ShellData[i];
                }
            }
        }
    }
}
