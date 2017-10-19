using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class GraphTests
    {
        readonly ITestOutputHelper output;

        public GraphTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_get_to_ganon()
        {
            //var romData = Utilities.LoadRom("rando.sfc");
            var romData = Utilities.LoadRom("..\\..\\..\\ER_er-no-glitches-0.4.7_normal-open-ganon_297664836.sfc"); // simple
            Random rand = new Random(0);

            RomEntranceCollection romEntrances = new RomEntranceCollection(romData);
            RomExitCollection romExits = new RomExitCollection(romData);
            RomChestCollection romChests = new RomChestCollection(romData);

            GraphData graphData = new GraphData(romData, new OptionFlags(), romEntrances, romExits, romChests);
            //foreach(var e in graphData.AllEdges)
            //{
            //    output.WriteLine(e.ToString());
            //}
            //output.WriteLine("Rom locations");
            //foreach (var r in romChests.Chests)
            //{
            //    output.WriteLine($"0x{r.Address.ToString("X")}\t0x{r.ItemId.ToString("X2")}");
            //}
            //output.WriteLine("Raw locations");
            //foreach(var r in graphData._rawItemLocationCollection.RawItemLocations.Values)
            //{
            //    output.WriteLine($"0x{r.LocationAddress.ToString("X")}\t0x{r.ItemId.ToString("X2")}");
            //}

            Graph graph = new Graph(graphData);

            var result = graph.FindPath(graphData.AllNodes["cave-links-house"], graphData.AllNodes["triforce-room"], true);
            output.WriteLine(result.ToString());
            Assert.Equal(true, result.Success);
        }
    }
}
