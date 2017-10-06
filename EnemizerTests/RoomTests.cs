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

            RoomCollection rc = new RoomCollection(romData, rand, new SpriteRequirementCollection());
            foreach(var r in rc.Rooms)
            {
                output.WriteLine($"RoomId: {r.RoomId}, RoomName: {r.RoomName}, RoomGfx: {r.GraphicsBlockId}, sprite count: {r.Sprites.Count}, sprites: {String.Join(",", r.Sprites.Select(x => (x.IsOverlord ? "1" : "") + x.SpriteId.ToString("X2") + (x.HasAKey ? "(HasKey)" : "") ))}");
            }
        }

        [Fact]
        public void load_object_pointer_table()
        {
            RomData romData = Utilities.LoadRom("rando.sfc");
            var d = new DungeonObjectDataPointerCollection(romData);

            output.WriteLine($"Room\tSnesAddress\tRomAddress");
            foreach (var p in d.RoomDungeonObjectDataPointers)
            {
                output.WriteLine($"{p.RoomId}\t{p.SnesAddress.ToString("X")}\t{p.ROMAddress.ToString("X")}");
            }
        }
    }
}
