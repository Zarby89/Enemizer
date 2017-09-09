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
                RandomizeRoomSprites(room);
            }
        }

        void RandomizeRoomSprites(Room room)
        {
            var spriteGroup = spriteGroupCollection.SpriteGroups.First(x => x.DungeonGroupId == room.GraphicsBlockId);

            var possibleSprites = spriteGroup.PossibleEnemySprites.ToArray();

            if (possibleSprites.Length > 0)
            {
                var spritesToUpdate = room.Sprites.Where(x => spriteRequirementCollection.RandomizableSprites.Select(y => y.SpriteId).Contains(x.SpriteId))
                    .ToList();

                
                // TODO: something like hacky for shutters.
                var keySprites = spritesToUpdate.Where(x => x.HasAKey || room.IsShutterRoom).ToList();

                var killableSprites = spriteRequirementCollection.KillableSprites.Where(x => possibleSprites.Contains(x.SpriteId)).Select(x => x.SpriteId).ToList();

                if (keySprites.Count > 0 && killableSprites.Count == 0)
                {
                    throw new Exception("Key in room without any killable enemies");
                }

                Debug.Assert(possibleSprites.Contains(SpriteConstants.EmptySprite) == false);
                Debug.Assert(killableSprites.Contains(SpriteConstants.EmptySprite) == false);

                spritesToUpdate.Where(x => x.HasAKey == false).ToList()
                    .ForEach(x => x.SpriteId = (byte)possibleSprites[rand.Next(possibleSprites.Length)]);

                // TODO: something like hacky for shutters.
                spritesToUpdate.Where(x => x.HasAKey || room.IsShutterRoom).ToList()
                    .ForEach(x => x.SpriteId = (byte)killableSprites[rand.Next(killableSprites.Count)]);
            }
        }

        private void WriteRom()
        {
            roomCollection.UpdateRom();
        }
    }
}
