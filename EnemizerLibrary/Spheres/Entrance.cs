using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Entrance
    {
        RomData romData;
        public int EntranceIndex { get; set; }
        public byte EntranceNumber { get; set; }
        public int EntranceAddress { get; set; }
        public int ConnectToRoomId { get; set; }

        public string EntranceSourceName
        {
            get
            {
                return EntranceConstants.GetEntranceNameFromAddress(EntranceAddress);
            }
        }
        public string EntranceName
        {
            get
            {
                return EntranceConstants.GetEntranceName(EntranceNumber);
            }
        }

        public string ConnectsToRoomName
        {
            get
            {
                return EntranceConstants.GetEntranceName(EntranceNumber) + " - " + RoomIdConstants.GetRoomName(ConnectToRoomId);
            }
        }

        public Entrance(RomData romData, int entranceIndex)
        {
            this.romData = romData;
            this.EntranceIndex = entranceIndex;
            this.EntranceAddress = 0xDBB73 + entranceIndex;
            this.EntranceNumber = romData[EntranceAddress];

            this.ConnectToRoomId = (romData[0x14577 + (this.EntranceNumber*2) + 1] << 8) + romData[0x14577 + (this.EntranceNumber*2)];
        }
    }

    public class EntranceCollection
    {
        RomData romData;
        public List<Entrance> Entrances { get; set; } = new List<Entrance>();

        public EntranceCollection(RomData romData)
        {
            this.romData = romData;
        }

        public void LoadEntrances()
        {
            for(int i=0; i<0x81; i++)
            {
                Entrances.Add(new Entrance(romData, i));
            }
        }
    }
}
