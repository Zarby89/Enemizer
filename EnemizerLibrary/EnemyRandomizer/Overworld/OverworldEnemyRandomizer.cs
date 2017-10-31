using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldEnemyRandomizer
    {
        RomData romData;
        Random rand;
        SpriteGroupCollection spriteGroupCollection;
        SpriteRequirementCollection spriteRequirementCollection;
        OverworldAreaCollection areas;

        public OverworldEnemyRandomizer(RomData romData, Random rand, SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteGroupCollection = spriteGroupCollection;
            this.spriteRequirementCollection = spriteRequirementCollection;

            areas = new OverworldAreaCollection(romData, rand, spriteGroupCollection, spriteRequirementCollection);
        }

        public void RandomizeOverworldEnemies(OptionFlags optionFlags)
        {
            // TODO: replace this with something less hard coded because we also need to move squirrels and stuff in MS glade and probably Zora
            romData[0x04CF4F] = 0x10; //move bird from tree stump in lost woods // TODO: did this cause a crash?

            GenerateGroups();

            RandomizeAreas(optionFlags);

            WriteRom();
        }

        private void GenerateGroups()
        {
            spriteGroupCollection.RandomizeOverworldGroups();
        }

        private void RandomizeAreas(OptionFlags optionFlags)
        {
            areas.RandomizeAreaSpriteGroups(spriteGroupCollection);

            foreach (var area in areas.OverworldAreas)
            {
                // pick random sprite group
                //area.GraphicsBlockId = ??;
                area.RandomizeSprites(optionFlags);
                area.RandomizeBushSprite();
            }
        }

        private void WriteRom()
        {
            areas.UpdateRom();
        }
    }
}
