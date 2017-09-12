using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EnemizerLibrary
{

    public class RoomCollection
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        RomData romData;
        Random rand;
        SpriteRequirementCollection spriteRequirementCollection;

        public RoomCollection(RomData romData, Random rand, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteRequirementCollection = spriteRequirementCollection;
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
            foreach (var r in Rooms.Where(x => RoomIdConstants.RandomizeRooms.Contains(x.RoomId)))
            {
                List<SpriteRequirement> doNotUpdateSprites = spriteRequirementCollection
                                                            .DoNotRandomizeSprites
                                                            .Where(x => x.CanSpawnInRoom(r)
                                                                        && r.Sprites.Select(y => y.SpriteId).ToList().Contains(x.SpriteId)
                                                                )
                                                            .ToList();

                var possibleSpriteGroups = spriteGroups.GetPossibleDungeonSpriteGroups(r.Sprites.Any(x => x.HasAKey || r.IsShutterRoom), doNotUpdateSprites).ToList();

                //Debug.Assert(possibleSpriteGroups.Count > 0);

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