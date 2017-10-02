using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class ExitEdges
    {
        OverworldNodes _overworldNodes;
        RoomNodes _roomNodes;
        RawExitCollection _rawExitCollection;

        public Dictionary<int, Exit> _exits;

        public ExitEdges(OverworldNodes overworldNodes, RoomNodes roomNodes, RawExitCollection rawExitCollection)
        {
            this._overworldNodes = overworldNodes;
            this._roomNodes = roomNodes;
            this._rawExitCollection = rawExitCollection;

            FillExitEdges();
        }

        public List<Edge> Edges { get; private set; }
        void FillExitEdges()
        {
            Edges = new List<Edge>();
            _exits = new Dictionary<int, Exit>();

            foreach(var r in _rawExitCollection.RawExits)
            {
                RoomNode room;
                if (!_roomNodes.Nodes.TryGetValue(r.LogicalRoomId, out room))
                {
                    throw new Exception($"FillExitEdges - Invalid roomId {r.LogicalRoomId}");
                }
                OverworldAreaNode area;
                if(!_overworldNodes.Nodes.TryGetValue(r.LogicalAreaId, out area))
                {
                    throw new Exception($"FillExitEdges - Invalid areaId {r.LogicalAreaId}");
                }
                _exits.Add(r.ExitRoomAddress, new Exit(r.ExitRoomAddress, r.ExitRoomName, r.ExitAreaAddress, r.ExitAreaName, room, area));
            }

            foreach(var e in _exits)
            {
                Edges.Add(new Edge(e.Value.Room, e.Value.Area));
            }
        }
    }
}
