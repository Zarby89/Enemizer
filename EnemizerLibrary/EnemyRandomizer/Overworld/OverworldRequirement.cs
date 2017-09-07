using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldGroupRequirement
    {
        public List<int> Areas { get; set; }
        public int? GroupId { get; set; }
        public int? Subgroup0 { get; set; }
        public int? Subgroup1 { get; set; }
        public int? Subgroup2 { get; set; }
        public int? Subgroup3 { get; set; }
        public OverworldGroupRequirement(int? GroupId, int? Subgroup0, int? Subgroup1, int? Subgroup2, int? Subgroup3, params int[] Areas)
        {
            if (GroupId == null && Subgroup0 == null && Subgroup1 == null && Subgroup2 == null && Subgroup3 == null)
            {
                throw new Exception("RoomGroupRequirement needs at least one non-null GroupId or Subgroup.");
            }

            this.Areas = Areas.ToList();
            this.GroupId = GroupId;
            this.Subgroup0 = Subgroup0;
            this.Subgroup1 = Subgroup1;
            this.Subgroup2 = Subgroup2;
            this.Subgroup3 = Subgroup3;
        }

    }
}
