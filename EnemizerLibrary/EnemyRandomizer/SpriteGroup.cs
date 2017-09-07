using System.Collections.Generic;

namespace EnemizerLibrary
{
    public class SpriteGroup
    {
        //const int SpriteGroupBaseAddress = 0x5B57; // wtf is this address? US?
        const int SpriteGroupBaseAddress = 0x5B97; // Dungeon Sprite Groups???

        public int GroupId { get; set; }
        public int DungeonGroupId
        {
            get
            {
                return GroupId - 0x40;
            }
            set
            {
                GroupId = value + 0x40;
            }
        }
        public int SubGroup0 { get; set; }
        public bool PreserveSubGroup0 { get; set; }
        public int SubGroup1 { get; set; }
        public bool PreserveSubGroup1 { get; set; }
        public int SubGroup2 { get; set; }
        public bool PreserveSubGroup2 { get; set; }
        public int SubGroup3 { get; set; }
        public bool PreserveSubGroup3 { get; set; }

        public List<int> ForceRoomsToGroup { get; set; }

        RomData romData;

        public SpriteGroup(RomData romData, int groupId)
        {
            this.romData = romData;

            this.GroupId = groupId;

            this.SubGroup0 = romData[SpriteGroupBaseAddress + (groupId * 4) + 0];
            this.SubGroup1 = romData[SpriteGroupBaseAddress + (groupId * 4) + 1];
            this.SubGroup2 = romData[SpriteGroupBaseAddress + (groupId * 4) + 2];
            this.SubGroup3 = romData[SpriteGroupBaseAddress + (groupId * 4) + 3];

            this.PreserveSubGroup0 = false;
            this.PreserveSubGroup1 = false;
            this.PreserveSubGroup2 = false;
            this.PreserveSubGroup3 = false;

            this.ForceRoomsToGroup = new List<int>();
        }

        public SpriteGroup(RomData romData, int groupId, params int[] forcedRooms)
            :this(romData, groupId)
        {
            this.ForceRoomsToGroup.AddRange(forcedRooms);
        }

        internal void UpdateRom()
        {
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 0] = (byte)SubGroup0;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 1] = (byte)SubGroup1;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 2] = (byte)SubGroup2;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 3] = (byte)SubGroup3;
        }
    }
}
