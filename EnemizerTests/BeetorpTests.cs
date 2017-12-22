using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class BeetorpTests
    {
        readonly ITestOutputHelper output;

        public BeetorpTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [ExcelData("beetorp.xls", "select * from TestData")]
        public void should_get_to_ganon(string filename, bool expected)
        {
            //var romData = Utilities.LoadRom("rando.sfc");
            output.WriteLine(filename);
            var romData = Utilities.LoadRom("..\\..\\..\\..\\rando_test_27_fixes\\" + filename); // simple

            Random rand = new Random(0);

            RomEntranceCollection romEntrances = new RomEntranceCollection(romData);
            RomExitCollection romExits = new RomExitCollection(romData);
            RomChestCollection romChests = new RomChestCollection(romData);

            GraphData graphData = new GraphData(romData, new OptionFlags(), romEntrances, romExits, romChests);

            Graph graph = new Graph(graphData);

            var result = graph.FindPath(graphData.AllNodes["cave-links-house"], graphData.AllNodes["triforce-room"], true);
            output.WriteLine(result.ToString());
            Assert.True(result.Success);
        }
    }
}
