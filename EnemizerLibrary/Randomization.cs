using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
//using System.Windows.Forms;

namespace EnemizerLibrary
{
    public partial class Randomization
    {
        Random rand;
        RomData ROM_DATA;
        int[] flags;
        //194 for firesnake
        //All the sprites address that are dropping keys //check 192,193,
        int[] smallCorridors_sprites = { 0x04DE29 };
        int[] key_sprite = new int[] { 0x04DA20, 0x04DA5C, 0x04DB7F, 0x04DD73, 0x04DDC3, 0x04DE07, 0x04E203, 0x04E20B, 0x04E326, 0x04E4F7, 0x04E70C, 0x04E7C8, 0x04E7FA, 0x04E200, 0x04E687, 0x04E991, 0x04E994, 0x04E997, 0x04E99A, 0x04E99D, 0x04E9A0, 0x04E9A3, 0x04E9A6, 0x04E9A9, 0x04E9AC, 0x04E9AF, 0x04E790, 0x04E78D, 0x04E78A };
        //All the rooms that need every sprite dead for doors to open
        int[] NeedKillable_doors = { 11, 27, 36, 40, 46, 49, 62, 68, 75, 83, 93, 107, 109, 110, 113, 117, 123, 125, 133, 135, 141, 165, 168, 176, 178, 182, 210, 216, 224, 239, 268, 291 };
        //room 113 is not in needkillable is it in key_sprite?
        //all the sprites "gfx" sheet 
        byte[] sprite_subset_0 = new byte[] { 22, 31, 47, 14 }; //70-72 part of guards we already have 4 guard set don't need more
        byte[] sprite_subset_1 = new byte[] { 44, 30, 32 };//73-13
        byte[] sprite_subset_2 = new byte[] { 12, 18, 23, 24, 28, 46, 34, 35, 39, 40, 38, 41, 36, 37, 42 };//19 trainee guard
        byte[] sprite_subset_3 = new byte[] { 17, 16, 27, 20, 82, 83 };
        //the sprites group avaiable in dungeons there's 60 of them some need to stay the same (npcs, bosses)
        byte[][] random_sprite_group = new byte[60][];
        byte[][] random_sprite_group_ow = new byte[43][];
        //all the sprites that can be used in the subset gfx sheet selected
        byte[][] subset_gfx_sprites = new byte[102][];
        //all the absorbable sprites fairy, bombs, rupees, arrows, ect, that can be used in any rooms without any specific set selected
        byte[] absorbable_sprites = new byte[] { 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF, 0xE0, 0xE1, 0xE2, 0xE3, 0xE4 };

        //All the rooms that require special handling
        int[] IcemanRoom = { 14, 126, 142, 158, 190 }; //these room need to be locked on the gfx ID : 28
        int[] WaterRoom = { 40, 56, 54, 70, 52, 22, 102 }; //these room need to be locked on the gfx ID : 17 //118 removed
        int[] ShadowRoom = { 62, 159 };//28,27,30

        int[] WallMasterRoom = { 57, 73, 86, 141, 104 };//force 82 on 3

        int[] bumperandcrystalRoom = {23,4,11,19,27,30,42,43,49,68,76,88,89,91,103,104,107,119,135,139,145,150,146,155,157,161,171,
        182,191,193,196,235, 64, 74, 165, 195,197,213,214,239,61,126};//also laser shooting eyes //can be 82 or 83 on 3
        //126 IR,



        int[] SwitchesRoom = { 2, 100, 267, 88 };// WM, ?? 73:no switch in that room

        int[] TonguesRoom = { 4, 206, 63, 35, 53, 55, 118 };
        //byte[] PushSwitchesRoom = { 53, 55, 118 }; //83 same as tongue
        int[] noStatueRoom = { 70, 208, 38, 43, 118, 54, 52, 22, 40,87,194,177 }; //do not generate statue in these rooms

        //ROOM 127 do not spawn  anti-faerie
        int[] canonRoom = { 92, 117 }; //47 on 0
        int[] canonRoom2 = { 185, 217, 268 };//46 on 2
        

        //For Keys
        byte[] NonKillable = {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x0B,0x15,0x1C,0x1E,0x21,0x5B,0x5C,0x5D,0x5E,0x5F,0x60,0x61,0x66,0x67,0x68,0x69,
        0x77,0x7D,0x7E,0x7F,0x82,0x84,0x8A,0x8E,0x90,0x93,0x95,0x96,0x97,0x98,0x9A,0xA1,0xAC,0xC5,0xC6,0xD0,0xD1,0xD8,0xD9,0xDA,0xDB,0xDC,
        0xDD,0xDE,0xDF,0xE0,0xDF,0xE0,0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,0xE7,0x6F,0x14,0x16,0x1A,0x27,0x28,0x29,0x2A,0x2F,0x32,0x31,0x35,0x36,
        0x38,0x37,0x3C,0x3F,0x40,0x4D,0x57,0x72,0x94,0x95,0x96,0x97,0x98,0x9E,0x9F,0xA0,0xA4,0xA8,0xA9,0xAF,0xAE,0xAD,0xB0,0xB1,0xB2,0xB8,
        0xB7,0xC2,0xCA,0x85,0x81,0x23,0x80,0xA8,0xA9,0x19,0x7C,0x26,0x91,0xAA};

        byte[] statis_sprites = new byte[]
        {0xCA,0x27,0x5D,0x5E,0x5F,0x60,0x7E,0x7F};

        //Non Killable in shutter doors Rooms
        byte[] NonKillable_shutter = {0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x0B,0x1E,0x21,0x66,0x67,0x68,0x69,
        0x77,0x82,0x84,0x8A,0x8E,0x90,0x93,0x9A,0xA1,0xC5,0xC6,0xD0,0x6F,0x14,0x16,0x1A,0x27,0x28,0x29,0x2A,0x2F,0x32,0x31,0x35,0x36,
        0x38,0x37,0x3C,0x3F,0x40,0x4D,0x57,0x72,0x94,0x95,0x96,0x97,0x98,0x9E,0x9F,0xA0,0xAF,0xAE,0xAD,0xB0,0xB1,0xB2,0xB8,
        0xB7,0xC2,0x85,0x81,0x80,0x91,0x26,0x81,0x19,0x81};


        byte[] bowSprites = {0x83,0x84};
        byte[] hammerSprites = { 0x8E };
        StreamWriter spoilerfile;
        bool spoiler = false;

        public Randomization(int seed, OptionFlags optionFlags, byte[] ROM_DATA, string filename = "newrom.sfc", string skin = "", bool spoiler = false, bool linkrandompalette = false) //Initialization of the randomization
        {
            //We should ask for a original ROM too to prevent any problem while checking the data or including these
            //data in the code [all the original sprites infos 0x3F * 5]
            //We need to patch the ROM first move all headers from their original location to 0x120090
            //Save the ROM as a new file instead of overwriting the original one
            //Save the flags used in a file to remember the last flags that were used
            this.flags = flags;
            this.ROM_DATA = new RomData(ROM_DATA);
            this.spoiler = spoiler;
            rand = new Random(seed);
            if (skin != "Default")
            {
                if (skin != "")
                {
                    if (skin == "Random")
                    {
                        string[] skins = Directory.GetFiles("sprites\\");
                        skin = skins[rand.Next(skins.Length)];
                    }
                    FileStream fsx = new FileStream(skin, FileMode.Open, FileAccess.Read);
                    byte[] skin_data = new byte[0x7078];
                    fsx.Read(skin_data, 0, 0x7078);
                    fsx.Close();
                    for (int i = 0; i < 0x7000; i++)
                    {
                        this.ROM_DATA[0x80000 + i] = skin_data[i];
                    }
                    for (int i = 0; i < 0x78; i++)
                    {
                        this.ROM_DATA[0x0DD308 + i] = skin_data[0x7000 + i];
                    }

                }

            }

            if (linkrandompalette)
            {
                Color c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (1 * 2), c, 0);

                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (2 * 2), c, 2);
                setColor(0xDD308 + (3 * 2), c, 0);
                setColor(0xDD308 + (12 * 2), c, 0);

                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (5 * 2), c, 0);

                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (6 * 2), c, 0);

                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (7 * 2), c, 0);


                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (8 * 2), c, 2);
                setColor(0xDD308 + (9 * 2), c, 0);

                c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
                setColor(0xDD308 + (10 * 2), c, 2);
                setColor(0xDD308 + (11 * 2), c, 0);

            }


            if (spoiler)
            {
                spoilerfile = new StreamWriter(seed.ToString() + " Spoiler.txt");
         
                spoilerfile.WriteLine("Spoiler Log Seed : " + seed.ToString());

            }

            create_subset_gfx();
            //dungeons
            if (optionFlags.RandomizeEnemies) // random sprites dungeons
            {
                
                create_sprite_group();
                patch_sprite_group();
                create_rooms_sprites();
            }

            //random sprite overworld
            if (optionFlags.RandomizeEnemies)
            {
                create_sprite_overworld_group();
                patch_sprite_group_ow();
                create_overworld_sprites();
            }


            //flags = 1;
            //Do the randomization based on flags!
            bool absorbable = optionFlags.EnemiesAbsorbable;
            bool bossmadness = optionFlags.BossMadness;
            //flag[4] = absorbable%
            //flag[5] = absorbable_items (bitwise)

                
            /*------------------
            |      Flags Informations        
            |0x01|1 - Dungeons Sprites Rand.
            |0x02|2 - Overworld Sprites Rand.
            |0x04|3 - Dungeons Palettes Rand.
            |0x08|4 - Sprite Palettes Rand.
            |0x10|5 - Oveworld Palettes Rand.
            |0x20|6 - Sprites HP Rand.
            |0x40|7 - Sprites DMG Rand.
            |0x80|8 - Sprites HP 0
            |0x100|9 - Bosses Shuffle
            |0x200|10 - Absorbable Sprites
            |0x400|11 - Bosses Madness
             */
             create_dungeons_properties();

            if(optionFlags.RandomizeEnemies)
            {
                Randomize_Dungeons_Sprite(absorbable);
            }

            if (optionFlags.RandomizeEnemies)
            {
                //WIP
                OverworldSpriteRandomizer.RandomizeOverworldSprite(this.rand, this.ROM_DATA, this.overworld_sprites, this.random_sprite_group_ow, this.subset_gfx_sprites, this.absorbable_sprites, absorbable);
            }

            if (optionFlags.RandomizeEnemyHealthRange)
            {
                Randomize_Sprites_HP(optionFlags.RandomizeEnemyHealthRangeAmount);
            }

            if (optionFlags.RandomizeEnemyDamage)
            {
                Randomize_Sprites_DMG(optionFlags.AllowEnemyZeroDamage);
            }
            //if (((flags[2]) == 0)) { Set_Sprites_ZeroHP(); } // flags[2] is optionFlags.RandomizeEnemyDamage

            if (optionFlags.RandomizeBosses)
            {
                Randomize_Bosses(bossmadness);
            }
            
            //if ((flags & 0x4000) == 0x4000)
            //{
                randomizePots(); //default on for now
            //}
            //reset seed for all these values so they can be optional
            rand = new Random(seed);
           // if ((flags & 0x04) == 0x04) {  }
            Randomize_Dungeons_Palettes();
            rand = new Random(seed);
            Randomize_Sprites_Palettes();
            //if ((flags & 0x08) == 0x08) { }
            rand = new Random(seed);
            
           // if ((flags & 0x10) == 0x10) {  } //WIP
            Randomize_Overworld_Palettes();
            rand = new Random(seed);
            //if ((flags & 0x1000) == 0x1000) { shuffle_music(); }
            /*if ((flags & 0x800) == 0x800)
            {
                black_all_dungeons();
            }
            if (spoiler)
            {
                spoilerfile.Close();
            }*/

            //Remove Trinexx Ice Floor : 
            this.ROM_DATA[0x04B37E] = 0xEA;
            this.ROM_DATA[0x04B37E+1] = 0xEA;
            this.ROM_DATA[0x04B37E+2] = 0xEA;
            this.ROM_DATA[0x04B37E+3] = 0xEA;
            /*this.ROM_DATA[0x5033 + 0x5E] = 0x24;
            this.ROM_DATA[0x5112 + 0x5E] = 0x93;
            this.ROM_DATA[0x51F1 + 0x5E] = 0x57;

            FileStream fsxx = new FileStream("weapons/mace.bin", FileMode.Open, FileAccess.Read);
            byte[] weapon_data = new byte[fsxx.Length];
            fsxx.Read(weapon_data, 0, (int)fsxx.Length);
            fsxx.Close();
            for (int i = 0; i < (int)weapon_data.Length; i++)
            {
                ROM_DATA[0x0121357 + i] = weapon_data[i];
            }*/

            // patch in our assembly binary data
            Patch patch = new Patch("patchData.json");
            patch.PatchRom(this.ROM_DATA);

            string fileName = "Enemizer " + Version.CurrentVersion + " - " + Path.GetFileName(filename);
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            this.ROM_DATA.WriteRom(fs);
            fs.Close();

            MessageBox.Show("Enemizer " + Version.CurrentVersion + " - " + Path.GetFileName(filename) + " Has been created in the enemizer folder !");
        }

        


        public bool scan_gtower(byte item) //0x08 = ice rod, 0x07 = fire rod
        {
            if (ROM_DATA[0xEAB8] == item) { return true; };
            if (ROM_DATA[0xEABB] == item) { return true; };
            if (ROM_DATA[0xEABE] == item) { return true; };
            if (ROM_DATA[0xEAC1] == item) { return true; };
            if (ROM_DATA[0xEAD3] == item) { return true; };
            if (ROM_DATA[0xEAD6] == item) { return true; };
            if (ROM_DATA[0xEAD9] == item) { return true; };
            if (ROM_DATA[0xEADC] == item) { return true; };
            if (ROM_DATA[0xEAC4] == item) { return true; };
            if (ROM_DATA[0xEAC7] == item) { return true; };
            if (ROM_DATA[0xEACA] == item) { return true; };
            if (ROM_DATA[0xEACD] == item) { return true; };
            if (ROM_DATA[0xEADF] == item) { return true; };
            if (ROM_DATA[0xEAE2] == item) { return true; };
            if (ROM_DATA[0xEAE5] == item) { return true; };
            if (ROM_DATA[0xEAE8] == item) { return true; };
            if (ROM_DATA[0xEAEB] == item) { return true; };
            if (ROM_DATA[0xEAEE] == item) { return true; };
            if (ROM_DATA[0xEAD0] == item) { return true; };
            if (ROM_DATA[0xEAFD] == item) { return true; };
            if (ROM_DATA[0xEB00] == item) { return true; };
            if (ROM_DATA[0xEB03] == item) { return true; };
            if (ROM_DATA[0xEB06] == item) { return true; };
            if (ROM_DATA[0xEAF4] == item) { return true; };
            if (ROM_DATA[0xEAF7] == item) { return true; };
            if (ROM_DATA[0xEAF1] == item) { return true; };
            if (ROM_DATA[0x180161] == item) { return true; };

            return false;
        }

        //create all the subset gfx set them on null because lots of them need to be unchanged
        public void create_subset_gfx()
        {
            for (int i = 0; i < 102; i++)
            {
                //subset_gfx_sprites[i] = null;
                subset_gfx_sprites[i] = new byte[] { };
            }
            //subset0
            
            subset_gfx_sprites[22] = new byte[] { 0x22, 0x11 };//DW Popo, Hinox, Snapdragon (require 23)
            subset_gfx_sprites[31] = new byte[] { 0x23, 0x24, 0x85, 0xA7, 0x02, 0x7E, 0x7F, 0x80 };//bari,stalfos,firebars
            subset_gfx_sprites[47] = new byte[] { 0x71 };//delalant,leever //0x64,0x63
            subset_gfx_sprites[14] = new byte[] { 0x19 };//ghini,thief
            subset_gfx_sprites[70] = new byte[] { 0x6A, 0x6B, 0x49, 0x43, 0x41, 0x42, 0x45, 0x48, 0x44, 0x4A, 0x4B };//need to be combined with 73 and 19 all guards
            subset_gfx_sprites[72] = new byte[] { 0x41, 0x42, 0x43, 0x45, 0x46, 0x47, 0x4B, 0x49 };//need to be combined with 73 and 19 all guards archers
            //subset1
            subset_gfx_sprites[44] = new byte[] { 0x4F, 0x4E, 0x61 }; //popo, beamos
            //13 contains guards same as 73
            subset_gfx_sprites[13] = new byte[] { }; //guards
            subset_gfx_sprites[73] = new byte[] {0x42,0x41,0x45 }; //guards
            subset_gfx_sprites[19] = new byte[] { }; //guards
            subset_gfx_sprites[30] = new byte[] { 0x26, 0x13, 0x18 }; //minihelma,minimoldorm,beetle
            subset_gfx_sprites[32] = new byte[] { 0x9C, 0x9D, 0x91, 0x8F }; //stalfos knight, shadow, blob
            //subset2
            subset_gfx_sprites[12] = new byte[] { 0x08, 0x58, 0x0F }; //octorock,crab,octobaloon
            subset_gfx_sprites[18] = new byte[] { 0x01, 0x4C }; //vulture, jazzhand, (Also contain tablets / rock in front of desert)
            subset_gfx_sprites[23] = new byte[] { 0x12 }; //pigmanspear, snapdragon (require 22) 
            subset_gfx_sprites[24] = new byte[] { 0x08 }; //dwoctorok
            subset_gfx_sprites[28] = new byte[] { 0x6D, 0x6E, 0x6F }; //rat,rope,keese, also the oldman
            subset_gfx_sprites[29] = new byte[] { };
            subset_gfx_sprites[46] = new byte[] { 0x84, 0x83 }; //green/red eyegores
            subset_gfx_sprites[34] = new byte[] { 0x9A, 0x81 }; //water sprites
            subset_gfx_sprites[35] = new byte[] { 0x8B }; //wallmaster,gibdo
            subset_gfx_sprites[39] = new byte[] { 0xC7, 0xCA, 0x5D, 0x5E, 0x5F, 0x60 };//chain chomp,pokey,rollers
            subset_gfx_sprites[40] = new byte[] { 0xA5, 0xA6, 0xC3 };//zazak,gibo (patrick star)
            subset_gfx_sprites[38] = new byte[] { 0x99 };//(A1) iceman,penguin
            subset_gfx_sprites[37] = new byte[] { 0x9B,0x20 };//wizzrobe,sluggula
            subset_gfx_sprites[41] = new byte[] { 0x9B };//wizzrobe
            subset_gfx_sprites[36] = new byte[] { 0x6D, 0x6E, 0x6F };//rat,rope,keese
            subset_gfx_sprites[42] = new byte[] { 0x8E, 0x5B, 0x5C };//turtle,kondongo ,also the digging game guy //0x86 kodongo problem
            //74 = lumberjack
            //subset3
            subset_gfx_sprites[16] = new byte[] { 0x51, 0x27, 0xC9 };//armos,deadrock,tektite
            subset_gfx_sprites[17] = new byte[] { 0x00, 0x0D };//raven,buzzblob
            subset_gfx_sprites[27] = new byte[] { 0xA8, 0xA9, 0xAA };//dw bomber/likelike
            subset_gfx_sprites[20] = new byte[] { 0xD0 };//lynel
            subset_gfx_sprites[74] = new byte[] { };//wizzrobe
            subset_gfx_sprites[80] = new byte[] {0x0B };//wizzrobe
            subset_gfx_sprites[81] = new byte[] { };//switches
            subset_gfx_sprites[93] = new byte[] { };//sanctuary mantle
            subset_gfx_sprites[82] = new byte[] { 0x8A, 0x1C, 0x15, 0x7D, }; //0x82 };//switches
            subset_gfx_sprites[83] = new byte[] { 0x8A, 0x1C, 0x15, 0x7D, }; //0x82 };//switches
        }

        public byte get_guard_subset_1()
        {
            int i = rand.Next(2);
            if (i == 0) { i = 73; };
            if (i == 1) { i = 13; };
            if (i == 2) { i = 13; };
            return (byte)i;
        }

        public byte[] fully_randomize_that_group()
        {
            return new byte[] { sprite_subset_0[rand.Next(sprite_subset_0.Length)], sprite_subset_1[rand.Next(sprite_subset_1.Length)],
            sprite_subset_2[rand.Next(sprite_subset_2.Length)],sprite_subset_3[rand.Next(sprite_subset_3.Length)]};
        }

        public void create_sprite_overworld_group()
        {
            for (int i = 0; i < 43; i++)
            {
                random_sprite_group_ow[i] = fully_randomize_that_group(); //group from 105 to 124 are empty
            }
            //Creations of the guards group :
            random_sprite_group_ow[0] = new byte[] { 72, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] }; //Do not randomize that group (Ending thing?)
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
        //
        public void create_sprite_group()
        {
            //Creations of the guards group :
            random_sprite_group[0] = new byte[] { }; //Do not randomize that group (Ending thing?)
            random_sprite_group[1] = new byte[] { 70, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[2] = new byte[] { 70, get_guard_subset_1(), 19, 83 }; //tongue switch group
            random_sprite_group[3] = new byte[] { 72, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[4] = new byte[] { 72, get_guard_subset_1(), 19, 82 }; //switch group
            random_sprite_group[5] = new byte[] { }; //Do not randomize that group (Npcs, Items, some others thing)
            random_sprite_group[6] = new byte[] { }; //Do not randomize that group (Sanctuary Mantle, Priest)
            random_sprite_group[7] = new byte[] { };//Do not randomize that group (Npcs, Arghus)
            random_sprite_group[8] = fully_randomize_that_group(); //Force Group 8 for Iceman subset2 on 38
            random_sprite_group[8][2] = 38;
            random_sprite_group[9] = new byte[] { };//Do not randomize that group (Armos Knight)
            random_sprite_group[10] = fully_randomize_that_group(); //Force Group 10 for Watersprites subset2 on 34
            random_sprite_group[10][2] = 34;
            random_sprite_group[11] = new byte[] { };//Do not randomize that group (Lanmolas)
            random_sprite_group[12] = new byte[] { }; //Do not randomize that group (Moldorm)
            random_sprite_group[13] = fully_randomize_that_group(); //(Link's House)/Sewer restore uncle (81)
            random_sprite_group[13][0] = 81;
            random_sprite_group[14] = new byte[] { }; //Do not randomize that group (Npcs)
            random_sprite_group[15] = new byte[] { };//Do not randomize that group (Npcs)
            random_sprite_group[16] = new byte[] { }; //Do not randomize that group (Minigame npcs, witch)
            random_sprite_group[17] = fully_randomize_that_group();//Force Group 17 for Shadow(Zoro) Subset 1 on 32
            random_sprite_group[17][1] = 32;
            random_sprite_group[18] = new byte[] { };//Do not randomize that group (Vitreous?,Agahnim)
            random_sprite_group[19] = fully_randomize_that_group();//Force group 19 for Wallmaster Subset2 on 35
            random_sprite_group[19][2] = 35;
            random_sprite_group[20] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[21] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[22] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[23] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[24] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[25] = fully_randomize_that_group();
            random_sprite_group[26] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[27] = fully_randomize_that_group();//Force group 27 for movingwallcanon subset0 on 47
            random_sprite_group[27][0] = 47;
            random_sprite_group[28] = fully_randomize_that_group();//Force group 27 for canon rooms subset2 on 46
            random_sprite_group[28][2] = 46;
            random_sprite_group[29] = fully_randomize_that_group();
            random_sprite_group[30] = fully_randomize_that_group();
            random_sprite_group[31] = fully_randomize_that_group();
            random_sprite_group[32] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[33] = fully_randomize_that_group();
            random_sprite_group[34] = new byte[] { }; //Do not randomize that group (Ganon)
            random_sprite_group[35] = new byte[] { }; //Do not randomize that group (Lanmolas)
            random_sprite_group[36] = fully_randomize_that_group();
            random_sprite_group[37] = fully_randomize_that_group();
            random_sprite_group[38] = fully_randomize_that_group();
            random_sprite_group[39] = fully_randomize_that_group();
            random_sprite_group[39][2] = 35;
            random_sprite_group[39][3] = 82;

            


            //room 88 require bumper, switch
            //room 104 require bumper, wall master
            random_sprite_group[40] = new byte[] { }; //Do not randomize that group (Npcs)
            for (int i = 41; i < 60; i++)
            {
                random_sprite_group[i] = fully_randomize_that_group(); //group from 105 to 124 are empty
            }

            random_sprite_group[29][0] = 47;
            random_sprite_group[29][2] = 46;
            random_sprite_group[30][0] = 14;
            random_sprite_group[31][1] = 32;
            random_sprite_group[31][2] = 28;
            random_sprite_group[38][2] = 38;
            random_sprite_group[38][3] = 82;
            random_sprite_group[33][2] = 34;
            random_sprite_group[36][3] = 83;
            random_sprite_group[37][3] = 82;
            random_sprite_group[41][2] = 35;

            for (int i = 0;i<statis_sprites.Length;i++)
            {
                ROM_DATA[0x6B44C + statis_sprites[i]] = (byte)(ROM_DATA[0x6B44C + statis_sprites[i]] | 0x40);


            }



        }

        public void patch_sprite_group()
        {
            for (int i = 0; i < 60; i++)
            {
                if (random_sprite_group[i].Length != 0)
                {
                    ROM_DATA[0x05C97 + (i * 4)] = random_sprite_group[i][0];
                    ROM_DATA[0x05C97 + (i * 4) + 1] = random_sprite_group[i][1];
                    ROM_DATA[0x05C97 + (i * 4) + 2] = random_sprite_group[i][2];
                    ROM_DATA[0x05C97 + (i * 4) + 3] = random_sprite_group[i][3];
                }
            }
        }

        public void patch_sprite_group_ow()
        {
            for (int i = 0; i < 43; i++)
            {
                if (random_sprite_group_ow[i].Length != 0)
                {
                    
                    ROM_DATA[0x05B97 + (i * 4)] = random_sprite_group_ow[i][0];
                    ROM_DATA[0x05B97 + (i * 4) + 1] = random_sprite_group_ow[i][1];
                    ROM_DATA[0x05B97 + (i * 4) + 2] = random_sprite_group_ow[i][2];
                    ROM_DATA[0x05B97 + (i * 4) + 3] = random_sprite_group_ow[i][3];

                }
            }
        }

        byte[][] dungeons_palettes = new byte[14][];
        public void Randomize_Dungeons_Palettes()
        {
            for (int i = 0; i < 20; i++)
            {
                randomize_wall(i);
                randomize_floors(i);
            }
            //grayscale_all_dungeons();
        }

        public void black_all_dungeons()
        {
            for (int i = 0; i < 3600; i++)
            {
                int j = (i / 180);
                if (((i - (j * 180)) < 120) || ((i - (j * 180)) >= 136))
                {
                    if (((i - (j * 180)) < 38) || ((i - (j * 180)) >= 44))
                    {
                        ROM_DATA[0xDD734 + i] = 0x00;
                    }
                }


            }

            //Remove Dark Room
            int[] dark_rooms = new int[] { 11, 25, 33, 34, 50, 65, 66, 106, 146, 147, 153, 181, 186, 192, 208, 228, 229, 230, 231, 240, 241 };
            for(int i = 0;i<dark_rooms.Length;i++)
            {
                ROM_DATA[0x120090 + ((dark_rooms[i] * 14))] = (byte)((ROM_DATA[0x120090 + ((dark_rooms[i] * 14))] & 0xFE));
            }

        }

        public Color getColor(short c)
        {
            return Color.FromArgb(((c & 0x1F) * 8), ((c & 0x3E0) >> 5) * 8, ((c & 0x7C00) >> 10) * 8);
        }

        public void grayscale_all_dungeons()
        {
            for (int i = 0; i < 3600; i+=2)
            {

                Color c = getColor((byte)((ROM_DATA[0xDD734 + i+1] << 8) + ROM_DATA[0xDD734 + i]));
                if (c.R == 40 && c.G == 40 && c.B == 40)
                {

                }
                else
                {

                    int sum = ((c.R + c.R + c.G + c.G + c.G + c.B) / 6);
                    setColor(0xDD734 + i, Color.FromArgb(sum, sum, sum), 0);
                }
            }

            //Remove Dark Room
            /*int[] dark_rooms = new int[] { 11, 25, 33, 34, 50, 65, 66, 106, 146, 147, 153, 181, 186, 192, 208, 228, 229, 230, 231, 240, 241 };
            for (int i = 0; i < dark_rooms.Length; i++)
            {
                ROM_DATA[0x120090 + ((dark_rooms[i] * 14))] = (byte)((ROM_DATA[0x120090 + ((dark_rooms[i] * 14))] & 0xFE));
            }*/

        }



        public void randomize_wall(int dungeon)
        {

            
            Color wall_color = Color.FromArgb(60+rand.Next(180), 60+rand.Next(180), 60+rand.Next(180));
            //byte shade = (byte)(6 + rand.Next(8) -((wall_color.R+wall_color.G+wall_color.B)/80));

            for (int i = 0; i < 5; i++)
            {
                
                byte shadex = (byte)(10 - (i * 2));
                setColor((0x0DD734 + (0xB4 * dungeon)) + (i * 2), wall_color, shadex);
                setColor((0x0DD770 + (0xB4 * dungeon)) + (i * 2), wall_color, shadex);
                setColor((0x0DD744 + (0xB4 * dungeon)) + (i * 2), wall_color, shadex);
                if (dungeon == 0)
                {
                    setColor((0x0DD7CA + (0xB4 * dungeon)) + (i * 2), wall_color, shadex);
                }
                /*setColor(0x0DD74C - (i * 2), Color.LimeGreen, (byte)(i * 2));
                setColor(0x0DD778 - (i * 2), Color.LimeGreen, (byte)(i * 2));
                setColor(0x0DD73C - (i * 2), Color.LimeGreen, (byte)(i * 2));*/
            }

            if (dungeon == 2)
            {
                setColor((0x0DD74E + (0xB4 * dungeon)), wall_color, 3);
                setColor((0x0DD74E+2 + (0xB4 * dungeon)), wall_color, 7);
                setColor((0x0DD73E + (0xB4 * dungeon)), wall_color, 3);
                setColor((0x0DD73E + 2 + (0xB4 * dungeon)), wall_color, 7);
                
            }

            //setColor(0x0DD76A + (0xB4 * dungeon), wall_color, (byte)(shade - 6));

            //Ceiling
            setColor(0x0DD7E4 + (0xB4 * dungeon), wall_color, (byte)(2)); //outer wall darker
            setColor(0x0DD7E6 + (0xB4 * dungeon), wall_color, (byte)(4)); //outter wall brighter


            Color pot_color = Color.FromArgb(60 + rand.Next(180), 60 + rand.Next(180), 60 + rand.Next(180));
            //Pots
            setColor(0x0DD75A + (0xB4 * dungeon), pot_color, 6);
            setColor(0x0DD75C + (0xB4 * dungeon), pot_color, 1);
            setColor(0x0DD75E + (0xB4 * dungeon), pot_color, 3);

            //Wall Contour?
            //f,c,m
            setColor(0x0DD76A + (0xB4 * dungeon), wall_color, 7);
            setColor(0x0DD76C + (0xB4 * dungeon), wall_color, 3);
            setColor(0x0DD76E + (0xB4 * dungeon), wall_color, 5);

            //Decoration?


            //WHAT ARE THOSE !!
            //setColor((0x0DD7DA + (0xB4 * dungeon)), wall_color, (byte)(shade - (0 * 4)));
            //setColor((0x0DD7DA + 2 + (0xB4 * dungeon)), wall_color, (byte)(shade - (1 * 4)));
        }

        public void randomize_floors(int dungeon)
        {


            Color floor_color1 = Color.FromArgb(60 + rand.Next(180), 60 + rand.Next(180), 60 + rand.Next(180));
            Color floor_color2 = Color.FromArgb(60 + rand.Next(180), 60 + rand.Next(180), 60 + rand.Next(180));
            Color floor_color3 = Color.FromArgb(60 + rand.Next(180), 60 + rand.Next(180), 60 + rand.Next(180));

            /*if (dungeon == 7)
            {
                Console.WriteLine("Dungeon = 7");
                for (int i = 0; i < 3; i++)
                {
                    setColor(0x0DD764 + (0xB4 * dungeon) + (i * 2), floor_color1, (byte)((shade1-1 ) - (i * 3)));
                    setColor(0x0DD782 + (0xB4 * dungeon) + (i * 2), floor_color1, (byte)((shade1) - (i * 3)));
                    setColor(0x0DD7A0 + (0xB4 * dungeon) + (i * 2), floor_color1, (byte)((shade2-1 ) - (i * 3)));
                    setColor(0x0DD7BE + (0xB4 * dungeon) + (i * 2), floor_color1, (byte)((shade2) - (i * 3)));

                    if (i <= 1)
                    {
                        setColor((0x0DD764 + (0xB4 * dungeon) + 8) + (i * 2), floor_color1, (byte)((shade1-1 ) - (i * 3)));
                        setColor((0x0DD782 + (0xB4 * dungeon) + 8) + (i * 2), floor_color1, (byte)((shade1) - (i * 3)));
                        setColor((0x0DD7A0 + (0xB4 * dungeon) + 8) + (i * 2), floor_color1, (byte)((shade2-1 ) - (i * 3)));
                        setColor((0x0DD7BE + (0xB4 * dungeon) + 8) + (i * 2), floor_color1, (byte)((shade2) - (i * 3)));
                    }
                }
                setColor(0x0DD7E2 + (0xB4 * dungeon), floor_color1, 3);
                setColor(0x0DD796 + (0xB4 * dungeon), floor_color1, 3);
            }
            else
            {*/
                for (int i = 0; i < 3; i++)
                {
                byte shadex = (byte)(6 - (i * 2));
                    setColor(0x0DD764 + (0xB4 * dungeon) + (i * 2), floor_color1, shadex);
                    setColor(0x0DD782 + (0xB4 * dungeon) + (i * 2), floor_color1, (byte)(shadex+3));

                setColor(0x0DD7A0 + (0xB4 * dungeon) + (i * 2), floor_color2, shadex);
                setColor(0x0DD7BE + (0xB4 * dungeon) + (i * 2), floor_color2, (byte)(shadex + 3));
                //setColor(0x0DD7A0 + (0xB4 * dungeon) + (i * 2), floor_color2, (byte)((shade2-1 ) - (i * 3)));
                //setColor(0x0DD7BE + (0xB4 * dungeon) + (i * 2), floor_color2, (byte)((shade2) - (i * 3)));

                /*if (i <= 1)
                {
                    setColor((0x0DD764 + (0xB4 * dungeon) + 8) + (i * 2), floor_color3, (byte)((shade3-1 ) - (i * 3)));
                    setColor((0x0DD782 + (0xB4 * dungeon) + 8) + (i * 2), floor_color3, (byte)((shade3) - (i * 3)));
                    setColor((0x0DD7A0 + (0xB4 * dungeon) + 8) + (i * 2), floor_color3, (byte)((shade3-1 ) - (i * 3)));
                    setColor((0x0DD7BE + (0xB4 * dungeon) + 8) + (i * 2), floor_color3, (byte)((shade3 ) - (i * 3)));
                }*/
            }

                setColor(0x0DD7E2 + (0xB4 * dungeon), floor_color3, 3);
                setColor(0x0DD796 + (0xB4 * dungeon), floor_color3, 4);
            //}
        }

        public void setColor(int address, Color col, byte shade)
        {

            int r = col.R;
            int g = col.G;
            int b = col.B;

            for (int i = 0; i < shade; i++)
            {
                r = (r - (r / 5));
                g = (g - (g/ 5));
                b = (b - (b / 5));
            }
            r = (r / 8);
            g = (g / 8);
            b = (b / 8);
            short s = (short)(((b) << 10) | ((g) << 5) | ((r) << 0));

            ROM_DATA[address] = (byte)(s & 0x00FF);
            ROM_DATA[address + 1] = (byte)((s >> 8) & 0x00FF);


        }



        public void Randomize_Sprites_Palettes()
        {
            //Do not change color of collectible items
            for (int j = 0; j < 0xF3; j++)
            {
                if (j <= 0xD7 || j >= 0xE7)
                {
                    ROM_DATA[0x6B359 + j] = (byte)((ROM_DATA[0x6B359 + j] & 0xF1) + (rand.Next(15) & 0x0E));
                }
            }
            //sprite_palette_new();

        }

        public void Randomize_Overworld_Palettes()
        {
            Color grass = Color.FromArgb(60 + (rand.Next(155)/2), 60 + rand.Next(155), 60 + rand.Next(155));
            Color grass2 = Color.FromArgb(60 + (rand.Next(155) / 2), 60 + rand.Next(155), 60 + rand.Next(155));
            Color grass3 = Color.FromArgb(60 + (rand.Next(155) / 2), 60 + rand.Next(155), 60 + rand.Next(155));
            Color dirt = Color.FromArgb(60 + rand.Next(155), 60 + rand.Next(155), 60 + rand.Next(155));
            Color dirt2 = Color.FromArgb(60 + rand.Next(155), 60 + rand.Next(155), 60 + rand.Next(155));
            //Color grass = Color.FromArgb(230, 230, 230);
            //Color dirt = Color.FromArgb(140,120,64);
            Color wall = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Color roof = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            Color btreetrunk = Color.FromArgb(172, 144, 96);
            Color treetrunk = Color.FromArgb(btreetrunk.R - 40 + rand.Next(80), btreetrunk.G - 20 + rand.Next(30), btreetrunk.B - 30 + rand.Next(60));
            Color treeleaf = Color.FromArgb(grass.R-20 + rand.Next(30), grass.G-20+rand.Next(30), grass.B-20+rand.Next(30));
            Color bridge = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            setColor(0x05FEA9, grass, 0);

            setColor(0x0DD4AC, grass, 2); //desert shadow
            setColor(0x0DE6DE, grass2, 2);
            setColor(0x0DE75C, grass2, 2);
            setColor(0x0DE786, grass2, 2);
            setColor(0x0DE794, grass2, 2);
            setColor(0x0DE99A, grass2, 2);

            setColor(0x0DE6E0 , grass2, 1);
            setColor(0x0DE6E2 , grass2, 0);

            setColor(0x0DD4AE , grass2, 1);
            setColor(0x0DE6E0 , grass2, 1);
            setColor(0x0DE9FA , grass2, 1);
            setColor(0x0DEA0E , grass2, 1);

            setColor(0x0DE9FE, grass2, 0);


            setColor(0x0DD3D2, grass2, 2);
            setColor(0x0DE88C, grass2, 2);
            setColor(0x0DE8A8, grass2, 2);
            setColor(0x0DE9F8, grass2, 2);
            setColor(0x0DEA4E, grass2, 2);
            setColor(0x0DEAF6, grass2, 2);
            setColor(0x0DEB2E, grass2, 2);
            setColor(0x0DEB4A, grass2, 2);

            int i = 0;
                setColor(0x0DE892 + (i * 70), grass, 1);
                setColor(0x0DE886 + (i * 70), grass, 0);

                setColor(0x0DE6D0 + (i * 70), grass, 1);//grass shade
                setColor(0x0DE6D2 + (i * 70), grass, 0); //grass



                setColor(0x0DE6FA + (i * 70), grass, 3);
                setColor(0x0DE6FC + (i * 70), grass, 0);//grass shade2
                setColor(0x0DE6FE + (i * 70), grass, 0);//??

                setColor(0x0DE884 + (i * 70), grass, 4);//tree shadow
                

                setColor(0x0DE70A + (i * 70), grass, 0); //grass?
                setColor(0x0DE708 + (i * 70), grass, 2); //bush?

                setColor(0x0DE70C + (i * 70), grass, 1); //bush?

                //Color.FromArgb(60 + rand.Next(155), 60 + rand.Next(155), 60 + rand.Next(155));
                setColor(0x0DE6D4 + (i * 70), dirt, 2);

                setColor(0x0DE6CA + (i * 70), dirt, 5);
                setColor(0x0DE6CC + (i * 70), dirt, 4);
                setColor(0x0DE6CE + (i * 70), dirt, 3);
                setColor(0x0DE6E2 + (i * 70), dirt, 2);

                setColor(0x0DE6D8 + (i * 70), dirt, 5);
                setColor(0x0DE6DA + (i * 70), dirt, 4);
                setColor(0x0DE6DC + (i * 70), dirt, 2);
                setColor(0x0DE6F0 + (i * 70), dirt, 2);

                setColor(0x0DE6E6 + (i * 70), dirt, 5);
                setColor(0x0DE6E8 + (i * 70), dirt, 4);
                setColor(0x0DE6EA + (i * 70), dirt, 2);
                setColor(0x0DE6EC + (i * 70), dirt, 4);
                setColor(0x0DE6EE + (i * 70), dirt, 2);
                setColor(0x0DE6F0 + (i * 70), dirt, 2);

            i = 1;
            setColor(0x0DE892 + (i * 70), grass3, 1);
            setColor(0x0DE886 + (i * 70), grass3, 0);

            setColor(0x0DE6D0 + (i * 70), grass3, 1);//grass shade
            setColor(0x0DE6D2 + (i * 70), grass3, 0); //grass



            setColor(0x0DE6FA + (i * 70), grass3, 3);
            setColor(0x0DE6FC + (i * 70), grass3, 0);//grass shade2
            setColor(0x0DE6FE + (i * 70), grass3, 0);//??

            setColor(0x0DE884 + (i * 70), grass3, 4);//tree shadow


            setColor(0x0DE70A + (i * 70), grass3, 0); //grass?
            setColor(0x0DE708 + (i * 70), grass3, 2); //bush?

            setColor(0x0DE70C + (i * 70), grass3, 1); //bush?

            //Color.FromArgb(60 + rand.Next(155), 60 + rand.Next(155), 60 + rand.Next(155));
            setColor(0x0DE6D4 + (i * 70), dirt, 2);

            setColor(0x0DE6CA + (i * 70), dirt, 5);
            setColor(0x0DE6CC + (i * 70), dirt, 4);
            setColor(0x0DE6CE + (i * 70), dirt, 3);
            setColor(0x0DE6E2 + (i * 70), dirt, 2);

            setColor(0x0DE6D8 + (i * 70), dirt, 5);
            setColor(0x0DE6DA + (i * 70), dirt, 4);
            setColor(0x0DE6DC + (i * 70), dirt, 3);
            setColor(0x0DE6F0 + (i * 70), dirt, 2);

            setColor(0x0DE6E6 + (i * 70), dirt, 5);
            setColor(0x0DE6E8 + (i * 70), dirt, 4);
            setColor(0x0DE6EA + (i * 70), dirt, 3);
            setColor(0x0DE6EC + (i * 70), dirt, 3);
            setColor(0x0DE6EE + (i * 70), dirt, 2);
            setColor(0x0DE6F0 + (i * 70), dirt, 1);
            

            //setColor(0x0DE6C8 + (i * 70), dirt2, 6);
            //setColor(0x0D7B40, dirt2, 4);
            //setColor(0x0D7CE0, dirt2, 4);
            //setColor(0x0D7E43, dirt2, 4);
            //setColor(0x0DB8A1, dirt2, 4);
            //setColor(0x0DCE9A, dirt2, 4);
            //setColor(0x0DD804, dirt2, 4);
            //setColor(0x0DD840, dirt2, 4);
            setColor(0x0DE31C, dirt2, 4);
            setColor(0x0DE660, dirt2, 4);
            setColor(0x0DE712, dirt2, 4);
            setColor(0x0DE720, dirt2, 4);
            setColor(0x0DE72E, dirt2, 4);
            setColor(0x0DE758, dirt2, 4);
            setColor(0x0DE766, dirt2, 4);
            setColor(0x0DE774, dirt2, 4);
            setColor(0x0DE996, dirt2, 4); //0x0DE992
            setColor(0x0DE9A4, dirt2, 4);
            setColor(0x0DEAD8, dirt2, 4);

            setColor(0x0DE654, grass3, 0);
            setColor(0x0DE75E, grass3, 0);
            setColor(0x0DE788, grass3, 0);
            setColor(0x0DE796, grass3, 0);
            setColor(0x0DE972, grass3, 0);
            setColor(0x0DE98E, grass3, 0);
            setColor(0x0DE99C, grass3, 0);

           /* setColor(0x0DE714, dirt2, 4);
            setColor(0x0DE722, dirt2, 4);
            setColor(0x0DE730, dirt2, 4);
            setColor(0x0DE732, dirt2, 4);
            setColor(0x0DE75A, dirt2, 4);
            setColor(0x0DE768, dirt2, 4);
            setColor(0x0DE776, dirt2, 4);
            setColor(0x0DE778, dirt2, 4);
            setColor(0x0DE998, dirt2, 4);
            setColor(0x0DE9A6, dirt2, 4);
            setColor(0x0DEABA, dirt2, 4);
            setColor(0x0DEADA, dirt2, 4);



            setColor(0x0DE664, dirt2, 4);
            setColor(0x0DE71A, dirt2, 4);
            setColor(0x0DE728, dirt2, 4);
            setColor(0x0DE734, dirt2, 4);
            setColor(0x0DE736, dirt2, 4);
            setColor(0x0DE760, dirt2, 4);
            setColor(0x0DE77A, dirt2, 4);
            setColor(0x0DE77C, dirt2, 4);
            setColor(0x0DE798, dirt2, 4);
            setColor(0x0DE980, dirt2, 4);
            setColor(0x0DE99E, dirt2, 4);
            setColor(0x0DE9AC, dirt2, 4);
            setColor(0x0DEAC4, dirt2, 4);*/


            //setColor(0x0D821A, dirt2, 5);
            //setColor(0x0D8982, dirt2, 5);
            //setColor(0x0D898E, dirt2, 5);
            //setColor(0x0D8997, dirt2, 5);
            //setColor(0x0D89A4, dirt2, 5);
            //setColor(0x0D89BF, dirt2, 5);
            //setColor(0x0D89CC, dirt2, 5);
            //setColor(0x0D8A22, dirt2, 5);

            /*setColor(0x0DE710, dirt2, 5);
            setColor(0x0DE756, dirt2, 5);
            setColor(0x0DE764, dirt2, 5);
            setColor(0x0DE772, dirt2, 5);
            setColor(0x0DE994, dirt2, 5);
            setColor(0x0DE9A2, dirt2, 5);
            setColor(0x0DEAD6, dirt2, 5);*/

            setColor(0x0DE992, dirt2, 5);
            setColor(0x0DE994, dirt2, 4);
            setColor(0x0DE996, dirt2, 3);
            setColor(0x0DE998, dirt2, 2);

            setColor(0x0DE99A, grass3, 0);
            setColor(0x0DE99C, grass3, 0);

            i = 2;
            setColor(0x0DE892 + (i * 70), grass3, 1);
            setColor(0x0DE886 + (i * 70), grass3, 0);

            setColor(0x0DE6D0 + (i * 70), grass3, 1);//grass shade
            setColor(0x0DE6D2 + (i * 70), grass3, 0); //grass



            setColor(0x0DE6FA + (i * 70), grass3, 3);
            setColor(0x0DE6FC + (i * 70), grass3, 0);//grass shade2
            setColor(0x0DE6FE + (i * 70), grass3, 0);//??

            setColor(0x0DE884 + (i * 70), grass3, 4);//tree shadow


            setColor(0x0DE70A + (i * 70), grass3, 0); //grass?
            setColor(0x0DE708 + (i * 70), grass3, 2); //bush?

            setColor(0x0DE70C + (i * 70), grass3, 1); //bush?

            //Color.FromArgb(60 + rand.Next(155), 60 + rand.Next(155), 60 + rand.Next(155));
            setColor(0x0DE6D4 + (i * 70), dirt2, 2);
            setColor(0x0DE6CA + (i * 70), dirt2, 5);
            setColor(0x0DE6CC + (i * 70), dirt2, 4);
            setColor(0x0DE6CE + (i * 70), dirt2, 3);
            setColor(0x0DE6E2 + (i * 70), dirt2, 2);

            setColor(0x0DE8C0 + (i * 70), dirt2, 6);
            setColor(0x0DE8CE + (i * 70), dirt2, 5);

            setColor(0x0DE6C6 + (i * 70), dirt2, 6);
            setColor(0x0DE6D8 + (i * 70), dirt2, 5);
            setColor(0x0DE6DA + (i * 70), dirt2, 4);
            setColor(0x0DE6DC + (i * 70), dirt2, 2);
            setColor(0x0DE6F0 + (i * 70), dirt2, 1);

            setColor(0x0DE6E4 + (i * 70), dirt2, 6);
            setColor(0x0DE6E6 + (i * 70), dirt2, 5);
            setColor(0x0DE6E8 + (i * 70), dirt2, 4);
            setColor(0x0DE6EA + (i * 70), dirt2, 3);
            setColor(0x0DE6EC + (i * 70), dirt2, 3);
            setColor(0x0DE6EE + (i * 70), dirt2, 2);
            setColor(0x0DE6F0 + (i * 70), dirt2, 1);



            //lake borders
            setColor(0x0DE91E, grass, 0);
            setColor(0x0DE920, dirt, 2);
            setColor(0x0DE916, dirt, 3);

            setColor(0x0DE92C, grass, 0);
            setColor(0x0DE93A, grass, 0);
            setColor(0x0DE93C, dirt, 2);


            setColor(0x0DE91C, grass, 1);

            setColor(0x0DE92A, grass, 1);
            setColor(0x0DE938, grass, 1);//darker?

            //zora domain
            setColor(0x0DEA1C, grass, 0);
            setColor(0x0DEA2A, grass, 0);
            setColor(0x0DEA30, grass, 0);

            setColor(0x0DEA2E, dirt, 5);
            setColor(0X067FE1, grass, 3); //Zora Domain Shadow

            setColor(0X0DE6D0, grass, 3); //Test2
            setColor(0x0DE884, grass, 3);
            setColor(0x0DE8AE, grass, 3);
            setColor(0x0DE8BE, grass, 3);
            setColor(0x0DE8E4, grass, 3);
            setColor(0x0DE938, grass, 3);
            setColor(0x0DE9C4, grass, 3);
            //Nothing Happen : 0x01E0F8,0x04E2DB,0x05FE75,0X067FAF
            //map changed : 0x0216B8

            setColor(0x0DE6D0, grass, 4);//tree shadow
                                         /*setColor(0x0DE87C, bridge, 6);
                                             setColor(0x0DE87E, bridge, 4);
                                             setColor(0x0DE880, bridge, 2);
                                             setColor(0x0DE882, bridge, 0);

                                             setColor(0x0DE86E, wall, 6);
                                             setColor(0x0DE870, wall, 4);
                                             setColor(0x0DE872, wall, 2);
                                             setColor(0x0DE878, wall, 0);


                                             */
            /*setColor(0x0DE88A, treetrunk, 2);
            setColor(0x0DE88C, treetrunk, 1);
            setColor(0x0DE88E, treetrunk, 0);
            */
            setColor(0x0DE890, treeleaf, 1);
            setColor(0x0DE894, treeleaf, 0);

                /*setColor(0x0DE874, roof, 4);
                setColor(0x0DE876, roof, 0);*/

        }

        public void Randomize_Sprites_HP(int rangeValue)
        {
            for (int j = 0; j < 0xF3; j++)
            {
                if (ROM_DATA[0x6B173 + j] != 0xFF)
                {
                    if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
                        && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3
                       && j != 0x8D && j != 0x7A && j != 0x7B && j != 0xCC && j != 0xCD && j != 0xA4 && j != 0xD6 && j != 0xD7)
                    {
                        int new_hp = ROM_DATA[0x6B173 + j] + rand.Next(-rangeValue, rangeValue);
                        if (new_hp >= 0xFF)
                        {
                            new_hp = 0xFF;
                        }
                        if (new_hp <= 0)
                        {
                            new_hp = 1;
                        }
                        ROM_DATA[0x6B173 + j] = (byte)new_hp;
                    }
                }
            }
        }

        public void shuffle_music()
        {
            for (int i = 0; i < 0x70; i++)
            {
                byte[] musics = new byte[] { 0x03, 0x07, 0x0B, 0x0E, 0x10, 0x11, 0x12, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x22, 0x23};
                byte m = (byte)rand.Next(musics.Length);
                m = musics[m];
                ROM_DATA[0x015592+i] = m;
            }
            byte[] originalmusicafter0x85 = new byte[] { 0x12, 0x1B, 0x12, 0xFF, 0xFF, 0xFF, 0x11, 0x11, 0x11, 0x11 };
            for (int i = 0; i < 0x0A; i++)
            {
                ROM_DATA[0x015602 + i] = originalmusicafter0x85[i];
            }
        }
        byte[] original_damage = new byte[] {2,4,0,8,8,16,32,32,24,64, 32, 32, 32, 32, 32, 32 };
        public void Randomize_Sprites_DMG(bool allowZeroDamage)
        {
            for (int j = 0; j < 0xF3; j++)
            {
                if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
&& j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3 && j != 0x8D
&& j != 0x7A && j != 0x7B && j != 0xCC && j != 0xCD && j != 0xA4 && j != 0xD6 && j != 0xD7)
                {

                    ROM_DATA[0x6B266 + j] = (byte)((ROM_DATA[0x6B266 + j] & 0xF8) + (byte)(rand.Next(8)));
                }
            }
        }
    

        public void Set_Sprites_ZeroHP()
        {
            for (int j = 0; j < 0xF3; j++)
            {
                if (ROM_DATA[0x6B173 + j] != 0xFF)
                {
                    if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
                        && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3
                       && j != 0x8D && j != 0x7A && j != 0x7B && j != 0xCC && j != 0xCD && j != 0xA4 && j != 0xD6 && j != 0xD7)
                    {
                        int new_hp = 1;
                        ROM_DATA[0x6B173 + j] = (byte)new_hp;
                    }
                }
            }
        }
        //SEPARATE HOUSE/CAVES FROM DUNGEONS RANDOMIZATION ENEMIES

        public enum room_position
        {
            topleft,topright,bottomleft,bottomright,middle
        }


        //Deprecated now using xkas to compile code
        /*public void move_header()
        {
            ROM_DATA[0x0B5E7] = 0x24;//change room header bank to bank to 24

            for (int i = 0; i < 320; i++)
            {
                //get pointer of that room
                byte[] roomPointer = new byte[4];//27502
                roomPointer[0] = ROM_DATA[(0x271E2 + (i * 2) + 0)];
                roomPointer[1] = ROM_DATA[(0x271E2 + (i * 2) + 1)];
                roomPointer[2] = 04;
                int address = BitConverter.ToInt32(roomPointer, 0);
                int pcadd = snestopc(address);

                for (int j = 0; j < 14; j++)
                {
                    ROM_DATA[0x120090 + (i * 14) + j] = ROM_DATA[pcadd + j];
                }
            }


            for (int i = 0; i < 320; i++)
            {
                //0x0271E2  //rewrite all room header address
                //0x120090
                ROM_DATA[0x0271E2 + (i * 2)] = ((byte)pctosnes(0x120090 + (i * 14)));
                ROM_DATA[0x0271E2 + (i * 2) + 1] = ((byte)(pctosnes((0x120090 + (i * 14))) >> 8));

            }
        }*/

        public int snestopc(int addr)
        {
            int temp = (addr & 0x7FFF) + ((addr / 2) & 0xFF8000);
            return (temp);
        }

        //Convert PC Address to Snes Address
        public int pctosnes(int addr)
        {
            byte[] b = BitConverter.GetBytes(addr);
            b[2] = (byte)(b[2] * 2);
            if (b[1] >= 0x80)
                b[2] += 1;
            else
                b[1] += 0x80;

            return BitConverter.ToInt32(b, 0);
        }


        int[][] overworld_sprites = new int[208][];
        int[] removed_sprites = new int[] { 0x04CF51, };
        int[] water_sprites = new int[] { 0x04D005, 0x04D18E, 0x04D227, 0x04D22A, 0x04D236, 0x04D245, 0x04D24B, 0x04D24E, 0x04D26A, 0x04D281, 0x04D28A, 0x04D2B0, 0x04CBF7, 0x04CC76, 0x04CC79, 0x04CC86, 0x04CDE6, 0x04CDE9, 0x04CDF5, 0x04CDF8, 0x04CDFE, 0x04CE01, 0x04CE50, 0x04CE5C, 0x04CE6E, 0x04CE8D, 0x04CE90, 0x04CE8D, 0x04CE90, 0x04CED0, };
        int[] water_rooms = new int[] { 0x00000F, 0x00002E, 0x000035, 0x000037, 0x00003B, 0x00003F, 0x00004F, 0x000056, 0x000057, 0x000070, 0x000075, 0x000076, 0x000077, 0x00007F, };
  

        int[][] room_sprites = new int[292][];

        //ROM_DATA[0x0271E2 + (i * 2)] = ((byte)pctosnes(0x120090 + (i* 14)));
        //ROM_DATA[0x0271E2 + (i * 2) + 1] = ((byte)(pctosnes((0x120090 + (i* 14))) >> 8));
        byte[][] shell_pointers = new byte[13][];
        /*int snes_shell_pointer_7 = 0;
        int snes_shell_pointer_200 = 0;
        int snes_shell_pointer_41 = 0; //USELESS
        int snes_shell_pointer_51 = 0;
        int snes_shell_pointer_90 = 0;
        int snes_shell_pointer_144 = 0;
        int snes_shell_pointer_172 = 0;
        int snes_shell_pointer_6 = 0;
        int snes_shell_pointer_222 = 0;
        int snes_shell_pointer_164 = 0;
        int snes_shell_pointer_22 = 0;
        int snes_shell_pointer_108 = 0;
        int snes_shell_pointer_77 = 0;*/
        public void patch_bosses()
        {
           



            //write_rom_data(0x123000, ice_shell);
            /*
 * 0xF8000*room
7 : 0FCAEE, Length:0139
200 : 051585, Length:0015
41 : 0FC186, Length:006A //USELESSS NO POINTER
51 : 0F878A, Length:000C
90 : 0FA7CB, Length:001E
144 : 0FBA9E, Length:0018
172 : 0FD9AF, Length:002D
6 : 0FA15A, Length:003C
222 : 0FCAE1, Length:000D
164 : 0FE700, Length:0045
28 : 0FF749, Length:0043
108 :0FFA56, Length:0052
77 : 0FFD41, Length:011F
*/
            //0x0122000 bosses rooms tiles : 
            int pos = 0;
            shell_pointers[0] = pctosnesbytes(0x122000 + pos);
            shell_pointers[2] = pctosnesbytes(0x122000 + pos); //Skull woods empty pointers
            write_rom_data(0x0122000 + pos, room_7_shell);
            pos += room_7_shell.Length;

            shell_pointers[1] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_200_shell);
            pos += room_200_shell.Length;


            shell_pointers[3] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_51_shell);
            pos += room_51_shell.Length;

            shell_pointers[4] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_90_shell);
            pos += room_90_shell.Length;

            shell_pointers[5] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_144_shell);
            pos += room_144_shell.Length;

            shell_pointers[6] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_172_shell);
            pos += room_172_shell.Length;

            shell_pointers[7] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_6_shell);
            pos += room_6_shell.Length;

            shell_pointers[8] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_222_shell);
            pos += room_222_shell.Length;

            shell_pointers[9] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_164_shell);
            pos += room_164_shell.Length;

            shell_pointers[10] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_28_shell);
            pos += room_28_shell.Length;

            shell_pointers[11] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_108_shell);
            pos += room_108_shell.Length;

            shell_pointers[12] = pctosnesbytes(0x122000 + pos);
            write_rom_data(0x0122000 + pos, room_77_shell);
            pos += room_77_shell.Length;
        }

        public byte[] pctosnesbytes(int pos)
        {
            int addr = pctosnes(pos);

            //ROM_DATA[2] = ((byte)addr);
            //ROM_DATA[1] = ((byte)(addr >> 8));
            //ROM_DATA[0] = ((byte)(addr >> 16));

            return new byte[] { (byte)(addr >> 16), ((byte)(addr >> 8)), ((byte)addr) };
        }


        public void write_rom_data(int pos, byte[] data)
        {
            for(int i =0;i<data.Length;i++)
            {
                ROM_DATA[pos + i] = data[i];
            }
        }










    }








}
