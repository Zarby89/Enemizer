using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SpriteGroup
    {
        //const int SpriteGroupBaseAddress = 0x5B57; // wtf is this address?
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
        }

        internal void UpdateRom()
        {
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 0] = (byte)SubGroup0;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 1] = (byte)SubGroup1;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 2] = (byte)SubGroup2;
            romData[SpriteGroupBaseAddress + (GroupId * 4) + 3] = (byte)SubGroup3;
        }
    }

    public class SpriteGroupCollection
    {
        public List<SpriteGroup> SpriteGroups { get; set; }
        public IEnumerable<SpriteGroup> UsableDungeonSpriteGroups
        {
            get
            {
                return SpriteGroups
                    .Where(x => DoNotRandomizeDungeonGroupIds.Contains(x.DungeonGroupId) == false)
                    .Where(x => x.DungeonGroupId > 0 && x.DungeonGroupId < 60);
            }
        }
        RomData romData { get; set; }
        Random rand { get; set; }
        public SpriteGroupCollection(RomData romData, Random rand)
        {
            this.romData = romData;
            this.rand = rand;
            SpriteGroups = new List<SpriteGroup>();
        }

        public void LoadSpriteGroups()
        {
            for(int i=0;i<144; i++)
            {
                SpriteGroup sg = new SpriteGroup(romData, i);
                SpriteGroups.Add(sg);
            }

            SetDefaultPreservationFlags();
        }

        void SetDefaultPreservationFlags()
        {
            // dungeon sprite groups = 60 total. 
            foreach (var sg in SpriteGroups)
            {
                if (sg.DungeonGroupId < 0 || sg.DungeonGroupId > 60)
                {
                    continue;
                }

                if (DoNotRandomizeDungeonGroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup0 = sg.PreserveSubGroup1 = sg.PreserveSubGroup2 = sg.PreserveSubGroup3 = true;
                    continue;
                }

                if (PreserveDungeonSubGroup0GroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup0 = true;
                }
                if (PreserveDungeonSubGroup1GroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup1 = true;
                }
                if (PreserveDungeonSubGroup2GroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup2 = true;
                }
                if (PreserveDungeonSubGroup3GroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup3 = true;
                }
            }
        }

        public void UpdateRom()
        {
            SpriteGroups.ForEach(x => x.UpdateRom());
        }

        public void RandomizeGroups()
        {
            // dungeon sprite groups = 60 total. 
            foreach (var sg in SpriteGroups)
            {
                if (sg.DungeonGroupId < 0 || sg.DungeonGroupId > 60)
                {
                    continue;
                }

                if(DoNotRandomizeDungeonGroupIds.Contains(sg.DungeonGroupId))
                {
                    continue;
                }

                if (SetGuardSubset1GroupIds.Contains(sg.DungeonGroupId))
                {
                    sg.PreserveSubGroup1 = true;
                    sg.SubGroup1 = GetRandomSubset1ForGuards();
                }

                if(sg.PreserveSubGroup0 == false)
                {
                    sg.SubGroup0 = GetRandomSubgroup0();
                }
                if (sg.PreserveSubGroup1 == false)
                {
                    sg.SubGroup1 = GetRandomSubgroup1();
                }
                if (sg.PreserveSubGroup2 == false)
                {
                    sg.SubGroup2 = GetRandomSubgroup2();
                }
                if (sg.PreserveSubGroup3 == false)
                {
                    sg.SubGroup3 = GetRandomSubgroup3();
                }

                FixPairedGroups(sg);
            }
        }

        private void FixPairedGroups(SpriteGroup sg)
        {
            // TODO: add any others
            // TODO: double check these
            if (sg.SubGroup0 == 22)
            {
                sg.SubGroup2 = 23;
                sg.PreserveSubGroup2 = true;
            }
            if(sg.SubGroup2 == 23)
            {
                sg.SubGroup0 = 22;
                sg.PreserveSubGroup0 = true;
            }
            if (sg.SubGroup0 == 70 || sg.SubGroup0 == 72)
            {
                sg.SubGroup1 = 73;
                sg.SubGroup2 = 19;
                sg.PreserveSubGroup1 = sg.PreserveSubGroup2 = true;
            }
            if(sg.SubGroup1 == 73 || sg.SubGroup1 == 13 || sg.SubGroup2 == 19)
            {
                sg.SubGroup0 = 70;
                sg.SubGroup1 = (sg.SubGroup1 == 73 || sg.SubGroup1 == 13) ? sg.SubGroup1 : 73;
                sg.SubGroup2 = 19;
                sg.PreserveSubGroup0 = sg.PreserveSubGroup1 = sg.PreserveSubGroup2 = true;
            }
        }

        byte GetRandomSubset1ForGuards()
        {
            //return 13;
            int i = rand.Next(2);
            if (i == 0) { i = 73; }
            if (i == 1) { i = 13; }
            if (i == 2) { i = 13; } // TODO: zarby this will never get called
            return (byte)i;
        }

        int GetRandomSubgroup0()
        {
            return PotentialSubset0[rand.Next(PotentialSubset0.Length)];
        }
        int GetRandomSubgroup1()
        {
            return PotentialSubset1[rand.Next(PotentialSubset1.Length)];
        }
        int GetRandomSubgroup2()
        {
            return PotentialSubset2[rand.Next(PotentialSubset2.Length)];
        }
        int GetRandomSubgroup3()
        {
            return PotentialSubset3[rand.Next(PotentialSubset3.Length)];
        }


        int[] DoNotRandomizeDungeonGroupIds = { 0, 5, 6, 7, 9, 11, 12, 14, 15, 16, 18, 20, 21, 22, 23, 24, 26, 32, 34, 35 };
        int[] PreserveDungeonSubGroup0GroupIds = { 1, 2, 3, 4, 13, 27, 29, 30 };
        int[] PreserveDungeonSubGroup1GroupIds = { 17, 31 };
        int[] PreserveDungeonSubGroup2GroupIds = { 1, 2, 3, 4, 8, 10, 19, 28, 29, 31, 33, 38, 39, 41 };
        int[] PreserveDungeonSubGroup3GroupIds = { 2, 4, 36, 37, 38, 39 };
        int[] SetGuardSubset1GroupIds = { 1, 2, 3, 4 };

        byte[] PotentialSubset0 = { 22, 31, 47, 14 }; //70-72 part of guards we already have 4 guard set don't need more
        byte[] PotentialSubset1 = { 44, 30, 32 };//73-13
        byte[] PotentialSubset2 = { 12, 18, 23, 24, 28, 46, 34, 35, 39, 40, 38, 41, 36, 37, 42 };//19 trainee guard
        byte[] PotentialSubset3 = { 17, 16, 27, 20, 82, 83 };
    }
}
