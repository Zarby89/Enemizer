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
            RoomGroupRequirementCollection reqCollection = new RoomGroupRequirementCollection();

            var rooms = new List<R>();

            foreach(var req in reqCollection.RoomRequirements)
            {
                req.Rooms.ForEach((roomId) =>
                {
                    var r = rooms.Find(x => x.RoomId == roomId);
                    if(r == null)
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
                    if(req.Subgroup2 != null)
                    {
                        r.Subgroup2.Add(req.Subgroup2);
                    }
                    if (req.Subgroup3 != null)
                    {
                        r.Subgroup3.Add(req.Subgroup3);
                    }
                });
            }

            foreach(var r in rooms)
            {
                bool duplicate = r.GroupId.Count > 1
                    || r.Subgroup0.Count > 1
                    || r.Subgroup1.Count > 1
                    || r.Subgroup2.Count > 1
                    || (r.Subgroup3.Count > 1 
                        && (r.Subgroup3.Count(x => x.HasValue && x.Value != 82 && x.Value != 83) > 1)
                        || (r.Subgroup3.Count(x => x.HasValue && x.Value != 82 && x.Value != 83) == 1
                            && r.Subgroup3.Count(x => x.Value == 82 || x.Value == 83) > 0));

                if(duplicate)
                {
                    output.WriteLine($"duplicates for room: {r.RoomId}");
                }
                Assert.Equal(false, duplicate);
            }
        }

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
