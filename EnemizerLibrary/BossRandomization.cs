using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public partial class Randomization
    {
        //Choose a boss for each rooms swap their pointers and change their position in the room !
        //Pointers of each rooms in PC format with PC Addresses : 
        //Boss Name / Room(DEC) / Pointer Address / PointerSNES / PointerPC
        List<DungeonProperties> dungeons = new List<DungeonProperties>();
        public void create_dungeons_properties()
        {

            //Moldorm Refight / 77 / 0x04D6C8 / 1EDF / 0x04DF1E  -  1 sprite    
            //Trinexx / 164 / 0x04D776 / BAE5 / 0x04E5BA - 3 sprites            
            //Kholdstare / 222 / 0x04D7EA / 01EA / 0x04EA01  -  3 sprites       
            //Arrghus / 6 / 0x04D63A / 97D9 / 0x04D997  -  14 sprites           
            //Moldorm / 7 / 0x04D63C / C3D9 / 0x04D9C3  -  1 sprite  //ID 0
            //Armos Knight / 200 / 0x04D7BE / 87E8 / 0x04E887  -  7 sprites //ID 1
            //Mothula / 41 / 0x04D680 / 31DC / 0x04DC31  -  1 sprite //ID 2
            //Lanmolas / 51 / 0x04D694 / CBDC / 0x04DCCB  -  3 sprites //ID 3
            //Helmasaure / 90 / 0x04D6E2 / 49E0 / 0x04E049  -  1 sprite //ID 4
            //Vitreous / 144 / 0x04D74E / 57E4 / 0x04E457  -  1 sprite //ID 5
            //Blind / 172 / 0x04D786 / 54E6 / 0x04E654  - 1 sprite //ID 6

            dungeons = new List<DungeonProperties>()
            {
                new DungeonProperties("Hera Tower",         0x04D63C, 7),   //should be fine
                new DungeonProperties("Eastern Palace",     0x04D7BE, 200), //should be fine
                new DungeonProperties("Skull Woods",        0x04D680, 41),  //should be fine
                new DungeonProperties("Desert Palace",      0x04D694, 51),  // require new pointer for hte room
                new DungeonProperties("Palace of Darkness", 0x04D6E2, 90),  //should be fine
                new DungeonProperties("Misery Mire",        0x04D74E, 144), //should be fine
                new DungeonProperties("Thieve Town",        0x04D786, 172), // require new pointer for hte room
                new DungeonProperties("Swamp Palace",       0x04D63A, 6),   //should be fine
                new DungeonProperties("Ice Palace",         0x04D7EA, 222), // require new pointer for hte room
                new DungeonProperties("Turtle Rock",        0x04D776, 164),
                new DungeonProperties("Gtower1 (Armos2)",   0x04D666, 28),  //0x04DB23
                new DungeonProperties("Gtower2 (Lanmo2)",   0x04D706, 108), //0x04E1BE
                new DungeonProperties("Gtower3 (Moldorm2)", 0x04D6C8, 77)
            };




            //NEED TO CHANGE BG2 to Addition for Kholdstare shell
            //NEED TO CHANGE BG2 to OnTop for Trinexx shell
            /*
             * 0xF8000*room
            7 : 0FCAEE, Length:0139
            200 : 051585, Length:0015
            41 : 0FC186, Length:006A //Kholdstare and Trinexx can't spawn there!
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
           

        }


        //If trinexx is in XX dungeon then write the new tiles of that dungeons there
        // Hera, Eastern, Skull, Desert, PoD, MM, TT, Swamp, Ice, Turtle, GT1, GT2, GT3


        //Debugging use only :

        public void Randomize_Bosses(bool shuffle)
        {
            bool error = false;
            patch_bosses();

            while (RandomizeBosses(shuffle) != true)
            {

            }

            RemoveBlindSpawnCode();

            RemoveMaidenFromThievesTown();

            //BYTE 4 of the header of the room trinexx is in must be setted on 04


        }

        private void RemoveBlindSpawnCode()
        {
            for (int i = 0; i < 15; i++) //REMOVE BLIND SPAWN CODE
            {
                ROM_DATA[0xEA081 + i] = 0xEA;
            }
        }

        private void RemoveMaidenFromThievesTown()
        {
            //REMOVE MAIDEN IN TT Basement
            ROM_DATA[0x04DE81] = 0x00;
        }

        private bool RandomizeBosses(bool shuffle)
        {
            dungeons.Clear();
            create_dungeons_properties();

            List<byte> bosses = new List<byte>();
            if (shuffle == true)
            {
                for (byte i = 0; i < 10; i++)
                {
                    bosses.Add(i);
                }
                bosses.Add(1); //gtower double
                bosses.Add(5);//gtower double
                bosses.Add(6);//gtower double
                Console.WriteLine("All Bosses Added in the pool");
            }
            else
            {
                for (int i = 0; i < 13; i++)
                {
                    byte boss = (byte)rand.Next(10);
                    if (i <= 2)//force at least 3 middle boss
                    {
                        boss = (byte)rand.Next(5);
                    }
                    bosses.Add(boss);
                    Console.WriteLine(BossConstants.BossNames[boss] + " Added in the pool");
                }
            }

            //try to place boss in hera and gtower moldorm first
            //Moldorm,Kholdstare,Vitreous,Helmasaure,Mothula

            //Palace of Darkness can't have Kholdstare

            while (bosses.Count > 0)
            {
                //Console.WriteLine("Infinite loop?");
                byte bosschosed = bosses[rand.Next(bosses.Count)];
                if (dungeons[0].boss == 255) //Hera Tower
                {
                    bosschosed = bosses[rand.Next(bosses.Count)];
                    if (bosschosed > 4)
                    {
                        continue;
                    }
                    if (bosschosed == 0) //if we pick kholdstare check if hera drop any major items if so then put him elsewhere
                    {

                        if (scan_gtower(0x07) || ROM_DATA[0x289B0] == 0x07)
                        {
                            continue;
                        }
                        //if it kholdstare then check if hera drop the green pendant and if sarasrala have a major item
                        if (CrystalConstants.CrystalTypeAddresses[0] == 0x00) //is pendant?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[0]] == 0x04) //if hera is green pendant
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x2F1FC])) //sarashala items
                                {
                                    continue;
                                }
                            }
                        }
                        if (CrystalConstants.CrystalTypeAddresses[0] == 0x40) //is crystal?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[0]] == 0x04 || ROM_DATA[CrystalConstants.CrystalAddresses[0]] == 0x01) //if hera is crystal 5 or crystal 6
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0xE980]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0xE983])) //fat fairy
                                {
                                    continue;
                                }
                            }
                        }
                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x180152]))
                        {
                            continue;
                        }
                    }
                    dungeons[0].boss = bosschosed;
                    bosses.Remove(bosschosed);
                }

                if (dungeons[12].boss == 255) //Gtower Moldorm
                {
                    bosschosed = bosses[rand.Next(bosses.Count)];
                    if (bosschosed > 4)
                    {
                        continue;
                    }

                    //NEED TO BE TESTED !!!!!!!!!!!!!
                    //New Boss code that should allow kholdstare to spawn anywhere
                    /*if (bosschosed == 0) {

                        continue;
                    }*/


                    dungeons[12].boss = bosschosed;
                    bosses.Remove(bosschosed);
                    //Console.WriteLine("Infinite Trinexxtry");
                }


                //MIGHT NOT NEED THAT CODE ANYMORE IT WAS PREVENTING KHOLDSTARE FROM SPAWNING IN POD BUT WITH NEW CODE IT SHOULD WORK
                /*if (dungeons[4].boss == 255) //Palace of Darkness can't be kholdstare
                {
                    bosschosed = bosses[rand.Next(bosses.Count)];
                    //NEED TO BE TESTED !!!!!!!!!!!!!
                    //New Boss code that should allow kholdstare to spawn anywhere
                    if (bosschosed == 0) {

                        continue;
                    }


                    if (bosschosed == 9) //if we pick trinexx check if hera drop any major items if so then put him elsewhere
                    {
                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x180153]))
                        {
                            continue;
                        }
                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[0xE980]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0xE983]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0x2F1FC]))
                        {
                            continue;
                        }
                    }
                    dungeons[4].boss = bosschosed;
                    bosses.Remove(bosschosed);
                    //Console.WriteLine("Infinite PoDtry");
                }*/


                byte dungeonChosed = (byte)rand.Next(12);
                if (bosses.Contains(8)) //since it can have multiple arrghus place all of them first where there's no drop
                {
                    dungeonChosed = (byte)rand.Next(12);
                    if (dungeons[dungeonChosed].boss != 255)
                    {
                        continue;
                    }


                    //NEW CODE FOR PENDANTS/CRYSTAL CHECKS

                    //Unless it is own dungeon check for crystals/pendants blocks

                    if (dungeonChosed != 7)
                    {

                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x00) //is pendant?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04) //if is green pendant
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x2F1FC])) //sarashala items
                                {
                                    continue;
                                }
                            }
                        }
                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x40) //is crystal?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04 || ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x01) //if is crystal 5 or crystal 6
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0xE980]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0xE983])) //fat fairy
                                {
                                    continue;
                                }
                            }
                        }

                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[BossConstants.BossDropItemAddresses[dungeonChosed]]))
                        {
                            continue;
                        }
                    }

                    if (scan_gtower(0x0A) || ROM_DATA[0x289B0] == 0x0A)
                    {

                        if (dungeons[7].boss == 255)
                        {
                            //put arrghus in his original location
                            dungeons[7].boss = 8;
                            bosses.Remove(8);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops Hookshot is in gtower can't put more than one arrghus!");
                            return false;
                        }
                    }
                    dungeons[dungeonChosed].boss = 8;
                    bosses.Remove(8);
                }


                //IF Kholdstare is not placed already place him first
                if (bosses.Contains(0))
                {
                    dungeonChosed = (byte)rand.Next(12);
                    if (dungeons[dungeonChosed].boss != 255)
                    {
                        continue;
                    }

                    //NEW CODE FOR PENDANTS/CRYSTAL CHECKS

                    //Unless it is own dungeon check for crystals/pendants blocks

                    if (dungeonChosed != 8)
                    {

                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x00) //is pendant?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04) //if is green pendant
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x2F1FC])) //sarashala items
                                {
                                    continue;
                                }
                            }
                        }
                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x40) //is crystal?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04 || ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x01) //if is crystal 5 or crystal 6
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0xE980]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0xE983])) //fat fairy
                                {
                                    continue;
                                }
                            }
                        }
                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[BossConstants.BossDropItemAddresses[dungeonChosed]]))
                        {
                            continue;
                        }
                    }

                    if (scan_gtower(0x07) || ROM_DATA[0x289B0] == 0x07)
                    {

                        if (dungeons[8].boss == 255)//???????????
                        {
                            //put kholdstare in his original location
                            dungeons[8].boss = 0;
                            bosses.Remove(0);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops fire rod is in GTOWER can't put more than 1 Kholdstare reset");
                            return false;
                        }
                    }

                    dungeons[dungeonChosed].boss = 0;
                    bosses.Remove(0);

                    //IF Trinexx is not placed already place him first
                }


                dungeonChosed = (byte)rand.Next(12);
                //IF Kholdstare is not placed already place him first
                if (bosses.Contains(9)) //since it can have multiple kholdstare place all of them first where there's no drop
                {
                    dungeonChosed = (byte)rand.Next(12);
                    if (dungeons[dungeonChosed].boss != 255)
                    {
                        dungeonChosed = (byte)rand.Next(12);
                    }


                    //NEW CODE FOR PENDANTS/CRYSTAL CHECKS

                    //Unless it is own dungeon check for crystals/pendants blocks

                    if (dungeonChosed != 9)
                    {

                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x00) //is pendant?
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04) //if is green pendant
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0x2F1FC])) //sarashala items
                                {
                                    continue;
                                }
                            }
                        }
                        if (CrystalConstants.CrystalTypeAddresses[dungeonChosed] == 0x40) //is crystal
                        {
                            if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x04 || ROM_DATA[CrystalConstants.CrystalAddresses[dungeonChosed]] == 0x01) //if is crystal 5 or crystal 6
                            {
                                if (ItemConstants.ImportantItems.Contains(ROM_DATA[0xE980]) || ItemConstants.ImportantItems.Contains(ROM_DATA[0xE983])) //fat fairy
                                {

                                    continue;
                                }
                            }
                        }
                        if (ItemConstants.ImportantItems.Contains(ROM_DATA[BossConstants.BossDropItemAddresses[dungeonChosed]]))
                        {
                            continue;
                        }
                    }


                    if (scan_gtower(0x07) || scan_gtower(0x08) || ROM_DATA[0x289B0] == 0x07 || ROM_DATA[0x289B0] == 0x08)
                    {


                        if (dungeons[9].boss == 255)//???????????
                        {
                            //put trinexx in his original location
                            dungeons[9].boss = 9;
                            bosses.Remove(9);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops Ice rod or fire rod is in GTOWER can't put more than 1 trinxx reset");
                            return false;
                        }
                    }
                    dungeons[dungeonChosed].boss = 9;
                    bosses.Remove(9);

                    //IF Trinexx is not placed already place him first
                }
                dungeonChosed = (byte)rand.Next(12);
                if (dungeons[dungeonChosed].boss == 255)
                {
                    bosschosed = bosses[rand.Next(bosses.Count)];
                    dungeons[dungeonChosed].boss = bosschosed;
                    bosses.Remove(bosschosed);
                }
                else
                {
                    continue;
                }

                if (bosses.Count == 0)
                {
                    foreach (DungeonProperties d in dungeons)
                    {
                        if (d.boss == 255)
                        {
                            Console.WriteLine("Missing Boss in a dungeon?? just retry!");
                            //retry from the start
                            return false;
                        }
                    }
                }
            }


            //TESTING CODE
            //TESTING CODE
            //TESTING CODE
            //TESTING CODE
            //TESTING CODE
            foreach (DungeonProperties d in dungeons)
            {
                /* int r = rand.Next(2);
                 if (r == 0)
                 {*/
                d.boss = 0;
                //}
                /*if (r == 1)
                {
                    d.boss = 9;
                }*/

            }
            //TESTING CODE
            //TESTING CODE
            //TESTING CODE
            //TESTING CODE
            //TESTING CODE

            int did = 0;
            foreach (DungeonProperties d in dungeons)
            {
                if (optionFlags.GenerateSpoilers)
                {
                    spoilerfile.WriteLine(d.name + " : " + BossConstants.BossNames[d.boss].ToString() + "  Drop : " + ROM_DATA[BossConstants.BossDropItemAddresses[did]]);
                }
                Console.WriteLine(d.name + " : " + BossConstants.BossNames[d.boss].ToString());
                ROM_DATA[d.pointerAddr] = BossConstants.BossPointers[d.boss][0];
                ROM_DATA[d.pointerAddr + 1] = BossConstants.BossPointers[d.boss][1];

                //Console.WriteLine((d.bossIn).Address.ToString("X6"));
                /*for (int i = 0; i < (d.bossIn).Pos_array.Length; i++)
                {
                    ROM_DATA[(d.bossIn).Address + i] = (d.bossIn).Pos_array[i]; //patch new position of every bosses
                }*/

                ROM_DATA[0x120090 + ((d.room * 14) + 3)] = BossConstants.BossGraphics[d.boss];
                //Console.WriteLine(d.bossIn.GetType().ToString());
                if (d.boss == 9) // trinexx?
                {
                    ROM_DATA[0x120090 + ((d.room * 14) + 4)] = 04;
                    ROM_DATA[0x120090 + ((d.room * 14) + 2)] = 13;
                    ROM_DATA[0x120090 + ((d.room * 14))] = 0x60;//BG2

                    ROM_DATA[(0xF8000 + (d.room * 3))] = shell_pointers[did][2];
                    ROM_DATA[(0xF8000 + (d.room * 3)) + 1] = shell_pointers[did][1];
                    ROM_DATA[(0xF8000 + (d.room * 3)) + 2] = shell_pointers[did][0];

                    byte[] Pointer = new byte[4];
                    Pointer[0] = ROM_DATA[(0xF8000 + (d.room * 3))];
                    Pointer[1] = ROM_DATA[(0xF8000 + (d.room * 3)) + 1];
                    Pointer[2] = ROM_DATA[(0xF8000 + (d.room * 3)) + 2];
                    int floors_address = snestopc(BitConverter.ToInt32(Pointer, 0));
                    ROM_DATA[floors_address] = 0xF0;
                }

                if (d.boss == 0) // kholdstare
                {
                    //ROM_DATA[0x120090 + ((d.room * 14) + 4)] = 04; effect
                    ROM_DATA[0x120090 + ((d.room * 14) + 4)] = 01;//Effect
                    ROM_DATA[0x120090 + ((d.room * 14) + 2)] = 11;//gfx
                    ROM_DATA[0x120090 + ((d.room * 14))] = 0xE0;//BG2

                    ROM_DATA[(0xF8000 + (d.room * 3))] = shell_pointers[did][2];
                    ROM_DATA[(0xF8000 + (d.room * 3)) + 1] = shell_pointers[did][1];
                    ROM_DATA[(0xF8000 + (d.room * 3)) + 2] = shell_pointers[did][0];

                    byte[] Pointer = new byte[4];
                    Pointer[0] = ROM_DATA[(0xF8000 + (d.room * 3))];
                    Pointer[1] = ROM_DATA[(0xF8000 + (d.room * 3)) + 1];
                    Pointer[2] = ROM_DATA[(0xF8000 + (d.room * 3)) + 2];
                    int floors_address = snestopc(BitConverter.ToInt32(Pointer, 0));
                    ROM_DATA[floors_address] = 0xF0;

                }

                did++;
            }

            return true;
        }

        //0x2E, 0xA0, 0xFF //trinexx
        //0x2D, 0xA1, 0xF9 //kholdstare
        byte[] room_6_shell = new byte[]
        {
        0xE1, 0x00, 0x0C, 0xA5, 0x7F, 0x6C, 0xA5, 0x80, 0xFF, 0xFF,  0x2D, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0,
        0xFF, 0x61, 0x18, 0xFF, 0xFF
        };

        byte[] room_7_shell = new byte[]
        {
            0x81, 0x1C, 0x0A, 0x4E, 0x0D, 0x0A, 0xAA, 0x0E, 0x0B, 0x51, 0x61, 0xC0, 0x2C, 0xA2, 0xB0, 0x20,
            0x0F, 0xB0, 0x22, 0x62, 0xFE, 0xC1, 0x02, 0xC9, 0x38, 0x01, 0xFF, 0xA3, 0x82, 0xBA, 0xE6, 0x10,
            0xE8, 0xAA, 0x62, 0xFF, 0x43, 0xB9, 0x53, 0x53, 0xE0, 0x91, 0x53, 0xE0, 0x53, 0x91, 0xE0, 0x91,
            0x91, 0xE0, 0x3C, 0x6B, 0xC2, 0x3D, 0x9B, 0xC3, 0x54, 0xA6, 0xC3, 0x5C, 0xAA, 0xC3, 0x68, 0xB1,
            0xC3, 0x75, 0xB0, 0xC3, 0x8F, 0xB1, 0xC3, 0x9B, 0xAA, 0xC3, 0xA6, 0xA0, 0xC3, 0xAD, 0x98, 0xC3,
            0xB4, 0x6A, 0xC2, 0x51, 0x3D, 0xC3, 0x45, 0x49, 0xC3, 0x3D, 0x51, 0xC3, 0x9C, 0x39, 0xC2, 0xA1,
            0x49, 0xC3, 0xAD, 0x51, 0xC3, 0x3A, 0x50, 0x8A, 0x38, 0x50, 0x22, 0x44, 0x44, 0x69, 0x44, 0x44,
            0x22, 0x58, 0x13, 0x05, 0x60, 0x15, 0x55, 0x78, 0x10, 0x3A, 0x08, 0x5B, 0x65, 0x0C, 0x61, 0x7F,
            0xC8, 0x39, 0x05, 0xE8, 0x5B, 0x66, 0xEC, 0x4A, 0x80, 0x58, 0xEB, 0x06, 0x60, 0xED, 0x56, 0x78,
            0xEC, 0x3B, 0x50, 0x38, 0x69, 0x50, 0x38, 0x5F, 0xA8, 0x38, 0x69, 0xA8, 0x44, 0x22, 0xB4, 0x44,
            0x69, 0xB4, 0x51, 0x22, 0xC6, 0x50, 0x8A, 0x3B, 0xC8, 0x22, 0x8B, 0xC8, 0x22, 0x74, 0xBC, 0x69,
            0x88, 0xBC, 0x69, 0x63, 0x3C, 0xC2, 0x66, 0x4F, 0x29, 0x64, 0x50, 0x6B, 0x5C, 0x54, 0x2B, 0x5C,
            0x58, 0x6B, 0x54, 0x5C, 0x2B, 0x54, 0x60, 0x6B, 0x4C, 0x64, 0x2B, 0x4E, 0x6B, 0x6B, 0x4C, 0x98,
            0x2D, 0x54, 0x9C, 0x6B, 0x54, 0xA0, 0x2D, 0x5C, 0xA4, 0x6B, 0x5C, 0xA8, 0x2D, 0x64, 0xAC, 0x6B,
            0x66, 0xB3, 0x2A, 0x98, 0xAC, 0x6A, 0x98, 0xA8, 0x2E, 0xA0, 0xA4, 0x6A, 0xA0, 0xA0, 0x2E, 0xA8,
            0x9C, 0x6A, 0xA8, 0x98, 0x2E, 0xB2, 0x6B, 0x6A, 0xA8, 0x64, 0x2C, 0xA8, 0x60, 0x6A, 0xA0, 0x5C,
            0x2C, 0xA0, 0x58, 0x6A, 0x98, 0x54, 0x2C, 0x98, 0x50, 0x6A, 0x68, 0x74, 0xC2, 0x68, 0x71, 0x27,
            0x68, 0x77, 0x6A, 0x74, 0x77, 0x6B, 0x68, 0x85, 0x28, 0xFC, 0x31, 0x72, 0x74, 0xAE, 0x04, 0x71,
            0xA0, 0xE0, 0x0A, 0x13, 0xA0, 0x0A, 0xBF, 0xA1, 0xBE, 0xF7, 0xA3, 0xC3, 0x11, 0xC0, 0xD1, 0x31,
            0x00, 0xFF, 0xFF, 0x61, 0x51, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0xFF, 0xFF

        };


        //0x2E, 0xA0, 0xFF //trinexx
        //0x2D, 0xA1, 0xF9 //kholdstare
        byte[] room_200_shell = new byte[]
        {
            0xE1, 0x00, 0x98, 0x92, 0x3A, 0x88, 0xAA, 0x65, 0xE8, 0xAA, 0x66, 0xFF, 0xFF, 0xAD, 0xA1, 0xF9,
            0xFF, 0xFF, 0xF0, 0xFF, 0x81, 0x18, 0xFF, 0xFF,
        };

        byte[] room_41_shell = new byte[]
{
0xE5, 0x00, 0x97, 0x9C, 0xDE, 0xB7, 0x9C, 0xDE, 0xD6, 0x9C, 0xDE, 0x97, 0xE4, 0xDE, 0xB7, 0xE4,
0xDE, 0xD6, 0xE4, 0xDE, 0x94, 0xA7, 0xDE, 0x94, 0xC7, 0xDE, 0xE4, 0xA7, 0xDE, 0xE4, 0xC7, 0xDE,
0xFF, 0xFF, 0x03, 0x03, 0xCA, 0x43, 0x03, 0xCA, 0x83, 0x03, 0xCA, 0xC3, 0x03, 0xCA, 0x03, 0x43,
0xCA, 0x43, 0x43, 0xCA, 0x83, 0x43, 0xCA, 0xC3, 0x43, 0xCA, 0x03, 0x83, 0xCA, 0x43, 0x83, 0xCA,
0x83, 0x83, 0xCA, 0xC3, 0x83, 0xCA, 0x03, 0xC3, 0xCA, 0x43, 0xC3, 0xCA, 0x83, 0xC3, 0xCA, 0xC3,
0xC3, 0xCA, 0xFF, 0xFF, 0x9F, 0xA7, 0xC6, 0xD4, 0xA7, 0xC6, 0xFE, 0xF9, 0xF4, 0xFF, 0x1E, 0x74,
0xFE, 0x5C, 0x74, 0xFF, 0x9C, 0x74, 0xF0, 0xFF, 0xFF, 0xFF,
};

        byte[] room_51_shell = new byte[]
{
0xE9, 0x00, 0xFF, 0xFF, 0x2D, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0x61, 0x18, 0xFF, 0xFF,
};

        byte[] room_90_shell = new byte[]
{
0xE9, 0x00, 0xA8, 0xA8, 0xDE, 0xB0, 0xA0, 0xDE, 0xB8, 0xA8, 0xDE, 0xC0, 0xA0, 0xDE, 0xC8, 0xA8,
0xDE, 0xD0, 0xA0, 0xDE, 0xFF, 0xFF,  0xAD, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0x81, 0x18, 0xFF,
0xFF,
};

        byte[] room_144_shell = new byte[]
        {
        0xE1, 0x00, 0x28, 0xEC, 0x56, 0x48, 0xEC, 0x56, 0x1B, 0xA2, 0xFF, 0xFF, 0xFF, 0x16, 0x9C, 0xFE,
        0x2D, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0x61, 0x18, 0xFF, 0xFF,
        };

        byte[] room_172_blind_room_shell = new byte[]
{
0xE9, 0x00, 0x88, 0xA4, 0x0D, 0x88, 0xD0, 0x0E, 0xE0, 0x90, 0x0F, 0xE0, 0xE4, 0x10, 0x89, 0xAB,
0x61, 0xE9, 0xAB, 0x62, 0x88, 0x91, 0xA0, 0x88, 0xE5, 0xA1, 0xE4, 0x91, 0xA2, 0xE4, 0xF5, 0xA3,
0xFF, 0xFF, 0xAD, 0xA1, 0xF9, 0xB1, 0xA8, 0xFF, 0xFF, 0xFF, 0xF0, 0xFF, 0x81, 0x18, 0xFF, 0xFF,
// TODO: remove blinds light
};

        byte[] room_222_shell = new byte[]
{
0xE4, 0x00, 0xFF, 0xFF, 0xAD, 0x21, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0xFF, 0xFF,
};

        byte[] room_164_shell = new byte[]
{// 0x2E, 0x98, 0xFF
0xE1, 0x00, 0xFC, 0x08, 0x00, 0x13, 0x80, 0x01, 0xFD, 0xC8, 0x02, 0x02, 0x93, 0x61, 0xFC, 0x0E,
0x81, 0x13, 0xE8, 0x02, 0xFD, 0xCE, 0x83, 0x72, 0x93, 0x62, 0x13, 0x93, 0xC4, 0x51, 0x93, 0xC4,
0x51, 0xC9, 0xC4, 0x10, 0xC9, 0xC4, 0x0E, 0x8D, 0xDE, 0x0D, 0x9C, 0xDE, 0x0C, 0xA5, 0xDE, 0x5E,
0x8C, 0xDE, 0x65, 0x94, 0xDE, 0x6C, 0x9C, 0xDE, 0xFF, 0xFF, 0x2D, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0,
0xFF, 0x61, 0x18, 0xFF, 0xFF,
};

        byte[] room_28_shell = new byte[]
{
0xE1, 0x00, 0x2D, 0x32, 0xA4, 0xA9, 0x1E, 0xDC, 0xA8, 0x91, 0x3A, 0x88, 0xAD, 0x76, 0xEC, 0xAD,
0x77, 0xA8, 0x50, 0x3D, 0xD0, 0x50, 0x3D, 0x30, 0xA9, 0x3D, 0x30, 0xC1, 0x3D, 0xFC, 0x69, 0x38,
0x97, 0x9F, 0xD1, 0xCD, 0x9F, 0xD1, 0x97, 0xDC, 0xD1, 0xCD, 0xDC, 0xD1, 0xBD, 0x32, 0xF9, 0xB1,
0x22, 0xF9, 0xC9, 0x22, 0xF9, 0xFF, 0xFF, 0xAD, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0x80, 0x36,
0x82, 0x18, 0x60, 0x28, 0xFF, 0xFF,
};
        byte[] room_108_shell = new byte[]
{
0xE2, 0x00, 0x17, 0x9F, 0xE8, 0x4D, 0x9F, 0xE8, 0x17, 0xDC, 0xE8, 0x4D, 0xDC, 0xE8, 0x18, 0xE1,
0xFE, 0x88, 0xAD, 0x76, 0x99, 0xBC, 0x33, 0x9B, 0xBB, 0x34, 0x9B, 0xCF, 0x34, 0xD8, 0xB8, 0x34,
0xD8, 0xCC, 0x34, 0xAF, 0xAA, 0xFE, 0xC7, 0xAA, 0xFE, 0xAF, 0xD2, 0xFE, 0xC7, 0xD2, 0xFE, 0x28,
0x11, 0x3A, 0x28, 0x91, 0x3A, 0xFC, 0xE1, 0x38, 0x2B, 0x33, 0xFA, 0x53, 0x33, 0xFA, 0x2B, 0x53,
0xFA, 0x53, 0x53, 0xFA, 0xFF, 0xFF, 0x2D, 0xA1, 0xF9, 0xFF, 0xFF, 0xF0, 0xFF, 0x82, 0x38, 0x60,
0x18, 0x83, 0x00, 0xFF, 0xFF,
};

        byte[] room_77_shell = new byte[]
{
0x82, 0x1C, 0x09, 0x34, 0x0D, 0x08, 0x3A, 0x61, 0x09, 0xC0, 0x0E, 0x08, 0xC2, 0x61, 0xD1, 0x10,
0x0F, 0xE8, 0x3A, 0x62, 0x5E, 0x1C, 0x03, 0x17, 0x49, 0x63, 0xDF, 0x4B, 0x64, 0xDC, 0xCA, 0x64,
0xFF, 0x7D, 0xCB, 0x9D, 0xDF, 0x04, 0x3B, 0x5B, 0xE0, 0x7B, 0x5B, 0xE0, 0xB8, 0x5B, 0xE0, 0x6A,
0xB1, 0xE0, 0x78, 0x54, 0xC2, 0x5B, 0x2A, 0xC2, 0x98, 0x2A, 0xC2, 0x21, 0x4B, 0xC3, 0x21, 0x7B,
0xC3, 0x21, 0xA1, 0xC3, 0x38, 0x7B, 0xC2, 0x48, 0x8A, 0xC2, 0x3A, 0xAA, 0xC2, 0x5B, 0x9C, 0xC2,
0xC9, 0x4B, 0xC3, 0xC9, 0x7B, 0xC3, 0xB8, 0x79, 0xC2, 0xA8, 0x88, 0xC2, 0x9B, 0x9B, 0xC2, 0x9B,
0xD0, 0xC2, 0xD0, 0xA3, 0xC2, 0x78, 0x8C, 0xC2, 0x15, 0x45, 0x22, 0x59, 0x1F, 0x69, 0xA5, 0x1F,
0x69, 0xC9, 0x45, 0x22, 0x68, 0xE4, 0x5E, 0x15, 0xB9, 0x22, 0x35, 0xB9, 0x69, 0x37, 0xD9, 0x22,
0x88, 0xD9, 0x22, 0x98, 0xD9, 0x69, 0x66, 0xCB, 0x2A, 0x69, 0xC9, 0x04, 0x79, 0xCB, 0xF9, 0x8D,
0xBA, 0xF9, 0x37, 0x57, 0x29, 0x87, 0x57, 0x29, 0x78, 0x5A, 0x6A, 0x84, 0x5A, 0x6B, 0x78, 0x65,
0x28, 0x35, 0x5B, 0x6B, 0x34, 0x7A, 0x2D, 0x44, 0x7E, 0x6B, 0x44, 0x8A, 0x2D, 0x54, 0x8E, 0x6B,
0x55, 0x9B, 0x2A, 0x78, 0x8E, 0x6A, 0x78, 0x89, 0x27, 0x84, 0x8E, 0x6B, 0x85, 0x9B, 0x2A, 0xA8,
0x8E, 0x6A, 0xA8, 0x8A, 0x2E, 0xB8, 0x7E, 0x6A, 0xB8, 0x7A, 0x2E, 0xC9, 0x5B, 0x6A, 0x66, 0xAF,
0x29, 0x65, 0xB1, 0x6B, 0x99, 0xB1, 0x6A, 0x38, 0x4B, 0x03, 0xA8, 0x4B, 0x03, 0x48, 0x13, 0x3A,
0x0C, 0x4A, 0x7F, 0xEC, 0x4A, 0x80, 0xFE, 0xE1, 0x39, 0x09, 0x11, 0xA0, 0xD5, 0x11, 0xA2, 0x09,
0xD5, 0xA1, 0xFF, 0xFF, 0x61, 0x51, 0xFF, 0x5B, 0x19, 0xDB, 0x98, 0x19, 0xDB, 0x11, 0x4B, 0xDB,
0x11, 0x8A, 0xDB, 0x39, 0x48, 0xDB, 0xA9, 0x48, 0xDB, 0xD9, 0x4B, 0xDB, 0xD9, 0x8B, 0xDB, 0x6A,
0xC8, 0xDB, 0x9B, 0xCA, 0xDB, 0xD9, 0xCA, 0xDB, 0xFF, 0xFF, 0xF0, 0xFF, 0x00, 0x1C, 0x22, 0x00,
0xFF, 0xFF,
};

        //NEED TO CHANGE BG2 to Addition for Kholdstare shell : Blockset on 11
        //NEED TO CHANGE BG2 to OnTop for Trinexx shell : Blockset on 13 i think







    }






}
