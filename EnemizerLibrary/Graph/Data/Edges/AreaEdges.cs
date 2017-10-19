using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class AreaEdges
    {
        OverworldNodes _overworldNodes;

        public AreaEdges(OverworldNodes overworldNodes)
        {
            this._overworldNodes = overworldNodes;
            this.Edges = new List<Edge>();
            FillAreaEdges();
        }

        class RawAreaEdge
        {
            public string srcId;
            public string destId;
            public bool isTwoWay;
            public string requirements;

            public RawAreaEdge(string srcId, string destId, bool isTwoWay, string requirements)
            {
                this.srcId = srcId;
                this.destId = destId;
                this.isTwoWay = isTwoWay;
                this.requirements = requirements;
            }
        }

        List<RawAreaEdge> rawAreaEdges = new List<RawAreaEdge>()
        {
            { new RawAreaEdge("00-lost-woods", "02-lumberjack-house", true, "") },
            { new RawAreaEdge("02-lumberjack-house", "0A-entrance-death-mountain", true, "") },
            { new RawAreaEdge("00-lost-woods", "10-path-between-lost-woods-kakariko", true, "") },
            { new RawAreaEdge("00-lost-woods", "11-kakariko-fortune-teller", true, "") },
            { new RawAreaEdge("0A-entrance-death-mountain", "12-pond-next-to-fortune-teller", true, "") },
            { new RawAreaEdge("11-kakariko-fortune-teller", "12-pond-next-to-fortune-teller", true, "") },
            { new RawAreaEdge("12-pond-next-to-fortune-teller", "13-sanctuary", true, "") },
            { new RawAreaEdge("13-sanctuary", "14-graveyard", true, "") },
            { new RawAreaEdge("14-graveyard", "14-graveyard-kings-tomb", true, "L2 Gloves") },
            { new RawAreaEdge("14-graveyard", "15-river-between-graveyard-witch", true, "") },
            { new RawAreaEdge("15-river-between-graveyard-witch", "16-witch-hut", true, "") },
            { new RawAreaEdge("16-witch-hut", "17-east-of-witch-hut", false, "L1 Gloves") },
            { new RawAreaEdge("17-east-of-witch-hut", "16-witch-hut", false, "L1 Gloves;Flippers") },
            { new RawAreaEdge("17-east-of-witch-hut", "0F-entrance-zora-domain", true, "") },
            { new RawAreaEdge("10-path-between-lost-woods-kakariko", "18-kakariko", true, "") },
            { new RawAreaEdge("11-kakariko-fortune-teller", "18-kakariko", true, "") },
            { new RawAreaEdge("12-pond-next-to-fortune-teller", "1A-between-kakariko-castle", true, "") },
            { new RawAreaEdge("1A-between-kakariko-castle", "1B-castle", true, "") },
            { new RawAreaEdge("15-river-between-graveyard-witch", "1D-bridge-between-graveyard-witch", true, "") },
            { new RawAreaEdge("1B-castle", "25-between-castle-eastern-top", true, "L1 Gloves") },
            { new RawAreaEdge("1D-bridge-between-graveyard-witch", "25-between-castle-eastern-top", true, "") },
            { new RawAreaEdge("18-kakariko", "22-smithy", true, "") },
            { new RawAreaEdge("18-kakariko", "29-kakariko-library", true, "") },
            { new RawAreaEdge("28-kakariko-maze-race", "29-kakariko-library", true, "") },
            { new RawAreaEdge("29-kakariko-library", "32-south-of-haunted-grove", true, "") },
            { new RawAreaEdge("32-south-of-haunted-grove", "2A-haunted-grove", true, "") },
            { new RawAreaEdge("32-south-of-haunted-grove", "33-northwest-swamp", true, "L1 Gloves") },
            { new RawAreaEdge("32-south-of-haunted-grove", "2B-between-haunted-link-house", true, "") },
            { new RawAreaEdge("1B-castle", "2B-between-haunted-link-house", true, "") },
            { new RawAreaEdge("1B-castle", "2C-link-house", true, "") },
            { new RawAreaEdge("2B-between-haunted-link-house", "2C-link-house", true, "") },
            { new RawAreaEdge("2C-link-house", "2D-between-castle-eastern-bottom", true, "") },
            { new RawAreaEdge("25-between-castle-eastern-top", "2D-between-castle-eastern-bottom", true, "") },
            { new RawAreaEdge("2D-between-castle-eastern-bottom", "2E-south-eastern-palace-left", true, "") },
            { new RawAreaEdge("2E-south-eastern-palace-left", "1E-eastern-palace", true, "") },
            { new RawAreaEdge("1E-eastern-palace", "2F-south-eastern-palace-right", true, "") },
            { new RawAreaEdge("2C-link-house", "34-northeast-swamp", true, "") },
            { new RawAreaEdge("2D-between-castle-eastern-bottom", "35-lake-hylia", true, "") },
            { new RawAreaEdge("2E-south-eastern-palace-left", "35-lake-hylia", true, "") },
            { new RawAreaEdge("33-northwest-swamp", "34-northeast-swamp", true, "") },
            { new RawAreaEdge("33-northwest-swamp", "3B-southwest-swamp", true, "") },
            { new RawAreaEdge("3B-southwest-swamp", "3A-between-desert-swamp", true, "") },
            { new RawAreaEdge("3A-between-desert-swamp", "30-desert", true, "") },
            { new RawAreaEdge("3B-southwest-swamp", "3C-southeast-swamp", true, "") },
            { new RawAreaEdge("34-northeast-swamp", "3C-southeast-swamp", true, "") },
            { new RawAreaEdge("3C-southeast-swamp", "35-lake-hylia", true, "") },
            { new RawAreaEdge("35-lake-hylia", "3F-between-lake-ice-cave", true, "") },
            { new RawAreaEdge("3F-between-lake-ice-cave", "37-ice-cave", true, "") },
            { new RawAreaEdge("03-west-death-mountain-lower", "05-east-death-mountain", true, "Hookshot") },
            { new RawAreaEdge("03-west-death-mountain-upper", "05-east-death-mountain", true, "Hammer") },
            { new RawAreaEdge("05-east-death-mountain", "07-east-death-mountain-turtle-warp", true, "") },
            { new RawAreaEdge("12-pond-next-to-fortune-teller", "3F-between-lake-ice-cave", true, "Flippers") },
            { new RawAreaEdge("15-river-between-graveyard-witch", "33-northwest-swamp", true, "Flippers") },
            { new RawAreaEdge("0F-entrance-zora-domain", "35-lake-hylia", true, "Flippers") },
            { new RawAreaEdge("40-skull-woods", "42-dw-lumberjack-house", true, "") },
            { new RawAreaEdge("42-dw-lumberjack-house", "4A-bumper-cave", true, "") },
            { new RawAreaEdge("40-skull-woods", "50-between-skull-woods-outcast", true, "") },
            { new RawAreaEdge("40-skull-woods", "51-outcast-fortune-teller", true, "") },
            { new RawAreaEdge("4A-bumper-cave", "52-pond-next-to-outcast-fortune-teller", true, "") },
            { new RawAreaEdge("51-outcast-fortune-teller", "52-pond-next-to-outcast-fortune-teller", true, "") },
            { new RawAreaEdge("52-pond-next-to-outcast-fortune-teller", "53-dw-sanctuary", true, "") },
            { new RawAreaEdge("53-dw-sanctuary", "54-dw-graveyard", true, "") },
            { new RawAreaEdge("54-dw-graveyard", "55-river-between-graveyard-witch", false, "Flippers") },
            { new RawAreaEdge("55-river-between-graveyard-witch", "56-dw-witch-hut", true, "") },
            { new RawAreaEdge("56-dw-witch-hut", "57-east-of-dw-witch-hut", false, "L1 Gloves") },
            { new RawAreaEdge("57-east-of-dw-witch-hut", "56-dw-witch-hut", false, "L1 Gloves;Flippers") },
            { new RawAreaEdge("57-east-of-dw-witch-hut", "4F-catfish", true, "") },
            { new RawAreaEdge("50-between-skull-woods-outcast", "58-outcast-village", true, "") },
            { new RawAreaEdge("51-outcast-fortune-teller", "58-outcast-village", true, "") },
            { new RawAreaEdge("52-pond-next-to-outcast-fortune-teller", "5A-between-outcast-pyramid", true, "") },
            { new RawAreaEdge("55-river-between-graveyard-witch", "5D-broken-bridge", true, "") },
            { new RawAreaEdge("5D-broken-bridge", "54-dw-graveyard", false, "Hookshot") },
            { new RawAreaEdge("58-outcast-village", "62-dw-smith-house", true, "L2 Gloves") },
            { new RawAreaEdge("58-outcast-village", "69-frog-smith", false, "") },
            { new RawAreaEdge("69-frog-smith", "58-outcast-village", false, "L2 Gloves") },
            { new RawAreaEdge("69-frog-smith", "68-digging-game", true, "") },
            { new RawAreaEdge("5B-pyramid", "65-between-pyramid-pod-top", true, "") },
            { new RawAreaEdge("5D-broken-bridge", "65-between-pyramid-pod-top", true, "") },
            { new RawAreaEdge("65-between-pyramid-pod-top", "6C-bomb-shop", true, "Hammer") },
            { new RawAreaEdge("65-between-pyramid-pod-top", "6E-south-pod-left", true, "") },
            { new RawAreaEdge("65-between-pyramid-pod-top", "75-dw-lake-hylia-lower", true, "Hammer") },
            { new RawAreaEdge("6E-south-pod-left", "5E-palace-of-darkness", true, "") },
            { new RawAreaEdge("5E-palace-of-darkness", "6F-south-pod-right", true, "") },
            { new RawAreaEdge("6E-south-pod-left", "75-dw-lake-hylia-lower", true, "Flippers") },
            { new RawAreaEdge("6C-bomb-shop", "75-dw-lake-hylia-upper", true, "") },
            { new RawAreaEdge("69-frog-smith", "72-south-dw-haunted-grove", true, "") },
            { new RawAreaEdge("72-south-dw-haunted-grove", "6A-dw-haunted-grove", true, "") },
            { new RawAreaEdge("72-south-dw-haunted-grove", "6B-between-grove-bomb-shop", true, "") },
            { new RawAreaEdge("72-south-dw-haunted-grove", "73-dw-northwest-swamp", true, "L1 Gloves") },
            { new RawAreaEdge("6B-between-grove-bomb-shop", "6C-bomb-shop", true, "") },
            { new RawAreaEdge("6C-bomb-shop", "74-dw-northeast-swamp", true, "") },
            { new RawAreaEdge("74-dw-northeast-swamp", "73-dw-northwest-swamp", true, "") },
            { new RawAreaEdge("73-dw-northwest-swamp", "7B-dw-southwest-swamp", true, "") },
            { new RawAreaEdge("7B-dw-southwest-swamp", "7A-between-mire-swamp", true, "") },
            { new RawAreaEdge("74-dw-northeast-swamp", "7C-dw-southeast-swamp", true, "") },
            { new RawAreaEdge("7B-dw-southwest-swamp", "7C-dw-southeast-swamp", true, "") },
            { new RawAreaEdge("7C-dw-southeast-swamp", "75-dw-lake-hylia-lower", false, "Flippers") },
            //{ new RawAreaEdge("74-dw-northeast-swamp", "7F-dw-between-lake-ice-cave", true, "Flippers") },
            { new RawAreaEdge("7F-dw-between-lake-ice-cave", "77-dw-ice-cave", true, "") },
            { new RawAreaEdge("45-dw-east-death-mountain", "47-turtle-rock", true, "") },
            { new RawAreaEdge("45-dw-east-death-mountain", "43-dw-west-death-mountain-upper", true, "") },
            { new RawAreaEdge("55-river-between-graveyard-witch", "7F-dw-between-lake-ice-cave", true, "Flippers") },
            { new RawAreaEdge("03-west-death-mountain-lower", "43-dw-west-death-mountain-lower", false, "") },
            { new RawAreaEdge("43-dw-west-death-mountain-lower", "03-west-death-mountain-lower", false, "Mirror") },
            { new RawAreaEdge("43-dw-west-death-mountain-lower", "03-west-death-mountain-upper", false, "Mirror") },
            { new RawAreaEdge("43-dw-west-death-mountain-upper", "03-west-death-mountain-upper", false, "Mirror") },
            { new RawAreaEdge("43-dw-west-death-mountain-lower", "03-west-death-mountain-spectical", false, "Mirror") },
            { new RawAreaEdge("43-dw-west-death-mountain-upper", "43-dw-west-death-mountain-lower", false, "") },
            { new RawAreaEdge("05-east-death-mountain", "45-dw-east-death-mountain", false, "L2 Gloves") },
            { new RawAreaEdge("07-east-death-mountain-turtle-warp", "47-turtle-rock", false, "L2 Gloves,Hammer") },
            { new RawAreaEdge("11-kakariko-fortune-teller", "51-outcast-fortune-teller", false, "Moon Pearl,L2 Gloves;Moon Pearl,Hammer,L1 Gloves") },
            { new RawAreaEdge("2F-south-eastern-palace-right", "6F-south-pod-right", false, "Moon Pearl,Hammer") },
            { new RawAreaEdge("33-northwest-swamp", "73-dw-northwest-swamp", false, "Moon Pearl,Hammer") },
            { new RawAreaEdge("30-desert", "70-mire", false, "Flute,L2 Gloves") },
            { new RawAreaEdge("00-lost-woods", "80-master-sword", true, "") },
            { new RawAreaEdge("0F-entrance-zora-domain", "81-zora-domain", true, "") },
            { new RawAreaEdge("35-lake-hylia", "80-hobo", true, "Flippers") },
            { new RawAreaEdge("30-desert-ledge", "30-desert", false, "") },
            { new RawAreaEdge("30-desert-ledge", "30-desert-ledge-boss-entrance", true, "L1 Gloves") },
            { new RawAreaEdge("30-desert", "30-desert-palace-main-entrance", false, "Book") },
            { new RawAreaEdge("35-lake-hylia", "75-dw-lake-hylia-ice-palace", false, "L2 Gloves") },
            { new RawAreaEdge("75-dw-lake-hylia-lower", "35-lake-hylia-island", false, "Mirror") },
            { new RawAreaEdge("7A-between-mire-swamp", "30-desert-bombos-tablet", false, "Mirror") },
            { new RawAreaEdge("30-desert-palace-east-entrance", "30-desert", false, "") },
            { new RawAreaEdge("32-south-of-haunted-grove-ledge", "32-south-of-haunted-grove", false, "") },
            { new RawAreaEdge("72-south-dw-haunted-grove", "32-south-of-haunted-grove-ledge", false, "Mirror") },
            { new RawAreaEdge("14-graveyard-ledge", "14-graveyard", false, "") },
            { new RawAreaEdge("54-dw-graveyard", "14-graveyard-ledge", false, "Mirror") },
            { new RawAreaEdge("54-dw-graveyard", "14-graveyard-kings-tomb", false, "Mirror") },
            { new RawAreaEdge("70-mire", "30-desert-checkerboard-ledge", false, "Mirror") },
            { new RawAreaEdge("70-mire", "30-desert-ledge", false, "Mirror") },
            { new RawAreaEdge("30-desert-checkerboard-ledge", "30-desert", false, "") },
            { new RawAreaEdge("75-dw-lake-hylia-upper", "75-dw-lake-hylia-lower", false, "Flippers") },
            { new RawAreaEdge("2C-link-house", "03-west-death-mountain-upper", false, "Flute") },
        };

        public List<Edge> Edges { get; private set; }

        private void FillAreaEdges()
        {
            foreach (var e in rawAreaEdges)
            {
                Edges.AddRange(Edge.MakeEdges(_overworldNodes.Nodes[e.srcId], _overworldNodes.Nodes[e.destId], e.requirements, e.isTwoWay));
            }
        }
    }
}
