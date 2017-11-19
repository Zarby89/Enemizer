using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DebugMode
    {
        RomData romData;
        OptionFlags optionFlags;

        public DebugMode(RomData romData, OptionFlags optionFlags)
        {
            this.romData = romData;
            this.optionFlags = optionFlags;
        }

        public void SetDebugMode()
        {
            var rand = new Random();
            var sr = new SpriteRequirementCollection();
            var sg = new SpriteGroupCollection(romData, rand, sr);
            sg.LoadSpriteGroups();
            var rc = new RoomCollection(romData, rand, sg, sr);
            var owa = new OverworldAreaCollection(romData, rand, sg, sr);
            rc.LoadRooms();

            if (optionFlags.DebugForceBoss)
            {
                // this is set in the boss randomizer because there is too much to do after picking a boss
            }
            if(optionFlags.DebugForceEnemy)
            {
                SetDungeonEnemy(rc);
                SetOverworldEnemy(owa);
                SetSpriteGroups(sg, sr);
            }
            if(optionFlags.DebugOpenShutterDoors)
            {
                RemoveKillRooms(rc);
            }
            if (optionFlags.DebugForceEnemyDamageZero)
            {
                SetEnemyDamageZero();
            }

            if (optionFlags.DebugShowRoomIdInRupeeCounter)
            {
                // put the room id in the rupee slot
                this.romData[0x1017A9] = 0xA0;
                this.romData[0x1017A9 + 1] = 0x00;
                this.romData[0x1017A9 + 2] = 0x7E;
            }

            rc.UpdateRom();
            owa.UpdateRom();
            sg.UpdateRom();
        }

        private void SetSpriteGroups(SpriteGroupCollection sg, SpriteRequirementCollection sr)
        {
            var req = sr.SpriteRequirements.FirstOrDefault(x => x.SpriteId == optionFlags.DebugForceEnemyId);
            var owg = sg.SpriteGroups.FirstOrDefault(x => x.GroupId == 7);
            var dg = sg.SpriteGroups.FirstOrDefault(x => x.GroupId == 68);
            if (req != null)
            {
                if(req.SubGroup0.Count > 0)
                {
                    owg.SubGroup0 = req.SubGroup0.FirstOrDefault();
                    dg.SubGroup0 = req.SubGroup0.FirstOrDefault();
                }
                if (req.SubGroup1.Count > 0)
                {
                    owg.SubGroup1 = req.SubGroup1.FirstOrDefault();
                    dg.SubGroup1 = req.SubGroup1.FirstOrDefault();
                }
                if (req.SubGroup2.Count > 0)
                {
                    owg.SubGroup2 = req.SubGroup2.FirstOrDefault();
                    dg.SubGroup2 = req.SubGroup2.FirstOrDefault();
                }
                if (req.SubGroup3.Count > 0)
                {
                    owg.SubGroup3 = req.SubGroup3.FirstOrDefault();
                    dg.SubGroup3 = req.SubGroup3.FirstOrDefault();
                }
                else
                {
                    // give switches and traps a fighting chance if the sprite doesn't need slot 3
                    dg.SubGroup3 = 82;
                }
            }
        }

        void SetDungeonEnemy(RoomCollection rc)
        {
            foreach(var r in rc.Rooms)
            {
                foreach(var s in r.Sprites)
                {
                    if(false == dontModifySprites.Contains(s.SpriteId))
                    {
                        s.SpriteId = optionFlags.DebugForceEnemyId;
                        r.GraphicsBlockId = 4;
                    }
                }
            }
        }

        void SetOverworldEnemy(OverworldAreaCollection owa)
        {
            foreach(var o in owa.OverworldAreas)
            {
                foreach(var s in o.Sprites)
                {
                    if(false == dontModifySprites.Contains(s.SpriteId))
                    {
                        s.SpriteId = optionFlags.DebugForceEnemyId;
                        o.GraphicsBlockId = 7;
                    }
                }
                o.BushSpriteId = optionFlags.DebugForceEnemyId;
            }
        }

        void RemoveKillRooms(RoomCollection rc)
        {
            if(optionFlags.DebugOpenShutterDoors)
            {
                var doc = new DungeonObjectDataPointerCollection(romData);
                foreach(var r in doc.RoomDungeonObjectDataPointers.Values.Where(x => shutterDoorRooms.Contains(x.RoomId)).ToList())
                {
                    r.MakeShutterDoorsNormal();
                }
                doc.WriteChangesToRom(AddressConstants.movedRoomObjectBaseAddress);

                var killTags = RoomTags.Tags.Where(x => x.Value.ToLower().Contains("kill")).Select(x => x.Key).ToList();

                foreach (var r in rc.Rooms.Where(x => shutterDoorRooms.Contains(x.RoomId)).ToList())
                {
                    if (killTags.Any(x => x == r.Tag1))
                    {
                        r.Tag1 = 0;
                    }
                    if (killTags.Any(x => x == r.Tag2))
                    {
                        r.Tag2 = 0;
                    }
                }
            }
        }

        public int[] shutterDoorRooms = 
        {
            RoomIdConstants.R2_HyruleCastle_SwitchRoom,
            RoomIdConstants.R4_TurtleRock_CrystalRollerRoom,
            //RoomIdConstants.R6_SwampPalace_Arrghus,
            //RoomIdConstants.R8_Cave_HealingFairy,
            RoomIdConstants.R11_PalaceofDarkness_TurtleRoom,
            //RoomIdConstants.R13_GanonsTower_Agahnim2,
            RoomIdConstants.R14_IcePalace_EntranceRoom,
            RoomIdConstants.R21_TurtleRock0x15,
            RoomIdConstants.R27_PalaceofDarkness_Mimics_MovingWallRoom,
            RoomIdConstants.R28_GanonsTower_IceArmos,
            RoomIdConstants.R30_IcePalace_BombFloor_BariRoom,
            RoomIdConstants.R31_IcePalace_Pengator_BigKeyRoom,
            //RoomIdConstants.R32_AgahnimsTower_Agahnim,
            RoomIdConstants.R36_TurtleRock_DoubleHokku_Bokku_BigchestRoom,
            RoomIdConstants.R38_SwampPalace_StatueRoom,
            RoomIdConstants.R42_PalaceofDarkness_BigHubRoom,
            RoomIdConstants.R43_PalaceofDarkness_MapChest_FairyRoom,
            RoomIdConstants.R48_AgahnimsTower_MaidenSacrificeChamber,
            RoomIdConstants.R49_TowerofHera_HardhatBeetlesRoom,
            //RoomIdConstants.R51_DesertPalace_Lanmolas,
            RoomIdConstants.R57_SkullWoods_GibdoKey_MothulaHoleRoom,
            RoomIdConstants.R61_GanonsTower_TorchRoom2,
            RoomIdConstants.R62_IcePalace_StalfosKnights_ConveyorHellway,
            RoomIdConstants.R63_IcePalace_MapChestRoom,
            RoomIdConstants.R65_HyruleCastle_FirstDarkRoom,
            RoomIdConstants.R68_ThievesTown_BigChestRoom,
            RoomIdConstants.R69_ThievesTown_JailCellsRoom,
            RoomIdConstants.R73_SkullWoods_GibdoTorchPuzzleRoom,
            RoomIdConstants.R74_PalaceofDarkness_EntranceRoom,
            RoomIdConstants.R75_PalaceofDarkness_Warps_SouthMimicsRoom,
            RoomIdConstants.R76_GanonsTower_Mini_HelmasaurConveyorRoom,
            RoomIdConstants.R78_IcePalace_Bomb_JumpRoom,
            RoomIdConstants.R83_DesertPalace_Popos2_BeamosHellwayRoom,
            RoomIdConstants.R87_SkullWoods_BigKeyRoom,
            RoomIdConstants.R91_GanonsTower_SpikePitRoom,
            RoomIdConstants.R93_GanonsTower_Gauntlet1_2_3,
            RoomIdConstants.R94_IcePalace_LonelyFirebar,
            RoomIdConstants.R99_DesertPalace_FinalSectionEntranceRoom,
            RoomIdConstants.R100_ThievesTown_WestAtticRoom,
            RoomIdConstants.R104_SkullWoods_KeyChest_TrapRoom,
            RoomIdConstants.R107_GanonsTower_MimicsRooms,
            RoomIdConstants.R108_GanonsTower_LanmolasRoom,
            RoomIdConstants.R109_GanonsTower_Gauntlet4_5,
            RoomIdConstants.R110_IcePalace_PengatorsRoom,
            RoomIdConstants.R113_HyruleCastle_BoomerangChestRoom,
            RoomIdConstants.R115_DesertPalace_BigChestRoom,
            RoomIdConstants.R117_DesertPalace_BigKeyChestRoom,
            RoomIdConstants.R123_GanonsTower,
            RoomIdConstants.R125_GanonsTower_Winder_WarpMazeRoom,
            RoomIdConstants.R126_IcePalace_HiddenChest_BombableFloorRoom,
            RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom,
            RoomIdConstants.R131_DesertPalace_WestEntranceRoom,
            RoomIdConstants.R133_DesertPalace_EastEntranceRoom,
            RoomIdConstants.R135_TowerofHera_TileRoom,
            RoomIdConstants.R139_GanonsTower_BlockPuzzle_SpikeSkip_MapChestRoom,
            RoomIdConstants.R140_GanonsTower_EastandWestDownstairs_BigChestRoom,
            RoomIdConstants.R141_GanonsTower_Tile_TorchPuzzleRoom,
            RoomIdConstants.R147_MiseryMire_DarkCaneFloorSwitchPuzzleRoom,
            RoomIdConstants.R150_GanonsTower_Torches1Room,
            RoomIdConstants.R156_GanonsTower_InvisibleFloorMazeRoom,
            RoomIdConstants.R159_IcePalace0x9F,
            RoomIdConstants.R165_GanonsTower_WizzrobesRooms,
            RoomIdConstants.R168_EasternPalace_StalfosSpawnRoom,
            RoomIdConstants.R169_EasternPalace_BigChestRoom,
            RoomIdConstants.R170_EasternPalace_MapChestRoom,
            //RoomIdConstants.R172_ThievesTown_BlindTheThief,
            RoomIdConstants.R176_AgahnimsTower_CircleofPots,
            RoomIdConstants.R178_MiseryMire_SlugRoom,
            RoomIdConstants.R181_TurtleRock_DarkMaze,
            RoomIdConstants.R182_TurtleRock_ChainChompsRoom,
            RoomIdConstants.R186_EasternPalace_DarkAntifairy_KeyPotRoom,
            RoomIdConstants.R188_ThievesTown_ConveyorToilet,
            RoomIdConstants.R190_IcePalace_BlockPuzzleRoom,
            RoomIdConstants.R192_AgahnimsTower_DarkBridgeRoom,
            RoomIdConstants.R193_MiseryMire_CompassChest_TileRoom,
            RoomIdConstants.R195_MiseryMire_BigChestRoom,
            RoomIdConstants.R199_TurtleRock_TorchPuzzle,
            //RoomIdConstants.R200_EasternPalace_ArmosKnights,
            RoomIdConstants.R201_EasternPalace_EntranceRoom,
            RoomIdConstants.R206_IcePalace_HoletoKholdstareRoom,
            RoomIdConstants.R208_AgahnimsTower_DarkMaze,
            RoomIdConstants.R209_MiseryMire_ConveyorSlug_BigKeyRoom,
            RoomIdConstants.R210_MiseryMire_Mire02_WizzrobesRoom,
            RoomIdConstants.R216_EasternPalace_PreArmosKnightsRoom,
            RoomIdConstants.R217_EasternPalace_CanonballRoom,
            RoomIdConstants.R218_EasternPalace,
            RoomIdConstants.R219_ThievesTown_Main_SouthWestEntranceRoom,
            RoomIdConstants.R224_AgahnimsTower_EntranceRoom,
            //RoomIdConstants.R227_Cave_HalfMagic,
            RoomIdConstants.R239_Cave_CrystalSwitch_5ChestsRoom,
            RoomIdConstants.R268_MimicCave,
            RoomIdConstants.R291_MiniMoldormCave,
        };

        void SetEnemyDamageZero()
        {
            for (int i = 0; i < 10; i++)
            {
                this.romData[0x3742D + 0 + (i * 3)] = 0; //green mail
                this.romData[0x3742D + 1 + (i * 3)] = 0; //blue mail
                this.romData[0x3742D + 2 + (i * 3)] = 0; //red mail
            }

            for (int j = 0; j < 0xF3; j++)
            {
                if (j != 0x54 && j != 0x09 && j != 0x53 && j != 0x88 && j != 0x89 && j != 0x53 && j != 0x8C && j != 0x92
                    && j != 0x70 && j != 0xBD && j != 0xBE && j != 0xBF && j != 0xCB && j != 0xCE && j != 0xA2 && j != 0xA3 && j != 0x8D
                    && j != 0x7A && j != 0x7B && j != 0xCC && j != 0xCD && j != 0xA4 && j != 0xD6 && j != 0xD7)
                {
                    this.romData[0x6B266 + j] = 0;
                }
            }
        }

        int[] dontModifySprites =
        {
            SpriteConstants.PullSwitch_GoodSprite,
            SpriteConstants.PullSwitch_Unused1Sprite,
            SpriteConstants.PullSwitch_TrapSprite,
            SpriteConstants.PullSwitch_Unused2Sprite,
            SpriteConstants.GargoylesDomainGateSprite,
            SpriteConstants.SahasrahlaAginahSprite,
            SpriteConstants.DwarvesSprite,
            SpriteConstants.ArrowInWall_MaybeSprite,
            SpriteConstants.StatueSprite,
            SpriteConstants.WeathervaneSprite,
            SpriteConstants.CrystalSwitchSprite,
            SpriteConstants.BugCatchingKidSprite,
            SpriteConstants.PushSwitchSprite,
            SpriteConstants.TalkingTreeSprite,
            SpriteConstants.StorytellersSprite,
            SpriteConstants.BlindHideoutAttendantSprite,
            SpriteConstants.SweepingLadySprite,
            SpriteConstants.MultipurposeSpriteSprite,
            SpriteConstants.LumberjacksSprite,
            SpriteConstants.TelepathicStones_NoIdeaWhatThisActuallyIsLikelyUnusedSprite,
            SpriteConstants.FluteBoysNotesSprite,
            SpriteConstants.RaceHPNPCsSprite,
            SpriteConstants.Person_MaybeSprite,
            SpriteConstants.FortuneTellerSprite,
            SpriteConstants.AngryBrothersSprite,
            SpriteConstants.PullForRupeesSpriteSprite,
            SpriteConstants.ScaredGirl2Sprite,
            SpriteConstants.InnkeeperSprite,
            SpriteConstants.WitchSprite,
            SpriteConstants.WaterfallSprite,
            SpriteConstants.ArrowTargetSprite,
            SpriteConstants.AverageMiddleAgedManSprite,
            SpriteConstants.HalfMagicBatSprite,
            SpriteConstants.DashItemSprite,
            SpriteConstants.VillageKidSprite,
            SpriteConstants.Signs_ChickenLadyAlsoShowedUp_ScaredLadiesOutsideHousesSprite,
            SpriteConstants.TutorialSoldierSprite,
            SpriteConstants.LightningLockSprite,
            SpriteConstants.GiantZoraSprite,
            SpriteConstants.ArmosKnightsSprite,
            SpriteConstants.LanmolasSprite,
            SpriteConstants.DesertPalaceBarriersSprite,
            SpriteConstants.BirdSprite,
            SpriteConstants.SquirrelSprite,
            SpriteConstants.MasterSwordSprite,
            SpriteConstants.ShootingGalleryProprietorSprite,
            SpriteConstants.MovingCannonBallShooters_RightSprite,
            SpriteConstants.MovingCannonBallShooters_LeftSprite,
            SpriteConstants.MovingCannonBallShooters_DownSprite,
            SpriteConstants.MovingCannonBallShooters_UpSprite,
            SpriteConstants.MirrorPortalSprite,
            SpriteConstants.ActivatoForThePonds_WhereYouThrowInItemsSprite,
            SpriteConstants.UnclePriestSprite,
            SpriteConstants.RunningManSprite,
            SpriteConstants.BottleSalesmanSprite,
            SpriteConstants.PrincessZeldaSprite,
            SpriteConstants.VillageElderSprite,
            SpriteConstants.AgahnimSprite,
            SpriteConstants.AgahnimEnergyBallSprite,
            SpriteConstants.MothulaSprite,
            SpriteConstants.MothulasBeamSprite,
            SpriteConstants.ArrghusSprite,
            SpriteConstants.ArrghusSpawnSprite,
            SpriteConstants.HelmasaurKingSprite,
            SpriteConstants.BumperSprite,
            SpriteConstants.EyeLaser_RightSprite,
            SpriteConstants.EyeLaser_LeftSprite,
            SpriteConstants.EyeLaser_DownSprite,
            SpriteConstants.EyeLaser_UpSprite,
            SpriteConstants.Ostrich_HauntedGroveSprite,
            SpriteConstants.FluteSprite,
            SpriteConstants.Birds_HauntedGroveSprite,
            SpriteConstants.FreezorSprite,
            SpriteConstants.KholdstareSprite,
            SpriteConstants.KholdstaresShellSprite,
            SpriteConstants.FallingIceSprite,
            SpriteConstants.MaidenSprite,
            SpriteConstants.AppleSprite,
            SpriteConstants.LostOldManSprite,
            SpriteConstants.DownPipeSprite,
            SpriteConstants.UpPipeSprite,
            SpriteConstants.RightPipeSprite,
            SpriteConstants.LeftPipeSprite,
            SpriteConstants.HylianInscriptionSprite,
            SpriteConstants.ThiefsChestSprite,
            SpriteConstants.BombSalesmanSprite,
            SpriteConstants.KikiSprite,
            SpriteConstants.MaidenInBlindDungeonSprite,
            SpriteConstants.FeudingFriendsOnDeathMountainSprite,
            SpriteConstants.WhirlpoolSprite,
            SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite,
            SpriteConstants.DrunkInTheInnSprite,
            SpriteConstants.Vitreous_LargeEyeballSprite,
            SpriteConstants.Vitreous_SmallEyeballSprite,
            SpriteConstants.VitreousLightningSprite,
            SpriteConstants.CatFish_QuakeMedallionSprite,
            SpriteConstants.AgahnimTeleportingZeldaToDarkworldSprite,
            SpriteConstants.BouldersSprite,
            SpriteConstants.MedusaSprite,
            SpriteConstants.FourWayFireballSpittersSprite,
            SpriteConstants.BigFairyWhoHealsYouSprite,
            SpriteConstants.TrinexxSprite,
            SpriteConstants.AnotherPartOfTrinexxSprite,
            SpriteConstants.YetAnotherPartOfTrinexxSprite,
            SpriteConstants.BlindTheThiefSprite,
            SpriteConstants.BunnyBeamSprite,
            SpriteConstants.DiggingGameProprietorSprite,
            SpriteConstants.GanonSprite,
            SpriteConstants.CopyOfGanon_ExceptInvincibleSprite,
            SpriteConstants.FairySprite,
            SpriteConstants.KeySprite,
            SpriteConstants.BigKeySprite,
            SpriteConstants.MushroomSprite,
            SpriteConstants.MagicShopDude_HisItemsIncludingTheMagicPowderSprite,
            SpriteConstants.HeartContainerSprite,
            SpriteConstants.HeartPieceSprite,
            SpriteConstants.MedallionTabletSprite,
            SpriteConstants.OW_OL_PersonsDoor,
            SpriteConstants.OW_OL_FallingRocks,
            SpriteConstants.OW_OL_CanonBalls,
            SpriteConstants.OW_OL_Unknown_F6,
            SpriteConstants.OW_OL_Unknown_F7,
            SpriteConstants.OW_OL_Unknown_F8,
            SpriteConstants.OW_OL_Unknown_F9,
            SpriteConstants.OW_OL_BlobDrop,
            SpriteConstants.OW_OL_WallMaster_ToHoulihan,
            SpriteConstants.OW_OL_FloorDropSquare,
            SpriteConstants.OW_OL_FloorDropNorth,
            SpriteConstants.OW_OL_FloorDropEast,
            SpriteConstants.OL_CanonBalls_EP4Walls,
            SpriteConstants.OL_CanonBalls_EPEntrance,
            SpriteConstants.OL_StalfosHeadTrap,
            SpriteConstants.OL_BombDrop_RopeTrap,
            SpriteConstants.OL_MovingFloor,
            SpriteConstants.OL_SlimeDropper,
            SpriteConstants.OL_Wallmaster,
            SpriteConstants.OL_FloorDrop_Square,
            SpriteConstants.OL_FloorDrop_Path,
            SpriteConstants.OL_RightEvil_PirogusuSpawner,
            SpriteConstants.OL_LeftEvil_PirogusuSpawner,
            SpriteConstants.OL_DownEvil_PirogusuSpawner,
            SpriteConstants.OL_UpEvil_PirogusuSpawner,
            SpriteConstants.OL_FlyingFloorTileTrap,
            SpriteConstants.OL_WizzrobeSpawner,
            SpriteConstants.OL_BlackSpawn_Zoro_BombHole,
            SpriteConstants.OL_4Skull_Trap_Pot,
            SpriteConstants.OL_Stalfos_Spawn_Trap_EP,
            SpriteConstants.OL_ArmosKnight_Trigger,
            SpriteConstants.OL_BombDrop_BombTrap,
            SpriteConstants.OL_SoldierAlerter_Blue,
            SpriteConstants.OL_SoldierAlerter_Green,
        }; // TODO: add more
    }
}
