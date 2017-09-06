using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using EnemizerLibrary;

namespace EnemizerTests
{
    public class Experiments
    {
        readonly ITestOutputHelper output;

        public Experiments(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestMethod1()
        {
            var romData = Utilities.LoadRom("rando.sfc");

            RoomCollection rc = new RoomCollection(romData, new Random());
            rc.LoadRooms();

            // invert the masks
            byte byte0mask = (byte)~SpriteConstants.SpriteSubtypeByte0RemoveMask;
            byte byte1mask = (byte)~SpriteConstants.OverlordRemoveMask;


            foreach (var r in rc.Rooms)
            {
                var sprites = r.Sprites.Where(x => (x.byte0 & byte0mask) > 0 || (x.byte1 & byte1mask) > 0);
                if(sprites.Any())
                {
                    output.WriteLine($"room: {r.RoomId} ({r.RoomName})");
                    foreach(var s in sprites)
                    {
                        output.WriteLine($"addr: {s.Address.ToString("X8")} \tID: {((s.byte1 & byte1mask) == 0xE0 ? s.SpriteId + 0x100 : s.SpriteId).ToString("X2")} \tName: { SpriteConstants.GetSpriteName(((s.byte1 & byte1mask) == 0xE0 ? s.SpriteId + 0x100 : s.SpriteId)) } \tBits: {Convert.ToString((((s.byte0 & byte0mask) >> 2) | ((s.byte1 & byte1mask) >> 5)), 2).PadLeft(5, '0')} \tHM P: {(((s.byte0 & byte0mask) >> 2) | ((s.byte1 & byte1mask) >> 5)).ToString("X2")}");

                    }
                    output.WriteLine("");
                }
            }
        }
    }
}
