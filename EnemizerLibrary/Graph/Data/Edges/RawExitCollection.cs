using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.Data
{
    public class RawExitCollection
    {
        public List<RawExit> RawExits = new List<RawExit>()
        {
            { new RawExit(0x15AEE, "Link's House", 0x15B8C, 0x15BDB, "Link's House", 0x2C, 0x0506, 260, "2C-link-house", "cave-links-house") },
            { new RawExit(0x15AF0, "Sanctuary Exit", 0x15B8D, 0x15BDD, "Sanctuary", 0x13, 0x001c, 18, "13-sanctuary", "sanctuary") },
            { new RawExit(0x15AF2, "Hyrule Castle Exit (West)", 0x15B8E, 0x15BDF, "Hyrule Castle Entrance (West)", 0x1B, 0x0016, 96, "1B-castle", "hyrule-west-entrance") },
            { new RawExit(0x15AF4, "Hyrule Castle Exit (South) / Hyrule Castle Entrance (Special Entrance)", 0x15B8F, 0x15BE1, "Hyrule Castle Entrance (South)", 0x1B, 0x0530, 97, "1B-castle", "hyrule-entrance") },
            { new RawExit(0x15AF6, "Hyrule Castle Exit (East)", 0x15B90, 0x15BE3, "Hyrule Castle Entrance (East)", 0x1B, 0x004a, 98, "1B-castle", "hyrule-east-entrance") },
            { new RawExit(0x15AF8, "Zora's Domain", 0x15B91, 0x15BE5, "Entrance to Zora's Domain", 0x0F, 0x001c, 65535, "0F-entrance-zora-domain", "zora-domain") },
            { new RawExit(0x15AFA, "Agahnim's Tower Boss Room", 0x15B92, 0x15BE7, "Warp to Pyramid After Defeating Agahnim", 0x5B, 0x002e, 32, "5B-pyramid", "agahnim-agahnim") },
            { new RawExit(0x15AFC, "Old Man Cave Exit (West)", 0x15B93, 0x15BE9, "Old Man Cave (West)", 0x0A, 0x03a0, 240, "0A-entrance-death-mountain", "cave-dm-entrance") },
            { new RawExit(0x15AFE, "Old Man Cave Exit (East)", 0x15B94, 0x15BEB, "Old Man Cave (East)", 0x03, 0x1402, 241, "03-west-death-mountain-lower", "cave-dm-entrance-exit") },
            { new RawExit(0x15B00, "Eastern Palace Exit", 0x15B95, 0x15BED, "Eastern Palace", 0x1E, 0x005a, 201, "1E-eastern-palace", "eastern-entrance") },
            { new RawExit(0x15B02, "Desert Palace Exit (South)", 0x15B96, 0x15BEF, "Desert Palace Entrance (South)", 0x30, 0x0314, 132, "30-desert-palace-main-entrance", "desert-main-entrance") },
            { new RawExit(0x15B04, "Desert Palace Exit (East)", 0x15B97, 0x15BF1, "Desert Palace Entrance (East)", 0x30, 0x02a8, 133, "30-desert-palace-east-entrance", "desert-east-entrance-hall") },
            { new RawExit(0x15B06, "Desert Palace Exit (West)", 0x15B98, 0x15BF3, "Desert Palace Entrance (West)", 0x30, 0x0280, 131, "30-desert-ledge", "desert-west-entrance") },
            { new RawExit(0x15B08, "Desert Palace Exit (North)", 0x15B99, 0x15BF5, "Desert Palace Entrance (North)", 0x30, 0x0016, 99, "30-desert-ledge-boss-entrance", "desert-boss-entrance") },
            { new RawExit(0x15B0A, "Elder House Exit (West)", 0x15B9A, 0x15BF7, "Elder House (West)", 0x18, 0x02bc, 242, "18-kakariko", "cave-old-lady-left") },
            { new RawExit(0x15B0C, "Elder House Exit (East)", 0x15B9B, 0x15BF9, "Elder House (East)", 0x18, 0x02c4, 243, "18-kakariko", "cave-old-lady-right") },
            { new RawExit(0x15B0E, "Two Brothers House Exit (West)", 0x15B9C, 0x15BFB, "Two Brothers House (West)", 0x28, 0x08a0, 244, "28-kakariko-maze-race", "cave-angry-brothers-exit") },
            { new RawExit(0x15B10, "Two Brothers House Exit (East)", 0x15B9D, 0x15BFD, "Two Brothers House (East)", 0x29, 0x0880, 245, "29-kakariko-library", "cave-angry-brothers-entrance") },
            { new RawExit(0x15B12, "Bat Cave Exit / Bat Cave (right) (Drop Entrance)", 0x15B9E, 0x15BFF, "Bat Cave Cave", 0x22, 0x0412, 227, "22-smithy", "cave-magic-bat") },
            { new RawExit(0x15B14, "Lumberjack Tree Exit / Lumberjack Tree (top) (Drop Entrance)", 0x15B9F, 0x15C01, "Lumberjack Tree Cave", 0x02, 0x0118, 226, "02-lumberjack-house", "cave-lumberjack-tree") },
            { new RawExit(0x15B16, "Dark Death Mountain Ascend Exit (Bottom)", 0x15BA0, 0x15C03, "Dark Death Mountain Ascend (Bottom)", 0x45, 0x0ee0, 248, "45-dw-east-death-mountain", "cave-super-bunny-entrance") },
            { new RawExit(0x15B18, "Dark Death Mountain Ascend Exit (Top)", 0x15BA1, 0x15C05, "Dark Death Mountain Ascend (Top)", 0x45, 0x0460, 232, "45-dw-east-death-mountain", "cave-super-bunny-exit") },
            { new RawExit(0x15B1A, "Turtle Rock Ledge Exit (West)", 0x15BA2, 0x15C07, "Dark Death Mountain Ledge (West)", 0x45, 0x07ca, 35, "45-dw-east-death-mountain-turtle-bridge", "turtle-lazer-exit") },
            { new RawExit(0x15B1C, "Bumper Cave Exit (Bottom)", 0x15BA3, 0x15C09, "Bumper Cave (Bottom)", 0x4A, 0x03a0, 251, "4A-bumper-cave", "cave-bumper-bottom") },
            { new RawExit(0x15B1E, "Bumper Cave Exit (Top)", 0x15BA4, 0x15C0B, "Bumper Cave (Top)", 0x4A, 0x00a0, 235, "4A-bumper-cave", "cave-bumper-top") },
            { new RawExit(0x15B20, "Turtle Rock Isolated Ledge Exit", 0x15BA5, 0x15C0D, "Turtle Rock Isolated Ledge Entrance", 0x45, 0x0ad4, 213, "45-dw-east-death-mountain-turtle-isolated", "turtle-lazer-chests") },
            { new RawExit(0x15B22, "Turtle Rock Ledge Exit (East)", 0x15BA6, 0x15C0F, "Dark Death Mountain Ledge (East)", 0x45, 0x07e0, 36, "45-dw-east-death-mountain-turtle-bridge", "turtle-big-chest") },
            { new RawExit(0x15B24, "Fairy Ascension Cave Exit (Bottom)", 0x15BA7, 0x15C11, "Fairy Ascension Cave (Bottom)", 0x05, 0x0dd4, 253, "05-east-death-mountain", "cave-east-dm-rocks-bottom") },
            { new RawExit(0x15B26, "Fairy Ascension Cave Exit (Top)", 0x15BA8, 0x15C13, "Fairy Ascension Cave (Top)", 0x05, 0x0ad4, 237, "05-east-death-mountain", "cave-east-dm-rocks-top") },
            { new RawExit(0x15B28, "Spiral Cave Exit", 0x15BA9, 0x15C15, "Spiral Cave (Bottom)", 0x05, 0x0cca, 254, "05-east-death-mountain", "cave-spiral-cave-exit") },
            { new RawExit(0x15B2A, "Spiral Cave Exit (Top)", 0x15BAA, 0x15C17, "Spiral Cave", 0x05, 0x07c8, 238, "05-east-death-mountain", "cave-spiral-cave-entrance") },
            { new RawExit(0x15B2C, "7 Chest Cave Exit (Bottom) (Upside-down Cave)", 0x15BAB, 0x15C19, "7 Chest Cave (Bottom)", 0x05, 0x0ee0, 255, "05-east-death-mountain", "cave-upside-down-shop") },
            { new RawExit(0x15B2E, "7 Chest Cave Exit (Middle) (Upside-down Cave)", 0x15BAC, 0x15C1B, "7 Chest Cave (Middle)", 0x05, 0x17e0, 239, "05-east-death-mountain", "cave-upside-down-5-chest") },
            { new RawExit(0x15B30, "7 Chest Cave Exit (Top) (Upside-down Cave)", 0x15BAD, 0x15C1D, "7 Chest Cave (Top)", 0x05, 0x0460, 223, "05-east-death-mountain", "cave-upside-down-top") },
            { new RawExit(0x15B32, "Spectacle Rock Cave Exit", 0x15BAE, 0x15C1F, "Spectacle Rock Cave (Bottom)", 0x03, 0x0d9c, 249, "03-west-death-mountain-lower", "cave-spectical-rock-exit") },
            { new RawExit(0x15B34, "Spectacle Rock Cave Exit (Top)", 0x15BAF, 0x15C21, "Spectacle Rock Cave", 0x03, 0x0eac, 250, "03-west-death-mountain-lower", "cave-spectical-rock-entrance-ledge") },
            { new RawExit(0x15B36, "Spectacle Rock Cave Exit (Peak)", 0x15BB0, 0x15C23, "Spectacle Rock Cave Peak", 0x03, 0x092c, 234, "03-west-death-mountain-lower", "cave-spectical-rock-upper-entrance") },
            { new RawExit(0x15B38, "Agahnims Tower Exit", 0x15BB1, 0x15C25, "Agahnims Tower", 0x1B, 0x0032, 224, "1B-castle", "agahnim-entrance") },
            { new RawExit(0x15B3A, "Swamp Palace Exit", 0x15BB2, 0x15C27, "Swamp Palace", 0x7B, 0x049e, 40, "7B-dw-southwest-swamp", "swamp-entrance") },
            { new RawExit(0x15B3C, "Dark Palace Exit", 0x15BB3, 0x15C29, "Palace of Darkness", 0x5E, 0x005a, 74, "5E-palace-of-darkness", "pod-entrance") },
            { new RawExit(0x15B3E, "Misery Mire Exit", 0x15BB4, 0x15C2B, "Misery Mire", 0x70, 0x0414, 152, "70-mire", "mire-entrance") },
            { new RawExit(0x15B40, "Skull Woods Second Section Exit (West) / Skull Woods Second Section (Drop Entrance)", 0x15BB5, 0x15C2D, "Skull Woods Second Section Door (West)", 0x40, 0x0c8e, 86, "40-skull-woods", "skull-pot-key-exit") },
            { new RawExit(0x15B42, "Skull Woods Second Section Exit (East)", 0x15BB6, 0x15C2F, "Skull Woods Second Section Door (East)", 0x40, 0x0eb8, 87, "40-skull-woods", "skull-gibdo-chest") },
            { new RawExit(0x15B44, "Skull Woods First Section Exit / Skull Woods First Section (Top) (Drop Entrance)", 0x15BB7, 0x15C31, "Skull Woods First Section Door", 0x40, 0x0f4c, 88, "40-skull-woods", "skull-big-chest") },
            { new RawExit(0x15B46, "Skull Woods Final Section Exit", 0x15BB8, 0x15C33, "Skull Woods Final Section", 0x40, 0x0282, 89, "40-skull-woods", "skull-boss-entrance") },
            { new RawExit(0x15B48, "Tower of Hera Exit", 0x15BB9, 0x15C35, "Tower of Hera", 0x03, 0x0050, 119, "03-west-death-mountain-upper", "hera-entrance") },
            { new RawExit(0x15B4A, "Ice Palace Exit", 0x15BBA, 0x15C37, "Ice Palace", 0x75, 0x0bc6, 14, "75-dw-lake-hylia-ice-palace", "ice-entrance") },
            { new RawExit(0x15B4C, "Death Mountain Return Cave Exit (West)", 0x15BBB, 0x15C39, "Death Mountain Return Cave (West)", 0x0A, 0x00a0, 230, "0A-entrance-death-mountain", "cave-dm-exit") },
            { new RawExit(0x15B4E, "Death Mountain Return Cave Exit (East)", 0x15BBC, 0x15C3B, "Death Mountain Return Cave (East)", 0x03, 0x0d82, 231, "03-west-death-mountain-lower", "cave-dm-exit-entrance") },
            { new RawExit(0x15B50, "Old Man House Exit (Bottom)", 0x15BBD, 0x15C3D, "Old Man House (Bottom)", 0x03, 0x181a, 228, "03-west-death-mountain-lower", "cave-old-man-house-entrance") },
            { new RawExit(0x15B52, "Old Man House Exit (Top)", 0x15BBE, 0x15C3F, "Old Man House (Top)", 0x03, 0x10c6, 229, "03-west-death-mountain-lower", "cave-old-man-house-back-exit") },
            { new RawExit(0x15B54, "Hyrule Castle Secret Entrance Exit / Hyrule Castle Secret Entrance (Drop Entrance)", 0x15BBF, 0x15C41, "Hyrule Castle Secret Entrance Stairs", 0x1B, 0x044a, 85, "1B-castle", "cave-uncle-death") },
            { new RawExit(0x15B56, "Turtle Rock Exit (Front)", 0x15BC0, 0x15C43, "Turtle Rock", 0x47, 0x0712, 214, "47-turtle-rock", "turtle-entrance") },
            { new RawExit(0x15B58, "Thieves Town Exit", 0x15BC1, 0x15C45, "Thieves Town", 0x58, 0x0b2e, 219, "58-outcast-village", "thieves-entrance") },
            { new RawExit(0x15B5A, "Thieves Forest Hideout Exit / Thieves Forest Hideout (top) (Drop Entrance)", 0x15BC2, 0x15C47, "Thieves Forest Hideout Stump", 0x00, 0x0f4e, 225, "00-lost-woods", "cave-thief-hut") },
            { new RawExit(0x15B5C, "Pyramid Exit", 0x15BC3, 0x15C49, "Pyramid Entrance", 0x5B, 0x0b0e, 16, "5B-pyramid", "ganon-fall") },
            { new RawExit(0x15B5E, "Ganons Tower Exit", 0x15BC4, 0x15C4B, "Ganons Tower", 0x43, 0x0052, 12, "43-dw-west-death-mountain-upper", "gt-entrance") },
            { new RawExit(0x15B60, "North Fairy Cave Exit", 0x15BC5, 0x15C4D, "North Fairy Cave", 0x15, 0x0088, 8, "15-river-between-graveyard-witch", "cave-north-fairy") },
            { new RawExit(0x15B62, "Kakariko Well Exit / Kakariko Well (top) (Drop Entrance)", 0x15BC6, 0x15C4F, "Kakariko Well Cave", 0x18, 0x0386, 47, "18-kakariko", "cave-kakariko-well") },
            { new RawExit(0x15B64, "Hookshot Cave Exit (South)", 0x15BC7, 0x15C51, "Hookshot Cave", 0x45, 0x04da, 60, "45-dw-east-death-mountain", "cave-hookshot-entrance") },
            { new RawExit(0x15B66, "Hookshot Cave Exit (North)", 0x15BC8, 0x15C53, "Hookshot Cave Back Entrance", 0x45, 0x004c, 44, "45-dw-east-death-mountain", "cave-hookshot-backdoor") },
            { new RawExit(0x15B68, "Houlihan Room", 0x15BC9, 0x15C55, "Link's House (from Houlihan)", 0x2C, 0x0506, 3, "2C-link-house", "cave-houlihan") },
        };
    }

    public class RawExit
    {
        public int ExitRoomAddress { get; set; }
        public string ExitRoomName { get; set; }
        public int ExitAreaAddress { get; set; }
        public int ExitVramAddress { get; set; }
        public string ExitAreaName { get; set; }
        public int AreaId { get; set; }
        public int VramValue { get; set; }
        public int RoomId { get; set; }
        public string LogicalAreaId { get; set; }
        public string LogicalRoomId { get; set; }

        public RawExit(int exitRoomAddress, string exitRoomName, int exitAreaAddress, int exitVramAddress, string exitAreaName, int areaId, int vramValue, int roomId, string logicalAreaId, string logicalRoomId)
        {
            ExitRoomAddress = exitRoomAddress;
            ExitRoomName = exitRoomName;
            ExitAreaAddress = exitAreaAddress;
            ExitVramAddress = exitVramAddress;
            ExitAreaName = exitAreaName;
            AreaId = areaId;
            VramValue = vramValue;
            RoomId = roomId;
            LogicalAreaId = logicalAreaId;
            LogicalRoomId = logicalRoomId;
        }
    }

}
