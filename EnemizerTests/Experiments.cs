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
        public void get_underworld_sprite_params_and_overlord()
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

        [Fact]
        public void get_overworld_sprites()
        {
            var romData = Utilities.LoadRom("rando.sfc");

            OverworldAreaCollection areas = new OverworldAreaCollection(romData);

            foreach(var owArea in areas.OverworldAreas)
            {
                //int offset = owArea.AreaId;

                //if (owArea.AreaId >= 0x40 && owArea.AreaId < 0x80)
                //{
                //    offset += 0x40;
                //}
                //if (owArea.AreaId >= 0x90 && owArea.AreaId < 0x110)
                //{
                //    offset -= 0x50;
                //}

                //int graphicsBlock = romData[0x07A81 + offset];

                output.WriteLine($"Map: {owArea.AreaId.ToString("X3")} ({owArea.AreaName})\tGraphics Block: {owArea.GraphicsBlockId} ({owArea.GraphicsBlockAddress.ToString("X4")})");

                //0x07A81 + i // should be pre aga LW

                //0x07AC1 + (i - 0x90) // should be post aga LW

                //0x07B01 + (i - 0x40) // should be pre aga DW

                // 0x07B41 // post aga DW???? doesn't look like it

                foreach (var s in owArea.Sprites)
                {
                    output.WriteLine($"Address: {s.SpriteAddress.ToString("X6")}\tSpriteId: {s.SpriteId.ToString("X2")}\t{SpriteConstants.GetSpriteName(s.SpriteId)}\tX: {s.SpriteX}\tY: {s.SpriteY}\tOverlord: {(s.SpriteId >= 0xF3 ? true : false).ToString()}");
                }
            }

            //for (int i = 0; i < 0x120; i++)
            //{
            //    var owArea = new OverworldArea(romData, i);

            //    output.WriteLine($"Map: {owArea.AreaId.ToString("X3")} ({owArea.AreaName})");
            //    foreach (var s in owArea.Sprites)
            //    {
            //        output.WriteLine($"Address: {s.SpriteAddress.ToString("X6")}\tSpriteId: {s.SpriteId.ToString("X2")}\t{SpriteConstants.GetSpriteName(s.SpriteId)}\tX: {s.SpriteX}\tY: {s.SpriteY}\tOverlord: {(s.SpriteId >= 0xF3 ? true : false).ToString()}");
            //    }
            //}

        }

        [Fact]
        public void fishing_expedition()
        {
            var romData = Utilities.LoadRom("rando.sfc");
            //output.WriteLine("hmm");

            for(int i=0; i<romData.Length-1; i++)
            {
                if(romData[i] == 0x81 && romData[i+1] == 0x7A)
                {
                    output.WriteLine($"Maybe something: 0x7A81 at {i.ToString("X8")}");
                    // maybe a hit
                    for (int j=0; j<0x20; j++)
                    {
                        if (j+1 >= romData.Length)
                            break;

                        if(romData[j] == 0x82 && romData[j+1] == 0x7A)
                        {
                            output.WriteLine($"Maybe something: 0x7A81 at {i.ToString("X8")}  0x7A82 at {j.ToString("X8")}");
                        }
                    }
                }
            }
        }
    }
}
