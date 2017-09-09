using EnemizerLibrary;
using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class SpriteGroupTests
    {
        readonly ITestOutputHelper output;

        public SpriteGroupTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void should_load_all_sprites_groups()
        {
            Random rand = new Random(0);

            RomData romData = Utilities.LoadRom("rando.sfc");
            var spriteRequirements = new SpriteRequirementCollection();

            SpriteGroupCollection sgc = new SpriteGroupCollection(romData, rand, spriteRequirements);
            sgc.LoadSpriteGroups();
            sgc.RandomizeGroups();
            sgc.UpdateRom();

            foreach(var sg in sgc.SpriteGroups)
            {
                output.WriteLine($"SpriteGroupID: {sg.GroupId} (Dungeon GroupId: {sg.DungeonGroupId}), Sub0: {sg.SubGroup0} Sub1: {sg.SubGroup1} Sub2: {sg.SubGroup2} Sub3: {sg.SubGroup3} ");
                if(sg.GroupId == 77)
                {
                    Assert.Equal(81, sg.SubGroup0);
                }
            }
        }
    }
}
