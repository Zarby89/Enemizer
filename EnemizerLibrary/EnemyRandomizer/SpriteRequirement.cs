using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SpriteRequirement
    {
        public int SpriteId { get; set; }
        public bool Overlord { get; set; }
        public int? GroupId { get; set; }
        public int? SubGroup0 { get; set; }
        public int? SubGroup1 { get; set; }
        public int? SubGroup2 { get; set; }
        public int? SubGroup3 { get; set; }

        public SpriteRequirement(int SpriteId, bool Overlord, int? GroupId, int? SubGroup0, int? SubGroup1, int? SubGroup2, int? SubGroup3)
        {
            this.SpriteId = SpriteId;
            this.Overlord = Overlord;
            this.GroupId = GroupId;
            this.SubGroup0 = SubGroup0;
            this.SubGroup1 = SubGroup1;
            this.SubGroup2 = SubGroup2;
            this.SubGroup3 = SubGroup3;
        }
    }

    public class SpriteRequirementCollection
    {
        public List<SpriteRequirement> SpriteRequirements { get; set; }

        public SpriteRequirementCollection()
        {
            SpriteRequirements = new List<SpriteRequirement>();
            
            // rat-guard = green recruit (0x4B) with sub 1=73, sub 2=28


            //SpriteRequirements.Add(new SpriteRequirement());
        }
    }
}
