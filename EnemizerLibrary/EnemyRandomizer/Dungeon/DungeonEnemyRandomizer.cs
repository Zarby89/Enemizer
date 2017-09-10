using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonEnemyRandomizer
    {
        Random rand;
        RomData romData;
        SpriteRequirementCollection spriteRequirementCollection;

        public SpriteGroupCollection spriteGroupCollection { get; set; }

        public RoomCollection roomCollection { get; set; }

        public DungeonEnemyRandomizer(RomData romData, Random rand, SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteGroupCollection = spriteGroupCollection;
            this.spriteRequirementCollection = spriteRequirementCollection;

            this.roomCollection = new RoomCollection(romData, rand, spriteRequirementCollection);
        }

        public void RandomizeDungeonEnemies()
        {
            GenerateGroups();

            RandomizeRooms();

            WriteRom();
        }

        private void GenerateGroups()
        {
            spriteGroupCollection.RandomizeGroups();
        }

        private void RandomizeRooms()
        {
            roomCollection.LoadRooms();

            roomCollection.RandomizeRoomSpriteGroups(spriteGroupCollection);

            foreach (var room in roomCollection.Rooms.Where(x => RoomIdConstants.RandomizeRooms.Contains(x.RoomId)))
            {
                room.RandomizeSprites(rand, spriteGroupCollection, spriteRequirementCollection);
                //RandomizeRoomSprites(room);
            }
        }

        private void WriteRom()
        {
            roomCollection.UpdateRom();
        }
    }
}
