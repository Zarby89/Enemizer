using EnemizerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class RoomRequirementTests
    {
        readonly ITestOutputHelper output;

        public RoomRequirementTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void check_rooms_are_only_in_one_group_except82and83()
        {
            // Ugly test but whatever works.
            List<R> rooms = GetRoomList();

            foreach (var r in rooms)
            {
                bool duplicate = r.GroupId.Distinct().Count() > 1
                    || r.Subgroup0.Distinct().Count() > 1
                    || r.Subgroup1.Distinct().Count() > 1
                    || r.Subgroup2.Distinct().Count() > 1
                    || (r.Subgroup3.Distinct().Count() > 1
                        && (r.Subgroup3.Distinct().Count(x => x.HasValue && x.Value != 82 && x.Value != 83) > 1)
                        || (r.Subgroup3.Distinct().Count(x => x.HasValue && x.Value != 82 && x.Value != 83) == 1
                            && r.Subgroup3.Distinct().Count(x => x.Value == 82 || x.Value == 83) > 0));

                if (duplicate)
                {
                    output.WriteLine($"duplicates for room: {r.RoomId}");
                }
                Assert.False(duplicate);
            }
        }

        List<R> GetRoomList()
        {
            RoomGroupRequirementCollection reqCollection = new RoomGroupRequirementCollection();

            var rooms = new List<R>();

            foreach (var req in reqCollection.RoomRequirements)
            {
                req.Rooms.ForEach((roomId) =>
                {
                    var r = rooms.Find(x => x.RoomId == roomId);
                    if (r == null)
                    {
                        r = new R() { RoomId = roomId };
                        rooms.Add(r);
                    }
                    if (req.GroupId != null)
                    {
                        r.GroupId.Add(req.GroupId);
                    }
                    if (req.Subgroup0 != null)
                    {
                        r.Subgroup0.Add(req.Subgroup0);
                    }
                    if (req.Subgroup1 != null)
                    {
                        r.Subgroup1.Add(req.Subgroup1);
                    }
                    if (req.Subgroup2 != null)
                    {
                        r.Subgroup2.Add(req.Subgroup2);
                    }
                    if (req.Subgroup3 != null)
                    {
                        r.Subgroup3.Add(req.Subgroup3);
                    }
                });
            }

            return rooms;
        }

        [Fact]
        public void check_room_82_only_groups()
        {
            List<R> rooms = GetRoomList();

            foreach (var r in rooms.Where(x => Needs82OnlyRooms.Contains(x.RoomId)))
            {
                bool duplicate = r.Subgroup3.Distinct().Count(x => x.HasValue) > 1;

                if (duplicate)
                {
                    output.WriteLine($"Group 82 Only Room: {r.RoomId} has non-82 also");
                }
                Assert.False(duplicate);
            }
        }

        [Fact]
        public void check_room_83_only_groups()
        {
            List<R> rooms = GetRoomList();

            foreach (var r in rooms.Where(x => Needs83OnlyRooms.Contains(x.RoomId)))
            {
                bool duplicate = r.Subgroup3.Distinct().Count(x => x.HasValue) > 1;

                if (duplicate)
                {
                    output.WriteLine($"Group 83 Only Room: {r.RoomId} has non-83 also");
                }
                Assert.False(duplicate);
            }
        }

        public int[] Needs82OnlyRooms =
        {
            RoomIdConstants.R2_HyruleCastle_SwitchRoom,
            RoomIdConstants.R26_PalaceofDarkness_BigChestRoom,
            RoomIdConstants.R61_GanonsTower_TorchRoom2,
            RoomIdConstants.R68_ThievesTown_BigChestRoom,
            RoomIdConstants.R86_SkullWoods_KeyPot_TrapRoom,
            RoomIdConstants.R88_SkullWoods_BigChestRoom,
            RoomIdConstants.R94_IcePalace_LonelyFirebar,
            RoomIdConstants.R100_ThievesTown_WestAtticRoom,
            RoomIdConstants.R124_GanonsTower_EastSideCollapsingBridge_ExplodingWallRoom,
            RoomIdConstants.R140_GanonsTower_EastandWestDownstairs_BigChestRoom,
            RoomIdConstants.R149_GanonsTower_FinalCollapsingBridgeRoom,
            RoomIdConstants.R195_MiseryMire_BigChestRoom,
            RoomIdConstants.R267_SwampFloodwayRoom
        };

        public int[] Needs83OnlyRooms =
        {
            RoomIdConstants.R4_TurtleRock_CrystalRollerRoom,
            RoomIdConstants.R53_SwampPalace_BigKey_BSRoom,
            RoomIdConstants.R55_SwampPalace_MapChest_WaterFillRoom,
            RoomIdConstants.R63_IcePalace_MapChestRoom,
            RoomIdConstants.R118_SwampPalace_WaterDrainRoom,
            RoomIdConstants.R206_IcePalace_HoletoKholdstareRoom
        };

        class R
        {
            public int RoomId { get; set; }
            public List<int?> GroupId { get; set; } = new List<int?>();
            public List<int?> Subgroup0 { get; set; } = new List<int?>();
            public List<int?> Subgroup1 { get; set; } = new List<int?>();
            public List<int?> Subgroup2 { get; set; } = new List<int?>();
            public List<int?> Subgroup3 { get; set; } = new List<int?>();
        }
    }
}
