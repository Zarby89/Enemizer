using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Chest
    {

        public string name;
        public int region;
        public int image_id;
        public int address;
        public byte itemin;
        public Chest(string name, int image_id, int region, int address, byte itemin = 255)
        {
            this.name = name;
            this.region = region;
            this.image_id = image_id;
            this.address = address;
            this.itemin = itemin;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

        //public static List<Chest> chest_list = new List<Chest>();
        /*public static void add_chests()
        {
            //var match = ;
            chest_list.Add(new Chest("Secret Entrance [Lamp]", 0, 0, 0xE971, 53));
            chest_list.Add(new Chest("Key Guard Room [Map]", 1, 0, 0xEB0C, 56));
            chest_list.Add(new Chest("Key Guard Room2 [Blue Boomerang]", 2, 0, 0xE974, 18));
            chest_list.Add(new Chest("Zelda Cell [Lamp]", 3, 0, 0xEB09, 53));
            chest_list.Add(new Chest("Dark Room [Key]", 4, 0, 0xE96E, 47));
            chest_list.Add(new Chest("Bomb Section (Chest 1)[3 Bombs]", 5, 0, 0xEB5D, 3));
            chest_list.Add(new Chest("Bomb Section (Chest 2)[300 Rupees]", 5, 0, 0xEB60, 11));
            chest_list.Add(new Chest("Bomb Section (Chest 3)[10 Arrows]", 5, 0, 0xEB63, 5));
            chest_list.Add(new Chest("Sanctuary [Sanc Heart]", 6, 0, 0xEA79, 42));
            chest_list.Add(new Chest("Agahnim Tower Dark Maze [Key]", 7, 0, 0xEAB2, 47));
            chest_list.Add(new Chest("Agahnim Tower 2Guard Room [Key]", 8, 0, 0xEAB5, 47));

            chest_list.Add(new Chest("Canon Ball Room [100 Rupees]", 0, 1, 0xE9B3, 10));
            chest_list.Add(new Chest("Compass Room [Compass]", 1, 1, 0xE977, 34));
            chest_list.Add(new Chest("Map Room [Map]", 2, 1, 0xE9F5, 56));
            chest_list.Add(new Chest("Spinning Anti-Faerie [Big Key]", 3, 1, 0xE9B9, 13));
            chest_list.Add(new Chest("Big Chest [Bow]", 4, 1, 0xE97D, 30));
            chest_list.Add(new Chest("Armos Knights [Boss Heart]", 5, 1, 0x180150, 44));

            chest_list.Add(new Chest("Item on the torch [Key]", 4, 2, 0x180160, 47));
            chest_list.Add(new Chest("Switch Room [Map]", 0, 2, 0xE9B6, 56));
            chest_list.Add(new Chest("Popo Room [Compass]", 2, 2, 0xE9CB, 34));
            chest_list.Add(new Chest("Canon Ball Room [Big key]", 3, 2, 0xE9C2, 13));
            chest_list.Add(new Chest("Big Chest [Power Glove]", 1, 2, 0xE98F, 68));
            chest_list.Add(new Chest("Lanmolas [Boss Heart]", 5, 2, 0x180151, 44));

            chest_list.Add(new Chest("Torch Room [Big Key]", 0, 3, 0xE9E6, 13));
            chest_list.Add(new Chest("Main Room [Map]", 1, 3, 0xE9AD, 56));
            chest_list.Add(new Chest("Big Chest [Moon Pearl]", 2, 3, 0xE9F8, 59));
            chest_list.Add(new Chest("Compass Room [Compass]", 3, 3, 0xE9FB, 34));
            chest_list.Add(new Chest("Moldorm [Boss Heart]", 4, 3, 0x180152, 44));

            chest_list.Add(new Chest("Basement Left [Key]", 0, 4, 0xEA5B, 47));
            chest_list.Add(new Chest("4 Skeleton Room [Key]", 1, 4, 0xEA49, 47));
            chest_list.Add(new Chest("Dark Basement Left [1 Arrow]", 2, 4, 0xEA4C, 1));
            chest_list.Add(new Chest("Dark Basement Right [Key]", 2, 4, 0xEA4F, 47));
            chest_list.Add(new Chest("Middle Room [Big Key]", 3, 4, 0xEA37, 13));
            chest_list.Add(new Chest("Map Room [Map]", 4, 4, 0xEA52, 56));
            chest_list.Add(new Chest("Bridge Main Room [Key]", 5, 4, 0xEA3D, 47));
            chest_list.Add(new Chest("Bomb Hole Bridge Main Room [Key]", 5, 4, 0xEA3A, 47));
            chest_list.Add(new Chest("Spike Room [5 Rupees]", 6, 4, 0xEA52, 4));
            chest_list.Add(new Chest("Compass Room [Compass]", 7, 4, 0xEA43, 34));
            chest_list.Add(new Chest("Big Chest [Hammer]", 8, 4, 0xEA40, 40));
            chest_list.Add(new Chest("Dark Room Bottom [Key]", 9, 4, 0xEA58, 47));
            chest_list.Add(new Chest("Dark Room Top [3 Bombs]", 10, 4, 0xEA55, 3));
            chest_list.Add(new Chest("Helmasaure [Boss Heart]", 11, 4, 0x180153, 44));

            chest_list.Add(new Chest("Entrance Room [Key]", 0, 5, 0xEA9D, 47));
            chest_list.Add(new Chest("Map Room [Map]", 1, 5, 0xE986, 56));
            chest_list.Add(new Chest("Compass Room [Compass]", 2, 5, 0xEAA0, 34));
            chest_list.Add(new Chest("Big Chest [Hookshot]", 3, 5, 0xE989, 45));
            chest_list.Add(new Chest("Falling Chest Left [20 Rupees]", 4, 5, 0xEAA3, 7));
            chest_list.Add(new Chest("Falling Chest Right [Big Key]", 5, 5, 0xEAA6, 13));
            chest_list.Add(new Chest("Drain Room Left Chest [20 Rupees]", 6, 5, 0xEAA9, 7));
            chest_list.Add(new Chest("Drain Room Right Chest [20 Rupees]", 6, 5, 0xEAAC, 7));
            chest_list.Add(new Chest("Waterfall Room [20 Rupees]", 7, 5, 0xEAAF, 7));
            chest_list.Add(new Chest("Arghus [Boss Heart]", 8, 5, 0x180154, 44));

            chest_list.Add(new Chest("Map Room [Map]", 0, 6, 0xE9A1, 56));
            chest_list.Add(new Chest("Big Chest [Fire Rod]", 1, 6, 0xE998, 37));
            chest_list.Add(new Chest("East of Big Chest [Key]", 2, 6, 0xE99B, 47));
            chest_list.Add(new Chest("Big Key Room [Big Key]", 3, 6, 0xE99E, 13));
            chest_list.Add(new Chest("Trap Room [Key]", 4, 6, 0xE9C8, 47));
            chest_list.Add(new Chest("Boss Entrance [Key]", 5, 6, 0xE9FE, 47));
            chest_list.Add(new Chest("Compass Room [Compass]", 6, 6, 0xE992, 34));
            chest_list.Add(new Chest("Mothula [Boss Heart]", 7, 6, 0x180155, 44));

            chest_list.Add(new Chest("Blind Cell [Key]", 0, 7, 0xEA13, 47));
            chest_list.Add(new Chest("Big Chest [Titan Mitts]", 1, 7, 0xEA10, 76));
            chest_list.Add(new Chest("Big Key Chest [Big Key]", 2, 7, 0xEA04, 13));
            chest_list.Add(new Chest("Entrance Room [Map]", 3, 7, 0xEA01, 56));
            chest_list.Add(new Chest("North Room [20 Rupees]", 4, 7, 0xEA0A, 7));
            chest_list.Add(new Chest("East Room [Compass]", 5, 7, 0xEA07, 34));
            chest_list.Add(new Chest("Bomb Hole Chest [3 Bombs]", 6, 7, 0xEA0D, 3));
            chest_list.Add(new Chest("Blind [Boss Heart]", 7, 7, 0x180156, 44));

            chest_list.Add(new Chest("Penguin Room [Compass]", 0, 8, 0xE9D4, 34));
            chest_list.Add(new Chest("Big Key Chest [Big Key]", 1, 8, 0xE9A4, 13));
            chest_list.Add(new Chest("Map Room [Map]", 2, 8, 0xE9DD, 56));
            chest_list.Add(new Chest("Spike Invisible Chest [Key]", 3, 8, 0xE9E0, 47));
            chest_list.Add(new Chest("Ice Man Chest [3 Bombs]", 4, 8, 0xE995, 3));
            chest_list.Add(new Chest("Big Chest [Blue Mail]", 5, 8, 0xE9AA, 14));
            chest_list.Add(new Chest("Switch Chest [Key]", 6, 8, 0xE9E3, 47));
            chest_list.Add(new Chest("Kholdstare [Boss Heart]", 7, 8, 0x180157, 44));

            chest_list.Add(new Chest("Big Key Chest [Big Key]", 0, 9, 0xEA6D, 13));
            chest_list.Add(new Chest("Compass Room [Compass]", 1, 9, 0xEA64, 34));
            chest_list.Add(new Chest("Switch Chest [Key]", 2, 9, 0xEA5E, 47));
            chest_list.Add(new Chest("Map Chest [Map]", 3, 9, 0xEA6A, 56));
            chest_list.Add(new Chest("Big Chest [Cane of Somaria]", 4, 9, 0xEA67, 32));
            chest_list.Add(new Chest("Spike Room [Key]", 5, 9, 0xE9DA, 47));
            chest_list.Add(new Chest("Bridge Chest [Key]", 6, 9, 0xEA61, 47));
            chest_list.Add(new Chest("Vitreous [Boss Heart]", 7, 9, 0x180158, 44));

            chest_list.Add(new Chest("Compass Room [Compass]", 0, 10, 0xEA22, 34));
            chest_list.Add(new Chest("Roller Room Left [Map]", 1, 10, 0xEA1C, 56));
            chest_list.Add(new Chest("Roller Room Right [Key]", 1, 10, 0xEA1F, 47));
            chest_list.Add(new Chest("Chomp Room [Key]", 2, 10, 0xEA16, 47));
            chest_list.Add(new Chest("Big Key Chest [Big Key]", 3, 10, 0xEA25, 13));
            chest_list.Add(new Chest("Big Chest [Mirror Shield]", 4, 10, 0xEA19, 57));
            chest_list.Add(new Chest("Roller Switch [Key]", 5, 10, 0xEA34, 47));
            chest_list.Add(new Chest("Lazer Room Top Right [1 Rupee]", 6, 10, 0xEA28, 2));
            chest_list.Add(new Chest("Lazer Room Top Left [5 Rupees]", 6, 10, 0xEA2B, 4));
            chest_list.Add(new Chest("Lazer Room Bottom Right [20 Rupees]", 6, 10, 0xEA2E, 7));
            chest_list.Add(new Chest("Lazer Room Bottom Left [Key]", 6, 10, 0xEA31, 47));
            chest_list.Add(new Chest("Trinexx [Boss Heart]", 7, 10, 0x180159, 44));

            chest_list.Add(new Chest("4Chest Skeleton Top Left [3 Bombs]", 0, 11, 0xEAB8, 3));
            chest_list.Add(new Chest("4Chest Skeleton Top Right [10 Arrows]", 0, 11, 0xEABB, 5));
            chest_list.Add(new Chest("4Chest Skeleton Bottom Left [20 Rupees]", 0, 11, 0xEABE, 7));
            chest_list.Add(new Chest("4Chest Skeleton Bottom Right [20 Rupees]", 0, 11, 0xEAC1, 7));
            chest_list.Add(new Chest("Map Room [Map]", 1, 11, 0xEAD3, 56));
            chest_list.Add(new Chest("Big Chest [Red Mail]", 2, 11, 0xEAD6, 72));
            chest_list.Add(new Chest("East Wing 1st Room Left Chest [10 Arrows]", 3, 11, 0xEAD9, 5));
            chest_list.Add(new Chest("East Wing 1st Room Right Chest [3 Bombs]", 3, 11, 0xEADC, 3));
            chest_list.Add(new Chest("4Chest Hidden Top Left [10 Arrows]", 4, 11, 0xEAC4, 5));
            chest_list.Add(new Chest("4Chest Hidden Top Right [10 Arrows]", 4, 11, 0xEAC7, 5));
            chest_list.Add(new Chest("4Chest Hidden Bottom Left [3 Bombs]", 4, 11, 0xEACA, 3));
            chest_list.Add(new Chest("4Chest Hidden Bottom Right [3 Bombs]", 4, 11, 0xEACD, 3));
            chest_list.Add(new Chest("Before Armos [10 Arrows]", 5, 11, 0xEADF, 5));
            chest_list.Add(new Chest("Tile Room [Key]", 6, 11, 0xEAE2, 47));
            chest_list.Add(new Chest("Compass Room Top Left [Compass]", 7, 11, 0xEAE5, 34));
            chest_list.Add(new Chest("Compass Room Top Right [1 Rupee]", 7, 11, 0xEAE8, 2));
            chest_list.Add(new Chest("Compass Room Bottom Left [20 Rupee]", 7, 11, 0xEAEB, 7));
            chest_list.Add(new Chest("Compass Room Bottom Right [10 Arrows]", 7, 11, 0xEAEE, 5));
            chest_list.Add(new Chest("Firebar Chest [Key]", 8, 11, 0xEAD0, 47));
            chest_list.Add(new Chest("MiniHelma Room Left [3 Bombs]", 9, 11, 0xEAFD, 3));
            chest_list.Add(new Chest("MiniHelma Room Right [3 Bombs]", 9, 11, 0xEB00, 3));
            chest_list.Add(new Chest("Before Moldorm [Key]", 10, 11, 0xEB03, 47));
            chest_list.Add(new Chest("After Moldorm [20 Rupees]", 11, 11, 0xEB06, 7));
            chest_list.Add(new Chest("3 Armos Chest Top Left [10 Arrows]", 12, 11, 0xEAF4, 5));
            chest_list.Add(new Chest("3 Armos Chest Top Right [3 Bombs]", 12, 11, 0xEAF7, 3));
            chest_list.Add(new Chest("3 Armos Chest Bottom [Big Key]", 12, 11, 0xEAF1, 13));
            chest_list.Add(new Chest("Item on Torch [Key]", 13, 11, 0x180161, 47));

            chest_list.Add(new Chest("DM Moldorm Cave 1st Chest [20 Rupees]", 0, 12, 0xEB2A, 7));
            chest_list.Add(new Chest("DM Moldorm Cave 2nd Chest [20 Rupees]", 0, 12, 0xEB2D, 7));
            chest_list.Add(new Chest("DM Moldorm Cave 3rd Chest [20 Rupees]", 0, 12, 0xEB30, 7));
            chest_list.Add(new Chest("DM Moldorm Cave 4th Chest [20 Rupees]", 0, 12, 0xEB33, 7));
            chest_list.Add(new Chest("DM Moldorm Cave 5th Chest [20 Rupees]", 0, 12, 0xEB36, 7));
            chest_list.Add(new Chest("LW DM Bomb Block Cave Left [3 Bombs]", 1, 12, 0xEB39, 3));
            chest_list.Add(new Chest("LW DM Bomb Block Cave Right [10 Arrows]", 1, 12, 0xEB3C, 5));
            chest_list.Add(new Chest("Spiral Cave Chest [50 Rupees]", 2, 12, 0xE9BF, 9));
            chest_list.Add(new Chest("Mimic Room [Heart Piece]", 3, 12, 0xE9C5, 63));
            chest_list.Add(new Chest("Sanctuary Pile of Rocks [Heart Piece]", 4, 12, 0xEB3F, 63));
            chest_list.Add(new Chest("Graveyard Tombstone [Magic Cape]", 5, 12, 0xE97A, 54));
            chest_list.Add(new Chest("Kakariko Well (Bomb part) [Heart Piece]", 6, 12, 0xEA8E, 63));
            chest_list.Add(new Chest("Kakariko Well bottom [3 Bombs]", 7, 12, 0xEA9A, 3));
            chest_list.Add(new Chest("Kakariko Well Top Left [20 Rupee]", 7, 12, 0xEA91, 7));
            chest_list.Add(new Chest("Kakariko Well Top Middle [20 Rupee]", 7, 12, 0xEA94, 7));
            chest_list.Add(new Chest("Kakariko Well Top Right [20 Rupee]", 7, 12, 0xEA97, 7));

            chest_list.Add(new Chest("Blind Hut (Bomb Part) [Heart Piece]", 8, 12, 0xEB0F, 63));
            chest_list.Add(new Chest("Blind Hut Bottom Left [20 Rupee]", 9, 12, 0xEB18, 7));
            chest_list.Add(new Chest("Blind Hut Top Left [20 Rupee]", 9, 12, 0xEB12, 7));
            chest_list.Add(new Chest("Blind Hut Top Right [20 Rupee]", 9, 12, 0xEB15, 7));
            chest_list.Add(new Chest("Blind Hut Bottom Right [20 Rupee]", 9, 12, 0xEB1B, 7));

            chest_list.Add(new Chest("Chicken House [10 Arrows]", 10, 12, 0xE9E9, 5));
            chest_list.Add(new Chest("Tavern [Bottle]", 11, 12, 0xE9CE, 21));
            chest_list.Add(new Chest("Sahasrahla Hut Left [50 Rupee]", 12, 12, 0xEA82, 9));
            chest_list.Add(new Chest("Sahasrahla Hut Middle [50 Rupee]", 12, 12, 0xEA85, 9));
            chest_list.Add(new Chest("Sahasrahla Hut Right [50 Rupee]", 12, 12, 0xEA88, 9));
            chest_list.Add(new Chest("Aginah Desert Cave [Heart Piece]", 13, 12, 0xE9F2, 63));
            chest_list.Add(new Chest("Dam Chest [3 Bombs]", 14, 12, 0xE98C, 3));
            chest_list.Add(new Chest("Lake Hylia Cave Left [3 Bombs]", 15, 12, 0xEB42, 3));
            chest_list.Add(new Chest("Lake Hylia Cave Left 2nd [3 Bombs]", 15, 12, 0xEB45, 3));
            chest_list.Add(new Chest("Lake Hylia Cave Right [3 Bombs]", 15, 12, 0xEB48, 3));
            chest_list.Add(new Chest("Lake Hylia Cave Right 2nd [3 Bombs]", 15, 12, 0xEB4B, 3));
            chest_list.Add(new Chest("Byrna Cave [Cane of Byrna]", 16, 12, 0xEA8B, 33));
            chest_list.Add(new Chest("Hookshot Cave Top [50 Rupee]", 17, 12, 0xEB51, 9));
            chest_list.Add(new Chest("Hookshot Cave Bottom [50 Rupee]", 17, 12, 0xEB54, 9));
            chest_list.Add(new Chest("Hookshot Cave Top [50 Rupee]", 18, 12, 0xEB57, 9));
            chest_list.Add(new Chest("Hookshot Cave Bottom [50 Rupee]", 18, 12, 0xEB5A, 9));
            chest_list.Add(new Chest("DW DM 2Chest Top [3 Bombs]", 19, 12, 0xEA7C, 3));
            chest_list.Add(new Chest("DW DM 2Chest Bottom [20 Rupee]", 19, 12, 0xEA7F, 7));
            chest_list.Add(new Chest("Ice Rod Cave [Ice Rod]", 20, 12, 0xEB4E, 46));
            chest_list.Add(new Chest("DW Kakariko House [300 Rupee]", 21, 12, 0xE9EC, 11));
            chest_list.Add(new Chest("West of Mire Left[Heart Piece]", 22, 12, 0xEA73, 63));
            chest_list.Add(new Chest("West of Mire Right[20 Rupee]", 22, 12, 0xEA76, 7));
            chest_list.Add(new Chest("NE Swamp Cave Topmost [20 Rupee]", 23, 12, 0xEB1E, 7));
            chest_list.Add(new Chest("NE Swamp Cave 2nd [20 Rupee]", 23, 12, 0xEB21, 7));
            chest_list.Add(new Chest("NE Swamp Cave 3rd [20 Rupee]", 23, 12, 0xEB24, 7));
            chest_list.Add(new Chest("NE Swamp Cave 4th [20 Rupee]", 23, 12, 0xEB27, 7));
            chest_list.Add(new Chest("DW C-Shaped House [300 Rupee]", 24, 12, 0xE9EF, 11));
            chest_list.Add(new Chest("Link's House [Lamp]", 25, 12, 0xE9BC, 53));
            chest_list.Add(new Chest("Fat Fairy Left [Silver Arrows]", 25, 12, 0xE980, 88));
            chest_list.Add(new Chest("Fat Fairy Right [Golden Sword]", 25, 12, 0xE983, 52));

            chest_list.Add(new Chest("Wood Bush Hole [Heart Piece]", 1, 13, 0x180000, 63));
            chest_list.Add(new Chest("Cave Spectacle Rock [Heart Piece]", 2, 13, 0x180002, 63));
            chest_list.Add(new Chest("Behind Cemetary  [Heart Piece]", 3, 13, 0x180004, 63));
            chest_list.Add(new Chest("Grove Mirror Cave [Heart Piece]", 8, 13, 0x180003, 63));
            chest_list.Add(new Chest("Desert Mirror Cave [Heart Piece]", 9, 13, 0x180005, 63));
            chest_list.Add(new Chest("Agahnim Tree Cave [Heart Piece]", 11, 13, 0x180001, 63));
            chest_list.Add(new Chest("Zora's Domain [Heart Piece]", 13, 13, 0x180149, 63));
            chest_list.Add(new Chest("Forest Mushroom [Mushroom]", 14, 13, 0x180013, 58));
            chest_list.Add(new Chest("Top Spectacle Rock [Heart Piece]", 15, 13, 0x180140, 63));
            chest_list.Add(new Chest("Ether Tablet [Ether]", 16, 13, 0x180016, 36));
            chest_list.Add(new Chest("Floating Island [Heart Piece]", 17, 13, 0x180141, 63));
            chest_list.Add(new Chest("Maze Race [Heart Piece]", 18, 13, 0x180142, 63));
            chest_list.Add(new Chest("Hylia Island [Heart Piece]", 19, 13, 0x180144, 63));
            chest_list.Add(new Chest("Dam Drain [Heart Piece]", 20, 13, 0x180145, 63));
            chest_list.Add(new Chest("Desert Ledge [Heart Piece]", 21, 13, 0x180143, 63));
            chest_list.Add(new Chest("Bombos Tablet [Bombos]", 22, 13, 0x180017, 19));
            chest_list.Add(new Chest("DM Entrance Ledge [Heart Piece]", 24, 13, 0x180146, 63));
            chest_list.Add(new Chest("Hammer Pegs [Heart Piece]", 26, 13, 0x180006, 63));
            chest_list.Add(new Chest("Pyramid [Heart Piece]", 27, 13, 0x180147, 63));
            chest_list.Add(new Chest("Library Bonk [Book]", 5, 13, 0x180012, 17));
            chest_list.Add(new Chest("Grove Dig [Flute Inactive]", 31, 13, 0x18014A, 61));
            chest_list.Add(new Chest("Digging Minigame [Heart Piece]", 0, 13, 0x180148, 63)); //Need image
            chest_list.Add(new Chest("Master Sword Pedestal [Master Sword]", 0, 13, 0x289B0, 50)); //Need image

            chest_list.Add(new Chest("Hobo Under Bridge [Bottle]", 0, 14, 0x33E7D, 21));
            chest_list.Add(new Chest("Sick Boy [Bug Net]", 4, 14, 0x339CF, 31));
            chest_list.Add(new Chest("Hylia Cave NPC [300 Rupee]", 6, 14, 0x180010, 11));
            chest_list.Add(new Chest("Chest Minigame [Heart Piece]", 7, 14, 0xEDA8, 63));
            chest_list.Add(new Chest("DW Swamp NE NPC [300 Rupee]", 10, 14, 0x180011, 11));
            chest_list.Add(new Chest("King Zora [Flippers]", 12, 14, 0xEE1C3, 38));
            chest_list.Add(new Chest("Catfish [Quake]", 23, 14, 0xEE185, 69));
            chest_list.Add(new Chest("Purple Chest [Bottle]", 25, 14, 0x33D68, 21));
            chest_list.Add(new Chest("Uncle [L1 sword and shield]", 28, 14, 0x2DF45, 48));
            chest_list.Add(new Chest("Bottle Merchant [Bottle]", 29, 14, 0x2EB18, 21));
            chest_list.Add(new Chest("Sahasrahla [Pegasus Boots]", 30, 14, 0x2F1FC, 62));
            chest_list.Add(new Chest("Tree Boy [Shovel]", 31, 14, 0x330C7, 74));
            chest_list.Add(new Chest("Witch [Powder]", 33, 14, 0x180014, 67));
            chest_list.Add(new Chest("Cursing Bat [1/2 Magic]", 34, 14, 0x180015, 79));
            chest_list.Add(new Chest("Smith [Tempered Sword]", 35, 14, 0x18002A, 51));
            chest_list.Add(new Chest("Old man [Mirror]", 0, 14, 0xF69FA, 55));
            chest_list.Add(new Chest("KEY ITEM IN HERA [Key]", 0, 14, 0x180162, 47));
            //chest_list.Add(new Chest("Fairy Sword Exchange [Butter Sword]", 0, 14, 0x180028,52)); //NEED IMAGE


        }//*/
    }
}
