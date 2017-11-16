using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using EnemizerLibrary;
using Newtonsoft.Json;
using System.IO;

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

            RoomCollection rc = new RoomCollection(romData, new Random(), new SpriteGroupCollection(romData, new Random(), new SpriteRequirementCollection()), new SpriteRequirementCollection());
            rc.LoadRooms();

            // invert the masks
            byte byte0mask = (byte)~SpriteConstants.SpriteSubtypeByte0RemoveMask;
            byte byte1mask = (byte)~SpriteConstants.OverlordRemoveMask;


            foreach (var r in rc.Rooms)
            {
                var sprites = r.Sprites.Where(x => (x.byte0 & byte0mask) > 0 || (x.byte1 & byte1mask) > 0);
                if (sprites.Any())
                {
                    output.WriteLine($"room: {r.RoomId} ({r.RoomName})");
                    foreach (var s in sprites)
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
            var romData = Utilities.LoadRom("..\\..\\..\\EnemizerGui\\bin\\Debug\\Enemizer 6.0 - alttp - VT_no-glitches-26_normal_open_none_830270265.sfc");
            //var romData = Utilities.LoadRom("..\\..\\..\\Need To Test Seeds\\workingweirdnpc.sfc");

            OverworldAreaCollection areas = new OverworldAreaCollection(romData, new Random(), new SpriteGroupCollection(romData, new Random(), new SpriteRequirementCollection()), new SpriteRequirementCollection());

            foreach (var owArea in areas.OverworldAreas)
            {
                output.WriteLine($"Map: {owArea.AreaId.ToString("X3")} ({owArea.AreaName})\tGraphics Block: {owArea.GraphicsBlockId} ({owArea.GraphicsBlockAddress.ToString("X4")})");

                foreach (var s in owArea.Sprites)
                {
                    output.WriteLine($"Address: {s.SpriteAddress.ToString("X6")}\tSpriteId: {s.SpriteId.ToString("X2")}\t{SpriteConstants.GetSpriteName(s.SpriteId)}\tX: {s.SpriteX}\tY: {s.SpriteY}\tOverlord: {(s.SpriteId >= 0xF3 ? true : false).ToString()}");
                }
            }
        }

        [Fact]
        public void get_overworld_sprites_seed_0()
        {
            Randomization r = new Randomization();
            OptionFlags o = new OptionFlags()
            {
                RandomizeBosses = true,
                RandomizeEnemies = true,
                GenerateSpoilers = true
            };

            FileStream fs = new FileStream("alttp - VT_no-glitches-26_normal_open_none_830270265.sfc", FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            RomData romData = new RomData(rom_data);
            romData = r.MakeRandomization(0, o, romData, "");

            //var romData = Utilities.LoadRom("rando.sfc");

            OverworldAreaCollection areas = new OverworldAreaCollection(romData, new Random(), new SpriteGroupCollection(romData, new Random(), new SpriteRequirementCollection()), new SpriteRequirementCollection());

            foreach (var owArea in areas.OverworldAreas)
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

            RoomCollection rc = new RoomCollection(romData, new Random(), sgc, spriteRequirements);
            rc.LoadRooms();

            OverworldAreaCollection areas = new OverworldAreaCollection(romData, new Random(), new SpriteGroupCollection(romData, new Random(), new SpriteRequirementCollection()), new SpriteRequirementCollection());


            var spriteGroupsJson = JsonConvert.SerializeObject(sgc.SpriteGroups.Select(x => new { x.GroupId, x.DungeonGroupId, x.SubGroup0, x.SubGroup1, x.SubGroup2, x.SubGroup3 }), Formatting.Indented);
            var roomJson = JsonConvert.SerializeObject(rc.Rooms.Select(x => new { x.RoomId, x.RoomName, x.GraphicsBlockId }), Formatting.Indented);
            var roomSpritesJson = JsonConvert.SerializeObject(rc.Rooms.Select(x => new { x.RoomId, Sprites = new { Sprites = x.Sprites.Select(y => new { y.SpriteId, y.SpriteName, y.Address, y.HasAKey, y.IsOverlord }) } }), Formatting.Indented);

            var areaJson = JsonConvert.SerializeObject(areas.OverworldAreas.Select(x => new { x.AreaId, x.AreaName, x.GraphicsBlockId }), Formatting.Indented);
            var areaSpritesJson = JsonConvert.SerializeObject(areas.OverworldAreas.Select(x => new { x.AreaId, Sprites = new { Sprites = x.Sprites.Select(y => new { y.SpriteId, y.SpriteName }) } }), Formatting.Indented);

            output.WriteLine(spriteGroupsJson);
            output.WriteLine(roomJson);
            output.WriteLine(roomSpritesJson);
            output.WriteLine(areaJson);
            output.WriteLine(areaSpritesJson);
        }

        [Fact]
        public void zarbys_chests()
        {
            var romData = Utilities.LoadRom("zarbychests.sfc");

            RomChestCollection c = new RomChestCollection(romData);
            c.LoadChests(new EnemizerLibrary.Data.RawItemLocationCollection());

            output.WriteLine("test");
        }

        [Fact]
        public void fishing_expedition()
        {
            var romData = Utilities.LoadRom("rando.sfc");
            //output.WriteLine("hmm");

            for (int i = 0; i < romData.Length - 1; i++)
            {
                if (romData[i] == 0x81 && romData[i + 1] == 0x7A)
                {
                    output.WriteLine($"Maybe something: 0x7A81 at {i.ToString("X8")}");
                    // maybe a hit
                    for (int j = 0; j < 0x20; j++)
                    {
                        if (j + 1 >= romData.Length)
                            break;

                        if (romData[j] == 0x82 && romData[j + 1] == 0x7A)
                        {
                            output.WriteLine($"Maybe something: 0x7A81 at {i.ToString("X8")}  0x7A82 at {j.ToString("X8")}");
                        }
                    }
                }
            }
        }

        [Fact]
        public void figure_out_entrances()
        {
            //var romData = Utilities.LoadRom("rando.sfc");
            var romData = Utilities.LoadRom("..\\..\\..\\ER_er-no-glitches-0.4.7_normal-open-ganon_297664836.sfc"); // simple
            //var romData = Utilities.LoadRom("..\\..\\..\\ER_er-no-glitches-0.4.7_normal-open-ganon_676766069.sfc"); // insanity

            RomEntranceCollection ec = new RomEntranceCollection(romData);
            ec.LoadEntrances();

            RomExitCollection exits = new RomExitCollection(romData);
            exits.LoadExits();

            foreach(var e in ec.Entrances)
            {
                output.WriteLine($"Address: {e.EntranceAddress.ToString("X")} - Value: {e.EntranceAreaId.ToString("X3")} - {e.EntranceAreaName} - {e.EntranceSourceName} - Entrance #: 0x{e.EntranceNumber.ToString("X2")} - -> Connects to: 0x{e.ConnectToRoomId.ToString("X3")} - {e.ConnectsToRoomName}");
            }

            foreach(var e in exits.Exits)
            {
                output.WriteLine($"RoomAddress: {e.RoomAddress.ToString("X")} Room: {e.RoomId} - {e.RoomName} -> Goes to: AreaAddress: {e.AreaAddress.ToString("X")} Area: {e.AreaId} - {e.ExitAreaName} - {e.AreaName}");
            }
        }

        [Fact]
        public void dump_rooms()
        {
            foreach(var r in RoomIdConstants.roomNames)
            {
                output.WriteLine($"roomId\t0x{r.Key.ToString("X3")}\tName\t{r.Value}");
            }
        }

        [Fact]
        public void dump_music_addresses()
        {
            int[] romAddresses = new int[] { 0xD373B, 0xD375B, 0xD90F8, 0xDA710, 0xDA7A4, 0xDA7BB, 0xDA7D2, 0xD5954, 0xD653B, 0xDA736, 0xDA752, 0xDA772, 0xDA792, 0xD5B47, 0xD5B5E, 0xD4306, 0xD6878, 0xD6883, 0xD6E48, 0xD6E76, 0xD6EFB, 0xD6F2D, 0xDA211, 0xDA35B, 0xDA37B, 0xDA38E, 0xDA39F, 0xDA5C3, 0xDA691, 0xDA6A8, 0xDA6DF, 0xD2349, 0xD3F45, 0xD42EB, 0xD48B9, 0xD48FF, 0xD543F, 0xD5817, 0xD5957, 0xD5ACB, 0xD5AE8, 0xD5B4A, 0xDA5DE, 0xDA608, 0xDA635, 0xDA662, 0xDA71F, 0xDA7AF, 0xDA7C6, 0xDA7DD, 0xD2F00, 0xDA3D5, 0xD249C, 0xD24CD, 0xD2C09, 0xD2C53, 0xD2CAF, 0xD2CEB, 0xD2D91, 0xD2EE6, 0xD38ED, 0xD3C91, 0xD3CD3, 0xD3CE8, 0xD3F0C, 0xD3F82, 0xD405F, 0xD4139, 0xD4198, 0xD41D5, 0xD41F6, 0xD422B, 0xD4270, 0xD42B1, 0xD4334, 0xD4371, 0xD43A6, 0xD43DB, 0xD441E, 0xD4597, 0xD4B3C, 0xD4BAB, 0xD4C03, 0xD4C53, 0xD4C7F, 0xD4D9C, 0xD5424, 0xD65D2, 0xD664F, 0xD6698, 0xD66FF, 0xD6985, 0xD6C5C, 0xD6C6F, 0xD6C8E, 0xD6CB4, 0xD6D7D, 0xD827D, 0xD960C, 0xD9828, 0xDA233, 0xDA3A2, 0xDA49E, 0xDA72B, 0xDA745, 0xDA765, 0xDA785, 0xDABF6, 0xDAC0D, 0xDAEBE, 0xDAFAC, 0xD9A02, 0xD9BD6, 0xD21CD, 0xD2279, 0xD2E66, 0xD2E70, 0xD2EAB, 0xD3B97, 0xD3BAC, 0xD3BE8, 0xD3C0D, 0xD3C39, 0xD3C68, 0xD3C9F, 0xD3CBC, 0xD401E, 0xD4290, 0xD443E, 0xD456F, 0xD47D3, 0xD4D43, 0xD4DCC, 0xD4EBA, 0xD4F0B, 0xD4FE5, 0xD5012, 0xD54BC, 0xD54D5, 0xD54F0, 0xD5509, 0xD57D8, 0xD59B9, 0xD5A2F, 0xD5AEB, 0xD5E5E, 0xD5FE9, 0xD658F, 0xD674A, 0xD6827, 0xD69D6, 0xD69F5, 0xD6A05, 0xD6AE9, 0xD6DCF, 0xD6E20, 0xD6ECB, 0xD71D4, 0xD71E6, 0xD7203, 0xD721E, 0xD8724, 0xD8732, 0xD9652, 0xD9698, 0xD9CBC, 0xD9DC0, 0xD9E49, 0xDAA68, 0xDAA77, 0xDAA88, 0xDAA99, 0xDAF04, 0xD1D28, 0xD1D41, 0xD1D5C, 0xD1D77, 0xD1EEE, 0xD311D, 0xD31D1, 0xD4148, 0xD5543, 0xD5B6F, 0xD65B3, 0xD6760, 0xD6B6B, 0xD6DF6, 0xD6E0D, 0xD73A1, 0xD814C, 0xD825D, 0xD82BE, 0xD8340, 0xD8394, 0xD842C, 0xD8796, 0xD8903, 0xD892A, 0xD91E8, 0xD922B, 0xD92E0, 0xD937E, 0xD93C1, 0xDA958, 0xDA971, 0xDA98C, 0xDA9A7, 0xD1D92, 0xD1DBD, 0xD1DEB, 0xD1F5D, 0xD1F9F, 0xD1FBD, 0xD1FDC, 0xD1FEA, 0xD20CA, 0xD21BB, 0xD22C9, 0xD2754, 0xD284C, 0xD2866, 0xD2887, 0xD28A0, 0xD28BA, 0xD28DB, 0xD28F4, 0xD293E, 0xD2BF3, 0xD2C1F, 0xD2C69, 0xD2CA1, 0xD2CC5, 0xD2D05, 0xD2D73, 0xD2DAF, 0xD2E3D, 0xD2F36, 0xD2F46, 0xD2F6F, 0xD2FCF, 0xD2FDF, 0xD302B, 0xD3086, 0xD3099, 0xD30A5, 0xD30CD, 0xD30F6, 0xD3154, 0xD3184, 0xD333A, 0xD33D9, 0xD349F, 0xD354A, 0xD35E5, 0xD3624, 0xD363C, 0xD3672, 0xD3691, 0xD36B4, 0xD36C6, 0xD3724, 0xD3767, 0xD38CB, 0xD3B1D, 0xD3B2F, 0xD3B55, 0xD3B70, 0xD3B81, 0xD3BBF, 0xD3D34, 0xD3D55, 0xD3D6E, 0xD3DC6, 0xD3E04, 0xD3E38, 0xD3F65, 0xD3FA6, 0xD404F, 0xD4087, 0xD417A, 0xD41A0, 0xD425C, 0xD4319, 0xD433C, 0xD43EF, 0xD440C, 0xD4452, 0xD4494, 0xD44B5, 0xD4512, 0xD45D1, 0xD45EF, 0xD4682, 0xD46C3, 0xD483C, 0xD4848, 0xD4855, 0xD4862, 0xD486F, 0xD487C, 0xD4A1C, 0xD4A3B, 0xD4A60, 0xD4B27, 0xD4C7A, 0xD4D12, 0xD4D81, 0xD4E90, 0xD4ED6, 0xD4EE2, 0xD5005, 0xD502E, 0xD503C, 0xD5081, 0xD51B1, 0xD51C7, 0xD51CF, 0xD51EF, 0xD520C, 0xD5214, 0xD5231, 0xD5257, 0xD526D, 0xD5275, 0xD52AF, 0xD52BD, 0xD52CD, 0xD52DB, 0xD549C, 0xD5801, 0xD58A4, 0xD5A68, 0xD5A7F, 0xD5C12, 0xD5D71, 0xD5E10, 0xD5E9A, 0xD5F8B, 0xD5FA4, 0xD651A, 0xD6542, 0xD65ED, 0xD661D, 0xD66D7, 0xD6776, 0xD68BD, 0xD68E5, 0xD6956, 0xD6973, 0xD69A8, 0xD6A51, 0xD6A86, 0xD6B96, 0xD6C3E, 0xD6D4A, 0xD6E9C, 0xD6F80, 0xD717E, 0xD7190, 0xD71B9, 0xD811D, 0xD8139, 0xD816B, 0xD818A, 0xD819E, 0xD81BE, 0xD829C, 0xD82E1, 0xD8306, 0xD830E, 0xD835E, 0xD83AB, 0xD83CA, 0xD83F0, 0xD83F8, 0xD844B, 0xD8479, 0xD849E, 0xD84CB, 0xD84EB, 0xD84F3, 0xD854A, 0xD8573, 0xD859D, 0xD85B4, 0xD85CE, 0xD862A, 0xD8681, 0xD87E3, 0xD87FF, 0xD887B, 0xD88C6, 0xD88E3, 0xD8944, 0xD897B, 0xD8C97, 0xD8CA4, 0xD8CB3, 0xD8CC2, 0xD8CD1, 0xD8D01, 0xD917B, 0xD918C, 0xD919A, 0xD91B5, 0xD91D0, 0xD91DD, 0xD9220, 0xD9273, 0xD9284, 0xD9292, 0xD92AD, 0xD92C8, 0xD92D5, 0xD9311, 0xD9322, 0xD9330, 0xD934B, 0xD9366, 0xD9373, 0xD93B6, 0xD97A6, 0xD97C2, 0xD97DC, 0xD97FB, 0xD9811, 0xD98FF, 0xD996F, 0xD99A8, 0xD99D5, 0xD9A30, 0xD9A4E, 0xD9A6B, 0xD9A88, 0xD9AF7, 0xD9B1D, 0xD9B43, 0xD9B7C, 0xD9BA9, 0xD9C84, 0xD9C8D, 0xD9CAC, 0xD9CE8, 0xD9CF3, 0xD9CFD, 0xD9D46, 0xDA35E, 0xDA37E, 0xDA391, 0xDA478, 0xDA4C3, 0xDA4D7, 0xDA4F6, 0xDA515, 0xDA6E2, 0xDA9C2, 0xDA9ED, 0xDAA1B, 0xDAA57, 0xDABAF, 0xDABC9, 0xDABE2, 0xDAC28, 0xDAC46, 0xDAC63, 0xDACB8, 0xDACEC, 0xDAD08, 0xDAD25, 0xDAD42, 0xDAD5F, 0xDAE17, 0xDAE34, 0xDAE51, 0xDAF2E, 0xDAF55, 0xDAF6B, 0xDAF81, 0xDB14F, 0xDB16B, 0xDB180, 0xDB195, 0xDB1AA, 0xD2B88, 0xD364A, 0xD369F, 0xD3747, 0xD213F, 0xD2174, 0xD229E, 0xD2426, 0xD4731, 0xD4753, 0xD4774, 0xD4795, 0xD47B6, 0xD4AA5, 0xD4AE4, 0xD4B96, 0xD4CA5, 0xD5477, 0xD5A3D, 0xD6566, 0xD672C, 0xD67C0, 0xD69B8, 0xD6AB1, 0xD6C05, 0xD6DB3, 0xD71AB, 0xD8E2D, 0xD8F0D, 0xD94E0, 0xD9544, 0xD95A8, 0xD9982, 0xD9B56, 0xDA694, 0xDA6AB, 0xDAE88, 0xDAEC8, 0xDAEE6, 0xDB1BF, 0xD210A, 0xD22DC, 0xD2447, 0xD5A4D, 0xD5DDC, 0xDA251, 0xDA26C, 0xD945E, 0xD967D, 0xD96C2, 0xD9C95, 0xD9EE6, 0xDA5C6, 0xD2047, 0xD24C2, 0xD24EC, 0xD25A4, 0xD3DAA, 0xD51A8, 0xD51E6, 0xD524E, 0xD529E, 0xD6045, 0xD81DE, 0xD821E, 0xD94AA, 0xD9A9E, 0xD9AE4, 0xDA289, 0xD2085, 0xD21C5, 0xD5F28 };
            foreach(var i in romAddresses)
            {
                output.WriteLine($"{EnemizerLibrary.Utilities.PCToSnesAddress(i).ToString("X")}");
            }
        }

        [Fact]
        public void get_enemizer_version_from_rom()
        {
            var romData = Utilities.LoadRom("..\\..\\..\\EnemizerGui\\bin\\Debug\\Enemizer 6.0.03 - ALttP - VT_no-glitches-26_normal-open-triforce-hunt_triforce-hunt_872136068 (EN350803105).sfc");

            Assert.Equal(EnemizerLibrary.Version.CurrentVersion, romData.EnemizerVersion.Trim());
        }

        [Fact]
        public void xkas_exports_should_load()
        {
            var xkas = XkasSymbols.Instance.Symbols;

            foreach(var x in xkas)
            {
                output.WriteLine($"{x.Key} {x.Value.ToString("X")}");
            }
        }
    }
}
