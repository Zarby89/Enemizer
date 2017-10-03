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
        public List<Dungeon> DungeonPool { get; set; } = new List<Dungeon>();

        protected OptionFlags optionFlags { get; set; }
        protected StreamWriter spoilerFile { get; set; }

        protected Random rand;
        protected BossPool bossPool;
        protected Graph graph;

        public BossRandomizer(Random rand, OptionFlags optionFlags, StreamWriter spoilerFile, Graph graph)
        {
            this.rand = rand;
            this.optionFlags = optionFlags;
            this.spoilerFile = spoilerFile;
            this.graph = graph;
            this.bossPool = new BossPool(rand);
        }

        public BossRandomizer(Random rand, Graph graph) : this(rand, new OptionFlags(), null, graph) { }

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
            bossPool.FillPool();
        }

        public void RandomizeRom(RomData romData)
        {
            FillDungeonPool();
            FillBossPool();

            GenerateRandomizedBosses();
            WriteRom(romData);
        }

        protected virtual void GenerateRandomizedBosses()
        {
            var dungeonQueue = new Queue<Dungeon>(this.DungeonPool);

            while(dungeonQueue.Count > 0)
            {
                var dungeon = dungeonQueue.Dequeue();

                var boss = bossPool.GetRandomBoss(dungeon.DisallowedBosses, graph);

                //var result = graph.FindPath("cave-links-house", "triforce-room");

                dungeon.SelectedBoss = boss;

                if(dungeon.SelectedBoss == null)
                {
                    var readdDungeon = this.DungeonPool.Where(x => x.SelectedBoss != null && dungeon.DisallowedBosses.Contains(x.SelectedBoss.BossType) == false).FirstOrDefault();
                    if(readdDungeon != null)
                    {
                        dungeonQueue.Enqueue(readdDungeon);
                        bossPool.ReaddBoss(readdDungeon.SelectedBoss);
                        readdDungeon.SelectedBoss = null;
                    }
                    dungeonQueue.Enqueue(dungeon);
                }
                else
                {
                    bossPool.RemoveBoss(boss);
                }
            }
        }

        private void WriteRom(RomData romData)
        {
            DungeonShells shells = new DungeonShells();
            shells.FillShells();
            shells.WriteShellsToRom(romData);

            if (optionFlags.GenerateSpoilers)
            {
                spoilerFile.WriteLine("Bosses:");
                foreach (var d in DungeonPool)
                {

                }
            }

            foreach (var dungeon in DungeonPool)
            {
                // spoilers
                if(optionFlags.GenerateSpoilers && spoilerFile != null)
                {
                    spoilerFile.WriteLine($"{dungeon.Name} : {dungeon.SelectedBoss.BossType}");
                    //spoilerfile.WriteLine(d.name + " : " + BossConstants.BossNames[d.boss].ToString() + "  Drop : " + ROM_DATA[BossConstants.BossDropItemAddresses[did]]);
                }

                // update boss pointer
                romData[dungeon.BossAddress] = dungeon.SelectedBoss.BossPointer[0];
                romData[dungeon.BossAddress + 1] = dungeon.SelectedBoss.BossPointer[1];

                // update boss graphics
                romData[0x120090 + ((dungeon.BossRoomId * 14) + 3)] = dungeon.SelectedBoss.BossGraphics;

                // update trinexx shell
                if(dungeon.SelectedBoss.BossType == BossType.Trinexx)
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
