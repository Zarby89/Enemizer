using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldAreaConstants
    {
        public static string GetAreaName(int areaId)
        {
            string areaName;

            if (areaNames.TryGetValue(areaId, out areaName))
            {
                return areaName;
            }

            return null;
        }

        static Dictionary<int, string> areaNames = new Dictionary<int, string>()
        {
            { 0x000, "Lost Woods" },
            { 0x001, "Lost Woods (2)" },
            { 0x002, "Lumber Jack House" },
            { 0x003, "West Death Mountain" },
            { 0x004, "West Death Mountain (2)" },
            { 0x005, "East Death Mountain" },
            { 0x006, "East Death Mountain (2)" },
            { 0x007, "East Death Mountain Warp for Turtle Rock" },
            { 0x008, "Lost Woods (3?)" },
            { 0x009, "Lost Woods (4?)" },
            { 0x00A, "Entrance to Death Mountain" },
            { 0x00B, "West Death Mountain (3?)" },
            { 0x00C, "West Death Mountain (4?)" },
            { 0x00D, "East Death Mountain (3?)" },
            { 0x00E, "East Death Mountain (3?)" },
            { 0x00F, "Entrance to Zora's Domain" },
            { 0x010, "Path Between Kakariko Village and Lost Woods" },
            { 0x011, "Kakariko Village Fortune Teller" },
            { 0x012, "Pond Between Kakariko Village Fortune Teller and Sanctuary" },
            { 0x013, "Sanctuary" },
            { 0x014, "Graveyard" },
            { 0x015, "River Between Graveyard and Witch's Hut" },
            { 0x016, "Witch's Hut" },
            { 0x017, "East of Witch's Hut" },
            { 0x018, "Kakariko Village" },
            { 0x019, "Kakariko Village (2)" },
            { 0x01A, "Forest Between Kakariko Village and Hyrule Castle" },
            { 0x01B, "Hyrule Castle" },
            { 0x01C, "Hyrule Castle (2)" },
            { 0x01D, "Bridge Between Graveyard and Witch's Hut" },
            { 0x01E, "Eastern Palace" },
            { 0x01F, "Eastern Palace (2)" },
            { 0x020, "Kakariko Village (3?)" },
            { 0x021, "Kakariko Village (4?)" },
            { 0x022, "Smithy" },
            { 0x023, "Hyrule Castle (3?)" },
            { 0x024, "Hyrule Castle (4?)" },
            { 0x025, "Path Between Hyrule Castle and Eastern Palace}} (top)" },
            { 0x026, "Eastern Palace (3?)" },
            { 0x027, "Eastern Palace (4?)" },
            { 0x028, "Kakariko Village Maze Race" },
            { 0x029, "Kakariko Village Library" },
            { 0x02A, "Haunted Grove" },
            { 0x02B, "Forest Between Haunted Grove and Link's House" },
            { 0x02C, "Link's House" },
            { 0x02D, "Path Between Hyrule Castle and Eastern Palace}} (bottom)" },
            { 0x02E, "Caves South of Eastern Palace (left)" },
            { 0x02F, "Caves South of Eastern Palace (right)" },
            { 0x030, "Desert of Mystery" },
            { 0x031, "Desert of Mystery (2)" },
            { 0x032, "South of Haunted Grove" },
            { 0x033, "Northwestern Great Swamp" },
            { 0x034, "Northeastern Great Swamp" },
            { 0x035, "Lake Hylia" },
            { 0x036, "Lake Hylia (2)" },
            { 0x037, "Ice Cave" },
            { 0x038, "Desert of Mystery (3?)" },
            { 0x039, "Desert of Mystery (4?)" },
            { 0x03A, "Path Between Desert of Mystery and Great Swamp" },
            { 0x03B, "Southwestern Great Swamp" },
            { 0x03C, "Southeastern Great Swamp" },
            { 0x03D, "Lake Hylia (3?)" },
            { 0x03E, "Lake Hylia (4?)" },
            { 0x03F, "Path Between Lake Hylia and Ice Cave " },


            { 0x040, "Lost Woods (DW)" },
            { 0x041, "Lumber Jack House (DW)" },
            { 0x042, "Lumber Jack House (DW) (2)" },
            { 0x043, "West Death Mountain (DW)" },
            { 0x044, "East Death Mountain (DW)" },
            { 0x045, "East Death Mountain (DW) (2)" },

            /*
$07 (7) 	$47 (71) 	512 	Turtle Rock
$0A (10) 	$4A (74) 	512 	Entrance to Death Mountain
$0F (15) 	$4F (79) 	512 	Entrance to Zora's Domain
$10 (16) 	$50 (80) 	512 	Path Between Kakariko Village and Lost Woods
$11 (17) 	$51 (81) 	512 	Kakariko Village Fortune Teller
$12 (18) 	$52 (82) 	512 	Pond Between Kakariko Village Fortune Teller and Sanctuary
$13 (19) 	$53 (83) 	512 	Sanctuary
$14 (20) 	$54 (84) 	512 	Graveyard
$15 (21) 	$55 (85) 	512 	River Between Graveyard and Witch's Hut
$16 (22) 	$56 (86) 	512 	Witch's Hut
$17 (23) 	$57 (87) 	512 	East of Witch's Hut
$18 (24) 	$58 (88) 	1024 	Kakariko Village
$1A (26) 	$5A (90) 	512 	Forest Between Kakariko Village and Hyrule Castle
$1B (27) 	$5B (91) 	1024 	Hyrule Castle
$1D (29) 	$5D (93) 	512 	Bridge Between Graveyard and Witch's Hut
$1E (30) 	$5E (94) 	1024 	Eastern Palace
$22 (34) 	$62 (98) 	512 	Smithy
$25 (37) 	$65 (101) 	512 	Path Between Hyrule Castle and Eastern Palace}} (top)
$28 (40) 	$68 (104) 	512 	Kakariko Village Maze Race
$29 (41) 	$69 (105) 	512 	Kakariko Village Library
$2A (42) 	$6A (106) 	512 	Haunted Grove
$2B (43) 	$6B (107) 	512 	Forest Between Haunted Grove and Link's House
$2C (44) 	$6C (108) 	512 	Link's House
$2D (45) 	$6D (109) 	512 	Path Between Hyrule Castle and Eastern Palace}} (bottom)
$2E (46) 	$6E (110) 	512 	Caves South of Eastern Palace (left)
$2F (47) 	$6F (111) 	512 	Caves South of Eastern Palace (right)
$30 (48) 	$70 (112) 	1024 	Desert of Mystery
$32 (50) 	$72 (114) 	512 	South of Haunted Grove
$33 (51) 	$73 (115) 	512 	Northwestern Great Swamp
$34 (52) 	$74 (116) 	512 	Northeastern Great Swamp
$35 (53) 	$75 (117) 	1024 	Lake Hylia
$37 (55) 	$77 (119) 	512 	Ice Cave
$3A (58) 	$7A (122) 	512 	Path Between Desert of Mystery and Great Swamp
$3B (59) 	$7B (123) 	512 	Southwestern Great Swamp
$3C (60) 	$7C (124) 	512 	Southeastern Great Swamp
$3F (63) 	$7F (127) 	512 	Path Between Lake Hylia and Ice Cave 
*/
        };
    }
}
