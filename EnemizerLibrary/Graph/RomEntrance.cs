using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RomEntrance
    {
        RomData romData;
        public int EntranceIndex { get; set; }
        public byte EntranceNumber { get; set; }
        public int EntranceAddress { get; set; }
        public int EntranceAreaId { get; set; }
        public int ConnectToRoomId { get; set; }

        public string EntranceSourceName
        {
            get
            {
                return EntranceConstants.GetEntranceNameFromAddress(EntranceAddress);
            }
        }
        public string EntranceAreaName
        {
            get
            {
                return OverworldAreaConstants.GetAreaName(EntranceAreaId);
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

        public RomEntrance(RomData romData)
        {
            this.romData = romData;
        }
    }

    public class RomNormalEntrance : RomEntrance
    {
        public RomNormalEntrance(RomData romData, int entranceIndex)
            :base(romData)
        {
            this.EntranceIndex = entranceIndex;
            this.EntranceAddress = 0xDBB73 + entranceIndex;
            this.EntranceNumber = romData[EntranceAddress];

            this.EntranceAreaId = (romData[0xDB96F + (entranceIndex * 2) + 1] << 8) + romData[0xDB96F + (entranceIndex * 2)];

            this.ConnectToRoomId = (romData[0x14577 + (this.EntranceNumber * 2) + 1] << 8) + romData[0x14577 + (this.EntranceNumber * 2)];
        }
    }
    public class RomDropEntrance : RomEntrance
    {
        public RomDropEntrance(RomData romData, int entranceIndex)
            :base(romData)
        {
            this.EntranceIndex = entranceIndex;
            this.EntranceAddress = 0xDB84C + entranceIndex;
            this.EntranceNumber = romData[EntranceAddress];

            this.EntranceAreaId = (romData[0xDB826 + (entranceIndex * 2) + 1] << 8) + romData[0xDB826 + (entranceIndex * 2)];

            // hope this is the same...
            this.ConnectToRoomId = (romData[0x14577 + (this.EntranceNumber * 2) + 1] << 8) + romData[0x14577 + (this.EntranceNumber * 2)];
        }
    }

    public class RomEntranceCollection
    {
        RomData romData;
        public List<RomEntrance> Entrances { get; set; } = new List<RomEntrance>();

        public RomEntranceCollection(RomData romData)
        {
            this.romData = romData;
        }

        public void LoadEntrances()
        {
            for (int i = 0; i < 0x13; i++)
            {
                Entrances.Add(new RomDropEntrance(romData, i));
            }

            for (int i=0; i<0x81; i++)
            {
                Entrances.Add(new RomNormalEntrance(romData, i));
            }
        }
    }
}
