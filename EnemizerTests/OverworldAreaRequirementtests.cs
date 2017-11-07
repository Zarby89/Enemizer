using EnemizerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace EnemizerTests
{
    public class OverworldAreaRequirementTests
    {
        readonly ITestOutputHelper output;

        public OverworldAreaRequirementTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void check_areas_are_only_in_one_group()
        {
            var areas = GetAreaList();

            foreach(var a in areas)
            {
                bool duplicate = a.GroupId.Distinct().Count() > 1;
                if (duplicate)
                {
                    output.WriteLine($"duplicates for area: {a.AreaId}");
                }

                Assert.False(duplicate);
            }

        }

        List<A> GetAreaList()
        {
            OverworldGroupRequirementCollection groupCollection = new OverworldGroupRequirementCollection();

            var areas = new List<A>();

            foreach (var req in groupCollection.OverworldRequirements)
            {
                req.Areas.ForEach((areaId) =>
                {
                    var a = areas.Find(x => x.AreaId == areaId);
                    if (a == null)
                    {
                        a = new A() { AreaId = areaId };
                        areas.Add(a);
                    }
                    if (req.GroupId != null)
                    {
                        a.GroupId.Add(req.GroupId);
                    }
                    if (req.Subgroup0 != null)
                    {
                        a.Subgroup0.Add(req.Subgroup0);
                    }
                    if (req.Subgroup1 != null)
                    {
                        a.Subgroup1.Add(req.Subgroup1);
                    }
                    if (req.Subgroup2 != null)
                    {
                        a.Subgroup2.Add(req.Subgroup2);
                    }
                    if (req.Subgroup3 != null)
                    {
                        a.Subgroup3.Add(req.Subgroup3);
                    }
                });
            }

            return areas;
        }


        class A
        {
            public int AreaId { get; set; }
            public List<int?> GroupId { get; set; } = new List<int?>();
            public List<int?> Subgroup0 { get; set; } = new List<int?>();
            public List<int?> Subgroup1 { get; set; } = new List<int?>();
            public List<int?> Subgroup2 { get; set; } = new List<int?>();
            public List<int?> Subgroup3 { get; set; } = new List<int?>();
        }
    }
}
