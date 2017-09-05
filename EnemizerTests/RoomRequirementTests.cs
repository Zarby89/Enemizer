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
            RoomGroupRequirementCollection reqCollection = new RoomGroupRequirementCollection();

            var rooms = new List<R>();

            foreach(var req in reqCollection.RoomRequirements.Where(x => x.Subgroup3 != 82 && x.Subgroup3 != 83))
            {
                req.Rooms.ForEach((roomId) =>
                {
                    var r = rooms.Find(x => x.RoomId == roomId);
                    if(r == null)
                    {
                        r = new R() { RoomId = roomId };
                        rooms.Add(r);
                    }
                    r.GroupId.Add(req.GroupId);
                    r.Subgroup0.Add(req.Subgroup0);
                    r.Subgroup1.Add(req.Subgroup1);
                    r.Subgroup2.Add(req.Subgroup2);
                    r.Subgroup3.Add(req.Subgroup3);
                });

                //var duplicate = req.Rooms.Any(x => reqCollection.RoomRequirements.Any(r => r.Rooms.Contains(x) 
                //                            && r != req
                //                            && (r.GroupId != req.GroupId)
                //                            && (r.Subgroup0 != req.Subgroup0 && ((r.Subgroup0 == null && req.Subgroup0 != null) || (r.Subgroup0 != null && req.Subgroup0 == null)))
                //                            && (r.Subgroup1 != req.Subgroup1)
                //                            && (r.Subgroup2 != req.Subgroup2)
                //                            && (r.Subgroup3 != req.Subgroup3)
                //                            //&& r.GroupId != null && req.GroupId != null && r.GroupId != req.GroupId
                //                            //&& r.Subgroup0 != null && req.Subgroup0 != null && r.Subgroup0 != req.Subgroup0
                //                            ));

                //if(duplicate)
                //{
                //    output.WriteLine($"req groupId: {req.GroupId}, sub0: {req.Subgroup0}, sub1: {req.Subgroup1}, sub2: {req.Subgroup2}, sub3: {req.Subgroup3}, rooms: {String.Join(",", req.Rooms)}");
                //}
                //Assert.Equal(false, duplicate);
            }

            foreach(var r in rooms)
            {
                bool duplicate = r.GroupId.Count > 1
                    || r.Subgroup0.Count > 1
                    || r.Subgroup1.Count > 1
                    || r.Subgroup2.Count > 1
                    || r.Subgroup3.Count > 1;

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
