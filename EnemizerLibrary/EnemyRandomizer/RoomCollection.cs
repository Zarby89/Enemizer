using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary
{

    public class RoomCollection
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        RomData romData { get; set; }
        Random rand { get; set; }
        public RoomCollection(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
        }

        public void LoadRooms()
        {
            int currentRoomId = 0;

            for (int i=0; i<638; i+=2)
            {
                Room r = new Room(currentRoomId, romData);
                r.LoadRoom();
                Rooms.Add(r);

                currentRoomId++;
            }
        }

        public void RandomizeRoomSpriteGroups(SpriteGroupCollection spriteGroups)
        {
            foreach(var r in Rooms.Where(x => RoomIdConstants.RandomizeRooms.Contains(x.RoomId)))
            {
                var possibleSpriteGroups = spriteGroups.UsableDungeonSpriteGroups.ToList();

                if(r.Sprites.Any(x => x.HasAKey))
                {
                    GroupSubsetPossibleSpriteCollection possibleSpriteCollection = new GroupSubsetPossibleSpriteCollection();

                    var killableSprites = possibleSpriteCollection.Sprites
                        .Where(x => SpriteConstants.NonKillable.Contains((byte)x.SpriteId) == false)
                        .Select(x => x.GroupSubsetId).ToArray();

                    possibleSpriteGroups = possibleSpriteGroups.Where(x => killableSprites.Contains(x.SubGroup0)
                        || killableSprites.Contains(x.SubGroup1)
                        || killableSprites.Contains(x.SubGroup2)
                        || killableSprites.Contains(x.SubGroup3)).ToList();
                }

                r.GraphicsBlockId = possibleSpriteGroups[rand.Next(possibleSpriteGroups.Count)].DungeonGroupId;
            }

            // force any rooms we need to
            foreach (var sg in spriteGroups.SpriteGroups.Where(x => x.ForceRoomsToGroup != null && x.ForceRoomsToGroup.Count > 0))
            {
                foreach (var forcedR in Rooms.Where(x => sg.ForceRoomsToGroup.Contains(x.RoomId)))
                {
                    forcedR.GraphicsBlockId = sg.DungeonGroupId;
                }
            }
        }

        public void UpdateRom()
        {
            Rooms.ForEach(x => x.UpdateRom());
        }
    }
}