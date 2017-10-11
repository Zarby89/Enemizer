using System;
using System.Linq;
using Xunit;
using EnemizerLibrary;
using System.IO;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class DungeonEnemyRandomizerTests
    {
        readonly ITestOutputHelper output;
        public const int seed = 0;
        public DungeonEnemyRandomizerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void default_settings_randomizes_enemies_in_dungeons()
        {
            RomData romData = Utilities.LoadRom("rando.sfc");

            Random rand = new Random(0);
            var requirements = new SpriteRequirementCollection();
            
            var spriteGroups = new SpriteGroupCollection(romData, rand, requirements);
            spriteGroups.LoadSpriteGroups();

            DungeonEnemyRandomizer der = new DungeonEnemyRandomizer(romData, rand, spriteGroups, requirements);
            der.RandomizeDungeonEnemies(new OptionFlags() { RandomizeEnemies = true, EnemiesAbsorbable = true });

            foreach(var sg in der.spriteGroupCollection.SpriteGroups)
            {
                output.WriteLine($"GroupID: {sg.GroupId} (Dungeon GroupId: {sg.DungeonGroupId}) - SG0: {sg.SubGroup0} - SG1: {sg.SubGroup1} - SG2: {sg.SubGroup2} - SG3: {sg.SubGroup3}");
            }

            foreach (var r in der.roomCollection.Rooms)
            {
                output.WriteLine($"RoomID: {r.RoomId} - GroupID: {r.GraphicsBlockId}");
                foreach (var s in r.Sprites)
                {
                    output.WriteLine($"\tSpriteID: {s.SpriteId} - Address: {s.Address}");
                }
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
