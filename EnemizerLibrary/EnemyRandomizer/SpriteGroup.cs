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
        public int SubGroup0 { get; set; }
        public bool PreserveSubGroup0 { get; set; }
        public int SubGroup1 { get; set; }
        public bool PreserveSubGroup1 { get; set; }
        public int SubGroup2 { get; set; }
        public bool PreserveSubGroup2 { get; set; }
        public int SubGroup3 { get; set; }
        public bool PreserveSubGroup3 { get; set; }

        public SpriteGroup(RomData romData, int groupId)
        {
            this.GroupId = groupId;

            this.SubGroup0 = romData[SpriteGroupBaseAddress + (groupId * 4) + 0];
            this.SubGroup1 = romData[SpriteGroupBaseAddress + (groupId * 4) + 1];
            this.SubGroup2 = romData[SpriteGroupBaseAddress + (groupId * 4) + 2];
            this.SubGroup3 = romData[SpriteGroupBaseAddress + (groupId * 4) + 3];
        }

        //public SpriteGroup(int groupId, int subGroup0, bool preserveGroup0, int subGroup1, bool preserveGroup1, int subGroup2, bool preserveGroup2, int subGroup3, bool preserveGroup3)
        //{
        //    this.GroupId = groupId;

        //    this.SubGroup0 = subGroup0;
        //    this.PreserveSubGroup0 = preserveGroup0;
        //    this.SubGroup1 = subGroup1;
        //    this.PreserveSubGroup1 = preserveGroup1;
        //    this.SubGroup2 = subGroup2;
        //    this.PreserveSubGroup2 = preserveGroup2;
        //    this.SubGroup3 = subGroup3;
        //    this.PreserveSubGroup3 = preserveGroup3;
        //}
    }

    public class SpriteGroupCollection
    {
        public List<SpriteGroup> SpriteGroups { get; set; }

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

            SetPreservationFlags();
        }

        public void UpdateRom()
        {

        }

        public void RandomizeGroups()
        {
            // dungeon sprites = 60 total. 
            foreach (var sg in SpriteGroups)
            {
                int dungeonGroupId = sg.GroupId - 0x40;
                if (dungeonGroupId < 0 || dungeonGroupId > 60)
                {
                    continue;
                }

                if (SetGuardSubset1GropuIds.Contains(dungeonGroupId))
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
            }
        }

        byte GetRandomSubset1ForGuards()
        {
            int i = rand.Next(2);
            if (i == 0) { i = 73; }
            if (i == 1) { i = 13; }
            if (i == 2) { i = 13; }
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

        void SetPreservationFlags()
        {
            // dungeon sprites = 60 total. 
            foreach(var sg in SpriteGroups)
            {
                int dungeonGroupId = sg.GroupId - 0x40;
                if(dungeonGroupId < 0 || dungeonGroupId > 60)
                {
                    continue;
                }

                if(DoNotRandomizeDungeonGroupIds.Contains(dungeonGroupId))
                {
                    sg.PreserveSubGroup0 = sg.PreserveSubGroup1 = sg.PreserveSubGroup2 = sg.PreserveSubGroup3 = true;
                    continue;
                }

                if(PreserveDungeonSubGroup0GroupIds.Contains(dungeonGroupId))
                {
                    sg.PreserveSubGroup0 = true;
                }
                if (PreserveDungeonSubGroup1GroupIds.Contains(dungeonGroupId))
                {
                    sg.PreserveSubGroup1 = true;
                }
                if (PreserveDungeonSubGroup2GroupIds.Contains(dungeonGroupId))
                {
                    sg.PreserveSubGroup2 = true;
                }
                if (PreserveDungeonSubGroup3GroupIds.Contains(dungeonGroupId))
                {
                    sg.PreserveSubGroup3 = true;
                }
            }
        }

        int[] DoNotRandomizeDungeonGroupIds = { 0, 5, 6, 7, 9, 11, 12, 14, 15, 16, 18, 20, 21, 22, 23, 24, 26, 32, 34, 35 };
        int[] PreserveDungeonSubGroup0GroupIds = { 1, 2, 3, 4, 13, 27, 29, 30 };
        int[] PreserveDungeonSubGroup1GroupIds = { 17, 31 };
        int[] PreserveDungeonSubGroup2GroupIds = { 1, 2, 3, 4, 8, 10, 19, 28, 29, 31, 33, 38, 39, 41 };
        int[] PreserveDungeonSubGroup3GroupIds = { 2, 4, 36, 37, 38, 39 };
        int[] SetGuardSubset1GropuIds = { 1, 2, 3, 4 };

        byte[] PotentialSubset0 = { 22, 31, 47, 14 }; //70-72 part of guards we already have 4 guard set don't need more
        byte[] PotentialSubset1 = { 44, 30, 32 };//73-13
        byte[] PotentialSubset2 = { 12, 18, 23, 24, 28, 46, 34, 35, 39, 40, 38, 41, 36, 37, 42 };//19 trainee guard
        byte[] PotentialSubset3 = { 17, 16, 27, 20, 82, 83 };

    }

    //public class SpriteGroup
    //{
    //    public byte GroupId { get; set; }
    //    public SpriteSubGroup SubGroup0 { get; set; }
    //    public SpriteSubGroup SubGroup1 { get; set; }
    //    public SpriteSubGroup SubGroup2 { get; set; }
    //    public SpriteSubGroup SubGroup3 { get; set; }
    //}

    //public class SpriteSubGroup
    //{
    //    public byte SubGroupId { get; set; }
    //    public List<Sprite> PossibleSprites { get; set; }

    //    public SpriteSubGroup(byte subGroupId)
    //    {
    //        this.SubGroupId = subGroupId;
    //    }
    //}

    //public class SpriteSubGroupCollections
    //{

    //}


    // TODO: this probably isn't needed
    // can probably just be an collection of bytes and will then be used to index into all subgroup collection with actual info
    //public class PossibleSpriteSubGroupCollection
    //{
    //    public List<SpriteSubGroup> PossibleSubGroup0 { get; set; }
    //    public List<SpriteSubGroup> PossibleSubGroup1 { get; set; }
    //    public List<SpriteSubGroup> PossibleSubGroup2 { get; set; }
    //    public List<SpriteSubGroup> PossibleSubGroup3 { get; set; }

    //    public PossibleSpriteSubGroupCollection()
    //    {
    //        PossibleSubGroup0 = new List<SpriteSubGroup>();
    //        PossibleSubGroup1 = new List<SpriteSubGroup>();
    //        PossibleSubGroup2 = new List<SpriteSubGroup>();
    //        PossibleSubGroup3 = new List<SpriteSubGroup>();

    //        PossibleSubGroup0.Add(new SpriteSubGroup(0x0E)); // 14
    //        PossibleSubGroup0.Add(new SpriteSubGroup(0x16)); // 22
    //        PossibleSubGroup0.Add(new SpriteSubGroup(0x1F)); // 31
    //        PossibleSubGroup0.Add(new SpriteSubGroup(0x2F)); // 47

    //        PossibleSubGroup1.Add(new SpriteSubGroup(0x1E)); // 30
    //        PossibleSubGroup1.Add(new SpriteSubGroup(0x20)); // 32
    //        PossibleSubGroup1.Add(new SpriteSubGroup(0x2C)); // 44

    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x0C)); // 12
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x12)); // 18
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x17)); // 23
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x18)); // 24
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x1C)); // 28
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x22)); // 34
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x23)); // 35
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x24)); // 36
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x25)); // 37
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x26)); // 38
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x27)); // 39
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x28)); // 40
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x29)); // 41
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x2A)); // 42
    //        PossibleSubGroup2.Add(new SpriteSubGroup(0x2E)); // 46

    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x10)); // 16
    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x11)); // 17
    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x14)); // 20
    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x1B)); // 27
    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x52)); // 82
    //        PossibleSubGroup3.Add(new SpriteSubGroup(0x53)); // 83
    //    }
    //}
}
