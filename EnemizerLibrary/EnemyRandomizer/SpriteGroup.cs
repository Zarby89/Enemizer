using System.Collections.Generic;
using System.Linq;

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
        SpriteRequirementCollection spriteRequirementsCollection;

        public SpriteGroup(RomData romData, SpriteRequirementCollection spriteRequirementsCollection, int groupId)
        {
            this.romData = romData;
            this.spriteRequirementsCollection = spriteRequirementsCollection;

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

        public SpriteGroup(RomData romData, SpriteRequirementCollection spriteRequirementsCollection, int groupId, params int[] forcedRooms)
            :this(romData, spriteRequirementsCollection, groupId)
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

        public IEnumerable<SpriteRequirement> GetPossibleEnemySprites(Room room, OptionFlags optionFlags = null)
        {
            // TODO: add more logic to this?
            // needs to check for two subgroups, etc.

            return spriteRequirementsCollection.GetUsableDungeonEnemySprites(optionFlags?.EnemiesAbsorbable == false).Where(x => x.SpriteInGroup(this) && x.CanSpawnInRoom(room));
        }

        public IEnumerable<SpriteRequirement> GetPossibleEnemySprites(OverworldArea area, OptionFlags optionFlags = null)
        {
            // TODO: add more logic to this?
            // needs to check for two subgroups, etc.

            return spriteRequirementsCollection.GetUsableOverworldEnemySprites(optionFlags?.EnemiesAbsorbable == false).Where(x => x.SpriteInGroup(this));
        }
    }
}
