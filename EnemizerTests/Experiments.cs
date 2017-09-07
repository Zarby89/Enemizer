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


            byte[] roomPointer = new byte[4];
            for (int i = 0; i < 0x120; i++)
            {
                roomPointer[0] = romData[(0x04C901 + (i * 2) + 0)];
                roomPointer[1] = romData[(0x04C901 + (i * 2) + 1)];
                roomPointer[2] = 09;

                int address = BitConverter.ToInt32(roomPointer, 0);

                int pcadd = EnemizerLibrary.Utilities.SnesToPCAddress(address);

                bool ff = false;
                int pos = 0;

                output.WriteLine($"Map: {i.ToString("X3")}");
                while (ff == false)
                {
                    if (romData[pcadd + pos] != 0xFF)
                    {
                        var spriteAddress = pcadd + pos + 2; //address
                        var spriteId = romData[pcadd + pos + 2]; //sprite
                        var spriteY = romData[pcadd + pos + 0]; // Y
                        var spriteX = romData[pcadd + pos + 1]; // X

                        //randomized(0), keeped(1) or deleted(2);

                        output.WriteLine($"Address: {spriteAddress.ToString("X6")}\tSpriteId: {spriteId.ToString("X2")}\t{SpriteConstants.GetSpriteName(spriteId)}\tX: {spriteX}\tY: {spriteY}\tOverlord: {(spriteId >= 0xF3 ? true : false).ToString()}");
                        //Console.WriteLine(address.ToString("X6") +" : " + ROM_DATA[pcadd + pos].ToString() + "," + ROM_DATA[pcadd + pos + 1].ToString() + "," + ROM_DATA[pcadd + pos + 2].ToString("X2"));

                        pos += 3;
                    }
                    else
                    {
                        ff = true;
                    }

                }
            }

        }
    }
}
