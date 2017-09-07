using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldGroupRequirementCollection
    {
        public List<OverworldGroupRequirement> OverworldRequirements { get; set; }

        public OverworldGroupRequirementCollection()
        {
            OverworldRequirements = new List<OverworldGroupRequirement>();

            FillOverworldRequirements();
        }

        void FillOverworldRequirements()
        {
            OverworldRequirements.Add(new OverworldGroupRequirement(7, null, null, null, null, 0x02));

            OverworldRequirements.Add(new OverworldGroupRequirement(16, null, null, null, null, 0x03));

            OverworldRequirements.Add(new OverworldGroupRequirement(4, null, null, null, null, 0x0F));

            OverworldRequirements.Add(new OverworldGroupRequirement(3, null, null, null, null, 0x14));

            OverworldRequirements.Add(new OverworldGroupRequirement(1, null, null, null, null, 0x1B));

            OverworldRequirements.Add(new OverworldGroupRequirement(6, null, null, null, null, 0x22, 0x28));

            OverworldRequirements.Add(new OverworldGroupRequirement(9, null, null, null, null, 0x30));

            OverworldRequirements.Add(new OverworldGroupRequirement(10, null, null, null, null, 0x3A));

            OverworldRequirements.Add(new OverworldGroupRequirement(22, null, null, null, null, 0x4F));

            OverworldRequirements.Add(new OverworldGroupRequirement(21, null, null, null, null, 0x62, 0x69));

            OverworldRequirements.Add(new OverworldGroupRequirement(27, null, null, null, null, 0x68));

        }
    }
}
