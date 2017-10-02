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
