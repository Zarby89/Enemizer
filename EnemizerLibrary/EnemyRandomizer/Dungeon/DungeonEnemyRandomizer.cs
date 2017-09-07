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

        public SpriteGroupCollection spriteGroupCollection { get; set; }
        public RoomCollection roomCollection { get; set; }

        public DungeonEnemyRandomizer(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;

            this.spriteGroupCollection = new SpriteGroupCollection(romData, rand);
            this.roomCollection = new RoomCollection(romData, rand);
        }

        public void RandomizeDungeonEnemies()
        {
            GenerateGroups();
            RandomizeRooms();
            WriteRom();
        }

        private void GenerateGroups()
        {
            spriteGroupCollection.LoadSpriteGroups();
            spriteGroupCollection.RandomizeGroups();
            spriteGroupCollection.UpdateRom();

            roomCollection.LoadRooms();
            roomCollection.RandomizeRoomSpriteGroups(spriteGroupCollection);

            GroupSubsetPossibleSpriteCollection possibleSpriteCollection = new GroupSubsetPossibleSpriteCollection();

            foreach(var room in roomCollection.Rooms.Where(x => RoomIdConstants.RandomizeRooms.Contains(x.RoomId)))
            {
                var spriteGroup = spriteGroupCollection.SpriteGroups.First(x => x.DungeonGroupId == room.GraphicsBlockId);

                var possibleSprites = possibleSpriteCollection.Sprites
                    .Where(x => spriteGroup.SubGroup0 == x.GroupSubsetId
                    || spriteGroup.SubGroup1 == x.GroupSubsetId
                    || spriteGroup.SubGroup2 == x.GroupSubsetId
                    || spriteGroup.SubGroup3 == x.GroupSubsetId).ToArray();

                if (possibleSprites.Length > 0)
                {
                    var spritesToUpdate = room.Sprites
                        .Where(x => x.SpriteId != SpriteConstants.KeySprite && x.SpriteId != SpriteConstants.BigKeySprite)
                        .Where(x => SpriteConstants.NpcSprites.Contains(x.SpriteId) == false)
                        //.Where(x => SpriteConstants.NonKillable.Contains(x.SpriteId) == false)
                        .ToList();

                    var keySprites = spritesToUpdate.Where(x => x.HasAKey).ToList();

                    var killableSprites = possibleSprites.Where(x => SpriteConstants.NonKillable.Contains((byte)x.SpriteId) == false).ToList();

                    if(keySprites.Count > 0 && killableSprites.Count == 0)
                    {
                        throw new Exception("Key in room without any killable enemies");
                    }

                    spritesToUpdate.Where(x => x.HasAKey == false).ToList()
                        .ForEach(x => x.SpriteId = (byte)possibleSprites[rand.Next(possibleSprites.Length)].SpriteId);

                    spritesToUpdate.Where(x => x.HasAKey).ToList()
                        .ForEach(x => x.SpriteId = (byte)killableSprites[rand.Next(killableSprites.Count)].SpriteId);
                }
            }
            // loop through rooms
            //      build list of combined requirements (?)
            //foreach(var r in roomCollection.Rooms.Where(x => x.Requirements != null && x.Requirements.Count > 0))
            //{

            //}

            // generate required groups first

            // generate remaining groups

            roomCollection.UpdateRom();

        }

        private void RandomizeRooms()
        {

        }

        private void WriteRom()
        {

        }
    }
}
