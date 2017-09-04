using System;
using System.Collections.Generic;

namespace EnemizerLibrary
{
    public class Room
    {
        // enemizer header patched rom
        const int dungeonHeaderOffsetAddress = 0x118000;
        const int dungeonHeaderBaseAddress = 0x120090;

        // vanilla jp game/normal rando rom
        //const int dungeonHeaderOffsetAddress = 0x018000;
        //const int dungeonHeaderBaseAddress = 0x27462;

        const int dungeonHeaderPointerTableBaseAddress = 0x271E2;
        const int dungeonSpritePointerTableBaseAddress = 0x4D62E;

        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public int GraphicsBlockId { get; set; }

        public List<DungeonSprite> Sprites { get; set; } = new List<DungeonSprite>();
        //public List<RoomRequirement> Requirements { get; set; }

        int RoomHeaderBaseAddress
        {
            get
            {
                return dungeonHeaderOffsetAddress + romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonHeaderPointerTableBaseAddress + (RoomId * 2) + 1] << 8);
            }
        }

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
                if(s.SpriteId == 3 && s.IsOverlord == false)
                {
                    throw new Exception("SpriteID 3 will crash the game");
                }

                if (s.IsOverlord == false)
                {
                    romData[s.Address + 1] = (byte)(romData[s.Address + 1] & SpriteConstants.OverlordRemoveMask);
                }
                romData[s.Address + 2] = s.SpriteId;
            }
        }

        private void UpdateHeader()
        {
            romData[RoomHeaderBaseAddress + 3] = (byte)(this.GraphicsBlockId);
        }

        void LoadHeader()
        {
            this.GraphicsBlockId = romData[RoomHeaderBaseAddress + 3];
        }

        void LoadSprites()
        {
            int roomSpriteBaseAddress = 0x40000 + romData[dungeonSpritePointerTableBaseAddress + (RoomId * 2)] + (romData[dungeonSpritePointerTableBaseAddress + (RoomId * 2) + 1] << 8);

            /*
            Byte 0: Stored to $0FB3. Corresponds with "Sort Spr" in Hyrule Magic. In a layered room this indicates sprites in the foreground are to be drawn on top of sprites in the background.

            Bit 0 - If 0, sprites will sort
            Bits 7,6,5,4,3,2,1 - Unknown, should all be 1
            */
            //byte byte0 = romData[roomSpriteBaseAddress];

            int i = 1;

            while(romData[roomSpriteBaseAddress + i] != 0xFF)
            {
                Sprites.Add(new DungeonSprite(romData, roomSpriteBaseAddress + i));
                i += 3; // sprites are 3 byte chunks
            }
        }
    }
}
