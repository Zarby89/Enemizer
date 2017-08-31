using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class RoomTests
    {
        readonly ITestOutputHelper output;

        public RoomTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_load_all_sprites_for_dungeon_rooms()
        {
            RomData romData = Utilities.LoadRom("rando.sfc");
            Random rand = new Random(0);

            RoomCollection rc = new RoomCollection(romData, rand);
            foreach(var r in rc.Rooms)
            {
                output.WriteLine($"RoomId: {r.RoomId}, RoomName: {r.RoomName}, RoomGfx: {r.GraphicsBlockId}, sprite count: {r.Sprites.Count}, sprites: {String.Join(",", r.Sprites.Select(x => (x.IsOverlord ? "1" : "") + x.SpriteId.ToString("X2") + (x.HasAKey ? "(HasKey)" : "") ))}");
            }
        }
    }
}
