using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Room
    {
        const int dungeonHeaderBaseAddress = 0x120090;
        const int dungeonHeaderPointerTableBaseAddress = 0x271E2;
        const int dungeonSpritePointerTableBaseAddress = 0x4D62E;

        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public int GraphicsBlockId { get; set; }

        public List<DungeonSprite> Sprites { get; set; } = new List<DungeonSprite>();
        //public List<RoomRequirement> Requirements { get; set; }

        RomData romData { get; set; }

        public Room(int roomId, RomData romData)
        {
            this.RoomId = roomId;
            this.RoomName = RoomIdConstants.GetRoomName(roomId);
            this.romData = romData;
        }

        public void LoadRoom()
        {
            LoadHeader();

            LoadSprites();
        }

        public void UpdateRom()
        {
            UpdateHeader();

            UpdateSprites();
        }

        private void UpdateSprites()
        {
            foreach(var s in Sprites)
            {
                romData[s.Address + 2] = s.SpriteId;
            }
        }

        private void UpdateHeader()
        {
            int roomHeaderBaseAddress = 0x118000 + romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2) + 1] << 8);
            //int roomHeaderBaseAddress = dungeonHeaderBaseAddress + (RoomId * 14);
            romData[roomHeaderBaseAddress + 3] = (byte)(this.GraphicsBlockId);// - 0x40);
        }

        void LoadHeader()
        {
            // I would prefer to do this, but you have to offset from 118000 which gets confusing
            int roomHeaderBaseAddress = 0x118000 + romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2) + 1] << 8);

            //int roomHeaderBaseAddress = dungeonHeaderBaseAddress + (RoomId * 14);

            this.GraphicsBlockId = romData[roomHeaderBaseAddress + 3]; // TODO: - 0x40 ??
        }

        void LoadSprites()
        {
            int roomSpriteBaseAddress = 0x40000 + romData[dungeonSpritePointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonSpritePointerTableBaseAddress + (RoomId * 2) + 1] << 8);

            byte byte0 = romData[roomSpriteBaseAddress];

            int i = 1;

            while(romData[roomSpriteBaseAddress + i] != 0xFF)
            {
                Sprites.Add(new DungeonSprite(romData, roomSpriteBaseAddress + i));
                i += 3; // sprites are 3 byte chunks
            }
        }
    }

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
        }

        public void UpdateRom()
        {
            Rooms.ForEach(x => x.UpdateRom());
        }
    }
}
