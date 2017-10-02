using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Edge
    {
        public Node SourceNode { get; set; }
        public Node DestinationNode { get; set; }
        public List<Requirement> Requirements { get; set; } = new List<Requirement>();

        bool Unlocked { get; set; } = false;
        public Edge LinkedEdge { get; set; }

        public static List<Edge> MakeEdges(Node sourceNode, Node destinationNode, string requirements, bool isTwoWay)
        {
            var ret = new List<Edge>();

            Edge srcDestEdge = new Edge(sourceNode, destinationNode, requirements);
            ret.Add(srcDestEdge);
            if (isTwoWay)
            {
                Edge destSrcEdge = new Edge(destinationNode, sourceNode, requirements);
                srcDestEdge.LinkedEdge = destSrcEdge;
                destSrcEdge.LinkedEdge = srcDestEdge;
                ret.Add(destSrcEdge);
            }

            return ret;
        }

        public Edge(Node sourceNode, Node destinationNode)
        {
            this.SourceNode = sourceNode;
            this.DestinationNode = destinationNode;
            Unlocked = true;
        }

        public Edge(Node sourceNode, Node destinationNode, string requirements)
            :this(sourceNode, destinationNode)
        {
            Unlocked = false;
            foreach (var r in requirements.Split(';'))
            {
                if (r.Length > 0)
                {
                    List<Item> items = new List<Item>();
                    foreach (var reqItem in r.Split(','))
                    {
                        Item item = Data.GameItems.Items.Values.Where(x => x.LogicalId == reqItem).FirstOrDefault();
                        if(item == null)
                        {
                            throw new Exception($"Edge constructor - {sourceNode.LogicalId}->{destinationNode.LogicalId} could not find item {reqItem}");
                        }
                        items.Add(item);
                    }
                    this.Requirements.Add(new Requirement(items.ToArray()));
                }
            }
        }

        public bool MeetsRequirements(List<Item> items)
        {
            if (Requirements.Count == 0)
            {
                return true;
            }

            if(this.Unlocked)
            {
                return true;
            }

            var needConsume = new List<ConsumableItem>();

            int swordCount = items.Where(x => x.LogicalId == "Progressive Sword").Count();
            int gloveCount = items.Where(x => x.LogicalId == "Progressive Gloves").Count();

            foreach (var r in Requirements)
            {
                int count = 0;
                foreach(var i in r.Requirements)
                {
                    if(i is ConsumableItem)
                    {
                        var c = i as ConsumableItem;
                        if(items.Contains(c) && items.Any(x => x == c && ((ConsumableItem)x).Usable))
                        {
                            count++;
                            needConsume.Add(c);
                        }
                    }
                    else if(items.Contains(i))
                    {
                        count++;
                    }
                    else if(i is BottleItem && items.Any(x => x is BottleItem))
                    {
                        count++;
                    }
                    else if(i is ProgressiveItem)
                    {
                        var split = i.LogicalId.Split(' ');
                        if(split.Length > 1)
                        {
                            int level = (int)char.GetNumericValue(split[0][1]);

                            switch(split[1])
                            {
                                case "Sword":
                                    if(swordCount >= level)
                                    {
                                        count++;
                                    }
                                    break;
                                case "Gloves":
                                    if(gloveCount >= level)
                                    {
                                        count++;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            throw new Exception("Edge.MeetsRequirements - Invalid progressive item");
                        }
                    }
                }
                if(count == r.Requirements.Count)
                {
                    this.Unlocked = true;
                    if(this.LinkedEdge != null)
                    {
                        this.LinkedEdge.Unlocked = true;
                    }
                    foreach(var c in needConsume)
                    {
                        c.Consume();
                    }
                    return true;
                }
                //if (r.Requirements.Intersect(items).Count() == r.Requirements.Count)
                //{
                //    return true;
                //}
            }
            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"Edge ");
            if (this.Requirements.Count > 0)
            {
                sb.Append($"(req: {String.Join("; ", this.Requirements.Select(x => String.Join(", ", x.Requirements.Select(y => y.Name))))})");
            }
            else
            {
                sb.Append($"(no req)");
            }

            sb.Append($"{this.SourceNode.LogicalId} -> {this.DestinationNode.LogicalId}");

            return sb.ToString();
        }
    }
}
