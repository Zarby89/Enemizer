using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class BossRandomizer
    {
        public List<Boss> PossibleBossesPool { get; set; } = new List<Boss>();

        public List<Dungeon> DungeonPool { get; set; } = new List<Dungeon>();

        OptionFlags optionFlags { get; set; }
        StreamWriter spoilerFile { get; set; }

        Random rand;
        public BossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile)
        {
            this.rand = rand;
            this.optionFlags = optionFlags;
            this.spoilerFile = spoilerFile;

            FillDungeonPool();
            FillBossPool();
        }

        public BossRandomizer(Random rand) : this(rand, new OptionFlags(), null) { }

        void FillDungeonPool()
        {
            DungeonPool.Add(new EasternPalaceDungeon());
            DungeonPool.Add(new DesertPalaceDungeon());
            DungeonPool.Add(new TowerOfHeraDungeon(1));
            DungeonPool.Add(new PalaceOfDarknessDungeon());
            DungeonPool.Add(new SwampPalaceDungeon());
            DungeonPool.Add(new SkullWoodsDungeon());
            DungeonPool.Add(new ThievesTownDungeon());
            DungeonPool.Add(new IcePalaceDungeon());
            DungeonPool.Add(new MiseryMireDungeon());
            DungeonPool.Add(new TurtleRockDungeon());
            DungeonPool.Add(new GT1Dungeon());
            DungeonPool.Add(new GT2Dungeon());
            DungeonPool.Add(new GT3Dungeon(2));
        }

        protected void FillBossPool()
        {
            FillBasePool();
            FillGTPool();
        }

        // TODO: add boss madness
        /*
        for (int i = 0; i < 13; i++)
        {
            byte boss = (byte)rand.Next(10);

            if (i <= 2) //force at least 3 middle of room boss
            {
                ///*KholdstareBossId = 0,MoldormBossId = 1,MothulaBossId = 2,VitreousBossId = 3,HelmasaurBossId = 4,
                boss = (byte)rand.Next(5);
            }
            bosses.Add(boss);
            Console.WriteLine(BossConstants.BossNames[boss] + " Added in the pool");
        }
         */

        protected void FillBasePool()
        {
            PossibleBossesPool.Add(new ArmosBoss());
            PossibleBossesPool.Add(new LanmolaBoss());
            PossibleBossesPool.Add(new MoldormBoss());
            PossibleBossesPool.Add(new HelmasaurBoss());
            PossibleBossesPool.Add(new ArrghusBoss());
            PossibleBossesPool.Add(new MothulaBoss());
            PossibleBossesPool.Add(new BlindBoss());
            PossibleBossesPool.Add(new KholdstareBoss());
            PossibleBossesPool.Add(new VitreousBoss());
            PossibleBossesPool.Add(new TrinexxBoss());
        }

        protected void FillGTPool()
        {
            PossibleBossesPool.Add(new ArmosBoss()); // GT1
            PossibleBossesPool.Add(new LanmolaBoss()); // GT2
            PossibleBossesPool.Add(new MoldormBoss()); // GT3
        }

        public void GenerateRandomizedBosses(RomData romData)
        {
            foreach(var dungeon in this.DungeonPool.OrderBy(x => x.Priority))
            {
                var possibleBosses = this.PossibleBossesPool.Where(x => dungeon.DisallowedBosses.Contains(x.BossType) == false);
                if(possibleBosses.Count() == 0)
                {
                    throw new Exception($"Couldn't find any possible bosses not disallowed for dungeon: {dungeon.Name}");
                }

                possibleBosses = possibleBosses.Where(x => x.CheckRules(dungeon, romData) == false);
                if (possibleBosses.Count() == 0)
                {
                    throw new Exception($"Couldn't find any possible bosses meeting item checks for dungeon: {dungeon.Name}");
                }

                Boss boss = possibleBosses.ElementAt(rand.Next(possibleBosses.Count()));

                dungeon.SelectedBoss = boss;

                this.PossibleBossesPool.Remove(boss);
            }
        }

        public void RandomizeRom(RomData romData)
        {
            GenerateRandomizedBosses(romData);
            WriteRom(romData);
        }

        private void WriteRom(RomData romData)
        {
            DungeonShells shells = new DungeonShells();
            shells.FillShells();
            shells.WriteShellsToRom(romData);

            foreach(var dungeon in DungeonPool)
            {
                // spoilers
                if(optionFlags.GenerateSpoilers && spoilerFile != null)
                {
                    spoilerFile.WriteLine($"dungeon name : boss name - drop: boss drop item");
                    //spoilerfile.WriteLine(d.name + " : " + BossConstants.BossNames[d.boss].ToString() + "  Drop : " + ROM_DATA[BossConstants.BossDropItemAddresses[did]]);
                }
                // update boss pointer
                romData[dungeon.BossAddress] = dungeon.SelectedBoss.BossPointer[0];
                romData[dungeon.BossAddress + 1] = dungeon.SelectedBoss.BossPointer[1];

                // update boss graphics
                romData[0x120090 + ((dungeon.BossRoomId * 14) + 3)] = dungeon.SelectedBoss.BossGraphics;

                // update trinexx shell
                if(dungeon.SelectedBoss.BossType == BossType.Trixnexx)
                {
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 4)] = 04;
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 2)] = 13;
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 0)] = 0x60; // BG2

                    byte[] shellpointer = shells.Shells.Where(x => x.DungeonType == dungeon.DungeonType).Select(x => x.Pointer).First();
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 0)] = shellpointer[2];
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 1)] = shellpointer[1];
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 2)] = shellpointer[0];

                    byte[] Pointer = new byte[4];
                    Pointer[0] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 0];
                    Pointer[1] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 1];
                    Pointer[2] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 2];
                    int floors_address = Utilities.SnesToPCAddress(BitConverter.ToInt32(Pointer, 0));
                    romData[floors_address] = 0xF0;
                }

                // update kholdstare shell
                if (dungeon.SelectedBoss.BossType == BossType.Kholdstare)
                {
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 4)] = 01;
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 2)] = 11;
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 0)] = 0xE0; // BG2

                    byte[] shellpointer = shells.Shells.Where(x => x.DungeonType == dungeon.DungeonType).Select(x => x.Pointer).First();
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 0)] = shellpointer[2];
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 1)] = shellpointer[1];
                    romData[0xF8000 + ((dungeon.BossRoomId * 3) + 2)] = shellpointer[0];

                    byte[] Pointer = new byte[4];
                    Pointer[0] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 0];
                    Pointer[1] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 1];
                    Pointer[2] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 2];
                    int floors_address = Utilities.SnesToPCAddress(BitConverter.ToInt32(Pointer, 0));
                    romData[floors_address] = 0xF0;
                }
            }

            RemoveBlindSpawnCode(romData);
            RemoveMaidenFromThievesTown(romData);
        }

        private void RemoveBlindSpawnCode(RomData romData)
        {
            for (int i = 0; i < 15; i++) //REMOVE BLIND SPAWN CODE
            {
                romData[0xEA081 + i] = AssemblyConstants.NoOp;
            }
        }

        private void RemoveMaidenFromThievesTown(RomData romData)
        {
            //REMOVE MAIDEN IN TT Basement
            romData[0x04DE81] = 0x00;
        }

    }
}
