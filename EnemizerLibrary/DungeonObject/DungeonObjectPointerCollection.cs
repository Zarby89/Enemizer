using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonObjectDataPointerCollection
    {
        public const int ObjectDataPointerTableAddress = 0xF8000;
        public const int TotalRooms = 320;

        public List<DungeonObjectDataPointer> RoomDungeonObjectDataPointers { get; private set; } = new List<DungeonObjectDataPointer>();

        RomData romData;

        public DungeonObjectDataPointerCollection(RomData romData)
        {
            this.romData = romData;
            for (int i=0; i<TotalRooms; i++)
            {
                RoomDungeonObjectDataPointers.Add(new DungeonObjectDataPointer(romData, i, romData.GetDataChunk(ObjectDataPointerTableAddress + (i * 3), 3)));
            }
        }
    }

    public class DungeonObjectDataPointer
    {
        int snesAddress;

        public int ROMAddress
        {
            get { return Utilities.SnesToPCAddress(snesAddress); }
            set { snesAddress = Utilities.PCToSnesAddress(value); }
        }

        public int SnesAddress
        {
            get { return snesAddress; }
            set { snesAddress = value; }
        }

        public byte[] SnesAddressBytes
        {
            get { return new byte[] { (byte)(snesAddress & 0xFF), (byte)((snesAddress >> 8) & 0xFF), (byte)((snesAddress >> 16) & 0xFF) }; }
        }

        public int RoomId { get; private set; }

        public DungeonObjectDataTable Data { get; private set; }

        public DungeonObjectDataPointer(RomData romData, int roomId, byte[] address)
        {
            RoomId = roomId;
            snesAddress = Utilities.SnesByteArrayTo24bitSnesAddress(address);
            Data = new DungeonObjectDataTable(romData, this.ROMAddress);
        }
    }
}
