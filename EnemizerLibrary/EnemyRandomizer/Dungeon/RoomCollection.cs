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
        SpriteGroupCollection spriteGroupCollection;
        SpriteRequirementCollection spriteRequirementCollection;

        public RoomCollection(RomData romData, Random rand, SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteGroupCollection = spriteGroupCollection;
            this.spriteRequirementCollection = spriteRequirementCollection;
        }

        public void LoadRooms()
        {
            int currentRoomId = 0;

            for (int i=0; i<0x250; i+=2) // 0x128 = 296 rooms
            {
                Room r = new Room(currentRoomId, romData, spriteGroupCollection, spriteRequirementCollection);
                r.LoadRoom();
                Rooms.Add(r);

                currentRoomId++;
            }
        }

        public void RandomizeRoomSpriteGroups(SpriteGroupCollection spriteGroups)
        {
            // skip rooms that are set to do not randomize because that would be pointless to process them
            foreach (var r in Rooms.Where(x => RoomIdConstants.DontRandomizeRooms.Contains(x.RoomId) == false))
            {
                List<SpriteRequirement> doNotUpdateSprites = spriteRequirementCollection
                                                            .DoNotRandomizeSprites
                                                            .Where(x => x.CanSpawnInRoom(r)
                                                                        && r.Sprites.Select(y => y.SpriteId).ToList().Contains(x.SpriteId)
                                                                )
                                                            .ToList();

                /* TODO: put this back after I figure out what I screwed up
                List<SpriteRequirement> forcedSprites = spriteRequirementCollection.SpriteRequirements
                                            .Where(x => false == x.CanBeRandomizedInRoom(r))
                                            .Where(x => x.CanSpawnInRoom(r)
                                                        && r.Sprites.Select(y => y.SpriteId).ToList().Contains(x.SpriteId)
                                                )
                                            .ToList();
                doNotUpdateSprites.AddRange(forcedSprites);

                //*/


                var possibleSpriteGroups = spriteGroups.GetPossibleDungeonSpriteGroups(r, doNotUpdateSprites).ToList();

                //Debug.Assert(possibleSpriteGroups.Count > 0);
                if (possibleSpriteGroups.Count > 0)
                {
                    r.GraphicsBlockId = possibleSpriteGroups[rand.Next(possibleSpriteGroups.Count)].DungeonGroupId;
                }
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