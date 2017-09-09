using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using EnemizerLibrary;
using Newtonsoft.Json;

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

            RoomCollection rc = new RoomCollection(romData, new Random(), new SpriteRequirementCollection());
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
                output.WriteLine($"Map: {owArea.AreaId.ToString("X3")} ({owArea.AreaName})\tGraphics Block: {owArea.GraphicsBlockId} ({owArea.GraphicsBlockAddress.ToString("X4")})");

                foreach (var s in owArea.Sprites)
                {
                    output.WriteLine($"Address: {s.SpriteAddress.ToString("X6")}\tSpriteId: {s.SpriteId.ToString("X2")}\t{SpriteConstants.GetSpriteName(s.SpriteId)}\tX: {s.SpriteX}\tY: {s.SpriteY}\tOverlord: {(s.SpriteId >= 0xF3 ? true : false).ToString()}");
                }
            }
        }

        [Fact]
        public void get_list_of_goodies()
        {
            var romData = Utilities.LoadRom("rando.sfc");
            var spriteRequirements = new SpriteRequirementCollection();

            SpriteGroupCollection sgc = new SpriteGroupCollection(romData, new Random(), spriteRequirements);
            sgc.LoadSpriteGroups();

            RoomCollection rc = new RoomCollection(romData, new Random(), spriteRequirements);
            rc.LoadRooms();

            OverworldAreaCollection areas = new OverworldAreaCollection(romData);
            

            var spriteGroupsJson = JsonConvert.SerializeObject(sgc.SpriteGroups.Select(x => new { x.GroupId, x.DungeonGroupId, x.SubGroup0, x.SubGroup1, x.SubGroup2, x.SubGroup3 }));
            var roomJson = JsonConvert.SerializeObject(rc.Rooms.Select(x => new { x.RoomId, x.RoomName, x.GraphicsBlockId }));
            var roomSpritesJson = JsonConvert.SerializeObject(rc.Rooms.Select(x => new { x.RoomId, Sprites = new { Sprites = x.Sprites.Select(y => new { y.SpriteId, y.SpriteName, y.Address, y.HasAKey, y.IsOverlord }) } }));

            var areaJson = JsonConvert.SerializeObject(areas.OverworldAreas.Select(x => new { x.AreaId, x.AreaName, x.GraphicsBlockId }));
            var areaSpritesJson = JsonConvert.SerializeObject(areas.OverworldAreas.Select(x => new { x.AreaId, Sprites = new { Sprites = x.Sprites.Select(y => new { y.SpriteId, y.SpriteName }) } }));

            output.WriteLine(spriteGroupsJson);
            output.WriteLine(roomJson);
            output.WriteLine(roomSpritesJson);
            output.WriteLine(areaJson);
            output.WriteLine(areaSpritesJson);
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
