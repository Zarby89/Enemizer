using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MoveHeaderASM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Random rand;
        byte[] ROM_DATA;
        int[] room = {2,4,9,10,11,14,17,19,21,22,23,25,26,27,30,31,33,34,36,38,39,40,42,43,46,49,50,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,73,74,75,76,
78,80,81,82,83,84,85,86,87,88,89,91,93,94,95,96,97,98,99,100,101,102,103,104,106,107,109,110,113,114,115,116,117,118,119,123,124,125,126,127,128,129,130,131,132,133,
135,139,140,141,142,145,146,147,149,152,153,155,156,157,158,159,160,161,165,166,167,168,169,170,171,174,176,177,178,179,182,183,184,186,187,188,190,192,193,194,195,
196,201,203,204,208,209,210,216,217,218,219,220,223,224,232,238,239,249,251,235,254,263,264,269,291,267,185};//,268

            //add 0x7E,0x7F
        byte[] dungeons_sprite = {0x02,0x13,0x15,0x18,0x19,0x20,0x22,0x23,0x24,0x26,0x41,0x42,0x43,0x44,0x45,
        0x46,0x48,0x49,0x4B,0x4E,0x4F,0x5B,0x5C,0x5D,0x5E,0x5F,0x60,0x61,0x63,0x64,0x6A,0x6B,0x6D,0x6E,
        0x6F,0x71,0x77,0x7D,0x80,0x81,0x83,0x84,0x85,0x86,0x8A,0x8B,0x8F,0x91,0x93,0x99,0x9B,0x9A,0xA6,
        0xA7,0xAA,0xC3,0xC7,0xD1,0xE3,0x7C};

        //
        byte[] IcemanRoom = { 14, 126, 142, 158, 190 }; //these room need to be locked on the gfx ID : 28
        byte[] WaterRoom = { 40, 56, 54, 70, 52, 22,102,118 }; //these room need to be locked on the gfx ID : 17
        byte[] ShadowRoom = {62,159};//28,27,30
        byte[] WallMasterRoom = {57,73,86,87,88,141 };
        byte[] bumperandcrystalRoom = {23,4,11,19,27,30,42,43,49,68,76,86,88,89,91,103,104,107,119,126,135,139,145,146,155,157,161,171,
        182,191,193,196,235};
        int[] SwitchesRoom = {2,100,267,73,88 };
        byte[] TonguesRoom = { 4,206,63 };
        byte[] PushSwitchesRoom = {53,55,118 };
        int[] noStatueRoom = {70,208,38 };
        byte[] canonRoom = {92,117}; //47 on 0 //46 on 3 for room 185
        byte[] canonRoom2 = { 185, 217}; //47 on 0 //46 on 3 for room 185

        //int[] NeedKillable_keys = { 14, 19, 33, 57, 61, 62, 113, 114, 128, 153, 182, 192, 193 };
        int[] key_sprite = new int[] { 0x04DA20, 0x04DA5C, 0x04DB7F, 0x04DD73, 0x04DDC3, 0x04DE07, 0x04E203, 0x04E20B, 0x04E326, 0x04E4F7, 0x04E70C, 0x04E7C8, 0x04E7FA, 0x04E200 };
        int[] NeedKillable_doors = { 11, 27, 36, 40, 49, 68, 75, 83, 93, 107, 109, 110, 117, 123, 125, 133, 135, 141,165, 168, 176, 178, 210, 216, 224, 239, 268, 291 };

        //int[] special_sprites = new int[] { 0x04E60A, 0x04E615, 0x04E618, 0x04E61B, 0x04E61E, 0x04D9E1, 0x04D9E4, 0x04D9ED, 0x04D9EA, 0x04D9E, 0x04DE61, 0x04E0DF, 0x04E3A6, 0x04E417, 0x04E7F1, 0x04E9B4 };
        // 0x04D9E1 , 0x04D9E4 , 0x04D9EA , 0x04D9ED
        byte[] sprite_subset_0 = new byte[] { 22, 31, 47,14}; //70-72 part of guards we already have 4 guard set don't need more
        byte[] sprite_subset_1 = new byte[] { 44, 30, 32 };//73-13
        byte[] sprite_subset_2 = new byte[] { 12, 18, 23, 24, 28, 46, 34, 35, 39, 40, 38, 41, 36, 42 };//19 trainee guard
        byte[] sprite_subset_3 = new byte[] { 17, 16, 27, 20, 82, 83 };
        byte[][] random_sprite_group = new byte[60][];
        byte[][] subset_gfx_sprites = new byte[84][];
        byte[] absorbable_sprites = new byte[] { 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF, 0xE0, 0xE1, 0xE2, 0xE3, 0xE4 };
        public void create_subset_gfx()
        {
            for(int i =0;i<84;i++)
            {
                subset_gfx_sprites[i] = null;
            }
            //subset0
            subset_gfx_sprites[22] = new byte[] {0x22,0x11 };//DW Popo, Hinox, Snapdragon (require 23)
            subset_gfx_sprites[31] = new byte[] { 0x23, 0x24, 0x85, 0xA7, 0x02,0x7E,0x7F,0x80 };//bari,stalfos,firebars
            subset_gfx_sprites[47] = new byte[] { 0x63, 0x64, 0x71 };//delalant,leever
            subset_gfx_sprites[14] = new byte[] { 0x19 };//ghini,thief
            subset_gfx_sprites[70] = new byte[] { 0x6A, 0x6B, 0x49, 0x43, 0x41, 0x42, 0x45, 0x48, 0x44, 0x4A, 0x4B };//need to be combined with 73 and 19 all guards
            subset_gfx_sprites[72] = new byte[] { 0x41, 0x42, 0x43, 0x45, 0x46, 0x47, 0x4B, 0x49 };//need to be combined with 73 and 19 all guards archers
            //subset1
            subset_gfx_sprites[44] = new byte[] { 0x4F, 0x4E, 0x61 }; //popo, beamos
            //13 contains guards same as 73
            subset_gfx_sprites[13] = new byte[] { }; //guards
            subset_gfx_sprites[73] = new byte[] { }; //guards
            subset_gfx_sprites[19] = new byte[] { }; //guards
            subset_gfx_sprites[30] = new byte[] { 0x26, 0x13, 0x18 }; //minihelma,minimoldorm,beetle
            subset_gfx_sprites[32] = new byte[] { 0x9C, 0x9D, 0x91, 0x8F }; //stalfos knight, shadow, blob
            //subset2
            subset_gfx_sprites[12] = new byte[] { 0x08, 0x58, 0x0F }; //octorock,crab,octobaloon
            subset_gfx_sprites[18] = new byte[] { 0x01, 0x4C }; //vulture, jazzhand
            subset_gfx_sprites[23] = new byte[] { 0x12 }; //pigmanspear, snapdragon (require 22) 
            subset_gfx_sprites[24] = new byte[] { 0x08 }; //dwoctorok
            subset_gfx_sprites[28] = new byte[] { 0x6D, 0x6E, 0x6F }; //rat,rope,keese, also the oldman
            subset_gfx_sprites[46] = new byte[] { 0x84, 0x83 }; //green/red eyegores
            subset_gfx_sprites[34] = new byte[] { 0x9A, 0x81 }; //water sprites
            subset_gfx_sprites[35] = new byte[] { 0x8B }; //wallmaster,gibdo
            subset_gfx_sprites[39] = new byte[] { 0xC7, 0xCA, 0x5D, 0x5E, 0x5F, 0x60 };//chain chomp,pokey,rollers
            subset_gfx_sprites[40] = new byte[] { 0xA5, 0xA6, 0xC3 };//zazak,gibo (patrick star)
            subset_gfx_sprites[38] = new byte[] { 0x99 };//(A1) iceman,penguin
            subset_gfx_sprites[41] = new byte[] { 0x9B };//wizzrobe
            subset_gfx_sprites[36] = new byte[] { 0x6D, 0x6E, 0x6F };//rat,rope,keese
            subset_gfx_sprites[42] = new byte[] { 0x86, 0x8E };//turtle,kondongo ,also the digging game guy
            //subset3
            subset_gfx_sprites[16] = new byte[] { 0x51, 0x27, 0xC9 };//armos,deadrock,tektite
            subset_gfx_sprites[17] = new byte[] { 0x00, 0x0D };//raven,buzzblob
            subset_gfx_sprites[27] = new byte[] { 0xA8, 0xA9, 0xAA };//dw bomber/likelike
            subset_gfx_sprites[20] = new byte[] { 0xD0};//lynel

            subset_gfx_sprites[81] = new byte[] { };//switches
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
        byte ps = 0, pt = 0, ws = 0, wm = 0, om = 0, sh = 0, mm = 0, im = 0, cc = 0,c2 = 0;
        public void create_sprite_group()
        {
            //Initialize Random Seed
            int seed = 0;
            if (textBox1.Text != "")
            {
                 seed = Convert.ToInt32((textBox1.Text));
            }
            else
            {
                rand = new Random();
                seed = rand.Next();
                textBox1.Text = seed.ToString();
            }
            rand = new Random(seed);//Need to put seed here use random for now

            //Creations of the guards group :
            random_sprite_group[0] = new byte[] { }; //Do not randomize that group (Ending thing?)
            random_sprite_group[1] = new byte[] { 70, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[2] = new byte[] { 70, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[3] = new byte[] { 72, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[4] = new byte[] { 72, get_guard_subset_1(), 19, sprite_subset_3[rand.Next(sprite_subset_3.Length)] };
            random_sprite_group[5] = new byte[] { }; //Do not randomize that group (Npcs, Items, some others thing)
            random_sprite_group[6] = new byte[] { }; //Do not randomize that group (Sanctuary Mantle, Priest)
            random_sprite_group[7] = new byte[] { };//Do not randomize that group (Npcs, Arghus)
            random_sprite_group[8] = fully_randomize_that_group();
            random_sprite_group[9] = new byte[] { };//Do not randomize that group (Armos Knight)
            random_sprite_group[10] = fully_randomize_that_group();
            random_sprite_group[11] = new byte[] { };//Do not randomize that group (Lanmolas)
            random_sprite_group[12] = new byte[] { }; //Do not randomize that group (Moldorm)
            random_sprite_group[13] = fully_randomize_that_group(); //(Link's House)/Sewer restore uncle (81)
            random_sprite_group[13][0] = 81;
            random_sprite_group[14] = new byte[] { }; //Do not randomize that group (Npcs)
            random_sprite_group[15] = new byte[] { };//Do not randomize that group (Npcs)
            random_sprite_group[16] = new byte[] { }; //Do not randomize that group (Minigame npcs, witch)
            random_sprite_group[17] = fully_randomize_that_group();
            random_sprite_group[18] = new byte[] { };//Do not randomize that group (Vitreous?,Agahnim)
            random_sprite_group[19] = fully_randomize_that_group();
            random_sprite_group[20] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[21] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[22] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[23] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[24] = new byte[] { }; //Do not randomize that group (Bosses)
            random_sprite_group[25] = fully_randomize_that_group();
            random_sprite_group[26] = new byte[] { };//Do not randomize that group (Bosses)
            random_sprite_group[27] = fully_randomize_that_group();
            random_sprite_group[28] = fully_randomize_that_group();
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
            random_sprite_group[40] = new byte[] { }; //Do not randomize that group (Npcs)
            for(int i = 41;i<60;i++)
            {
                random_sprite_group[i] = fully_randomize_that_group(); //group from 105 to 124 are empty
            }
            
            for (int i = 0; i < 60; i++)
            {
                if (random_sprite_group[i].Length != 0)
                {
                    if (random_sprite_group[i][3] == 82)
                    {
                        ps = (byte)i;
                    }
                    if (random_sprite_group[i][3] == 83)
                    {
                        pt = (byte)i;
                    }
                    if (random_sprite_group[i][2] == 34)
                    {
                        ws = (byte)i;
                    }
                    if (random_sprite_group[i][2] == 35)
                    {
                        wm = (byte)i;
                    }
                    if (random_sprite_group[i][2] == 38)
                    {
                        im = (byte)i;
                    }
                    if (random_sprite_group[i][2] == 28)
                    {
                        om = (byte)i;
                    }
                    if (random_sprite_group[i][1] == 32)
                    {
                        sh = (byte)i;
                    }
                    if (random_sprite_group[i][0] == 14)
                    {
                        mm = (byte)i;
                    }
                    if (random_sprite_group[i][0] == 47)
                    {
                        cc = (byte)i;
                    }
                    if (random_sprite_group[i][2] == 46)
                    {
                        c2 = (byte)i;
                    }

                }
            }
            //Check if we have at least one group with subset 3 setted to 82 if not then force guard group 1 for pull switches room
            //Check if we have at least one group with subset 3 setted to 83 if not then force guard group 2 for pull tongue room //push switch too
            //Check if we have at least one group with subset 2 setted to 34 for water sprites room if not then force it somewhere
            //Check if we have at least one group with subset 2 setted to 35 for wall master room if not then force it somewhere
            //Check if we have at least one group with subset 2 setted to 28 for old man cave if not then force it somewhere
            //Check if we have at least one group with subset 2 setted to 38 for iceman in ice rooms if not  then force it somewhere
            //Check if we have at least one group with subset 1 setted to 32 for shadow in ice rooms if not  then force it somewhere
            //Check if we have at least one group with subset 0 setted to 14 for minimoldorm cave
            if (ps == 0)
            {
                random_sprite_group[1][3] = 82;
                ps = 1;
            }

            if (pt == 0)
            {
                random_sprite_group[2][3] = 83;
                pt = 2;
            }

            if (mm == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length != 0)
                    {
                        random_sprite_group[i][0] = 14;
                        mm = (byte)i;
                        break;
                    }
                }
            }

            if (cc == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length != 0)
                    {
                        if (random_sprite_group[i][0] != 14)
                        {
                            random_sprite_group[i][0] = 47;
                            cc = (byte)i;
                            break;
                        }
                        
                    }
                }
            }

            if (sh == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length !=0)
                    {
                            random_sprite_group[i][1] = 32;
                            sh = (byte)i;
                            break;
                    }
                }
            }

            if (c2 == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length != 0)
                    {
                        if (random_sprite_group[i][2] != 38 | random_sprite_group[i][2] != 28 | random_sprite_group[i][2] != 34 | random_sprite_group[i][2] != 35)
                        {
                            random_sprite_group[i][2] = 46;
                            c2 = (byte)i;
                            break;
                        }
                    }
                }
            }



            if (wm == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length !=0)
                    {
                        if (random_sprite_group[i][2] != 38 | random_sprite_group[i][2] != 28 | random_sprite_group[i][2] != 34 | random_sprite_group[i][2] != 46)
                        {
                            random_sprite_group[i][2] = 35;
                            wm = (byte)i;
                            break;
                        }
                    }
                }
            }

            if (om == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length !=0)
                    {
                        if (random_sprite_group[i][2] != 38 | random_sprite_group[i][2] != 35 | random_sprite_group[i][2] != 34 | random_sprite_group[i][2] != 46)
                        {
                            random_sprite_group[i][2] = 28;
                            om = (byte)i;
                            break;
                        }
                    }
                }
            }

            if (im == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length!=0)
                    {
                        if (random_sprite_group[i][2] != 28 | random_sprite_group[i][2] != 35 | random_sprite_group[i][2] != 34 | random_sprite_group[i][2] != 46)
                        {
                            random_sprite_group[i][2] = 38;
                            im = (byte)i;
                            break;
                        }
                    }
                }
            }

            if (ws == 0)
            {
                for (int i = 5; i < 60; i++)
                {
                    if (random_sprite_group[i].Length !=0)
                    {
                        if (random_sprite_group[i][2] != 28 | random_sprite_group[i][2] != 35 | random_sprite_group[i][2] != 38 | random_sprite_group[i][2] != 46)
                        {
                            random_sprite_group[i][2] = 34;
                            ws = (byte)i;
                            break;
                        }
                    }
                }
            }

            


        }

        public void patch_sprite_group()
        {
            for (int i = 0; i < 60; i++)
            {
                if (random_sprite_group[i].Length != 0)
                {
                    ROM_DATA[0x05C97 + (i * 4)] = random_sprite_group[i][0];
                    ROM_DATA[0x05C97 + (i * 4)+1] = random_sprite_group[i][1];
                    ROM_DATA[0x05C97 + (i * 4)+2] = random_sprite_group[i][2];
                    ROM_DATA[0x05C97 + (i * 4)+3] = random_sprite_group[i][3];
                }
            }
        }


        byte[][] dungeons_palettes = new byte[14][];

        //Invincible Sprites
        byte[] NonKillable = {0x02,0x03,0x04,0x05,0x06,0x07,0x0B,0x15,0x1C,0x1E,0x21,0x5B,0x5C,0x5D,0x5E,0x5F,0x60,0x61,0x66,0x67,0x68,0x69,0x77,0x7D,0x7E,0x7F,0x82,0x84,0x8A,
0x8E,0x90,0x93,0x95,0x96,0x97,0x98,0x9A,0xA1,0xAC,0xC5,0xC6,0xD0,0xD1,0xD8,0xD9,0xDA,0xDB,0xDC,0xDD,0xDE,0xDF,0xE0,0xDF,0xE0,0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,0xE7,0x6F,0x14,0x16,0x1A,0x27,0x28,0x29,0x2A,
        0x2F,0x32,0x31,0x35,0x36,0x38,0x37,0x3C,0x3F,0x40,0x4D,0x57,0x72,0x94,0x95,0x96,0x97,0x98,0x9E,0x9F,0xA0,0xA4,0xA8,0xA9,0xAF,0xAE,0xAD,0xB0,0xB1,0xB2,0xB8,0xB7,0xC2,0xCA,0x85,0x81,0x23,0x80};

        private void button1_Click(object sender, EventArgs e)
        {
            if (file_to_patch == "")
            {
                openFileDialog2.ShowDialog();

            }
            create_sprite_group();
            create_subset_gfx();
            //0,2,5,12,13,14,21,31,33,41-56,64,65
            byte[] bugged_cave_palettes = new byte[] { 0, 2, 5, 12, 13, 14, 21, 31,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56, 33, 64, 65 };
            dungeons_palettes[0] = new byte[]{ 0,1,3,8,15,16,18,24,37,44,45,47,60,71,72,105,111,120,121,122,134,136,138,148,154,223,225};//Caves(0) 
            dungeons_palettes[1] = new byte[] { 1,2,32,48,64,65,80,81,82,96,97,98,176,192,208,224};//Castle(1) 
            dungeons_palettes[2] = new byte[] { 4,19,20,21,35,36,164,180,181,182,183,196,197,198,199,213,214};//TurtleRock(2)
            dungeons_palettes[3] = new byte[] { 6,22,38,40,52,53,54,55,56,70,84,102,118};//Swamp(3)
            dungeons_palettes[4] = new byte[] { 7,23,39,49,119,167,135};//Hera(4) 
            dungeons_palettes[5] = new byte[] { 9,10,11,25,26,27,42,43,58,59,74,75,90,106,135};//PoD(5)
            dungeons_palettes[6] = new byte[] { 12,13,28,29,61,76,77,91,92,93,107,108,109,123,124,125,139,140,141,149,150,155,156,157,165,166};//GTower(6)
            dungeons_palettes[7] = new byte[] { 14,30,31,46,62,63,78,79,94,95,110,126,127,142,158,159,173,174,175,189,190,191,205,206,207,221,222};// IcePalace(7)
            dungeons_palettes[8] = new byte[] { 17,33,34,50,66,85,112,113,114,128,129,130};// Escape(8)
            dungeons_palettes[9] = new byte[] { 41,57,73,86,87,88,89,103,104};//SkullWoods(9)
            dungeons_palettes[10] = new byte[] { 51,67,83,99,115,116,117,131,132,133};//Desert(10)
            dungeons_palettes[11] = new byte[] { 68,69,100,101,171,172,187,188,203,204,219,220};//Thieve(11)
            dungeons_palettes[12] = new byte[] { 137,153,168,169,170,184,185,186,216,217,218,201,200};//Eastern(12)
            dungeons_palettes[13] = new byte[] { 143,144,145,146,147,151,152,160,161,162,163,177,178,179,193,194,195,202,209,210,211,212,215};//MiseryMire(13)

           /* FileStream fs = new FileStream(file_to_patch, FileMode.Open, FileAccess.ReadWrite);
            ROM_DATA = new byte[2097152];
            fs.Read(ROM_DATA, 0, (int)2097152);
            fs.Close();*/
            patch_sprite_group();

            List<byte> dynamic_sprite = new List<byte>();
           //Random r = new Random();
            foreach (int b in room)
            {
                
                if (b == 40) //if room is Swamp Entrances
                {
                    //allow water bug to be a killable sprite
                    NonKillable = new byte[]{
                        0x02,0x03,0x04,0x05,0x06,0x07,0x0B,0x15,0x1C,0x1E,0x21,0x5B,0x5C,0x5D,0x5E,0x5F,0x60,0x61,0x66,0x67,0x68,0x69,0x77,0x7D,0x7E,0x7F,0x82,0x84,0x8A,
0x8E,0x90,0x93,0x95,0x96,0x97,0x98,0x9A,0xA1,0xAC,0xC5,0xC6,0xD0,0xD1,0xD8,0xD9,0xDA,0xDB,0xDC,0xDD,0xDE,0xDF,0xE0,0xDF,0xE0,0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,0xE7,0x6F,0x14,0x16,0x1A,0x27,0x28,0x29,0x2A,
        0x2F,0x32,0x31,0x35,0x36,0x38,0x37,0x3C,0x3F,0x40,0x4D,0x57,0x72,0x94,0x95,0x96,0x97,0x98,0x9E,0x9F,0xA0,0xA4,0xA8,0xA9,0xAF,0xAE,0xAD,0xB0,0xB1,0xB2,0xB8,0xB7,0xC2,0xCA,0x85,0x23,0x80};

                }
                else
                {
                    //else nop !
                    NonKillable =new byte []{
                        0x02,0x03,0x04,0x05,0x06,0x07,0x0B,0x15,0x1C,0x1E,0x21,0x5B,0x5C,0x5D,0x5E,0x5F,0x60,0x61,0x66,0x67,0x68,0x69,0x77,0x7D,0x7E,0x7F,0x82,0x84,0x8A,
0x8E,0x90,0x93,0x95,0x96,0x97,0x98,0x9A,0xA1,0xAC,0xC5,0xC6,0xD0,0xD1,0xD8,0xD9,0xDA,0xDB,0xDC,0xDD,0xDE,0xDF,0xE0,0xDF,0xE0,0xE1,0xE2,0xE3,0xE4,0xE5,0xE6,0xE7,0x6F,0x14,0x16,0x1A,0x27,0x28,0x29,0x2A,
        0x2F,0x32,0x31,0x35,0x36,0x38,0x37,0x3C,0x3F,0x40,0x4D,0x57,0x72,0x94,0x95,0x96,0x97,0x98,0x9E,0x9F,0xA0,0xA4,0xA8,0xA9,0xAF,0xAE,0xAD,0xB0,0xB1,0xB2,0xB8,0xB7,0xC2,0xCA,0x85,0x81,0x23,0x80};

                }

                bool needkill = false;
                //Pick a sprite group in the group
                byte g = 0;
                pick_a_group:
                byte ng = (byte)rand.Next(60);
                if (random_sprite_group[ng].Length == 0)
                {
                    goto pick_a_group;
                }
                g = ng;

                if (glitched_checkbox.Checked == false)
                {
                    foreach (byte bb in bumperandcrystalRoom)
                    {
                        if (b == bb)
                        {
                            if (random_sprite_group[ng][3] == 82 || random_sprite_group[ng][3] == 83)
                            {

                            }
                            else
                            {
                                goto pick_a_group;
                            }
                        }
                    }

                    foreach (byte bb in WaterRoom)
                    {
                        if (b == bb)
                        {
                            ng = ws;
                            break;
                        }
                    }
                    foreach (byte bb in ShadowRoom)
                    {
                        if (b == bb)
                        {
                            ng = sh;
                            break;
                        }
                    }
                    foreach (int bb in SwitchesRoom)
                    {
                        if (b == bb)
                        {
                            ng = ps;
                            break;
                        }
                    }
                    foreach (int bb in TonguesRoom)
                    {
                        if (b == bb)
                        {
                            ng = pt;
                            break;
                        }
                    }
                    foreach (int bb in PushSwitchesRoom)
                    {
                        if (b == bb)
                        {
                            ng = pt;
                            break;
                        }
                    }
                    foreach (byte bb in IcemanRoom)
                    {
                        if (b == bb)
                        {
                            ng = im;
                            break;
                        }
                    }

                    foreach (byte bb in WallMasterRoom)
                    {
                        if (b == bb)
                        {
                            ng = wm;
                            break;
                        }
                    }
                    foreach (byte bb in canonRoom)
                    {
                        if (b == bb)
                        {
                            ng = cc;
                            break;
                        }
                    }
                    foreach (byte bb in canonRoom2)
                    {
                        if (b == bb)
                        {
                            ng = c2;
                            break;
                        }
                    }

                    if (b == 291)
                    {
                        ng = mm;
                        g = mm;
                    }
                    if (b == 85)
                    {
                        ng = 13;
                        g = 13;
                    }
                }
                g = ng;


                dynamic_sprite.Clear();
                //getsubset0 of group ng
                foreach (byte en in subset_gfx_sprites[random_sprite_group[ng][0]])
                {
                    dynamic_sprite.Add(en);
                }
                //getsubset0 of group ng
                foreach (byte en in subset_gfx_sprites[random_sprite_group[ng][1]])
                {
                    dynamic_sprite.Add(en);
                }
                //getsubset0 of group ng
                foreach (byte en in subset_gfx_sprites[random_sprite_group[ng][2]])
                {
                    dynamic_sprite.Add(en);
                }
                //getsubset0 of group ng
                foreach (byte en in subset_gfx_sprites[random_sprite_group[ng][3]])
                {
                    if (en != 0xBB)
                    {
                        dynamic_sprite.Add(en);
                    }
                }
                //add unrequired gfx sprites
                dynamic_sprite.Add(0xC6); dynamic_sprite.Add(0xC5);

                //add absorbable sprites
                if (absorbable_checkbox.Checked)
                {
                    //for(int i = 0;i< absorbable_sprites.Length;i++)
                    //{
                        //pick 2 random absorbable in the pool not more
                    dynamic_sprite.Add(absorbable_sprites[rand.Next(absorbable_sprites.Length)]);
                    dynamic_sprite.Add(absorbable_sprites[rand.Next(absorbable_sprites.Length)]);
                    //}

                }
                else
                {
                    //dynamic_sprite.Add(0xE4); dynamic_sprite.Add(0xE3); //key and fairy because they are fun
                }

                //g = dynamic_sprite;
                int nbrofsprite = dynamic_sprite.Count;
                richTextBox1.AppendText("Nbr of sprite possible for room[" + b.ToString() + "] : " + nbrofsprite.ToString() +" GFX:"+ng.ToString()+" [" + random_sprite_group[ng][0] +"," + random_sprite_group[ng][1] + "," + random_sprite_group[ng][2] + "," + random_sprite_group[ng][3] + "]"+ "\n");
                foreach(int ro in NeedKillable_doors)
                {
                    if (b == ro)
                    {
                        needkill = true;
                        break;
                    }
                }

                for(int i = 0;i<room_sprites[b].Length;i++) //for each sprite in that room select one of the group selected
                {
                    byte selectedSprite = 0;
                    //check if that room require defeatable sprites
                    if (needkill == true)
                    {
                        bool found = false;
                        byte spr_r = 0;
                        while (found == false)
                        {
                            repeatloop:
                            spr_r = (byte)rand.Next(nbrofsprite);
                            for (int j =0;j< NonKillable.Length ;j++)
                            {
                                
                                if (dynamic_sprite[spr_r] == NonKillable[j])
                                {
                                    found = false;
                                    goto repeatloop;
                                    //break;
                                }
                            }
                            selectedSprite = dynamic_sprite[spr_r];
                            found = true;
                        }
                    }
                    else
                    {


                        retryr:
                        byte spr_r = (byte)rand.Next(nbrofsprite);
                        for(int j = 0;j<noStatueRoom.Length;j++) // statue handling prevent statue from spawning in some rooms
                        {
                            if (b == noStatueRoom[j])
                            {
                                if (spr_r == 0x1C)
                                {
                                    goto retryr;
                                }
                            }
                        }


                        for (int jj = 0; jj < key_sprite.Length; jj++)
                        {
                            if (room_sprites[b][i] == key_sprite[jj])
                            {
                                for (int j = 0; j < NonKillable.Length; j++)
                                {
                                    if (dynamic_sprite[spr_r] == NonKillable[j])
                                    {
                                        goto retryr;
                                    }
                                }
                            }
                        }

                        selectedSprite = dynamic_sprite[spr_r];
                        
                    }
                   // for (int jj = 0; jj < special_sprites.Length; jj++)
                   // {
                    //    if (special_sprites[jj] == room_sprites[b][i])
                    //    {
                            ROM_DATA[room_sprites[b][i] - 1] = (byte)(ROM_DATA[room_sprites[b][i] - 1] & 0x1F);
                    //    }
                    //}
                    ROM_DATA[room_sprites[b][i]] = selectedSprite;


                }
                ROM_DATA[0x120090 + ((b * 14) + 3)] = g;
                //richTextBox1.AppendText("Writed at "+(0x120090 + ((b * 14) + 4)).ToString("X6")+" gfx :" + g + "\n");
            }



            for(int i =0;i<14;i++) //dungeons palettes
            {
                byte g = (byte)rand.Next(71);
                byte gfx = (byte)rand.Next(23);
                byte floor = (byte)rand.Next(255);
                for (int j = 0; j < dungeons_palettes[i].Length; j++)
                {
                    if (palettes_checkbox.Checked)
                    {
                        if (j == 0)
                        {
                            recheckpalette:
                            for (int k = 0; k < bugged_cave_palettes.Length; k++)
                            {
                                if (g == bugged_cave_palettes[k])
                                {
                                    g = (byte)rand.Next(71);
                                    goto recheckpalette;
                                }
                                    
                            }
                        }
                        ROM_DATA[0x120090 + ((dungeons_palettes[i][j] * 14) + 1)] = g;
                    }
                    if (gfx_checkbox.Checked)
                    {
                        ROM_DATA[0x120090 + ((dungeons_palettes[i][j] * 14) + 2)] = gfx;
                    }
                    if (floors_checkbox.Checked)
                    {
                        //get pointer of that room
                        byte[] roomPointer = new byte[4];//27502
                        roomPointer[0] = ROM_DATA[(0xF8000 + (j * 3) + 0)];
                        roomPointer[1] = ROM_DATA[(0xF8000 + (j * 3) + 1)];
                        roomPointer[2] = ROM_DATA[(0xF8000 + (j * 3) + 2)];
                        int address = BitConverter.ToInt32(roomPointer, 0);
                        int pcadd = snestopc(address);
                        ROM_DATA[pcadd] = floor;
                    }
                }
            }
            for (int j = 0; j < 0xF3; j++)
            {


                if (palette_e_checkbox.Checked)
                {
                    //Do not change color of collectible items
                    if (j <= 0xD7 || j >= 0xE7)
                    {
                        ROM_DATA[0x6B359 + j] = (byte)((ROM_DATA[0x6B359 + j] & 0xF1) + (rand.Next(15) & 0x0E));
                    }
                }
                if (hp_checkbox.Checked)
                {
                    if (ROM_DATA[0x6B173 + j] != 0xFF)
                    {
                        if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
                            && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3
                           )
                        {
                            int new_hp = ROM_DATA[0x6B173 + j];
                            new_hp += (-20 + rand.Next(40));
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

                if (zerohp_checkbox.Checked)
                {
                    if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
    && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3
   )
                    {
                        byte new_hp = (byte)(1);
                        ROM_DATA[0x6B173 + j] = new_hp;
                    }
                }

                if (damage_checkbox.Checked)
                {
                    if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
    && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3
   )
                    {
                        ROM_DATA[0x6B266 + j] = (byte)(rand.Next(8));
                    }
                }

            }

            FileStream fs = new FileStream(file_to_patch, FileMode.Open, FileAccess.Write);
            fs.Write(ROM_DATA, 0, 2097152);
            fs.Close();
            
        }
            




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
        int[][] room_sprites = new int[292][];
        byte[][] sprite_group = new byte[40][];
        byte[] sprite_groups = new byte[16] { 1,4,8,10,17,19,25,27,28,29,30,31,33,37,38,39};

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        string file_to_patch = "";

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://zarby89.github.io/Enimizer/");
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            file_to_patch = openFileDialog2.FileName;
            FileStream fs = new FileStream(file_to_patch, FileMode.Open, FileAccess.ReadWrite);
            ROM_DATA = new byte[2097152];
            fs.Read(ROM_DATA, 0, (int)2097152);
            fs.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Zarby89/Enimizer/issues");
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (e.Cancel)
            {

            }
            else
            {
                /*richTextBox1.AppendText(Path.GetDirectoryName(Application.ExecutablePath));
                file_to_patch = openFileDialog1.FileName;
                Process process = new Process();
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                process.StartInfo = new ProcessStartInfo("xkas.exe", "main.asm " + file_to_patch);
                process.Start();*/

                file_to_patch = openFileDialog1.FileName;
                FileStream fs = new FileStream(file_to_patch, FileMode.Open, FileAccess.ReadWrite);
                ROM_DATA = new byte[2097152];
                fs.Read(ROM_DATA, 0, (int)2097152);
                fs.Close();

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

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            update_flags();

            for (int i = 0;i<292;i++)
            {
                room_sprites[i] = null;
            }



            room_sprites[2] = new int[]{ 0x04D936,0x04D939,0x04D93C,0x04D93F,0x04D942,0x04D960,0x04D963};

            room_sprites[4] = new int[]{ 0x04D96B,0x04D96E,0x04D971,0x04D974,0x04D983,0x04D98F,0x04D992,0x04D995};

            room_sprites[9] = new int[]{ 0x04D9D0,0x04D9D3,0x04D9D6};


            room_sprites[10] = new int[]{ 0x04D9DB,0x04D9DE, 0x04D9E1 , 0x04D9E4 , 0x04D9EA , 0x04D9ED };

            room_sprites[11] = new int[]{ 0x04D9F5,0x04D9F8,0x04D9FB,0x04D9FE,0x04DA01,0x04DA04,0x04DA07,0x04DA0A,0x04DA0D};

            room_sprites[14] = new int[]{ 0x04DA1A,0x04DA1D,0x04DA20};

            

            room_sprites[17] = new int[]{ 0x04DA28,0x04DA2B,0x04DA2E,0x04DA31,0x04DA34,0x04DA37,0x04DA3A,0x04DA3D};

            room_sprites[19] = new int[]{ 0x04DA4D,0x04DA50,0x04DA53,0x04DA56,0x04DA5C,0x04DA68, 0x04DA65 , 0x04DA59 };

            room_sprites[21] = new int[]{ 0x04DA99,0x04DA9C,0x04DA9F,0x04DAA2,0x04DAA5,0x04DAA8};

            room_sprites[22] = new int[]{ 0x04DAAD,0x04DAB0,0x04DAB3,0x04DAB6,0x04DAB9,0x04DABC,0x04DABF};

            room_sprites[23] = new int[]{ 0x04DACD,0x04DAD0,0x04DAD3,0x04DAD6,0x04DAD9,0x04DADC};

            room_sprites[25] = new int[]{ 0x04DAE1,0x04DAE4,0x04DAE7,0x04DAEA};

            room_sprites[26] = new int[]{ 0x04DAEF,0x04DAF2,0x04DAF5,0x04DAF8,0x04DAFB,0x04DAFE,0x04DB01,0x04DB04,0x04DB0A};

            room_sprites[27] = new int[]{ 0x04DB18,0x04DB1B,0x04DB1E,0x04DB21};

            room_sprites[30] = new int[]{ 0x04DB4C,0x04DB4F,0x04DB52,0x04DB55,0x04DB58,0x04DB5B};

            room_sprites[31] = new int[]{ 0x04DB60,0x04DB63,0x04DB66,0x04DB69,0x04DB6C,0x04DB6F,0x04DB72,0x04DB75};
            
            room_sprites[33] = new int[]{ 0x04DB7F,0x04DB85,0x04DB88,0x04DB8B,0x04DB8E,0x04DB91,0x04DB94,0x04DB97,0x04DB9A,0x04DB9D,0x04DBA0};

            room_sprites[34] = new int[]{ 0x04DBA5,0x04DBA8,0x04DBAB,0x04DBAE,0x04DBB1,0x04DBB4,0x04DBB7};

            room_sprites[36] = new int[]{ 0x04DBCD,0x04DBD0,0x04DBD3,0x04DBD6,0x04DBD9,0x04DBDC,0x04DBDF};

           

            room_sprites[38] = new int[]{ 0x04DC07,0x04DBE6, 0x04DBE9,0x04DBEC,0x04DBEF,0x04DBF2,0x04DBF5,0x04DBFB,0x04DBFE,0x04DC01,0x04DC04};

            room_sprites[39] = new int[]{ 0x04DC0C,0x04DC0F,0x04DC12,0x04DC15,0x04DC18,0x04DC1B,0x04DC1E};

            room_sprites[40] = new int[]{0x04DC2F};

            room_sprites[42] = new int[]{ 0x04DC42,0x04DC45,0x04DC48,0x04DC4B,0x04DC4E,0x04DC51};

            room_sprites[43] = new int[]{ 0x04DC5C,0x04DC5F,0x04DC62,0x04DC65,0x04DC68,0x04DC6B};

            room_sprites[46] = new int[]{ 0x04DC7E,0x04DC81,0x04DC84,0x04DC87,0x04DC8A,0x04DC8D};

            room_sprites[49] = new int[]{ 0x04DC9D,0x04DCA0,0x04DCA3,0x04DCA6,0x04DCA9,0x04DCAC,0x04DCAF,0x04DCB2,0x04DCB5,0x04DCB8};

            room_sprites[50] = new int[]{ 0x04DCBD,0x04DCC0,0x04DCC3,0x04DCC6,0x04DCC9};

            room_sprites[52] = new int[]{ 0x04DCD9,0x04DCDC,0x04DCDF,0x04DCE5,0x04DCE8,0x04DCEB, 0x04DCE2 };

            room_sprites[53] = new int[] {0x04DCF6,0x04DCF9,0x04DCFC,0x04DCFF,0x04DD02,0x04DD08,0x04DD0B,0x04DD0E, 0x04DD05 };

            room_sprites[54] = new int[]{ 0x04DD16, 0x04DD19, 0x04DD22, 0x04DD28, 0x04DD2B }; //disabled for crash issue//0x04DD16,0x04DD19,0x04DD1C,0x04DD22,0x04DD28,0x04DD2B,0x04DD2E,0x04DD31

            room_sprites[55] = new int[]{ 0x04DD39,0x04DD3C,0x04DD3F,0x04DD42,0x04DD48,0x04DD4B,0x04DD4E,0x04DD51, 0x04DD45 };

            room_sprites[56] = new int[]{ 0x04DD56,0x04DD59,0x04DD5C,0x04DD5F,0x04DD62,0x04DD65,0x04DD68};
            
            room_sprites[57] = new int[]{ 0x04DD6D,0x04DD73,0x04DD79,0x04DD7C,0x04DD7F,0x04DD82};

            room_sprites[58] = new int[]{ 0x04DD87,0x04DD8A,0x04DD8D,0x04DD90,0x04DD93,0x04DD96};

            room_sprites[59] = new int[]{ 0x04DD9B,0x04DD9E,0x04DDA1,0x04DDA4,0x04DDA7,0x04DDAA,0x04DDAD};

            room_sprites[60] = new int[]{ 0x04DDB2,0x04DDB5,0x04DDB8};

            room_sprites[61] = new int[]{ 0x04DDC3,0x04DDC9,0x04DDCC,0x04DDCF,0x04DDD2,0x04DDD5,0x04DDDB,0x04DDDE,0x04DDE1,0x04DDE4,0x04DDE7};

            room_sprites[62] = new int[] { 0x04DDEF, 0x04DDF2, 0x04DE04, 0x04DE07, 0x04DE0D, 0x04DE10 }; //Removed for crash issue

            room_sprites[63] = new int[]{ 0x04DE18,0x04DE1E,0x04DE21};

            room_sprites[64] = new int[]{ 0x04DE26,0x04DE29,0x04DE2F,0x04DE32,0x04DE35};

            room_sprites[65] = new int[]{ 0x04DE3C,0x04DE3F,0x04DE42,0x04DE45};

            room_sprites[66] = new int[]{ 0x04DE4A,0x04DE4D,0x04DE50,0x04DE53,0x04DE56,0x04DE59};

            room_sprites[67] = new int[]{ 0x04DE5E, 0x04DE61 };

            room_sprites[68] = new int[]{ 0x04DE6C,0x04DE6F,0x04DE72,0x04DE75,0x04DE78,0x04DE7E};

            room_sprites[69] = new int[]{ 0x04DE86,0x04DE8C,0x04DE8F,0x04DE9B,0x04DE9E,0x04DEA1, 0x04DE89, 0x04DE92, 0x04DE95, 0x04DE98 };

            room_sprites[70] = new int[]{ 0x04DEA6,0x04DEAC,0x04DEB2};

            room_sprites[73] = new int[]{ 0x04DEB9,0x04DEBC,0x04DEBF,0x04DEC2,0x04DEC5,0x04DEC8,0x04DECE,0x04DED1,0x04DED4,0x04DED7,0x04DEDA,0x04DEDD};

            room_sprites[74] = new int[]{ 0x04DEE5,0x04DEE8};

            room_sprites[75] = new int[]{ 0x04DEED,0x04DEF0,0x04DEF3,0x04DEF6,0x04DEF9,0x04DEFC,0x04DEFF,0x04DF02};

            room_sprites[76] = new int[]{ 0x04DF0D,0x04DF10,0x04DF13,0x04DF16,0x04DF19,0x04DF1C};

            room_sprites[78] = new int[]{ 0x04DF26,0x04DF29,0x04DF2C,0x04DF2F};

            room_sprites[80] = new int[]{ 0x04DF3F,0x04DF42,0x04DF45};

            room_sprites[81] = new int[]{ 0x04DF4D,0x04DF50};

            room_sprites[82] = new int[]{ 0x04DF55,0x04DF58,0x04DF5B};

            room_sprites[83] = new int[]{ 0x04DF60,0x04DF63,0x04DF66,0x04DF69,0x04DF6C,0x04DF6F,0x04DF72,0x04DF75,0x04DF78,0x04DF7B,0x04DF7E,0x04DF81,0x04DF84};

            room_sprites[84] = new int[]{ 0x04DF89,0x04DF8C,0x04DF8F,0x04DF92,0x04DF95,0x04DF98,0x04DF9B,0x04DF9E};

            room_sprites[85] = new int[]{ 0x04DFA6,0x04DFA9};

            room_sprites[86] = new int[]{ 0x04DFB7,0x04DFBA,0x04DFBD,0x04DFC0,0x04DFC6,0x04DFC9,0x04DFCC,0x04DFD2};

            room_sprites[87] = new int[]{ 0x04DFD7,0x04DFDA,0x04DFDD,0x04DFE0,0x04DFE3,0x04DFE6,0x04DFEC,0x04DFEF,0x04DFF2,0x04DFF5,0x04DFF8,0x04DFFB,0x04DFFE,0x04E001};

            room_sprites[88] = new int[]{ 0x04E009,0x04E00C,0x04E012,0x04E015,0x04E01B,0x04E01E,0x04E021};

            room_sprites[89] = new int[]{ 0x04E026,0x04E029,0x04E032,0x04E038,0x04E03B,0x04E03E,0x04E041,0x04E044,0x04E047};

            room_sprites[91] = new int[] { 0x04E057, 0x04E05A, 0x04E05D, 0x04E060 };//remove some sprite for lag//0x04E063,0x04E066,0x04E069};

            room_sprites[93] = new int[]{ 0x04E07F,0x04E082,0x04E085,0x04E088,0x04E08B,0x04E091,0x04E094,0x04E097,0x04E0A3};

            room_sprites[94] = new int[]{ 0x04E0AB,0x04E0AE,0x04E0B1,0x04E0B4};

            room_sprites[95] = new int[]{ 0x04E0B9,0x04E0BC,0x04E0BF};

            room_sprites[96] = new int[]{ 0x04E0C4};

            room_sprites[97] = new int[]{ 0x04E0C9,0x04E0CC,0x04E0CF};

            room_sprites[98] = new int[]{ 0x04E0D4,0x04E0D7,0x04E0DA};

            room_sprites[99] = new int[]{ 0x04E0E2, 0x04E0DF };

            room_sprites[100] = new int[]{ 0x04E0E7,0x04E0ED,0x04E0F0,0x04E0F3,0x04E0F6,0x04E0F9};

            room_sprites[101] = new int[]{ 0x04E110,0x04E113,0x04E116,0x04E119,0x04E11C};

            room_sprites[102] = new int[]{ 0x04E121,0x04E127,0x04E12A,0x04E133,0x04E136,0x04E139,0x04E13C,0x04E142};

            room_sprites[103] = new int[]{ 0x04E14A,0x04E14D,0x04E150,0x04E153,0x04E156,0x04E159,0x04E15C,0x04E15F,0x04E162};

            room_sprites[104] = new int[]{ 0x04E173,0x04E179,0x04E17C};

            room_sprites[106] = new int[]{ 0x04E181,0x04E184,0x04E187,0x04E18A,0x04E18D,0x04E190};

            room_sprites[107] = new int[]{ 0x04E19B,0x04E19E,0x04E1A1,0x04E1A7,0x04E1AA,0x04E1AD,0x04E1B0,0x04E1B3,0x04E1B6,0x04E1B9,0x04E1BC};

            room_sprites[109] = new int[]{ 0x04E1D2,0x04E1D5,0x04E1D8,0x04E1DB,0x04E1DE,0x04E1E1,0x04E1E4,0x04E1E7,0x04E1EA};

            room_sprites[110] = new int[]{ 0x04E1EF,0x04E1F2,0x04E1F5,0x04E1F8,0x04E1FB};

            room_sprites[113] = new int[]{ 0x04E200,0x04E203};
            

            room_sprites[114] = new int[]{ 0x04E20B,0x04E211};

            room_sprites[115] = new int[]{ 0x04E216,0x04E219,0x04E21C,0x04E21F,0x04E222,0x04E225};

            room_sprites[116] = new int[]{ 0x04E22D,0x04E230,0x04E233,0x04E236,0x04E239,0x04E23C,0x04E23F,0x04E242};

            room_sprites[117] = new int[]{ 0x04E247,0x04E24A,0x04E24D,0x04E250,0x04E253,0x04E256,0x04E25F,0x04E262};

            // room_sprites[118] = new int[]{ 0x04E26A,0x04E26D,0x04E270,0x04E273,0x04E276,0x04E279,}; // Might be aproblem

            room_sprites[118] = new int[] { 0x04E26A, 0x04E26D, 0x04E270, 0x04E273 , 0x04E279 };

            room_sprites[119] = new int[]{ 0x04E27E,0x04E28A,0x04E28D};

            room_sprites[123] = new int[]{ 0x04E292,0x04E295,0x04E298,0x04E29B,0x04E29E,0x04E2A1,0x04E2A7,0x04E2AA,0x04E2AD,0x04E2B0};

            room_sprites[124] = new int[]{ 0x04E2B5,0x04E2B8,0x04E2BB,0x04E2BE,0x04E2C1,0x04E2C4};

            room_sprites[125] = new int[]{ 0x04E2D8,0x04E2DB,0x04E2E1,0x04E2E4,0x04E2EA, 0x04E2DE, 0x04E2E7 };

            room_sprites[126] = new int[]{ 0x04E2F2,0x04E2F5,0x04E2FE,0x04E301};

            room_sprites[127] = new int[]{ 0x04E306,0x04E309,0x04E30C,0x04E30F,0x04E312,0x04E315,0x04E318,0x04E31B};


            room_sprites[128] = new int[]{ 0x04E323,0x04E326};

            room_sprites[129] = new int[]{ 0x04E32E,0x04E331};

            room_sprites[130] = new int[]{ 0x04E336,0x04E339,0x04E33C};

            room_sprites[131] = new int[]{ 0x04E341,0x04E344,0x04E347,0x04E34A,0x04E34D,0x04E350,0x04E353,0x04E356,0x04E359,0x04E35C};

            room_sprites[132] = new int[]{ 0x04E361,0x04E364,0x04E367,0x04E36A,0x04E36D,0x04E370,0x04E373};

            room_sprites[133] = new int[]{ 0x04E378,0x04E37B,0x04E37E,0x04E381,0x04E384,0x04E387,0x04E38A,0x04E38D,0x04E390,0x04E393};

            
            room_sprites[135] = new int[]{ 0x04E39A,0x04E39D,0x04E3A0,0x04E3A3,0x04E3B2,0x04E3B5,0x04E3B8,0x04E3BE, 0x04E3A6 };

            room_sprites[139] = new int[]{ 0x04E3D4,0x04E3D7,0x04E3DA,0x04E3DD,0x04E3E0};

            room_sprites[140] = new int[]{ 0x04E3F7,0x04E3FA,0x04E3FD,0x04E400,0x04E406,0x04E409,0x04E40F, 0x04E40C, 0x04E403 };

           
            room_sprites[141] = new int[]{ 0x04E41A,0x04E41D,0x04E420,0x04E423,0x04E426,0x04E42C,0x04E42F,0x04E432,0x04E435,0x04E438,0x04E43B, 0x04E417 };

            room_sprites[142] = new int[]{ 0x04E443,0x04E446,0x04E449,0x04E44C,0x04E44F,0x04E452,0x04E455};

            room_sprites[145] = new int[]{ 0x04E462,0x04E468,0x04E46B,0x04E46E,0x04E471};

            room_sprites[146] = new int[]{ 0x04E47C,0x04E47F,0x04E482,0x04E485,0x04E488,0x04E48E,0x04E491,0x04E494,0x04E497};

            room_sprites[147] = new int[]{ 0x04E49C,0x04E49F,0x04E4A2,0x04E4A5,0x04E4A8,0x04E4AB,0x04E4AE,0x04E4B1};

            room_sprites[149] = new int[]{ 0x04E4B6,0x04E4B9,0x04E4BC,0x04E4BF};

            room_sprites[152] = new int[]{ 0x04E4DD,0x04E4E0,0x04E4E3,0x04E4E6,0x04E4E9};

           
            room_sprites[153] = new int[]{ 0x04E4EE,0x04E4F1,0x04E4F4,0x04E4F7,0x04E4FD,0x04E500,0x04E503,0x04E506,0x04E509,0x04E50C};

            room_sprites[155] = new int[]{ 0x04E51A,0x04E51D,0x04E520,0x04E523,0x04E526,0x04E529,0x04E52C,0x04E52F,0x04E532,0x04E535};

            room_sprites[156] = new int[]{ 0x04E53A,0x04E53D,0x04E540,0x04E543,0x04E546,0x04E549};

            room_sprites[157] = new int[]{ 0x04E554,0x04E557,0x04E55A,0x04E55D,0x04E560,0x04E563,0x04E566,0x04E569};

            room_sprites[158] = new int[]{ 0x04E56E,0x04E571,0x04E574,0x04E577};

            room_sprites[159] = new int[] { 0x04E58B, 0x04E58E };//Might cause crashes // 0x04E57F,0x04E582,0x04E585,0x04E588,0x04E58B,0x04E58E,};

            
            room_sprites[160] = new int[]{ 0x04E593,0x04E596, 0x04E599 };

            room_sprites[161] = new int[]{ 0x04E5A1,0x04E5A4,0x04E5A7,0x04E5AA,0x04E5AD,0x04E5B0,0x04E5B3,0x04E5B6};

         
            room_sprites[165] = new int[]{ 0x04E5C8,0x04E5CB,0x04E5CE,0x04E5D1,0x04E5D4,0x04E5D7,0x04E5DA,0x04E5DD,0x04E5E6,0x04E5E9};

            //room_sprites[166] = new int[]{ 0x04E5EE,0x04E5F1,};
            room_sprites[166] = new int[] { };

            room_sprites[167] = new int[]{ 0x04E5F6,0x04E5F9};
            
            room_sprites[168] = new int[]{ 0x04E5FE,0x04E601,0x04E604,0x04E607, 0x04E60A };

            room_sprites[169] = new int[]{ 0x04E60F,0x04E612,0x04E621,0x04E624, 0x04E615 , 0x04E618 , 0x04E61B , 0x04E61E };

            room_sprites[170] = new int[]{ 0x04E629,0x04E62C,0x04E62F,0x04E632,0x04E635,0x04E638};

            room_sprites[171] = new int[]{ 0x04E640,0x04E643,0x04E646,0x04E649,0x04E64C,0x04E64F,0x04E652};

            room_sprites[174] = new int[]{ 0x04E65C,0x04E65F};

            room_sprites[176] = new int[]{ 0x04E669,0x04E66C,0x04E66F,0x04E672,0x04E675,0x04E678,0x04E67B,0x04E67E,0x04E681,0x04E684,0x04E687,0x04E68D,0x04E690};

            room_sprites[177] = new int[]{ 0x04E695,0x04E698,0x04E69B,0x04E69E,0x04E6A1,0x04E6A4,0x04E6A7,0x04E6AA,0x04E6AD,0x04E6B0};

            room_sprites[178] = new int[]{ 0x04E6B5,0x04E6B8,0x04E6BB,0x04E6BE,0x04E6C1,0x04E6C4,0x04E6C7,0x04E6CA,0x04E6CD,0x04E6D0,0x04E6D3,0x04E6D6,0x04E6D9,0x04E6DC};

            room_sprites[179] = new int[]{ 0x04E6E1,0x04E6E4,0x04E6E7,0x04E6EA,0x04E6ED};

            room_sprites[182] = new int[]{ 0x04E6FD,0x04E700,0x04E709,0x04E70C,0x04E715,0x04E718};

            room_sprites[183] = new int[]{ 0x04E71D,0x04E720};

            room_sprites[184] = new int[]{ 0x04E725,0x04E728,0x04E72B,0x04E72E,0x04E731,0x04E734};

            room_sprites[185] = new int[] { };

            room_sprites[186] = new int[]{ 0x04E73E,0x04E741,0x04E744,0x04E747,0x04E74A,0x04E74D,0x04E750};

            room_sprites[187] = new int[]{ 0x04E755,0x04E758,0x04E75B,0x04E75E,0x04E761,0x04E764,0x04E76A,0x04E76D,0x04E770,0x04E773, 0x04E767 };

            room_sprites[188] = new int[]{ 0x04E77B,0x04E77E,0x04E781,0x04E784,0x04E78A,0x04E78D,0x04E790,0x04E799, 0x04E778, 0x04E787, 0x04E793 };

            room_sprites[190] = new int[]{ 0x04E7A0,0x04E7A6,0x04E7A9,0x04E7AC,0x04E7AF,0x04E7B2};
  
            room_sprites[192] = new int[]{ 0x04E7BF,0x04E7C2,0x04E7C5,0x04E7C8,0x04E7CE,0x04E7D1,0x04E7D4,0x04E7D7};


            room_sprites[193] = new int[]{ 0x04E7DF,0x04E7E2,0x04E7E5,0x04E7E8,0x04E7EE,0x04E7F4,0x04E7F7,0x04E7FA, 0x04E7F1 };

            room_sprites[194] = new int[]{ 0x04E80B,0x04E80E,0x04E811,0x04E814,0x04E81A, 0x04E817, 0x04E808, 0x04E805 };

            room_sprites[195] = new int[]{ 0x04E81F,0x04E831,0x04E834};

            room_sprites[196] = new int[]{ 0x04E845,0x04E848,0x04E84B,0x04E84E,0x04E851,0x04E854};

            room_sprites[201] = new int[]{ 0x04E8A1,0x04E8A4,0x04E8A7};

            room_sprites[203] = new int[]{ 0x04E8AC,0x04E8B5,0x04E8B8,0x04E8BB,0x04E8BE,0x04E8C1,0x04E8C4,0x04E8C7,0x04E8CA,0x04E8CD, 0x04E8B2, 0x04E8AF };

            room_sprites[204] = new int[]{ 0x04E8D2,0x04E8D5,0x04E8DB,0x04E8DE,0x04E8E1,0x04E8EA,0x04E8ED,0x04E8F0,0x04E8F3,0x04E8F6,0x04E8F9, 0x04E8D8, 0x04E8E4 };

            room_sprites[208] = new int[]{ 0x04E918,0x04E91B,0x04E91E,0x04E921,0x04E924,0x04E927,0x04E92A,0x04E92D,0x04E930,0x04E933,0x04E936};

            room_sprites[209] = new int[]{ 0x04E93B,0x04E93E,0x04E941,0x04E944,0x04E947,0x04E94A,0x04E94D,0x04E950};

            room_sprites[210] = new int[]{ 0x04E955,0x04E958,0x04E95B,0x04E95E,0x04E961,0x04E964,0x04E967,0x04E96A,0x04E96D,0x04E970};

            room_sprites[216] = new int[]{ 0x04E991,0x04E994,0x04E997,0x04E99A,0x04E99D,0x04E9A0,0x04E9A3,0x04E9A6,0x04E9A9,0x04E9AC,0x04E9AF};

            room_sprites[217] = new int[]{ 0x04E9B7,0x04E9BA,0x04E9BD, 0x04E9B4 };

            room_sprites[218] = new int[]{ 0x04E9C2,0x04E9C5};

            room_sprites[219] = new int[]{ 0x04E9CA,0x04E9CD,0x04E9D0,0x04E9D6, 0x04E9D3, 0x04E9DC, 0x04E9D9 };

            room_sprites[220] = new int[]{ 0x04E9E4,0x04E9E7,0x04E9EA,0x04E9ED,0x04E9F0,0x04E9F3,0x04E9FF, 0x04E9E1, 0x04E9F6, 0x04E9F9, 0x04E9FC };

            room_sprites[223] = new int[]{ 0x04EA0F,0x04EA12};

            room_sprites[224] = new int[]{ 0x04EA17,0x04EA1A,0x04EA1D,0x04EA20};

            room_sprites[232] = new int[] { 0x04EA8D, 0x04EA90, 0x04EA93, 0x04EA96, };

            room_sprites[238] = new int[] { 0x04EAA5, 0x04EAA8, 0x04EAAB, 0x04EAAE, 0x04EAB1, };

            room_sprites[239] = new int[] { 0x04EAB6, 0x04EAB9, 0x04EABC, };

            room_sprites[249] = new int[] { 0x04EB13, 0x04EB16, 0x04EB19, 0x04EB1C, };

            room_sprites[235] = new int[] { };

            room_sprites[251] = new int[] { };

            room_sprites[254] = new int[] { 0x04EB4A, 0x04EB4D, 0x04EB50, 0x04EB53, 0x04EB56, };

            room_sprites[263] = new int[] { 0x04EB8C, 0x04EB8F };

            room_sprites[264] = new int[] { 0x04EB94, 0x04EB97, 0x04EB9A, 0x04EB9D, };

            room_sprites[267] = new int[] { 0x04EBBE };

            room_sprites[268] = new int[] { 0x04EBC3, 0x04EBC6, 0x04EBC9, 0x04EBCC, 0x04EBCF, 0x04EBD2, 0x04EBD5, 0x04EBD8, };

            room_sprites[269] = new int[] { 0x04EBDD, 0x04EBE0, };

            room_sprites[291] = new int[] { 0x04EC6F, 0x04EC72, 0x04EC75, 0x04EC78, };



        }
        int flags = 0;
        public void update_flags()
        {
            //0
            flags = 0;
            if (hp_checkbox.Checked)
            {
                flags = (flags | 1);
            }

            //1
            if (damage_checkbox.Checked)
            {
                flags = (flags | 2);
            }

            //2
            if (palette_e_checkbox.Checked)
            {
                flags = (flags | 4); 
            }

            //3
            if (anyboss_checkbox.Checked)
            {
                flags = (flags | 8);
            }


            //4
            if (bosses_checkbox.Checked)
            {
                flags = (flags | 16);
            }

            if (zerohp_checkbox.Checked)
            {
                flags = (flags | 32);
            }

            if (glitched_checkbox.Checked)
            {
                flags = (flags | 64);
            }


            if (palettes_checkbox.Checked)
            {
                flags = (flags | 128);
            }

            //1
            if (floors_checkbox.Checked)
            {
                flags = (flags | 256);
            }

            //2
            if (gfx_checkbox.Checked)
            {
                flags = (flags | 512);
            }

            //3
            if (absorbable_checkbox.Checked)
            {
                flags = (flags | 1024);
            }

            //4


            if (overworld_checkbox.Checked)
            {
                flags = (flags | 2048);
            }

            //5
            if (randsprites_checkbox.Checked)
            {
                flags = (flags | 4096);
            }

            //6
            if (layout_checkbox.Checked)
            {
                flags = (flags | 8192);
            }

            if (extra_checkbox.Checked)
            {
                flags = (flags | 16384);
            }
            //divide the flags into 4bit byte char + 65
            //enough for 4 now
            byte f1 = (byte)(flags & 0xF);
            byte f2 = (byte)((flags >> 4) & 0xF);
            byte f3 = (byte)((flags >> 8) & 0xF);
            byte f4 = (byte)((flags >> 12) & 0xF);
            textBox2.Text = ((char)(f1 + 65)).ToString()+ ((char)(f2 + 65)).ToString()+ ((char)(f3 + 65)).ToString()+ ((char)(f4 + 65)).ToString(); //(flags).ToString();
        }


        private void all_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).ContainsFocus)
            {
                update_flags();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            anyboss_checkbox.Checked = true;
            bosses_checkbox.Checked = false;
            hp_checkbox.Checked = true;
            damage_checkbox.Checked = true;
            palette_e_checkbox.Checked = true;
            palettes_checkbox.Checked = true;
            gfx_checkbox.Checked = true;
            floors_checkbox.Checked = true;
            randsprites_checkbox.Checked = true;
            layout_checkbox.Checked = true;
            zerohp_checkbox.Checked = true;
            glitched_checkbox.Checked = true;
            update_flags();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).ContainsFocus)
            {
                update_flags_from_text();
            }
        }


        public void update_flags_from_text()
        {
            //0
            if (textBox2.Text.Length == 0)
            {
                flags = 0;
            }
            flags = 0;
            if (textBox2.Text.Length != 0)
            {
                flags += (int)(textBox2.Text[0] - 65);
                if (textBox2.Text.Length >= 2)
                { 
                flags += (int)((textBox2.Text[1] - 65) << 4) ;
                }
                if (textBox2.Text.Length >= 3)
                {
                    flags += (int)((textBox2.Text[2] - 65) << 8);
                }
                if (textBox2.Text.Length >= 4)
                {
                    flags += (int)((textBox2.Text[3] - 65) << 12);
                }
            }
            hp_checkbox.Checked = false;
            damage_checkbox.Checked = false;
            palette_e_checkbox.Checked = false;
            anyboss_checkbox.Checked = false;
            bosses_checkbox.Checked = false;
            zerohp_checkbox.Checked = false;
            glitched_checkbox.Checked = false;
            palettes_checkbox.Checked = false;
            floors_checkbox.Checked = false;
            gfx_checkbox.Checked = false;
            absorbable_checkbox.Checked = false;
            overworld_checkbox.Checked = false;
            randsprites_checkbox.Checked = false;
            layout_checkbox.Checked = false;
            extra_checkbox.Checked = false;

            if ((flags & 1) == 1)
            {
                hp_checkbox.Checked = true;
            }
            //1
            if ((flags & 2) == 2)
            {
                damage_checkbox.Checked = true;
            }
            //2
            if ((flags & 4) == 4)
            {
                palette_e_checkbox.Checked = true;
            }
            //3
            if ((flags & 8) == 8)
            {
                anyboss_checkbox.Checked = true;
            }
            //4
            if ((flags & 16) == 16)
            {
                bosses_checkbox.Checked = true;
            }
            //5
            if ((flags & 32) == 32)
            {
                zerohp_checkbox.Checked = true;
            }
            //6
            if ((flags & 64) == 64)
            {
                glitched_checkbox.Checked = true;
            }

            if ((flags & 128) == 1128)
            {
                palettes_checkbox.Checked = true;
            }
            //1
            if ((flags & 256) == 256)
            {
                floors_checkbox.Checked = true;
            }

            //2
            if ((flags & 512) == 512)
            {
                gfx_checkbox.Checked = true;
            }
            //3
            if ((flags & 1024) == 1024)
            {
                absorbable_checkbox.Checked = true;
            }

            //4
            if ((flags & 2056) == 2056)
            {
                overworld_checkbox.Checked = true;
            }
            //5
            if ((flags & 4096) == 4096)
            {
                randsprites_checkbox.Checked = true;
            }

            //6
            if ((flags & 8192) == 8192)
            {
                layout_checkbox.Checked = true;
            }

            if ((flags & 16384) == 16384)
            {
                extra_checkbox.Checked = true;
            }



        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            update_flags_from_text();
        }
    }
}
