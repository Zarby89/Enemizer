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

        void LoadHeader()
        {
            // I would prefer to do this, but you have to offset from 118000 which gets confusing
            //int roomHeaderBaseAddress = 0x118000 + romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2) + 1] << 8);

            int roomHeaderBaseAddress = dungeonHeaderBaseAddress + (RoomId * 14);

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
                i += 3;
            }
        }
    }

    public class RoomCollection
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        RomData romData { get; set; }
        public RoomCollection(RomData romData)
        {
            this.romData = romData;

            // TODO: Consider moving this to the caller so we can make LoadRooms virtual? (in case we want to use this with other rom hacks)
            LoadRooms();
        }

        void LoadRooms()
        {
            //int dungeonSpriteTableBaseAddress = 0x4D62E;
            int currentRoomId = 0;

            for (int i=0; i<638; i+=2)
            {
                Room r = new Room(currentRoomId, romData);
                r.LoadRoom();
                Rooms.Add(r);

                currentRoomId++;
            }

            //for (int i=0; i<0x300; i+=2)
            //{
            //    int roomSpriteDataAddress = 0x40000 + romData[dungeonSpriteTableBaseAddress + i] + (romData[dungeonSpriteTableBaseAddress + i+1]<<8);
            //    Rooms.Add(new Room(currentRoomId, roomSpriteDataAddress, romData));
            //    currentRoomId++;
            //}
        }
    }

    public enum RoomRequirement
    {
        NeedsKillableForDoors,
        HasIceMan,
        WaterRoom,
        ShadowRoom,
        WallmasterRoom, // TOOD: I don't like this name
        BumperAndCrystalRoom,
        SwitchRoom,
        TonguesRoom,
        NoStatueRoom,
        CanonRoom,
        HasKey // TODO: not sure if we care about this or if we should do it from sprite side
    }
}
