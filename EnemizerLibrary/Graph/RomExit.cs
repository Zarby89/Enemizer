using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RomExit
    {
        RomData romData;
        public int ExitIndex { get; set; }
        public int RoomAddress { get; set; }
        public int RoomId { get; set; }
        public int AreaAddress { get; set; }
        public int AreaId { get; set; }
        public string RoomName
        {
            get
            {
                return ExitConstants.GetExitRoomName(RoomId);
            }
        }
        public string AreaName
        {
            get
            {
                return OverworldAreaConstants.GetAreaName(AreaId);
            }
        }

        public string ExitAreaName
        {
            get
            {
                return ExitConstants.GetExitAreaName(AreaAddress);
            }
        }

        public RomExit(RomData romData, int index)
        {
            this.romData = romData;
            this.ExitIndex = index;

            this.RoomAddress = 0x15AEE + (index * 2);
            this.AreaAddress = 0x15B8C + index;

            this.RoomId = (romData[RoomAddress+1] << 8) + romData[RoomAddress];
            this.AreaId = romData[AreaAddress];
        }
    }

    public class RomExitCollection
    {
        RomData romData;

        public List<RomExit> Exits { get; set; } = new List<RomExit>();

        public RomExitCollection(RomData romData)
        {
            this.romData = romData;
        }

        public void LoadExits()
        {
            for(int i=0; i<0x4F; i++)
            {
                var exit = new RomExit(romData, i);
                //if (exit.RoomId <= 295) // leave out the weird exits
                //{
                    Exits.Add(exit);
                //}
            }
        }
    }
}
