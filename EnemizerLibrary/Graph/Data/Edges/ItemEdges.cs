using System;
using System.Collections.Generic;

namespace EnemizerLibrary.Data
{
    public class ItemEdges
    {
        GraphData _graphData;

        public ItemEdges(GraphData graphData, RawItemEdgeCollection rawItemEdges)
        {
            this._graphData = graphData;
            this.Edges = new List<Edge>();
            FillItemEdges(rawItemEdges);
        }

        public List<Edge> Edges { get; private set; }

        void FillItemEdges(RawItemEdgeCollection rawItemEdges)
        {
            foreach(var e in rawItemEdges.RawItemEdges)
            {
                Node src;
                Node dest;
                if(!_graphData.AllNodes.TryGetValue(e.SrcId, out src))
                {
                    throw new Exception($"FillItemEdges - invalid source {e.SrcId}");
                }
                if (!_graphData.AllNodes.TryGetValue(e.DestId, out dest))
                {
                    throw new Exception($"FillItemEdges - invalid destination {e.DestId}");
                }
                Edges.Add(new Edge(src, dest, e.Requirements));
            }
        }
    }
}
