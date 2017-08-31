using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class SpriteTests
    {
        readonly ITestOutputHelper output;

        public SpriteTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_load_all_sprites_for_dungeon_rooms()
        {
            byte[] ROM_DATA = Utilities.LoadRom("rando.sfc");
            RomData romData = new RomData(ROM_DATA);

            RoomCollection rc = new RoomCollection(romData);
            foreach(var r in rc.Rooms)
            {
                output.WriteLine($"RoomId: {r.RoomId}, sprite count: {r.Sprites.Count}, sprites: {String.Join(",", r.Sprites.Select(x => (x.IsOverlord ? "1" : "") + x.SpriteId.ToString("X2") ))}");
            }
        }
    }
}
