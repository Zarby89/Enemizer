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
            OverworldRequirements.Add(new OverworldGroupRequirement(7, null, null, null, null, OverworldAreaConstants.A002_LumberJackHouse));

            OverworldRequirements.Add(new OverworldGroupRequirement(16, null, null, null, null, OverworldAreaConstants.A003_WestDeathMountain));

            OverworldRequirements.Add(new OverworldGroupRequirement(4, null, null, null, null, OverworldAreaConstants.A00F_EntrancetoZorasDomain));

            OverworldRequirements.Add(new OverworldGroupRequirement(3, null, null, null, null, OverworldAreaConstants.A014_Graveyard));

            OverworldRequirements.Add(new OverworldGroupRequirement(1, null, null, null, null, OverworldAreaConstants.A01B_HyruleCastle));

            OverworldRequirements.Add(new OverworldGroupRequirement(6, null, null, null, null, OverworldAreaConstants.A022_Smithy, 
                                                                                               OverworldAreaConstants.A028_KakarikoVillageMazeRace));

            OverworldRequirements.Add(new OverworldGroupRequirement(9, null, null, null, null, OverworldAreaConstants.A030_DesertofMystery));

            OverworldRequirements.Add(new OverworldGroupRequirement(10, null, null, null, null, OverworldAreaConstants.A03A_PathBetweenDesertofMysteryandGreatSwamp));

            OverworldRequirements.Add(new OverworldGroupRequirement(22, null, null, null, null, OverworldAreaConstants.A04F_Catfish_DW));

            OverworldRequirements.Add(new OverworldGroupRequirement(21, null, null, null, null, OverworldAreaConstants.A062_Smithy_DW, 
                                                                                                OverworldAreaConstants.A069_VillageofOutcastsFrogSmith_DW));

            OverworldRequirements.Add(new OverworldGroupRequirement(27, null, null, null, null, OverworldAreaConstants.A068_DiggingGame_DW));

            OverworldRequirements.Add(new OverworldGroupRequirement(13, null, null, 76, null, OverworldAreaConstants.A016_WitchsHut));
            OverworldRequirements.Add(new OverworldGroupRequirement(29, null, 77, null, 21, OverworldAreaConstants.A069_VillageofOutcastsFrogSmith_DW));
        }
    }
}
