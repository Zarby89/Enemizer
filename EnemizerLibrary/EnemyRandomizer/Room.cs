using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Room
    {
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public List<DungeonSprite> Sprites { get; set; } = new List<DungeonSprite>();
        //public List<RoomRequirement> Requirements { get; set; }

        RomData romData { get; set; }
        int roomSpriteBaseAddress;
        public Room(int roomId, int roomSpriteBaseAddress, RomData romData)
        {
            this.RoomId = roomId;
            this.RoomName = RoomIdConstants.GetRoomName(roomId);
            this.roomSpriteBaseAddress = roomSpriteBaseAddress;
            this.romData = romData;

            LoadSprites();
        }

        void LoadSprites()
        {
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

            LoadRooms();

            ///*
            //// No mobs
            //Room hyruleCastleNorthCorridor = new Room();
            //hyruleCastleNorthCorridor.RoomName = "Hyrule Castle (North Corridor)";
            //hyruleCastleNorthCorridor.RoomId = 0x01;
            //Rooms.Add(hyruleCastleNorthCorridor);
            //*/

            //Room hyruleCastleSwitchRoom = new Room();
            //hyruleCastleSwitchRoom.RoomName = "Hyrule Castle (Switch Room)"; // rat room before sanctuary
            //hyruleCastleSwitchRoom.RoomId = 0x02;
            //// needs switches?
            //Rooms.Add(hyruleCastleSwitchRoom);

            //Room hyruleCastleBombableStockRoom = new Room();
            //hyruleCastleBombableStockRoom.RoomName = "Hyrule Castle (Bombable Stock Room)";
            //hyruleCastleBombableStockRoom.RoomId = 0x11;
            //Rooms.Add(hyruleCastleBombableStockRoom);

            //Room hyruleCastleKeyRatRoom = new Room();
            //hyruleCastleKeyRatRoom.RoomName = "Hyrule Castle (Key-rat Room)";
            //hyruleCastleKeyRatRoom.RoomId = 0x21;
            //// needs killable key guard/rat
            //Rooms.Add(hyruleCastleKeyRatRoom);

            //Room hyruleCastleSewerTextTriggerRoom = new Room();
            //hyruleCastleSewerTextTriggerRoom.RoomName = "Hyrule Castle (Sewer Text Trigger Room)";
            //hyruleCastleSewerTextTriggerRoom.RoomId = 0x22;
            //Rooms.Add(hyruleCastleSewerTextTriggerRoom);

            //Room hyruleCastleSewerKeyChestRoom = new Room();
            //hyruleCastleSewerKeyChestRoom.RoomName = "Hyrule Castle (Sewer Key Chest Room)";
            //hyruleCastleSewerKeyChestRoom.RoomId = 0x32;
            //Rooms.Add(hyruleCastleSewerKeyChestRoom);

            //Room hyruleCastleFirstDarkRoom = new Room();
            //hyruleCastleFirstDarkRoom.RoomName = "Hyrule Castle (First Dark Room)";
            //hyruleCastleFirstDarkRoom.RoomId = 0x41;
            //Rooms.Add(hyruleCastleFirstDarkRoom);

            //Room hyruleCastleSixRopesRoom = new Room();
            //hyruleCastleSixRopesRoom.RoomName = "Hyrule Castle (6 Ropes Room)";
            //hyruleCastleSixRopesRoom.RoomId = 0x42;
            //Rooms.Add(hyruleCastleSixRopesRoom);

            //Room hyruleCastleWestCorridor = new Room();
            //hyruleCastleWestCorridor.RoomName = "Hyrule Castle (West Corridor)";
            //hyruleCastleWestCorridor.RoomId = 0x50;
            //Rooms.Add(hyruleCastleWestCorridor);

            //Room hyruleCastleThroneRoom = new Room();
            //hyruleCastleThroneRoom.RoomName = "Hyrule Castle (Throne Room)";
            //hyruleCastleThroneRoom.RoomId = 0x51;
            //Rooms.Add(hyruleCastleThroneRoom);

            //Room hyruleCastleEastCorridor = new Room();
            //hyruleCastleEastCorridor.RoomName = "Hyrule Castle (East Corridor)";
            //hyruleCastleEastCorridor.RoomId = 0x52;
            //Rooms.Add(hyruleCastleEastCorridor);

            //Room hyruleCastleWestEntranceRoom = new Room();
            //hyruleCastleWestEntranceRoom.RoomName = "Hyrule Castle (West Entrance Room)";
            //hyruleCastleWestEntranceRoom.RoomId = 0x60;
            //Rooms.Add(hyruleCastleWestEntranceRoom);

            //Room hyruleCastleMainEntranceRoom = new Room();
            //hyruleCastleMainEntranceRoom.RoomName = "Hyrule Castle (Main Entrance Room)";
            //hyruleCastleMainEntranceRoom.RoomId = 0x61;
            //Rooms.Add(hyruleCastleMainEntranceRoom);

            //Room hyruleCastleEastEntranceRoom = new Room();
            //hyruleCastleEastEntranceRoom.RoomName = "Hyrule Castle (East Entrance Room)";
            //hyruleCastleEastEntranceRoom.RoomId = 0x62;
            //Rooms.Add(hyruleCastleEastEntranceRoom);

            ///*
            //// No mobs
            //Room hyruleCastleSmallCorridorToJailCells = new Room();
            //hyruleCastleSmallCorridorToJailCells.RoomName = "Hyrule Castle (Small Corridor to Jail Cells)";
            //hyruleCastleSmallCorridorToJailCells.RoomId = 0x70;
            //Rooms.Add(hyruleCastleSmallCorridorToJailCells);
            //*/

            //Room hyruleCastleBoomerangChestRoom = new Room();
            //hyruleCastleBoomerangChestRoom.RoomName = "Hyrule Castle (Boomerang Chest Room)";
            //hyruleCastleBoomerangChestRoom.RoomId = 0x71;
            //// needs killable key guard
            //Rooms.Add(hyruleCastleBoomerangChestRoom);

            //Room hyruleCastleMapChestRoom = new Room();
            //hyruleCastleMapChestRoom.RoomName = "Hyrule Castle (Map Chest Room)";
            //hyruleCastleMapChestRoom.RoomId = 0x72;
            //// needs killable key guard
            //Rooms.Add(hyruleCastleMapChestRoom);

            //Room hyruleCastleJailCellRoom = new Room();
            //hyruleCastleJailCellRoom.RoomName = "Hyrule Castle (Jail Cell Room)";
            //hyruleCastleJailCellRoom.RoomId = 0x80;
            //// needs killable big key guard
            //Rooms.Add(hyruleCastleJailCellRoom);

            //Room hyruleCastle = new Room();
            //hyruleCastle.RoomName = "Hyrule Castle (Two Green Guards After Chasm Room)";
            //hyruleCastle.RoomId = 0x81;
            //Rooms.Add(hyruleCastle);

            //Room hyruleCastleBasementChasmRoom = new Room();
            //hyruleCastleBasementChasmRoom.RoomName = "Hyrule Castle (Basement Chasm Room)";
            //hyruleCastleBasementChasmRoom.RoomId = 0x82;
            //Rooms.Add(hyruleCastleBasementChasmRoom);

            //Room castleSecretEntrance = new Room();
            //castleSecretEntrance.RoomName = "Castle Secret Entrance / Uncle Death Room ";
            //castleSecretEntrance.RoomId = 0x55;
            ////needs group 13, subgroup 0 = 81
            //Rooms.Add(castleSecretEntrance);


        }

        void LoadRooms()
        {
            int dungeonSpriteTableBaseAddress = 0x4D62E;
            int currentRoomId = 0;
            for (int i=0; i<0x300; i+=2)
            {
                int roomSpriteDataAddress = 0x40000 + romData[dungeonSpriteTableBaseAddress + i] + (romData[dungeonSpriteTableBaseAddress + i+1]<<8);
                Rooms.Add(new Room(currentRoomId, roomSpriteDataAddress, romData));
                currentRoomId++;
            }
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
