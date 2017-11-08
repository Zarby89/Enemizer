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

        public void RandomizeRom(RomData romData, SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            FillDungeonPool();
            FillBossPool();

            GenerateRandomizedBosses();
            SetBossSpriteGroups(spriteGroupCollection, spriteRequirementCollection);
            WriteRom(romData);
        }

        void SetBossSpriteGroups(SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            foreach(var d in this.DungeonPool)
            {
                var group = spriteGroupCollection.SpriteGroups.Where(x => x.DungeonGroupId == d.SelectedBoss.BossGraphics).FirstOrDefault();
                var sprite = spriteRequirementCollection.SpriteRequirements.Where(x => x.SpriteId == d.SelectedBoss.BossSpriteId).FirstOrDefault();
                if(group != null && sprite != null)
                {
                    group.PreserveSubGroup0 = sprite.SubGroup0.Count > 0;
                    group.PreserveSubGroup1 = sprite.SubGroup1.Count > 0;
                    group.PreserveSubGroup2 = sprite.SubGroup2.Count > 0;
                    group.PreserveSubGroup3 = sprite.SubGroup3.Count > 0;
                }
            }
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

                var boss = bossPool.GetRandomBoss(dungeon, graph);

                var result = graph.FindPath("cave-links-house", "triforce-room");

                dungeon.SelectedBoss = boss;

                if(dungeon.SelectedBoss == null)
                {
                    //if(false && CanGetBossRoomAndDefeat(dungeon, result))
                    //{
                    //    boss = bossPool.GetNextBoss();
                    //    dungeon.SelectedBoss = boss;
                    //}
                    //else
                    //{

                    var readdPool = this.DungeonPool.Where(x => x.SelectedBoss != null && dungeon.DisallowedBosses.Contains(x.SelectedBoss.BossType) == false).ToList();
                    //var readdDungeon = this.DungeonPool.Where(x => x.SelectedBoss != null && dungeon.DisallowedBosses.Contains(x.SelectedBoss.BossType) == false).FirstOrDefault();
                    if(readdPool.Count > 0)
                    {
                        var readdDungeon = readdPool[rand.Next(0, readdPool.Count)];
                        boss = readdDungeon.SelectedBoss;
                        dungeon.SelectedBoss = readdDungeon.SelectedBoss;
                        bossPool.ReaddBoss(boss);

                        dungeonQueue.Enqueue(readdDungeon);
                        readdDungeon.SelectedBoss = null;

                        // update the graph
                        graph.UpdateDungeonBoss(readdDungeon);
                    }

                    //}
                }

                if (dungeon.SelectedBoss != null)
                {
                    // update the graph
                    graph.UpdateDungeonBoss(dungeon);

                    bossPool.RemoveBoss(boss);
                }
            }
        }

        private void WriteRom(RomData romData)
        {
            //DungeonShells shells = new DungeonShells();
            //shells.FillShells();

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
                romData[dungeon.DungeonRoomSpritePointerAddress] = dungeon.SelectedBoss.BossPointer[0];
                romData[dungeon.DungeonRoomSpritePointerAddress + 1] = dungeon.SelectedBoss.BossPointer[1];

                // update boss graphics
                romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 3)] = dungeon.SelectedBoss.BossGraphics;

                // update trinexx shell
                if(dungeon.SelectedBoss.BossType == BossType.Trinexx && dungeon.BossRoomId != RoomIdConstants.R164_TurtleRock_Trinexx)
                {
                    // TODO: figure out the X/Y coord
                    roomObjects.AddShellAndMoveObjectData(dungeon.BossRoomId, dungeon.ShellX, dungeon.ShellY-2, dungeon.ClearLayer2, 0xFF2);

                    // see "Header contents:" section of rom log
                    romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 0)] = 0x60; // BG2 (upper 3 bits are "BG2")
                    //romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 2)] = 13; // byte 2: gets stored to $0AA2 (blockset (tileset) in Hyrule Magic)
                    romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 4)] = 04; // byte 4: gets stored to $00AD ("Effect" in Hyrule Magic)
                }

                // update kholdstare shell
                if (dungeon.SelectedBoss.BossType == BossType.Kholdstare && dungeon.BossRoomId != RoomIdConstants.R222_IcePalace_Kholdstare)
                {
                    roomObjects.AddShellAndMoveObjectData(dungeon.BossRoomId, dungeon.ShellX, dungeon.ShellY, dungeon.ClearLayer2, 0xF95);

                    // TODO: fix this. "debug" flag is set on one of these bytes
                    romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 0)] = 0xE0; // BG2
                    //romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 2)] = 11; // I suspect this
                    romData[AddressConstants.dungeonHeaderBaseAddress + ((dungeon.BossRoomId * 14) + 4)] = 01; 
                }

                // remove trinexx shell
                if (dungeon.SelectedBoss.BossType != BossType.Trinexx && dungeon.BossRoomId == RoomIdConstants.R164_TurtleRock_Trinexx)
                {
                    // TODO: figure out the X/Y coord
                    roomObjects.RemoveShellAndMoveObjectData(dungeon.BossRoomId, 0xFF2);
                }

                // remove kholdstare shell
                if (dungeon.SelectedBoss.BossType != BossType.Kholdstare && dungeon.BossRoomId == RoomIdConstants.R222_IcePalace_Kholdstare)
                {
                    // TODO: figure out the X/Y coord
                    roomObjects.RemoveShellAndMoveObjectData(dungeon.BossRoomId, 0xF95);
                }

                if(dungeon.DungeonType == DungeonType.GanonsTower1 || dungeon.DungeonType == DungeonType.GanonsTower2)
                {
                    // TODO: clean this up and move it
                    // override the sprites pointer and make some new ones in an "unused" spot in rom 
                    // (in the room sprite pointer table, it has way more room than is used)
                    int startingAddress = 0x0;
                    if(dungeon.DungeonType == DungeonType.GanonsTower1)
                    {
                        startingAddress = 0x4D87E;
                    }
                    else
                    {
                        startingAddress = 0x4D8B6;
                    }

                    romData[dungeon.DungeonRoomSpritePointerAddress] = (byte)(startingAddress & 0xFF);
                    romData[dungeon.DungeonRoomSpritePointerAddress + 1] = (byte)((startingAddress >> 8) & 0xFF);

                    romData[startingAddress++] = 0x00;
                    romData.WriteDataChunk(startingAddress, dungeon.SelectedBoss.BossSpriteArray);
                    startingAddress += dungeon.SelectedBoss.BossSpriteArray.Length;
                    // hack for arrghus
                    if(dungeon.DungeonType == DungeonType.GanonsTower1 && dungeon.SelectedBoss.BossType == BossType.Arrghus)
                    {
                        // only write 2 sprites (should be 2x fairy)
                        romData.WriteDataChunk(startingAddress, dungeon.ExtraSprites, 6);
                        startingAddress += 6;
                    }
                    else
                    {
                        romData.WriteDataChunk(startingAddress, dungeon.ExtraSprites);
                        startingAddress += dungeon.ExtraSprites.Length;
                    }
                    romData[startingAddress] = 0xFF;
                }
            }

            roomObjects.WriteChangesToRom(AddressConstants.movedRoomObjectBaseAddress);
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

        protected bool CanGetBossRoomAndDefeat(Dungeon dungeon, GraphResult result)
        {
            bool bossRoom = false;
            bool bossWin = false;
            switch (dungeon.BossRoomId)
            {
                case RoomIdConstants.R200_EasternPalace_ArmosKnights:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "eastern-armos");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Eastern Boss]");
                    break;
                case RoomIdConstants.R51_DesertPalace_Lanmolas:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "desert-lanmolas");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Desert Boss]");
                    break;
                case RoomIdConstants.R7_TowerofHera_Moldorm:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "hera-moldorm");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Hera Boss]");
                    break;
                case RoomIdConstants.R90_PalaceofDarkness_HelmasaurKing:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "pod-helmasaur");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[PoD Boss]");
                    break;
                case RoomIdConstants.R6_SwampPalace_Arrghus:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "swamp-arrghus");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Swamp Boss]");
                    break;
                case RoomIdConstants.R41_SkullWoods_Mothula:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "skull-mothula");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Skull Boss]");
                    break;
                case RoomIdConstants.R172_ThievesTown_BlindTheThief:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "thieves-blind");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Thieves Boss]");
                    break;
                case RoomIdConstants.R222_IcePalace_Kholdstare:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "ice-kholdstare");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Ice Boss]");
                    break;
                case RoomIdConstants.R144_MiseryMire_Vitreous:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "mire-vitreous");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Mire Boss]");
                    break;
                case RoomIdConstants.R164_TurtleRock_Trinexx:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "turtle-trinexx");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[Turtle Boss]");
                    break;
                case RoomIdConstants.R28_GanonsTower_IceArmos:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "gt-armos");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[GT Armos Boss]");
                    break;
                case RoomIdConstants.R108_GanonsTower_LanmolasRoom:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "gt-lanmolas");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[GT Lanmolas Boss]");
                    break;
                case RoomIdConstants.R77_GanonsTower_MoldormRoom:
                    bossRoom = result.NodesVisited.Any(x => x.LogicalId == "gt-moldorm");
                    bossWin = result.ItemsObtained.Any(x => x.LogicalId == "[GT Moldorm Boss]");
                    break;
            }
            // if we can't even get to the room no need to switch it, because something else is blocking the seed
            return !bossRoom || bossWin;
        }
    }
}
