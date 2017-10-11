using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldEntrance
    {
        public int EntranceAddress { get; set; }
        public string EntranceName { get; set; }
        public OverworldAreaNode Area { get; set; }
        public Entrance Entrance { get; set; }
        public string Requirements { get; set; }
        public OverworldEntrance(int entranceAddress, string entranceName, OverworldAreaNode area, Entrance entrance, string requirements)
        {
            this.EntranceAddress = entranceAddress;
            this.EntranceName = entranceName;
            this.Area = area;
            this.Entrance = entrance;
            this.Requirements = requirements;
        }
    }

    public class Entrance
    {
        public string LogicalEntranceId { get; set; }
        public int EntranceId { get; set; }
        public string EntranceName { get; set; }
        public RoomNode Room { get; set; }
        public Entrance(string logicalEntranceId, int entranceId, string entranceName, RoomNode room)
        {
            this.LogicalEntranceId = logicalEntranceId;
            this.EntranceId = entranceId;
            this.EntranceName = entranceName;
            this.Room = room;
        }
    }
}
