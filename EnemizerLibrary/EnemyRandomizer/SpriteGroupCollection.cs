using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary
{
    public class SpriteGroupCollection
    {
        List<SpriteGroup> spriteGroups = new List<SpriteGroup>();
        public List<SpriteGroup> SpriteGroups
        {
            get
            {
                if (!Loaded)
                {
                    throw new Exception("SpriteGroupCollection - Tried to access SpriteGroups without loading them");
                }
                return this.spriteGroups;
            }
            set
            {
                this.spriteGroups = value;
            }
        }

        public IEnumerable<SpriteGroup> UsableOverworldSpriteGroups
        {
            get
            {
                // TODO: what should be the max
                // let's assume max of 64 because that is where dungeons start
                // but HM lets you go up to 79....
                return SpriteGroups
                    .Where(x => x.GroupId > 0 && x.GroupId < 0x40);
            }
        }

        public IEnumerable<SpriteGroup> UsableDungeonSpriteGroups
        {
            get
            {
                return DungeonSpriteGroups;
                    //.Where(x => DoNotUseForDungeonGroupIds.Contains(x.DungeonGroupId) == false);
            }
        }

        IEnumerable<SpriteGroup> DungeonSpriteGroups
        {
            get
            {
                return SpriteGroups.Where(x => x.DungeonGroupId > 0 && x.DungeonGroupId < 60);
            }
        }

        public IEnumerable<SpriteGroup> GetPossibleDungeonSpriteGroups(Room room, bool needsKey = false, bool needsKillable = false, List<SpriteRequirement> doNotUpdateSprites = null)
        {
            var req = dungeonReqs.GetGroupRequirementForRoom(room);

            if (needsKey == false && needsKillable == false && (doNotUpdateSprites == null || doNotUpdateSprites.Count == 0)
                && req.GroupId.Count == 0
                && req.SubGroup0.Count == 0 && req.SubGroup1.Count == 0 && req.SubGroup2.Count == 0 && req.SubGroup3.Count == 0)
            {
                return UsableDungeonSpriteGroups;
            }

            //var usableGroups = spriteRequirementsCollection.SpriteRequirements.Where(x => x.GroupId)
            var killableGroupIds = spriteRequirementsCollection.KillableSprites.SelectMany(x => x.GroupId).ToList();
            var killableSub0Ids = spriteRequirementsCollection.KillableSprites.SelectMany(x => x.SubGroup0).ToList();
            var killableSub1Ids = spriteRequirementsCollection.KillableSprites.SelectMany(x => x.SubGroup1).ToList();
            var killableSub2Ids = spriteRequirementsCollection.KillableSprites.SelectMany(x => x.SubGroup2).ToList();
            var killableSub3Ids = spriteRequirementsCollection.KillableSprites.SelectMany(x => x.SubGroup3).ToList();

            var keysGroupIds = spriteRequirementsCollection.KillableSprites.Where(x => x.CannotHaveKey == false).SelectMany(x => x.GroupId).ToList();
            var keysSub0Ids = spriteRequirementsCollection.KillableSprites.Where(x => x.CannotHaveKey == false).SelectMany(x => x.SubGroup0).ToList();
            var keysSub1Ids = spriteRequirementsCollection.KillableSprites.Where(x => x.CannotHaveKey == false).SelectMany(x => x.SubGroup1).ToList();
            var keysSub2Ids = spriteRequirementsCollection.KillableSprites.Where(x => x.CannotHaveKey == false).SelectMany(x => x.SubGroup2).ToList();
            var keysSub3Ids = spriteRequirementsCollection.KillableSprites.Where(x => x.CannotHaveKey == false).SelectMany(x => x.SubGroup3).ToList();

            // TODO: gotta be a better way to do this
            var doNotUpdateGroupIds = doNotUpdateSprites.SelectMany(x => x.GroupId).ToList();
            var doNotUpdateSub0Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup0).ToList();
            var doNotUpdateSub1Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup1).ToList();
            var doNotUpdateSub2Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup2).ToList();
            var doNotUpdateSub3Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup3).ToList();

            return UsableDungeonSpriteGroups
                .Where(x => doNotUpdateGroupIds.Count == 0 || doNotUpdateGroupIds.Contains((byte)x.GroupId))
                .Where(x => doNotUpdateSub0Ids.Count == 0 || doNotUpdateSub0Ids.Contains((byte)x.SubGroup0))
                .Where(x => doNotUpdateSub1Ids.Count == 0 || doNotUpdateSub1Ids.Contains((byte)x.SubGroup1))
                .Where(x => doNotUpdateSub2Ids.Count == 0 || doNotUpdateSub2Ids.Contains((byte)x.SubGroup2))
                .Where(x => doNotUpdateSub3Ids.Count == 0 || doNotUpdateSub3Ids.Contains((byte)x.SubGroup3))
                .Where(x => req.GroupId.Count == 0 || req.GroupId.Contains((byte)x.DungeonGroupId))
                .Where(x => req.SubGroup0.Count == 0 || req.SubGroup0.Contains((byte)x.SubGroup0))
                .Where(x => req.SubGroup1.Count == 0 || req.SubGroup1.Contains((byte)x.SubGroup1))
                .Where(x => req.SubGroup2.Count == 0 || req.SubGroup2.Contains((byte)x.SubGroup2))
                .Where(x => req.SubGroup3.Count == 0 || req.SubGroup3.Contains((byte)x.SubGroup3))
                .Where(x => needsKillable == false 
                            || killableGroupIds.Contains((byte)x.GroupId)
                            || killableSub0Ids.Contains((byte)x.SubGroup0)
                            || killableSub1Ids.Contains((byte)x.SubGroup1)
                            || killableSub2Ids.Contains((byte)x.SubGroup2)
                            || killableSub3Ids.Contains((byte)x.SubGroup3)
                            )
                .Where(x => needsKey == false
                            || keysGroupIds.Contains((byte)x.GroupId)
                            || keysSub0Ids.Contains((byte)x.SubGroup0)
                            || keysSub1Ids.Contains((byte)x.SubGroup1)
                            || keysSub2Ids.Contains((byte)x.SubGroup2)
                            || keysSub3Ids.Contains((byte)x.SubGroup3)
                            )
                ;
        }

        public IEnumerable<SpriteGroup> GetPossibleOverworldSpriteGroups(List<SpriteRequirement> doNotUpdateSprites = null)
        {
            if (doNotUpdateSprites == null || doNotUpdateSprites.Count == 0)
            {
                return UsableOverworldSpriteGroups;
            }

            // TODO: gotta be a better way to do this
            var doNotUpdateGroupIds = doNotUpdateSprites.SelectMany(x => x.GroupId).ToList();
            var doNotUpdateSub0Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup0).ToList();
            var doNotUpdateSub1Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup1).ToList();
            var doNotUpdateSub2Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup2).ToList();
            var doNotUpdateSub3Ids = doNotUpdateSprites.SelectMany(x => x.SubGroup3).ToList();

            return UsableOverworldSpriteGroups
                .Where(x => doNotUpdateGroupIds.Count == 0 || doNotUpdateGroupIds.Contains((byte)x.GroupId))
                .Where(x => doNotUpdateSub0Ids.Count == 0 || doNotUpdateSub0Ids.Contains((byte)x.SubGroup0))
                .Where(x => doNotUpdateSub1Ids.Count == 0 || doNotUpdateSub1Ids.Contains((byte)x.SubGroup1))
                .Where(x => doNotUpdateSub2Ids.Count == 0 || doNotUpdateSub2Ids.Contains((byte)x.SubGroup2))
                .Where(x => doNotUpdateSub3Ids.Count == 0 || doNotUpdateSub3Ids.Contains((byte)x.SubGroup3))
                ;
        }

        RomData romData { get; set; }
        Random rand { get; set; }
        RoomGroupRequirementCollection dungeonReqs;
        SpriteRequirementCollection spriteRequirementsCollection { get; set; }
        public bool Loaded { get; private set; }

        public SpriteGroupCollection(RomData romData, Random rand, SpriteRequirementCollection spriteRequirementsCollection)
        {
            this.romData = romData;
            this.rand = rand;
            this.spriteRequirementsCollection = spriteRequirementsCollection;

            dungeonReqs = new RoomGroupRequirementCollection();

            SpriteGroups = new List<SpriteGroup>();
        }

        public void LoadSpriteGroups()
        {
            Loaded = true;

            for(int i=0;i<144; i++)
            {
                SpriteGroup sg = new SpriteGroup(romData, spriteRequirementsCollection, i);

                SpriteGroups.Add(sg);
            }

            SetupRequiredOverworldGroups();

            SetupRequiredDungeonGroups();
        }

        void SetupRequiredOverworldGroups()
        {
            OverworldGroupRequirementCollection owReqs = new OverworldGroupRequirementCollection();

            var rGroup = owReqs.OverworldRequirements.Where(x => x.GroupId != null);
            foreach(var g in rGroup)
            {
                UsableOverworldSpriteGroups.Where(x => g.GroupId == x.GroupId).ToList()
                    .ForEach((x) =>
                    {
                        if(g.Subgroup0 == null && g.Subgroup1 == null && g.Subgroup2 == null && g.Subgroup3 == null)
                        {
                            // lazy
                            x.PreserveSubGroup0 = true;
                            x.PreserveSubGroup1 = true;
                            x.PreserveSubGroup2 = true;
                            x.PreserveSubGroup3 = true;
                        }

                        if (g.Subgroup0 != null)
                        {
                            x.SubGroup0 = (int)(g.Subgroup0 ?? x.SubGroup0);
                            x.PreserveSubGroup0 = true;
                        }
                        if (g.Subgroup1 != null)
                        {
                            x.SubGroup1 = (int)(g.Subgroup1 ?? x.SubGroup1);
                            x.PreserveSubGroup1 = true;
                        }
                        if (g.Subgroup2 != null)
                        {
                            x.SubGroup2 = (int)(g.Subgroup2 ?? x.SubGroup2);
                            x.PreserveSubGroup2 = true;
                        }
                        if (g.Subgroup3 != null)
                        {
                            x.SubGroup3 = (int)(g.Subgroup3 ?? x.SubGroup3);
                            x.PreserveSubGroup3 = true;
                        }
                        //if (g.GroupId != null)
                        //{
                        //    x.ForceRoomsToGroup.AddRange(g.Rooms);
                        //}
                    });
            }
        }

        void SetupRequiredDungeonGroups()
        {
            dungeonReqs = new RoomGroupRequirementCollection();

            var rGroup = dungeonReqs.RoomRequirements.Where(x => x.GroupId != null);
            foreach(var g in rGroup)
            {
                DungeonSpriteGroups.Where(x => g.GroupId == x.DungeonGroupId).ToList()
                    .ForEach((x) => {
                        if(g.Subgroup0 != null)
                        {
                            x.SubGroup0 = (int)g.Subgroup0;
                            x.PreserveSubGroup0 = true;
                        }
                        if (g.Subgroup1 != null)
                        {
                            x.SubGroup1 = (int)g.Subgroup1;
                            x.PreserveSubGroup1 = true;
                        }
                        if (g.Subgroup2 != null)
                        {
                            x.SubGroup2 = (int)g.Subgroup2;
                            x.PreserveSubGroup2 = true;
                        }
                        if (g.Subgroup3 != null)
                        {
                            x.SubGroup3 = (int)g.Subgroup3;
                            x.PreserveSubGroup3 = true;
                        }
                        if (g.GroupId != null)
                        {
                            x.ForceRoomsToGroup.AddRange(g.Rooms);
                        }
                    }
                );
            }

            var duplicateRooms = dungeonReqs.RoomRequirements.Where(x => x.GroupId == null)
                .SelectMany(dr => dr.Rooms, (dr, r) => new RoomReq() { RoomId = r, Sub0 = dr.Subgroup0, Sub1 = dr.Subgroup1, Sub2 = dr.Subgroup2, Sub3 = dr.Subgroup3 })
                .ToList();

            var roomsDict = new Dictionary<int, RoomReq>();

            foreach(var d in duplicateRooms)
            {
                if(!roomsDict.ContainsKey(d.RoomId))
                {
                    roomsDict[d.RoomId] = d;
                }

                if (d.Sub0 != null)
                {
                    roomsDict[d.RoomId].Sub0 = d.Sub0;
                }
                if (d.Sub1 != null)
                {
                    roomsDict[d.RoomId].Sub1 = d.Sub1;
                }
                if (d.Sub2 != null)
                {
                    roomsDict[d.RoomId].Sub2 = d.Sub2;
                }
                if (d.Sub3 != null)
                {
                    roomsDict[d.RoomId].Sub3 = d.Sub3;
                }
            }

            var rooms = roomsDict.Values.ToList();

            foreach(var r in rooms)
            {
                // UGGGGGGGGGGGGGGGGGGGGGGG
                // TODO: check if we already saved one for another room and skip this room
                if(DungeonSpriteGroups
                    .Where(x => r.Sub0 == null || (x.SubGroup0 == r.Sub0 && x.PreserveSubGroup0 == true))
                    .Where(x => r.Sub1 == null || (x.SubGroup1 == r.Sub1 && x.PreserveSubGroup1 == true))
                    .Where(x => r.Sub2 == null || (x.SubGroup2 == r.Sub2 && x.PreserveSubGroup2 == true))
                    .Where(x => r.Sub3 == null || (x.SubGroup3 == r.Sub3 && x.PreserveSubGroup3 == true))
                    .Any())
                {
                    // skip this room because we already have a subgroup that will work
                    continue;
                }


                var possibleSubs = DungeonSpriteGroups.Where(y => (y.PreserveSubGroup0 == false)
                                                                    || y.PreserveSubGroup1 == false
                                                                    || y.PreserveSubGroup2 == false
                                                                    || y.PreserveSubGroup3 == false)
                                                    .ToList();

                if(r.Sub0 != null)
                {
                    possibleSubs = possibleSubs.Where(x => x.PreserveSubGroup0 == false).ToList();
                }
                if (r.Sub1 != null)
                {
                    possibleSubs = possibleSubs.Where(x => x.PreserveSubGroup1 == false).ToList();
                }
                if (r.Sub2 != null)
                {
                    possibleSubs = possibleSubs.Where(x => x.PreserveSubGroup2 == false).ToList();
                }
                if (r.Sub3 != null)
                {
                    possibleSubs = possibleSubs.Where(x => x.PreserveSubGroup3 == false).ToList();
                }

                var updateSub = possibleSubs[rand.Next(possibleSubs.Count)];

                if (r.Sub0 != null)
                {
                    updateSub.SubGroup0 = (int)r.Sub0;
                    updateSub.PreserveSubGroup0 = true;
                }
                if (r.Sub1 != null)
                {
                    updateSub.SubGroup1 = (int)r.Sub1;
                    updateSub.PreserveSubGroup1 = true;
                }
                if (r.Sub2 != null)
                {
                    updateSub.SubGroup2 = (int)r.Sub2;
                    updateSub.PreserveSubGroup2 = true;
                }
                if (r.Sub3 != null)
                {
                    updateSub.SubGroup3 = (int)r.Sub3;
                    updateSub.PreserveSubGroup3 = true;
                }
            }
        }

        class RoomReq
        {
            public int RoomId { get; set; }
            public int? Sub0 { get; set; }
            public int? Sub1 { get; set; }
            public int? Sub2 { get; set; }
            public int? Sub3 { get; set; }
        }

        public void UpdateRom()
        {
            SpriteGroups.ForEach(x => x.UpdateRom());
        }

        public void RandomizeDungeonGroups()
        {
            // dungeon sprite groups = 60 total. 
            foreach (var sg in UsableDungeonSpriteGroups)
            {
                if (sg.PreserveSubGroup1 == false && SetGuardSubset1GroupIds.Contains(sg.DungeonGroupId))
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

                //FixPairedGroups(sg);
            }
        }

        public void RandomizeOverworldGroups()
        {
            foreach(var sg in UsableOverworldSpriteGroups)
            {
                //if (sg.PreserveSubGroup1 == false && SetGuardSubset1GroupIds.Contains(sg.DungeonGroupId))
                //{
                //    sg.PreserveSubGroup1 = true;
                //    sg.SubGroup1 = GetRandomSubset1ForGuards();
                //}

                if (sg.PreserveSubGroup0 == false)
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


        /*
        * Don't randomize
            1,
            5,
            7,
            13,
            14, // manually set
            15,
            18,
            23,
            24,
            34,
            40
        */
        /*
        * Remake
            14 = 71, 73, 76, 80 (change rooms 18, 264, 261, 266)
        */
        
        // TODO: can probably remove this
        //int[] DoNotRandomizeDungeonGroupIds = { 1, 5, 7, 13, 14, 15, 18, 23, 24, 34, 40,
        //    9, 11, 12, 20, 21, 22, 26, 28, 32 }; // bosses. need to change to just preserve the relevant sub group

        // keep this
        int[] DoNotUseForDungeonGroupIds = { 1, 5, 7, 13, 14, 15, 18, 23, 24, 34, 40 };

        //int[] DoNotRandomizeDungeonGroupIds = { 0, 5, 6, 7, 9, 11, 12, 14, 15, 16, 18, 20, 21, 22, 23, 24, 26, 32, 34, 35 };

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

    struct ManualGroup
    {
        public int GroupId { get; set; }
        public int Subset0 { get; set; }
        public int Subset1 { get; set; }
        public int Subset2 { get; set; }
        public int Subset3 { get; set; }
        public int[] ForceRooms { get; set; }
    }
}