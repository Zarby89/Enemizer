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
            OverworldRequirements.Add(new OverworldGroupRequirement(7, null, null, 74, null, OverworldAreaConstants.A002_LumberJackHouse));

            OverworldRequirements.Add(new OverworldGroupRequirement(16, null, null, 18, 16, OverworldAreaConstants.A003_WestDeathMountain,
                                                                                            OverworldAreaConstants.A093_WestDeathMountain_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(7, null, null, null, 17, OverworldAreaConstants.A00A_EntrancetoDeathMountain,
                                                                                             OverworldAreaConstants.A09A_EntrancetoDeathMountain_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(4, null, null, null, null, OverworldAreaConstants.A00F_EntrancetoZorasDomain,
                                                                                               OverworldAreaConstants.A09F_EntrancetoZorasDomain_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(3, null, null, null, 14, OverworldAreaConstants.A014_Graveyard,
                                                                                             OverworldAreaConstants.A0A4_Graveyard_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(1, null, null, null, 29, OverworldAreaConstants.A01B_HyruleCastle,
                                                                                             OverworldAreaConstants.A0AB_HyruleCastle_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(6, null, null, null, null, OverworldAreaConstants.A022_Smithy, 
                                                                                               OverworldAreaConstants.A028_KakarikoVillageMazeRace,
                                                                                               OverworldAreaConstants.A0B2_Smithy_PostAga,
                                                                                               OverworldAreaConstants.A0B8_KakarikoVillageMazeRace_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(8, null, null, 18, null, OverworldAreaConstants.A030_DesertofMystery,
                                                                                             OverworldAreaConstants.A0C0_DesertofMystery_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(10, null, null, null, null, OverworldAreaConstants.A03A_PathBetweenDesertofMysteryandGreatSwamp,
                                                                                                OverworldAreaConstants.A0CA_PathBetweenDesertofMysteryandGreatSwamp_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(22, null, null, 24, null, OverworldAreaConstants.A04F_Catfish_DW,
                                                                                              OverworldAreaConstants.A0DF_Catfish_DW_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(21, 21, null, null, 21, OverworldAreaConstants.A062_Smithy_DW, 
                                                                                            OverworldAreaConstants.A0F2_Smithy_DW_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(27, null, 42, null, null, OverworldAreaConstants.A068_DiggingGame_DW,
                                                                                              OverworldAreaConstants.A0F8_DiggingGame_DW_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(13, null, null, 76, null, OverworldAreaConstants.A016_WitchsHut,
                                                                                              OverworldAreaConstants.A0A6_WitchsHut_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(29, null, 77, null, 21, OverworldAreaConstants.A069_VillageofOutcastsFrogSmith_DW,
                                                                                            OverworldAreaConstants.A0F9_VillageofOutcastsFrogSmith_DW_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(15, null, null, 78, null, OverworldAreaConstants.A02A_HauntedGrove,
                                                                                              OverworldAreaConstants.A0BA_HauntedGrove_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(17, null, null, null, 76, OverworldAreaConstants.A06A_HauntedGrove_DW,
                                                                                              OverworldAreaConstants.A0FA_HauntedGrove_DW_PostAga));

            // replace 55 with penguins (38)
            OverworldRequirements.Add(new OverworldGroupRequirement(12, null, null, 55, 54, OverworldAreaConstants.A080_MasterSwordGlade_UnderBridge,
                                                                                            OverworldAreaConstants.A110_MasterSwordGlade_UnderBridge_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(14, null, null, 12, 68, OverworldAreaConstants.A081_ZorasDomain, 
                                                                                            OverworldAreaConstants.A111_ZorasDomain_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(26, 15, null, null, null, OverworldAreaConstants.A092_LumberJackHouse_PostAga));

            OverworldRequirements.Add(new OverworldGroupRequirement(23, null, null, null, 25, OverworldAreaConstants.A05E_PalaceofDarkness_DW,
                                                                                             OverworldAreaConstants.A0EE_PalaceofDarkness_DW_PostAga));
            /*
        public void create_sprite_overworld_group()
        {
            for (int i = 0; i < 43; i++)
            {
                random_sprite_group_ow[i] = fully_randomize_that_group(); //group from 105 to 124 are empty
            }
            //Creations of the guards group :
            random_sprite_group_ow[0] = new byte[] { 72, get_guard_subset_1(), 19, SpriteConstants.sprite_subset_3[rand.Next(SpriteConstants.sprite_subset_3.Length)] }; //Do not randomize that group (Ending thing?)
            random_sprite_group_ow[1] = new byte[] { 70, get_guard_subset_1(), 19, 29 };
            random_sprite_group_ow[2] = new byte[] {72,73,19,29 };
            random_sprite_group_ow[3][3] = 14;
            random_sprite_group_ow[4][2] = 12;
            random_sprite_group_ow[6] = new byte[] {79,73,74,80 };
            random_sprite_group_ow[7][2] = 74;
            random_sprite_group_ow[8][2] = 18; //death montain tablet
            random_sprite_group_ow[9][2] = 18; //desert tablet and rocks
            random_sprite_group_ow[10] = new byte[] {0,73,0,17 };
            random_sprite_group_ow[14] = new byte[] { 93, 44, 12, 68 };
            random_sprite_group_ow[15] = new byte[] {0,0,78,0 };
            random_sprite_group_ow[16][2] = 18;
            random_sprite_group_ow[16][3] = 16;
            random_sprite_group_ow[21] = new byte[] {21,13,23,21 }; 
            random_sprite_group_ow[22] = new byte[] {22,13,24,25 };
            random_sprite_group_ow[27] = new byte[] {75,42,92,21 };

            random_sprite_group_ow[12] = new byte[] { 0, 0, 55, 54};

        }
             */
        }
    }
}
