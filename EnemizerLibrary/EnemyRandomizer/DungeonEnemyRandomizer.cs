using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonEnemyRandomizer
    {
        Random rand { get; set; }
        RomData romData { get; set; }

        RoomCollection roomCollection { get; set; }

        public DungeonEnemyRandomizer(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
            this.roomCollection = new RoomCollection(romData);
        }

        public void RandomizeDungeonEnemies()
        {
            GenerateGroups();
            RandomizeRooms();
            WriteRom();
        }

        private void GenerateGroups()
        {
            // loop through rooms
            //      build list of combined requirements (?)
            //foreach(var r in roomCollection.Rooms.Where(x => x.Requirements != null && x.Requirements.Count > 0))
            //{

            //}

            // generate required groups first

            // generate remaining groups

        }

        private void RandomizeRooms()
        {

        }

        private void WriteRom()
        {

        }
    }
}
