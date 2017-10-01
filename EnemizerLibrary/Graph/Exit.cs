using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Exit
    {
        public int ExitRoomAddress { get; set; }
        public string ExitRoomName { get; set; }
        public int ExitAreaAddress { get; set; }
        public string ExitAreaName { get; set; }
        public OverworldAreaNode Area { get; set; }
        public RoomNode Room { get; set; }

        public Exit(int exitRoomAddress, string exitRoomName, int exitAreaAddress, string exitAreaName, RoomNode room, OverworldAreaNode area)
        {
            ExitRoomAddress = exitRoomAddress;
            ExitRoomName = exitRoomName;
            ExitAreaAddress = exitAreaAddress;
            ExitAreaName = exitAreaName;
            Area = area;
            Room = room;
        }
    }
}
