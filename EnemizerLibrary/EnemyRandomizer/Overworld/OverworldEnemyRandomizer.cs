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

        public OverworldEnemyRandomizer(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
        }

        public void RandomizeOverworldEnemies()
        {
            // TODO: replace this with something less hard coded because we need to move squirrels and stuff in MS glade
            romData[0x04CF4F] = 0x10; //move bird from tree stump in lost woods

            // Build list of Overworld Sprite Groups
            // Randomize them
            // Save groups

            OverworldAreaCollection areas = new OverworldAreaCollection(romData);
            foreach(var area in areas.OverworldAreas)
            {
                // pick random sprite group
                //area.GraphicsBlockId = ??;

                // randomize sprites for area
                foreach(var s in area.Sprites)
                {
                    //s.SpriteId = ??;
                }
            }

            areas.SaveAreas();
        }
    }
}
