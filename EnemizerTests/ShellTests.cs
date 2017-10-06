using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class ShellTests
    {
        readonly ITestOutputHelper output;

        public ShellTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void all_shells_should_have_F9_at_offset()
        {
            Assert.Equal(0xF9, DungeonConstants.room_6_shell[DungeonConstants.room_6_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_7_shell[DungeonConstants.room_7_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_200_shell[DungeonConstants.room_200_shell_index]);
            //Assert.Equal(0xF9, DungeonConstants.room_41_shell[DungeonConstants.room_6_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_51_shell[DungeonConstants.room_51_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_90_shell[DungeonConstants.room_90_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_144_shell[DungeonConstants.room_144_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_172_blind_room_shell[DungeonConstants.room_172_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_222_shell[DungeonConstants.room_222_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_164_shell[DungeonConstants.room_164_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_28_shell[DungeonConstants.room_28_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_108_shell[DungeonConstants.room_108_shell_index]);
            Assert.Equal(0xF9, DungeonConstants.room_77_shell[DungeonConstants.room_77_shell_index]);
        }

        [Fact]
        public void calculate_shell_bytes()
        {
            /*
			Subtype 2 objects are those with an index >= 0xFC

			1st, 2nd, & 3rd bytes:  Third Byte Second Byte  Byte
                                    ffdd dddd  eeee cccc   aaaa aabb
			
			The a bits are unused, but after all they are the marker for this type of object
            subtype.
            
			The b, c, e, and f bits are transformed into a VRAM tilemap address:
			
			000c cccf fbbe eee0
            
            Might I add this is one messed up format?

			The d bits are used as an index into the table at $8470. Since such indicies
			are going to be even, the d bits are transformed into: 0000 0000 0ddd ddd0
             */
            //byte[] bytes = { 0x2E, 0x98, 0xFF };
            //var f = (bytes[2] & 0xC0) >> 6;
            //var d = (bytes[2] & 0x3F);
            //var e = ((bytes[1] & 0xF0) >> 4);
            //var c = (bytes[1] & 0x0F);
            //var a = ((bytes[0] & 0xFC) >> 2);
            //var b = (bytes[0] & 0x03);

            //var vramAddress = (c << 9) | (f << 7) | (b << 5) | (e << 1);
            //output.WriteLine($"f:{f.ToString("X")} d:{d.ToString("X")} e:{e.ToString("X")} c:{c.ToString("X")} a:{a.ToString("X")} b:{b.ToString("X")}  vram:{vramAddress.ToString("X")}");

            SubType2Object trinexx = new SubType2Object(new byte[] { 0x2E, 0x98, 0xFF });
            output.WriteLine($"oid:{trinexx.OID.ToString("X3")}, x:{trinexx.XCoord.ToString("X2")}, y:{trinexx.YCoord.ToString("X2")}");

            SubType2Object trinexxFromCoord = new SubType2Object(0x0B, 0x26, 0xFF2);
            output.WriteLine($"bytes:{trinexxFromCoord.Bytes[0].ToString("X2")}, {trinexxFromCoord.Bytes[1].ToString("X2")}, {trinexxFromCoord.Bytes[2].ToString("X2")}, ");

            //var oid = ((bytes[2] << 4) | 0x80 | (((bytes[1] & 0x03) << 2) | ((bytes[0] & 0x03))) );
            //var posX = (byte)((bytes[0] & 0xFC) >> 2);
            //var posY = (byte)((bytes[1] & 0xFC) >> 2);

            //var sizeX = (byte)((bytes[0] & 0x03));
            //var sizeY = (byte)((bytes[1] & 0x03));
            //var sizeXY = (byte)(((sizeX << 2) + sizeY));


            //var b1 = ((posX << 2) & 0xFC) | (oid & 0x03);
            //var b2 = ((posY << 2) & 0xFC) | ((oid >> 2) & 0x03); ;
            //var b3 = 0xF0 | ((oid >> 4) & 0x0F);

            //output.WriteLine($"oid:{oid.ToString("X")}, x:{posX.ToString("X")}, y:{posY.ToString("X")}, x(tile):{(posX/16).ToString("X")}, y(tile):{(posY/16).ToString("X")}");
            //output.WriteLine($"{b1.ToString("X2")}, {b2.ToString("X2")}, {b3.ToString("X2")}");
        }

        [Fact]
        public void calculate_shell_bytes2()
        {
            //SubType2Object trinexx = new SubType2Object(new byte[] { 0x6E, 0x48, 0xFF }); // 6E 48 FF // x1B, y12, 0FF:2
            SubType2Object trinexx = new SubType2Object(new byte[] { 0x61, 0x51, 0xFF }); 
            //SubType2Object trinexx = new SubType2Object(new byte[] { 0x2E, 0x98, 0xFF });
            output.WriteLine($"oid:{trinexx.OID.ToString("X3")}, x:{trinexx.XCoord.ToString("X2")}, y:{trinexx.YCoord.ToString("X2")}");

            SubType2Object trinexxFromCoord = new SubType2Object(0x0B, 0x26, 0xFF2);
            output.WriteLine($"bytes:{trinexxFromCoord.Bytes[0].ToString("X2")}, {trinexxFromCoord.Bytes[1].ToString("X2")}, {trinexxFromCoord.Bytes[2].ToString("X2")}, ");

            SubType2Object kholdstareFromCoord = new SubType2Object(0x2B, 0x08, 0xF95);
            output.WriteLine($"bytes:{kholdstareFromCoord.Bytes[0].ToString("X2")}, {kholdstareFromCoord.Bytes[1].ToString("X2")}, {kholdstareFromCoord.Bytes[2].ToString("X2")}, ");

        }
    }
}
