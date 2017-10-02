using EnemizerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EnemizerLibrary
{
    /// <summary>
    /// Aliases to make it easier to find stuff
    /// </summary>
    public class GraphData
    {
        RomData romData { get; set; }
        RomEntranceCollection romEntrances { get; set; }
        RomExitCollection romExits { get; set; }

        public GraphData()
        {
            RawEntranceCollection rawEntranceCollection = new RawEntranceCollection();
            RawExitCollection rawExitCollection = new RawExitCollection();

            FillNodesAndEdges(rawEntranceCollection, rawExitCollection);
        }

        public GraphData(RomData romData, RomEntranceCollection romEntrances, RomExitCollection romExits)
        {
            this.romData = romData;
            this.romEntrances = romEntrances;
            this.romExits = romExits;

            RawEntranceCollection rawEntranceCollection = new RawEntranceCollection();
            RawExitCollection rawExitCollection = new RawExitCollection();
            UpdateFromRom(rawEntranceCollection, rawExitCollection);
            FillNodesAndEdges(rawEntranceCollection, rawExitCollection);
        }

        void UpdateFromRom(RawEntranceCollection rawEntranceCollection, RawExitCollection rawExitCollection)
        {
            UpdateEntrances(rawEntranceCollection);
            UpdateExits(rawExitCollection);
            UpdateItems();
        }

        void UpdateEntrances(RawEntranceCollection rawEntranceCollection)
        {
            List<RawEntrance> originalEntrances = rawEntranceCollection.RawEntrances.ToList();
            List<RawOverworldEntrance> originalOverworldEntrances = rawEntranceCollection.RawOverworldEntrances.ToList();

            foreach(var e in romEntrances.Entrances)
            {
                var newEntrance = originalEntrances.Where(x => x.EntranceId == e.EntranceNumber).FirstOrDefault();
                var owEntrance = rawEntranceCollection.RawOverworldEntrances.Where(x => x.EntranceAddress == e.EntranceAddress).FirstOrDefault();

                if(owEntrance != null && newEntrance != null)
                {
                    owEntrance.LogicalEntranceId = newEntrance.LogicalEntranceId;
                }
            }
        }

        void UpdateExits(RawExitCollection rawExitCollection)
        {
            List<RawExit> originalExits = rawExitCollection.RawExits.ToList();

            foreach(var e in romExits.Exits)
            {
                var exit = rawExitCollection.RawExits.Where(x => x.ExitRoomAddress == e.RoomAddress).FirstOrDefault();
                var newExit = originalExits.Where(x => x.RoomId == e.RoomId).FirstOrDefault();

                if(exit != null && newExit != null)
                {
                    exit.RoomId = newExit.RoomId;
                    exit.LogicalRoomId = newExit.LogicalRoomId;
                    exit.ExitRoomName = newExit.ExitRoomName;
                }
            }
        }

        void UpdateItems()
        {

        }

        public List<string> DumpData()
        {
            var ret = new List<string>();

            StringBuilder nodes = new StringBuilder();
            nodes.AppendLine($"LogicalId\tName");
            foreach (var n in AllNodes)
            {
                nodes.AppendLine($"{n.Value.LogicalId}\t{n.Value.Name}");
            }
            ret.Add(nodes.ToString());

            StringBuilder edges = new StringBuilder();
            edges.AppendLine($"SourceLogicalId\tDestinationLogicalId\tRequirements");
            foreach (var e in AllEdges)
            {
                edges.AppendLine($"{e.SourceNode.LogicalId}\t{e.DestinationNode.LogicalId}\t{String.Join("; ", e.Requirements.Select(x => String.Join(", ", x.Requirements.Select(y => y.Name))))}");
            }
            ret.Add(edges.ToString());


            StringBuilder entranceOutside = new StringBuilder();
            entranceOutside.AppendLine($"EntranceAddress\tEntranceName\tAreaLogicalId\tAreaId\tAreaName\tEntranceRoomLogicalId\tEntranceRoomId\tEntranceRoomName\tRoomLogicalId\tRoomId\tLogicalRoomName\tRoomRoomName\tRequirements");
            foreach (var e in this._entranceEdges._overworldEntrances)
            {
                entranceOutside.AppendLine($"0x{e.Value.EntranceAddress.ToString("X")}\t{e.Value.EntranceName}\t{e.Value.Area.LogicalId}\t0x{e.Value.Area.AreaId.ToString("X2")}\t{e.Value.Area.Name}\t{e.Value.Entrance.LogicalEntranceId}\t0x{e.Value.Entrance.EntranceId.ToString("X2")}\t{e.Value.Entrance.EntranceName}\t{e.Value.Entrance.Room.LogicalId}\t{e.Value.Entrance.Room.RoomId}\t{e.Value.Entrance.Room.Name}\t{e.Value.Entrance.Room.RoomName}\t{e.Value.Requirements}");
            }
            ret.Add(entranceOutside.ToString());

            StringBuilder exits = new StringBuilder();
            exits.AppendLine($"ExitRoomAddress\tExitRoomName\tRoomLogicalId\tLogicalRoomName\tRoomId\tRoomName\tExitAreaAddress\tExitAreaName\tAreaLogicalId\tAreaName\tAreaId");
            foreach(var e in this._exitEdges._exits)
            {
                exits.AppendLine($"0x{e.Value.ExitRoomAddress.ToString("X")}\t{e.Value.ExitRoomName}\t{e.Value.Room.LogicalId}\t{e.Value.Room.Name}\t{e.Value.Room.RoomId}\t{e.Value.Room.RoomName}\t0x{e.Value.ExitAreaAddress.ToString("X")}\t{e.Value.ExitAreaName}\t{e.Value.Area.LogicalId}\t{e.Value.Area.Name}\t0x{e.Value.Area.AreaId.ToString("X2")}");
            }
            ret.Add(exits.ToString());

            return ret;
        }

        OverworldNodes _overworldNodes;
        RoomNodes _roomNodes;
        BossNodes _bossNodes;
        ItemLocations _itemNodes;

        AreaEdges _areaEdges;
        RoomEdges _roomEdges;
        ItemEdges _itemEdges;
        EntranceEdges _entranceEdges;
        ExitEdges _exitEdges;

        public Dictionary<string, OverworldAreaNode> OverworldNodes { get { return _overworldNodes.Nodes; } }
        public Dictionary<string, RoomNode> RoomNodes { get { return _roomNodes.Nodes; } }
        public List<Edge> AreaEdges { get { return _areaEdges.Edges; } }
        public List<Edge> RoomEdges { get { return _roomEdges.Edges; } }
        public Dictionary<string, LogicalBoss> BossNodes { get { return _bossNodes.Nodes; } }
        public Dictionary<string, string> DungeonKeys { get; } = Data.Dungeons.DungeonKeys;
        public Dictionary<string, string> DungeonBigKeys { get; } = Data.Dungeons.DungeonBigKeys;
        public Dictionary<string, Item> GameItems { get; } = Data.GameItems.Items;
        public Dictionary<string, ItemLocation> ItemLocations { get { return _itemNodes.Nodes; } }
        public List<Edge> ItemEdges { get { return _itemEdges.Edges; } }


        void FillNodesAndEdges(RawEntranceCollection rawEntranceCollection, RawExitCollection rawExitCollection)
        {
            _overworldNodes = new Data.OverworldNodes();
            _roomNodes = new Data.RoomNodes();
            _bossNodes = new Data.BossNodes();
            _itemNodes = new Data.ItemLocations();
            FillAllNodes();

            _areaEdges = new Data.AreaEdges(_overworldNodes);
            _roomEdges = new Data.RoomEdges(_roomNodes, _overworldNodes, _bossNodes);
            _itemEdges = new ItemEdges(this);

            _entranceEdges = new EntranceEdges(_overworldNodes, _roomNodes, rawEntranceCollection);
            _exitEdges = new ExitEdges(_overworldNodes, _roomNodes, rawExitCollection);

            FillAllEdges();
        }

        public Dictionary<string, Node> AllNodes { get; private set; }

        void FillAllNodes()
        {
            AllNodes = new Dictionary<string, Node>();

            foreach (var node in _overworldNodes.Nodes)
            {
                AllNodes.Add(node.Key, node.Value);
            }
            foreach (var node in _roomNodes.Nodes)
            {
                AllNodes.Add(node.Key, node.Value);
            }
            foreach (var node in _bossNodes.Nodes)
            {
                AllNodes.Add(node.Key, node.Value);
            }
            foreach (var node in _itemNodes.Nodes)
            {
                AllNodes.Add(node.Key, node.Value);
            }
        }

        public List<Edge> AllEdges { get; private set; }

        void FillAllEdges()
        {
            AllEdges = new List<Edge>();

            AllEdges.AddRange(_areaEdges.Edges);
            AllEdges.AddRange(_roomEdges.Edges);
            AllEdges.AddRange(_itemEdges.Edges);
            AllEdges.AddRange(_entranceEdges.Edges);
            AllEdges.AddRange(_exitEdges.Edges);
        }
    }
}
