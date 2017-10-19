using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class EntranceEdges
    {
        OverworldNodes _overworldNodes;
        RoomNodes _roomNodes;
        RawEntranceCollection _rawEntranceCollection;
        RawItemLocationCollection _itemLocations;

        public Dictionary<int, OverworldEntrance> _overworldEntrances;
        public Dictionary<string, Entrance> _entrances;

        public EntranceEdges(OverworldNodes overworldNodes, RoomNodes roomNodes, RawEntranceCollection rawEntranceCollection, RawItemLocationCollection itemLocations)
        {
            this._overworldNodes = overworldNodes;
            this._roomNodes = roomNodes;
            this._rawEntranceCollection = rawEntranceCollection;
            this._itemLocations = itemLocations;
            FillEntranceEdges();
        }

        public List<Edge> Edges { get; private set; }
        void FillEntranceEdges()
        {
            Edges = new List<Edge>();
            _overworldEntrances = new Dictionary<int, OverworldEntrance>();
            _entrances = new Dictionary<string, Entrance>();

            foreach(var r in _rawEntranceCollection.RawEntrances)
            {
                RoomNode room;
                if(!_roomNodes.Nodes.TryGetValue(r.LogicalRoomId, out room))
                {
                    throw new Exception($"FillEntranceEdges - Invalid roomId {r.LogicalRoomId}");
                }
                _entrances.Add(r.LogicalEntranceId, new Entrance(r.LogicalEntranceId, r.EntranceId, r.EntranceName, room));
            }

            foreach(var r in _rawEntranceCollection.RawOverworldEntrances)
            {
                OverworldAreaNode area;
                if(!_overworldNodes.Nodes.TryGetValue(r.LogicalAreaId, out area))
                {
                    throw new Exception($"FillEntranceEdges - Invalid areaId {r.LogicalAreaId}");
                }

                Entrance entrance;
                if(!_entrances.TryGetValue(r.LogicalEntranceId, out entrance))
                {
                    throw new Exception($"FillEntranceEdges - Invalid entranceId {r.LogicalEntranceId}");
                }
                //if(r.Requirements.Contains(GameItems.GetLogicalId(GameItems._Misery_Mire_Token_)))
                //{
                //    r.Requirements.Replace(GameItems.GetLogicalId(GameItems._Misery_Mire_Token_), _itemLocations.RawItemLocations.Values.Where(x => x.LocationAddress == RomChest.MiseryMireMedallionAddress).Select(x => x.ItemName).FirstOrDefault());
                //}
                //if (r.Requirements.Contains(GameItems.GetLogicalId(GameItems._Turtle_Rock_Token_)))
                //{
                //    r.Requirements.Replace(GameItems.GetLogicalId(GameItems._Turtle_Rock_Token_), _itemLocations.RawItemLocations.Values.Where(x => x.LocationAddress == RomChest.TurtleRockMedallionAddress).Select(x => x.ItemName).FirstOrDefault());
                //}
                _overworldEntrances.Add(r.EntranceAddress, new OverworldEntrance(r.EntranceAddress, r.EntranceName, area, entrance, r.Requirements));
            }

            foreach(var e in _overworldEntrances)
            {
                Edges.Add(new Edge(e.Value.Area, e.Value.Entrance.Room, e.Value.Requirements));

                if(e.Value.Entrance.Room.RoomId > 0xFF && e.Value.Entrance.Room.RoomId != RoomIdConstants.R260_LinksHouse) // exclude link's house because it has an exit
                {
                    Edges.Add(new Edge(e.Value.Entrance.Room, e.Value.Area, e.Value.Requirements));
                }
                // room -> area(?) only for fake ones
            }
        }
    }
}
