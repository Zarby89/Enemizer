using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public partial class Randomization
    {
        // TODO: move these
        private const int SahasrahlaItemAddress = 0x2F1FC;
        private const int CrystalTypePendant = 0x00;
        private const int CrystalGreenPendant = 0x04;
        private const int CrystalTypeCrystal = 0x40;
        private const int Crystal5 = 0x04;
        private const int Crystal6 = 0x01;
        private const int FatFairyItem1Address = 0xE980;
        private const int FatFairyItem2Address = 0xE983;
        private const int MasterSwordPedestal = 0x289B0;

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

        public void Randomize_Bosses(bool bossMadness)
        {
            bool error = false;
            patch_bosses();

            while (TryRandomizeBosses(bossMadness) != true)
            {
                // loop until we succeed (some bosses can't be in certain places so we have to retry)
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

        private bool TryRandomizeBosses(bool bossMadness)
        {
            dungeons.Clear();
            create_dungeons_properties();

            List<byte> bosses = ShuffleBosses(bossMadness);

            //try to place boss in hera and gtower moldorm first
            //Moldorm,Kholdstare,Vitreous,Helmasaure,Mothula

            //Palace of Darkness can't have Kholdstare. Why?

            if(TryRandomizeBossList(bosses) == false)
            {
                return false;
            }

            //TESTING CODE
            //foreach (DungeonProperties d in dungeons)
            //{
            //    /* int r = rand.Next(2);
            //     if (r == 0)
            //     {*/
            //    d.boss = 0;
            //    //}
            //    /*if (r == 1)
            //    {
            //        d.boss = 9;
            //    }*/
            //}
            //TESTING CODE

            UpdateDungeonsInRom();

            return true;
        }

        private List<byte> ShuffleBosses(bool bossMadness)
        {
            List<byte> bosses = new List<byte>();
            // TODO: add a 3rd mode that will randomize the gt bosses without bossmadness set
            if (bossMadness == false)
            {
                for (byte i = 0; i < 10; i++)
                {
                    bosses.Add(i);
                }
                bosses.Add(1);//gtower double
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

            return bosses;
        }


        private bool TryRandomizeBossList(List<byte> bosses)
        {
            while (bosses.Count > 0)
            {
                byte selectedBoss = bosses[rand.Next(bosses.Count)];

                //Tower of Hera
                if (dungeons[DungeonConstants.TowerOfHeraDungeonId].boss == 255)
                {
                    selectedBoss = bosses[rand.Next(bosses.Count)];

                    /* KholdstareBossId = 0, MoldormBossId = 1, MothulaBossId = 2, VitreousBossId = 3, HelmasaurBossId = 4,*/
                    if (selectedBoss > 4)
                    {
                        continue;
                    }

                    if (selectedBoss == 0) //if we pick kholdstare check if hera drop any major items if so then put him elsewhere
                    {
                        if (CheckGTowerAndPedestalForItems(ItemConstants.FireRod))
                        {
                            continue;
                        }

                        //if it kholdstare then check if hera drop the green pendant and if sarasrala have a major item
                        if(CheckIfContainsImportantItems(DungeonConstants.TowerOfHeraDungeonId))
                        {
                            continue;
                        }
                    }

                    dungeons[DungeonConstants.TowerOfHeraDungeonId].boss = selectedBoss;
                    bosses.Remove(selectedBoss);
                }

                //Gtower Moldorm
                if (dungeons[DungeonConstants.GTower3DungeonId].boss == 255)
                {
                    selectedBoss = bosses[rand.Next(bosses.Count)];

                    /* KholdstareBossId = 0, MoldormBossId = 1, MothulaBossId = 2, VitreousBossId = 3, HelmasaurBossId = 4,*/
                    if (selectedBoss > 4)
                    {
                        continue;
                    }

                    dungeons[DungeonConstants.GTower3DungeonId].boss = selectedBoss;
                    bosses.Remove(selectedBoss);
                }


                byte selectedDungeon = (byte)rand.Next(12);

                //since it can have multiple arrghus place all of them first where there's no drop
                if (bosses.Contains(BossConstants.ArrghusBossId)) 
                {
                    selectedDungeon = (byte)rand.Next(12);

                    if (dungeons[selectedDungeon].boss != 255)
                    {
                        continue;
                    }

                    if (selectedDungeon != DungeonConstants.SwampPalaceDungeonId)
                    {
                        if (CheckIfContainsImportantItems(selectedDungeon))
                        {
                            continue;
                        }
                    }

                    if (CheckGTowerAndPedestalForItems(ItemConstants.Hookshot))
                    {
                        if (dungeons[DungeonConstants.SwampPalaceDungeonId].boss == 255)
                        {
                            //put arrghus in his original location
                            dungeons[DungeonConstants.SwampPalaceDungeonId].boss = BossConstants.ArrghusBossId;
                            bosses.Remove(BossConstants.ArrghusBossId);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops Hookshot is in gtower can't put more than one arrghus!");
                            return false;
                        }
                    }
                    dungeons[selectedDungeon].boss = BossConstants.ArrghusBossId;
                    bosses.Remove(BossConstants.ArrghusBossId);
                }


                //IF Kholdstare is not placed already place him first
                if (bosses.Contains(BossConstants.KholdstareBossId))
                {
                    selectedDungeon = (byte)rand.Next(12);

                    if (dungeons[selectedDungeon].boss != 255)
                    {
                        continue;
                    }

                    if (selectedDungeon != DungeonConstants.IcePalaceDungeonId)
                    {
                        if (CheckIfContainsImportantItems(selectedDungeon))
                        {
                            continue;
                        }
                    }

                    if (CheckGTowerAndPedestalForItems(ItemConstants.FireRod))
                    {
                        if (dungeons[DungeonConstants.IcePalaceDungeonId].boss == 255)
                        {
                            //put kholdstare in his original location
                            dungeons[DungeonConstants.IcePalaceDungeonId].boss = BossConstants.KholdstareBossId;
                            bosses.Remove(BossConstants.KholdstareBossId);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops fire rod is in GTOWER can't put more than 1 Kholdstare reset");
                            return false;
                        }
                    }

                    dungeons[selectedDungeon].boss = BossConstants.KholdstareBossId;
                    bosses.Remove(BossConstants.KholdstareBossId);
                }

                selectedDungeon = (byte)rand.Next(12);

                //IF Trinexx is not placed already place him first
                if (bosses.Contains(BossConstants.TrixnessBossId))
                {
                    selectedDungeon = (byte)rand.Next(12);

                    if (dungeons[selectedDungeon].boss != 255)
                    {
                        selectedDungeon = (byte)rand.Next(12);
                    }

                    if (selectedDungeon != DungeonConstants.TurtleRockDungeonId)
                    {
                        if (CheckIfContainsImportantItems(selectedDungeon))
                        {
                            continue;
                        }
                    }

                    if (CheckGTowerAndPedestalForItems(ItemConstants.FireRod, ItemConstants.IceRod))
                    {
                        if (dungeons[DungeonConstants.TurtleRockDungeonId].boss == 255)
                        {
                            //put trinexx in his original location
                            dungeons[DungeonConstants.TurtleRockDungeonId].boss = BossConstants.TrixnessBossId;
                            bosses.Remove(BossConstants.TrixnessBossId);
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Woops Ice rod or fire rod is in GTOWER can't put more than 1 trinxx reset");
                            return false;
                        }
                    }

                    dungeons[selectedDungeon].boss = BossConstants.TrixnessBossId;
                    bosses.Remove(BossConstants.TrixnessBossId);
                }

                selectedDungeon = (byte)rand.Next(12);

                if (dungeons[selectedDungeon].boss == 255)
                {
                    selectedBoss = bosses[rand.Next(bosses.Count)];
                    dungeons[selectedDungeon].boss = selectedBoss;
                    bosses.Remove(selectedBoss);
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

            return true;
        }

        private bool CheckIfContainsImportantItems(int dungeonId)
        {
            //if it kholdstare then check if hera drop the green pendant and if sarasrala have a major item
            if (CrystalConstants.CrystalTypeAddresses[dungeonId] == CrystalTypePendant) //is pendant?
            {
                if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonId]] == CrystalGreenPendant) //if hera is green pendant
                {
                    if (ItemConstants.ImportantItems.Contains(ROM_DATA[SahasrahlaItemAddress])) //sarashala items
                    {
                        return true;
                    }
                }
            }
            if (CrystalConstants.CrystalTypeAddresses[dungeonId] == CrystalTypeCrystal) //is crystal?
            {
                if (ROM_DATA[CrystalConstants.CrystalAddresses[dungeonId]] == Crystal5 || ROM_DATA[CrystalConstants.CrystalAddresses[dungeonId]] == Crystal6) //if hera is crystal 5 or crystal 6
                {
                    if (ItemConstants.ImportantItems.Contains(ROM_DATA[FatFairyItem1Address]) || ItemConstants.ImportantItems.Contains(ROM_DATA[FatFairyItem2Address])) //fat fairy
                    {
                        return true;
                    }
                }
            }
            if (ItemConstants.ImportantItems.Contains(ROM_DATA[BossConstants.TowerOfHeraBossDropItemAddress]))
            {
                return true;
            }

            return false;
        }

        private bool CheckGTowerAndPedestalForItems(params byte[] items)
        {
            foreach(var item in items)
            {
                if(scan_gtower(item))
                {
                    return true;
                }
                if(ROM_DATA[ItemConstants.MasterSwordPedestalAddress] == item)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateDungeonsInRom()
        {
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
        }


        //NEED TO CHANGE BG2 to Addition for Kholdstare shell : Blockset on 11
        //NEED TO CHANGE BG2 to OnTop for Trinexx shell : Blockset on 13 i think



    }






}
