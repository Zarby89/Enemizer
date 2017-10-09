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
        protected StringBuilder spoilerFile { get; set; }

        protected Random rand;
        protected BossPool bossPool;
        protected Graph graph;

        public BossRandomizer(Random rand, OptionFlags optionFlags, StringBuilder spoilerFile, Graph graph)
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

                if (optionFlags.DebugMode)
                {
                    dungeon.SelectedBoss = new KholdstareBoss();
                    //dungeon.SelectedBoss = new TrinexxBoss();
                    continue;
                }

                var boss = bossPool.GetRandomBoss(dungeon.DisallowedBosses, graph);

                //var result = graph.FindPath("cave-links-house", "triforce-room");

                dungeon.SelectedBoss = boss;

                if(dungeon.SelectedBoss == null)
                {
                    var readdDungeon = this.DungeonPool.Where(x => x.SelectedBoss != null && dungeon.DisallowedBosses.Contains(x.SelectedBoss.BossType) == false).FirstOrDefault();
                    if(readdDungeon != null)
                    {
                        boss = readdDungeon.SelectedBoss;
                        dungeon.SelectedBoss = readdDungeon.SelectedBoss;

                        dungeonQueue.Enqueue(readdDungeon);
                        readdDungeon.SelectedBoss = null;

                        // update the graph
                        graph.UpdateDungeonBoss(readdDungeon);
                    }
                }

                if(dungeon.SelectedBoss != null)
                {
                    // update the graph
                    graph.UpdateDungeonBoss(dungeon);

                    bossPool.RemoveBoss(boss);
                }
            }
        }

        private void WriteRom(RomData romData)
        {
            DungeonShells shells = new DungeonShells();
            shells.FillShells();

            if (optionFlags.GenerateSpoilers && spoilerFile != null)
            {
                spoilerFile.AppendLine("Bosses:");
            }

            DungeonObjectDataPointerCollection roomObjects = new DungeonObjectDataPointerCollection(romData);
            
            foreach (var dungeon in DungeonPool)
            {
                // spoilers
                if(optionFlags.GenerateSpoilers && spoilerFile != null)
                {
                    spoilerFile.AppendLine($"{dungeon.Name} : {dungeon.SelectedBoss.BossType}");
                    //spoilerfile.WriteLine(d.name + " : " + BossConstants.BossNames[d.boss].ToString() + "  Drop : " + ROM_DATA[BossConstants.BossDropItemAddresses[did]]);
                }

                // update boss pointer
                romData[dungeon.BossAddress] = dungeon.SelectedBoss.BossPointer[0];
                romData[dungeon.BossAddress + 1] = dungeon.SelectedBoss.BossPointer[1];

                // update boss graphics
                romData[0x120090 + ((dungeon.BossRoomId * 14) + 3)] = dungeon.SelectedBoss.BossGraphics;

                // update trinexx shell
                if(dungeon.SelectedBoss.BossType == BossType.Trinexx && dungeon.BossRoomId != RoomIdConstants.R164_TurtleRock_Trinexx)
                {
                    // TODO: figure out the X/Y coord
                    roomObjects.AddShellAndMoveObjectData(dungeon.BossRoomId, dungeon.ShellX, dungeon.ShellY-2, dungeon.ClearLayer2, 0xFF2);

                    // see "Header contents:" section of rom log
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 0)] = 0x60; // BG2 (upper 3 bits are "BG2")
                    //romData[0x120090 + ((dungeon.BossRoomId * 14) + 2)] = 13; // byte 2: gets stored to $0AA2 (blockset (tileset) in Hyrule Magic)
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 4)] = 04; // byte 4: gets stored to $00AD ("Effect" in Hyrule Magic)

                    //var shell = shells.Shells.Where(x => x.DungeonType == dungeon.DungeonType).FirstOrDefault();
                    //byte[] shellpointer = shell.Pointer;
                    //shell.ShellData[shell.ShellByteOffset] = 0xFF; // change shell to trinexx

                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 0)] = shellpointer[2];
                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 1)] = shellpointer[1];
                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 2)] = shellpointer[0];

                    //byte[] Pointer = new byte[4];
                    //Pointer[0] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 0];
                    //Pointer[1] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 1];
                    //Pointer[2] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 2];
                    //int floors_address = Utilities.SnesToPCAddress(BitConverter.ToInt32(Pointer, 0));
                    //romData[floors_address] = 0xF0;
                }

                // update kholdstare shell
                if (dungeon.SelectedBoss.BossType == BossType.Kholdstare && dungeon.BossRoomId != RoomIdConstants.R222_IcePalace_Kholdstare)
                {
                    roomObjects.AddShellAndMoveObjectData(dungeon.BossRoomId, dungeon.ShellX, dungeon.ShellY, dungeon.ClearLayer2, 0xF95);

                    // TODO: fix this. "debug" flag is set on one of these bytes
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 0)] = 0xE0; // BG2
                    //romData[0x120090 + ((dungeon.BossRoomId * 14) + 2)] = 11; // I suspect this
                    romData[0x120090 + ((dungeon.BossRoomId * 14) + 4)] = 01; 

                    //var shell = shells.Shells.Where(x => x.DungeonType == dungeon.DungeonType).FirstOrDefault();
                    //byte[] shellpointer = shell.Pointer;
                    //shell.ShellData[shell.ShellByteOffset] = 0xF9; // change shell to kholdstare (should be kholdstare by default)

                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 0)] = shellpointer[2];
                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 1)] = shellpointer[1];
                    //romData[0xF8000 + ((dungeon.BossRoomId * 3) + 2)] = shellpointer[0];

                    //byte[] Pointer = new byte[4];
                    //Pointer[0] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 0];
                    //Pointer[1] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 1];
                    //Pointer[2] = romData[(0xF8000 + (dungeon.BossRoomId * 3)) + 2];
                    //int floors_address = Utilities.SnesToPCAddress(BitConverter.ToInt32(Pointer, 0));
                    //romData[floors_address] = 0xF0; // change the floor fill type so hera floor doesn't become black
                }
            }

            roomObjects.WriteChangesToRom(0x122000);
            //shells.WriteShellsToRom(romData);
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
