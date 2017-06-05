using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enemizer
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

            dungeons.Add(new DungeonProperties("Hera Tower", 0x04D63C, 7));//should be fine
            dungeons.Add(new DungeonProperties("Eastern Palace", 0x04D7BE, 200));//should be fine
            dungeons.Add(new DungeonProperties("Skull Woods", 0x04D680, 41));//should be fine
            dungeons.Add(new DungeonProperties("Desert Palace", 0x04D694, 51)); // require new pointer for hte room
            dungeons.Add(new DungeonProperties("Palace of Darkness", 0x04D6E2, 90));//should be fine
            dungeons.Add(new DungeonProperties("Misery Mire", 0x04D74E, 144));//should be fine
            dungeons.Add(new DungeonProperties("Thieve Town", 0x04D786, 172)); // require new pointer for hte room
            dungeons.Add(new DungeonProperties("Swamp Palace", 0x04D63A, 6));//should be fine
            dungeons.Add(new DungeonProperties("Ice Palace", 0x04D7EA, 222));// require new pointer for hte room
            dungeons.Add(new DungeonProperties("Turtle Rock", 0x04D776, 164));
            dungeons.Add(new DungeonProperties("Gtower1 (Armos2)", 0x04D666, 28)); //0x04DB23
            dungeons.Add(new DungeonProperties("Gtower2 (Lanmo2)", 0x04D706, 108)); //0x04E1BE
            dungeons.Add(new DungeonProperties("Gtower3 (Moldorm2)", 0x04D6C8, 77));
        }

        int[] itemsAddress = new int[] { 0x180152, 0x180150, 0x180155, 0x180151, 0x180153, 0x180158, 0x180156, 0x180154, 0x180157, 0x180159, 0x0, 0x0, 0x0 };

        byte[] majorItems = new byte[] { 0x32, 0x1D, 0x0F, 0x16, 0x2B, 0x2C, 0x2D, 0x3C, 0x3C, 0x3D, 0x48, 0x3A, 0x3B, 0x0B, 0x15, 0x18, 0x10, 0x07, 0x08, 0x1E, 0x09, 0x0A, 0x24, 0x12, 0x19, 0x1A, 0x29, 0x1F, 0x14, 0x4B, 0x0D, 0x1B, 0x13, 0x1C, 0x5E, 0x61 };


        byte[][] bossOrder = new byte[][] { new byte[] { 0x01, 0xEA },new byte[] { 0xC3, 0xD9 }, new byte[] { 0x31, 0xDC },
                                            new byte[] { 0x57, 0xE4 },new byte[] { 0x49, 0xE0 },new byte[] { 0x87, 0xE8 },
                                            new byte[] { 0xCB, 0xDC },new byte[] { 0x54, 0xE6 },new byte[] { 0x97, 0xD9 }, new byte[] { 0xBA, 0xE5 } };
        byte[] bossGfx = new byte[] { 22, 12, 26, 22, 21, 9, 11, 32, 20, 23 };
        //Debugging use only :
        string[] bossNames = new string[] { "Khold", "Moldorm", "Moth", "Vitty", "Helma", "Armos", "Lanmo", "Blind", "Arrghus", "Trinexx" };

        public void Randomize_Bosses(bool shuffle)
        {
            bool error = false;
            patch_bosses();
            for (int j = 0; j < 20; j++)
            {
            retry:
                dungeons.Clear();
                create_dungeons_properties();
                Console.WriteLine("Randomization : " + j.ToString());
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
                        Console.WriteLine(bossNames[boss] + " Added in the pool");
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
                                if (majorItems.Contains(ROM_DATA[0x180152]))
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

                        if (bosschosed == 0) {

                            continue;
                        }


                        dungeons[12].boss = bosschosed;
                        bosses.Remove(bosschosed);
                        //Console.WriteLine("Infinite Trinexxtry");
                    }

                    if (dungeons[4].boss == 255) //Palace of Darkness can't be kholdstare
                    {
                        bosschosed = bosses[rand.Next(bosses.Count)];
                        if (bosschosed == 0)
                        {
                            continue;
                        }
                        if (bosschosed == 9) //if we pick trinexx check if hera drop any major items if so then put him elsewhere
                        {
                            if (majorItems.Contains(ROM_DATA[0x180153]))
                            {
                                continue;
                            }
                        }
                        dungeons[4].boss = bosschosed;
                        bosses.Remove(bosschosed);
                        //Console.WriteLine("Infinite PoDtry");
                    }
                    byte dungeonChosed = (byte)rand.Next(12);
                    if (bosses.Contains(8)) //since it can have multiple arrghus place all of them first where there's no drop
                    {
                        dungeonChosed = (byte)rand.Next(12);
                        if (dungeons[dungeonChosed].boss != 255)
                        {
                            continue;
                        }
                        if (majorItems.Contains(ROM_DATA[itemsAddress[dungeonChosed]]))
                        {
                            continue;
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
                                goto retry;
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
                        if (majorItems.Contains(ROM_DATA[itemsAddress[dungeonChosed]]))
                        {
                            continue;
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
                                goto retry;
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
                        if (majorItems.Contains(ROM_DATA[itemsAddress[dungeonChosed]]))
                        {
                            continue;
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
                                goto retry;
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
                                goto retry;
                            }
                        }
                    }
                }




                int did = 0;
                foreach (DungeonProperties d in dungeons)
                {
                    if (spoiler)
                    {
                        spoilerfile.WriteLine(d.name + " : " + bossNames[d.boss].ToString() + "  Drop : " + ROM_DATA[itemsAddress[did]]);
                    }
                    Console.WriteLine(d.name + " : " + bossNames[d.boss].ToString());
                    ROM_DATA[d.pointerAddr] = bossOrder[d.boss][0];
                    ROM_DATA[d.pointerAddr + 1] = bossOrder[d.boss][1];
                    //Console.WriteLine((d.bossIn).Address.ToString("X6"));
                    /*for (int i = 0; i < (d.bossIn).Pos_array.Length; i++)
                    {
                        ROM_DATA[(d.bossIn).Address + i] = (d.bossIn).Pos_array[i]; //patch new position of every bosses
                    }*/
                    ROM_DATA[0x120090 + ((d.room * 14) + 3)] = bossGfx[d.boss];
                    //Console.WriteLine(d.bossIn.GetType().ToString());
                    if (d.boss == 9)
                    {
                        ROM_DATA[0x120090 + ((d.room * 14) + 4)] = 04;
                        ROM_DATA[0x120090 + ((d.room * 14) + 2)] = 13;
                    }
                    if (d.boss == 0)
                    {
                        if (majorItems.Contains(ROM_DATA[itemsAddress[did]]))
                        {
                            Console.WriteLine("Error With ITEM !");
                            error = true;
                        }
                    }
                    if (d.boss == 9)
                    {
                        if (majorItems.Contains(ROM_DATA[itemsAddress[did]]))
                        {
                            Console.WriteLine("Error With ITEM !");
                            error = true;
                        }
                    }
                    if (d.name == "Hera Tower")
                    {
                        if (d.boss > 4)
                        {
                            Console.WriteLine("Problem In Hera !");
                            error = true;
                        }
                    }
                    if (d.name == "Palace of Darkness")
                    {
                        if (d.boss == 0)
                        {
                            Console.WriteLine("Kholdstare in PoD !");
                            error = true;
                        }
                    }
                    if (d.name == "Gtower3 (Moldorm2)")
                    {
                        if (d.boss == 0)
                        {
                            Console.WriteLine("Kholdstare in GTOWER 3 !");
                            error = true;
                        }
                        if (d.boss > 4)
                        {
                            Console.WriteLine("Problem in GTOWER 3 !");
                            error = true;
                        }
                    }


                    did++;
                }
            }
            if (error == true)
            {
                Console.WriteLine("Error Found during the test generation !");
            }
            else
            {
                Console.WriteLine("No Error Found during test generation !");
            }

            for (int i = 0; i < 15; i++) //REMOVE BLIND SPAWN CODE
            {
                ROM_DATA[0xEA081 + i] = 0xEA;
            }
            //REMOVE MAIDEN IN TT Basement
            ROM_DATA[0x04DE81] = 0x00;
            //BYTE 4 of the header of the room trinexx is in must be setted on 04


        }
    }
}
