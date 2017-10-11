using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class GraphResult
    {
        public List<Node> OrderedNodesVisited { get; private set; } = new List<Node>();
        public List<Node> NodesVisited { get; private set; } = new List<Node>();
        public List<Item> ItemsObtained { get; private set; } = new List<Item>();
        public List<Edge> EdgesNotRetried { get; private set; } = new List<Edge>();
        public bool Success { get; set; }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(Success ? "Succeeded in traversing graph to goal." : "Failed to reach goal.");
            result.AppendLine("--------------");
            result.AppendLine($"Visited Nodes: {String.Join(", ", OrderedNodesVisited.Select(x => x.LogicalId))}");
            result.AppendLine("--------------");
            result.AppendLine($"Items Obtained: {String.Join(", ", ItemsObtained.Select(x => x.LogicalId))}");
            result.AppendLine("--------------");
            result.AppendLine($"Edges Not Retried: {String.Join(", ", EdgesNotRetried.Select(x => x.ToString()))}");
            return result.ToString();
        }
    }

    public class Graph
    {
        public Graph()
        {

        }

        public Graph(GraphData graphData)
        {
            foreach(var node in graphData.AllNodes)
            {
                this.Nodes.Add(node.Value);
            }

            foreach(var edge in graphData.AllEdges)
            {
                this.Edges.Add(edge);
            }
        }

        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> Edges { get; set; } = new List<Edge>();

        public void UpdateDungeonBoss(Dungeon dungeon)
        {
            Node bossNode = GetBossNodeFromRoom(dungeon.BossRoomId);

            if (bossNode == null)
            {
                throw new Exception($"Graph.UpdateDungeonBoss - boss not found for room {dungeon.BossRoomId}");
            }

            Edge edge = Edges.Where(x => x.DestinationNode == bossNode).FirstOrDefault();

            string requirements;

            if (dungeon.SelectedBoss == null)
            {
                // set back to default
                requirements = "";
                switch(dungeon.BossRoomId)
                {
                    case RoomIdConstants.R6_SwampPalace_Arrghus:
                        requirements = "Hookshot";
                        break;
                    case RoomIdConstants.R222_IcePalace_Kholdstare:
                        requirements = "Fire Rod;Bombos,L1 Sword";
                        break;
                    case RoomIdConstants.R164_TurtleRock_Trinexx:
                        requirements = "Fire Rod,Ice Rod";
                        break;
                }
            }
            else
            {
                requirements = dungeon.SelectedBoss.Requirements;
            }

            if (edge != null)
            {
                edge.Requirements = Requirement.MakeRequirementListFromString(requirements);
            }
        }

        public Node GetBossNodeFromRoom(int roomId)
        {
            Node bossNode = null;
            switch (roomId)
            {
                case RoomIdConstants.R200_EasternPalace_ArmosKnights:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Eastern Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R51_DesertPalace_Lanmolas:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Desert Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R7_TowerofHera_Moldorm:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Hera Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R90_PalaceofDarkness_HelmasaurKing:
                    bossNode = Nodes.Where(x => x.LogicalId == "[PoD Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R6_SwampPalace_Arrghus:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Swamp Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R41_SkullWoods_Mothula:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Skull Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R172_ThievesTown_BlindTheThief:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Thieves Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R222_IcePalace_Kholdstare:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Ice Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R144_MiseryMire_Vitreous:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Mire Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R164_TurtleRock_Trinexx:
                    bossNode = Nodes.Where(x => x.LogicalId == "[Turtle Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R28_GanonsTower_IceArmos:
                    bossNode = Nodes.Where(x => x.LogicalId == "[GT Armos Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R108_GanonsTower_LanmolasRoom:
                    bossNode = Nodes.Where(x => x.LogicalId == "[GT Lanmolas Boss]").FirstOrDefault();
                    break;
                case RoomIdConstants.R77_GanonsTower_MoldormRoom:
                    bossNode = Nodes.Where(x => x.LogicalId == "[GT Moldorm Boss]").FirstOrDefault();
                    break;
            }
            return bossNode;
        }

        public GraphResult FindPath(string source, string dest, bool exhaustiveSearch = false, List<Item> startingItems = null, string requirements = null)
        {
            var reqList = Requirement.MakeRequirementListFromString(requirements);
            var sourceNode = Nodes.Where(x => x.LogicalId == source).FirstOrDefault();
            var destNode = Nodes.Where(x => x.LogicalId == dest).FirstOrDefault();

            return FindPath(sourceNode, destNode, exhaustiveSearch, startingItems, reqList);
        }

        public GraphResult FindPath(Node sourceNode, Node destinationNode, bool exhaustiveSearch = false, List<Item> startingItems = null, List<Requirement> requirements = null)
        {
            var orderedVisit = new LinkedList<Node>();

            var obtainedItems = new List<Item>();
            if(startingItems != null)
            {
                obtainedItems.AddRange(startingItems);
            }
            var retryQueue = new Queue<Edge>();

            //var startingEdges = Edges.Where(x => x.SourceNode == sourceNode);
            var visitedNodes = new LinkedList<Node>();
            //visitedNodes.AddLast(sourceNode);
            //orderedVisit.AddLast(sourceNode);

            var nextToVisit = new Queue<Node>();
            //foreach(var se in startingEdges)
            //{
            //    nextToVisit.Enqueue(se.DestinationNode);
            //}
            nextToVisit.Enqueue(sourceNode);

            // TODO: is there a better way to do this for exhaustiveSearches?
            bool foundDestination = false;

            while(nextToVisit.Count > 0)
            {
                var next = nextToVisit.Dequeue();

                if(visitedNodes.Contains(next))
                {
                    // already visited
                    continue;
                }

                visitedNodes.AddLast(next);
                orderedVisit.AddLast(next);

                if (next is ItemLocation)
                {
                    var item = ((ItemLocation)next).Item;
                    if(item is ConsumableItem)
                    {
                        ((ConsumableItem)item).IncreaseCount();
                    }
                    obtainedItems.Add(item);
                }
                if(next is LogicalBoss)
                {
                    obtainedItems.Add(((LogicalBoss)next).Boss);
                }

                if(next == destinationNode && (requirements == null || requirements.Count == 0 || (requirements.Count > 0 && requirements.Any(x => x.RequirementsMet(obtainedItems)))))
                {
                    foundDestination = true;

                    if (!exhaustiveSearch || (exhaustiveSearch && retryQueue.Count == 0))
                    {
                        // found it
                        var result = new GraphResult();
                        result.Success = true;
                        result.ItemsObtained.AddRange(obtainedItems);
                        result.OrderedNodesVisited.AddRange(orderedVisit.ToList());
                        result.NodesVisited.AddRange(visitedNodes.ToList());
                        result.EdgesNotRetried.AddRange(retryQueue.ToList());
                        return result;
                    }
                }

                var nextEdges = Edges.Where(x => x.SourceNode == next)
                    .Where(x => !exhaustiveSearch || (exhaustiveSearch && x.SourceNode != destinationNode))
                    .Where(x => x.MeetsRequirements(obtainedItems))
                    .Where(x => !visitedNodes.Contains(x.DestinationNode));
                foreach (var e in nextEdges)
                {
                    nextToVisit.Enqueue(e.DestinationNode);
                }

                var retryEdges = Edges.Where(x => x.SourceNode == next)
                    .Where(x => !exhaustiveSearch || (exhaustiveSearch && x.SourceNode != destinationNode))
                    .Where(x => !x.MeetsRequirements(obtainedItems));
                foreach(var e in retryEdges)
                {
                    retryQueue.Enqueue(e);
                }

                // if we run out - retry all our retry queue items
                if (nextToVisit.Count == 0)
                {
                    var tempRetry = new Queue<Edge>();
                    while(retryQueue.Count > 0)
                    {
                        var e = retryQueue.Dequeue();
                        if(e.MeetsRequirements(obtainedItems))
                        {
                            nextToVisit.Enqueue(e.SourceNode);
                            visitedNodes.Remove(e.SourceNode);
                        }
                        else
                        {
                            tempRetry.Enqueue(e);
                        }
                    }
                    retryQueue = tempRetry;
                }
            }

            GraphResult ret = new GraphResult();
            ret.Success = foundDestination;
            ret.ItemsObtained.AddRange(obtainedItems);
            ret.OrderedNodesVisited.AddRange(orderedVisit.ToList());
            ret.NodesVisited.AddRange(visitedNodes.ToList());
            ret.EdgesNotRetried.AddRange(retryQueue.ToList());
            return ret;
        }
    }
}
