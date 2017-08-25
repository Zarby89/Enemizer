using System;
using Xunit;
using EnemizerLibrary;
using System.IO;
using System.Diagnostics;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class BossRandomizer
    {
        readonly ITestOutputHelper output;

        public BossRandomizer(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void randomize_with_seed_xxx_should_produce_rom()
        {
            int seed = 0;

            OptionFlags optionFlags = new OptionFlags();
            optionFlags.RandomizeBosses = true;
            optionFlags.RandomizeBossesType = RandomizeBossesType.Basic;

            byte[] ROM_DATA = LoadRom("rando.sfc");
            string outputFilename = "test.sfc";
            Randomization r = new Randomization();
            RomData rom = r.MakeRandomization(seed, optionFlags, ROM_DATA, outputFilename, "Default");

            // this is just in case something goes wrong we can get the output and compare it in beyond compare or another program
            //string fileName = "test1output.sfc";
            //FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            //rom.WriteRom(fs);
            //fs.Close();


            byte[] testRom = LoadRom("test1.sfc");
            for(int i=0; i<testRom.Length; i++)
            {
                if (testRom[i] != rom[i])
                {
                    output.WriteLine($"i = {i} - testRom[i] = {testRom[i]}  rom[i] = {rom[i]}");
                }
                Assert.Equal(testRom[i], rom[i]);
            }
        }

        public byte[] LoadRom(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            return rom_data;
        }
    }
}
