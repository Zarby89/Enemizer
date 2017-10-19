using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary.Data
{
    public class RoomEdges
    {
        RoomNodes _roomNodes;
        OverworldNodes _overworldNodes;
        BossNodes _bossNodes;
        RawRoomEdgeCollection _rawRoomEdges;

        public RoomEdges(RoomNodes roomNodes, OverworldNodes overworldNodes, BossNodes bossNodes, RawRoomEdgeCollection rawRoomEdges)
        {
            this._roomNodes = roomNodes;
            this._overworldNodes = overworldNodes;
            this._bossNodes = bossNodes;
            this._rawRoomEdges = rawRoomEdges;
            this.Edges = new List<Edge>();
            FillRoomEdges();
        }

        public List<Edge> Edges { get; private set; }

        void FillRoomEdges()
        {
            foreach (var e in _rawRoomEdges.RawRoomEdges)
            {
                RoomNode src = _roomNodes.Nodes[e.srcId];
                // TODO: clean this up
                Node dest = null;
                RoomNode destTemp;
                if (!_roomNodes.Nodes.TryGetValue(e.destId, out destTemp))
                {
                    if (e.destId.StartsWith("{", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith("}", StringComparison.OrdinalIgnoreCase))
                    {
                        string d = e.destId.Split('{', '}').ToArray()[1];
                        OverworldAreaNode temp;
                        if (!_overworldNodes.Nodes.TryGetValue(d, out temp))
                        {
                            throw new Exception("Fill Room Edges - Invalid destination area");
                        }
                        dest = temp;
                    }
                    else if(e.destId == "<key>")
                    {
                        // keys
                        var dungeon = Dungeons.GetDungeonFromRoom(src.RoomId);
                        var dungeonKey = Dungeons.DungeonKeys[dungeon];
                        Item item = GameItems.Items.Values.Where(x => x.LogicalId == dungeonKey).FirstOrDefault();
                        if(item == null)
                        {
                            throw new Exception($"FillRoomEdges - Invalid dungeon item <key> {dungeonKey}");
                        }
                        dest = new ItemLocation(e.srcId + e.destId, e.srcId + e.destId, item);
                    }
                    else if (e.destId.StartsWith("<", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith(">", StringComparison.OrdinalIgnoreCase))
                    {
                        Item item = GameItems.Items.Values.Where(x => x.LogicalId == e.destId).FirstOrDefault();
                        if (item == null)
                        {
                            throw new Exception($"FillRoomEdges - Invalid special item {e.destId}");
                        }
                        dest = new ItemLocation(e.srcId + e.destId, e.srcId + e.destId, item);
                    }
                    else if(e.destId.StartsWith("[", StringComparison.OrdinalIgnoreCase) && e.destId.EndsWith("]", StringComparison.OrdinalIgnoreCase))
                    {
                        // Bosses
                        LogicalBoss temp;
                        if(!_bossNodes.Nodes.TryGetValue(e.destId, out temp))
                        {
                            throw new Exception("Fill Room Edges - Invalid boss");
                        }
                        dest = temp;
                    }
                    else
                    {
                        throw new Exception($"Fill Room Edges - Unknown destination type {e.destId}");
                    }
                }
                else
                {
                    dest = destTemp;
                }

                Edges.AddRange(Edge.MakeEdges(src, dest, e.requirements, e.isTwoWay));
            }
        }
    }
}
