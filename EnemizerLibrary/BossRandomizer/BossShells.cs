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
        public int ShellByteOffset { get; set; }
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

            AddShell(DungeonType.SkullWoods, offset, null, 0); // reuse ToH because skull woods is empty????

            offset += AddShell(DungeonType.TowerOfHera, offset, DungeonConstants.room_7_shell, DungeonConstants.room_7_shell_index);

            offset += AddShell(DungeonType.EasternPalace, offset, DungeonConstants.room_200_shell, DungeonConstants.room_200_shell_index);

            offset += AddShell(DungeonType.DesertPalace, offset, DungeonConstants.room_51_shell, DungeonConstants.room_51_shell_index);

            offset += AddShell(DungeonType.PalaceOfDarkness, offset, DungeonConstants.room_90_shell, DungeonConstants.room_90_shell_index);

            offset += AddShell(DungeonType.MiseryMire, offset, DungeonConstants.room_144_shell, DungeonConstants.room_144_shell_index);

            offset += AddShell(DungeonType.ThievesTown, offset, DungeonConstants.room_172_blind_room_shell, DungeonConstants.room_172_shell_index);

            offset += AddShell(DungeonType.SwampPalace, offset, DungeonConstants.room_6_shell, DungeonConstants.room_6_shell_index);

            offset += AddShell(DungeonType.IcePalace, offset, DungeonConstants.room_222_shell, DungeonConstants.room_222_shell_index);

            offset += AddShell(DungeonType.TurtleRock, offset, DungeonConstants.room_164_shell, DungeonConstants.room_164_shell_index);

            offset += AddShell(DungeonType.GanonsTower1, offset, DungeonConstants.room_28_shell, DungeonConstants.room_28_shell_index);

            offset += AddShell(DungeonType.GanonsTower2, offset, DungeonConstants.room_108_shell, DungeonConstants.room_108_shell_index);

            offset += AddShell(DungeonType.GanonsTower3, offset, DungeonConstants.room_77_shell, DungeonConstants.room_77_shell_index);
        }

        private int AddShell(DungeonType dungeonType, int offset, byte[] shellData, int shellByteOffset)
        {
            DungeonShell shell = new DungeonShell();
            shell.DungeonType = dungeonType;
            shell.RomPointer = 0x122000 + offset;
            shell.Pointer = Utilities.PCAddressToSnesByteArray(0x122000 + offset);
            shell.ShellData = shellData;
            shell.ShellByteOffset = shellByteOffset;
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
