using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SpriteGroup
    {
        public byte GroupId { get; set; }
        public SpriteSubGroup SubGroup0 { get; set; }
        public SpriteSubGroup SubGroup1 { get; set; }
        public SpriteSubGroup SubGroup2 { get; set; }
        public SpriteSubGroup SubGroup3 { get; set; }
    }

    public class SpriteSubGroup
    {
        public byte SubGroupId { get; set; }
        public List<Sprite> PossibleSprites { get; set; }

        public SpriteSubGroup(byte subGroupId)
        {
            this.SubGroupId = subGroupId;
        }
    }

    public class SpriteSubGroupCollections
    {

    }


    // TODO: this probably isn't needed
    // can probably just be an collection of bytes and will then be used to index into all subgroup collection with actual info
    public class PossibleSpriteSubGroupCollection
    {
        public List<SpriteSubGroup> PossibleSubGroup0 { get; set; }
        public List<SpriteSubGroup> PossibleSubGroup1 { get; set; }
        public List<SpriteSubGroup> PossibleSubGroup2 { get; set; }
        public List<SpriteSubGroup> PossibleSubGroup3 { get; set; }

        public PossibleSpriteSubGroupCollection()
        {
            PossibleSubGroup0 = new List<SpriteSubGroup>();
            PossibleSubGroup1 = new List<SpriteSubGroup>();
            PossibleSubGroup2 = new List<SpriteSubGroup>();
            PossibleSubGroup3 = new List<SpriteSubGroup>();

            PossibleSubGroup0.Add(new SpriteSubGroup(0x0E)); // 14
            PossibleSubGroup0.Add(new SpriteSubGroup(0x16)); // 22
            PossibleSubGroup0.Add(new SpriteSubGroup(0x1F)); // 31
            PossibleSubGroup0.Add(new SpriteSubGroup(0x2F)); // 47

            PossibleSubGroup1.Add(new SpriteSubGroup(0x1E)); // 30
            PossibleSubGroup1.Add(new SpriteSubGroup(0x20)); // 32
            PossibleSubGroup1.Add(new SpriteSubGroup(0x2C)); // 44

            PossibleSubGroup2.Add(new SpriteSubGroup(0x0C)); // 12
            PossibleSubGroup2.Add(new SpriteSubGroup(0x12)); // 18
            PossibleSubGroup2.Add(new SpriteSubGroup(0x17)); // 23
            PossibleSubGroup2.Add(new SpriteSubGroup(0x18)); // 24
            PossibleSubGroup2.Add(new SpriteSubGroup(0x1C)); // 28
            PossibleSubGroup2.Add(new SpriteSubGroup(0x22)); // 34
            PossibleSubGroup2.Add(new SpriteSubGroup(0x23)); // 35
            PossibleSubGroup2.Add(new SpriteSubGroup(0x24)); // 36
            PossibleSubGroup2.Add(new SpriteSubGroup(0x25)); // 37
            PossibleSubGroup2.Add(new SpriteSubGroup(0x26)); // 38
            PossibleSubGroup2.Add(new SpriteSubGroup(0x27)); // 39
            PossibleSubGroup2.Add(new SpriteSubGroup(0x28)); // 40
            PossibleSubGroup2.Add(new SpriteSubGroup(0x29)); // 41
            PossibleSubGroup2.Add(new SpriteSubGroup(0x2A)); // 42
            PossibleSubGroup2.Add(new SpriteSubGroup(0x2E)); // 46

            PossibleSubGroup3.Add(new SpriteSubGroup(0x10)); // 16
            PossibleSubGroup3.Add(new SpriteSubGroup(0x11)); // 17
            PossibleSubGroup3.Add(new SpriteSubGroup(0x14)); // 20
            PossibleSubGroup3.Add(new SpriteSubGroup(0x1B)); // 27
            PossibleSubGroup3.Add(new SpriteSubGroup(0x52)); // 82
            PossibleSubGroup3.Add(new SpriteSubGroup(0x53)); // 83
        }
    }
}
