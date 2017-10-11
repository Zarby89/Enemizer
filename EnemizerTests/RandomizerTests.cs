using System;
using System.Linq;
using Xunit;
using EnemizerLibrary;
using System.IO;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class RandomizerTests
    {
        readonly ITestOutputHelper output;
        public const int seed = 0;
        public RandomizerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void randomize_10_seeds_in_memory()
        {
            byte[] rom_data = LoadRom("rando.sfc");

            Random rand = new Random(0);

            OptionFlags options = MakeOptions();

            Randomization randomizer = new Randomization();

            for(int i=0; i<10; i++)
            {
                RomData romData = new RomData(rom_data);
                randomizer.MakeRandomization(rand.Next(), options, romData);
            }
        }

        [Fact]
        public void randomize_100_seeds_in_memory()
        {
            byte[] rom_data = LoadRom("rando.sfc");

            Random rand = new Random(0);

            OptionFlags options = MakeOptions();

            Randomization randomizer = new Randomization();

            for (int i = 0; i < 100; i++)
            {
                RomData romData = new RomData(rom_data);
                randomizer.MakeRandomization(rand.Next(), options, romData);
            }
        }

        [Fact]
        public void randomize_100_seeds_in_memory_with_absorbables()
        {
            byte[] rom_data = LoadRom("rando.sfc");

            Random rand = new Random(0);

            OptionFlags options = MakeOptions();
            options.EnemiesAbsorbable = true;

            Randomization randomizer = new Randomization();

            for (int i = 0; i < 100; i++)
            {
                RomData romData = new RomData(rom_data);
                randomizer.MakeRandomization(rand.Next(), options, romData);
            }
        }


        byte[] LoadRom(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            return rom_data;
        }

        OptionFlags MakeOptions()
        {
            return new OptionFlags()
            {
                BossMadness = true,
                EnemiesAbsorbable = true,
                RandomizeBosses = true,
                RandomizeBossesType = RandomizeBossesType.Chaos,
                RandomizeBushEnemyChance = true,
                RandomizeDungeonPalettes = true,
                RandomizeEnemies = true,
                RandomizeEnemiesType = RandomizeEnemiesType.Chaos,
                RandomizeOverworldPalettes = true,
                RandomizePots = true,
                GenerateSpoilers = true
            };
        }
    }
}
