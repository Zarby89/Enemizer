using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SpriteRequirement
    {
        public int SpriteId { get; set; }
        public string SpriteName
        {
            get
            {
                return SpriteConstants.GetSpriteName(SpriteId);
            }
        }
        public bool Overlord { get; set; }
        public bool Boss { get; set; }
        public bool DoNotRandomize { get; set; }
        public bool Killable { get; set; }
        public bool NPC { get; set; }
        public bool NeverUseDungeon { get; set; }
        public bool NeverUseOverworld { get; set; }
        public bool CannotHaveKey { get; set; }
        public bool IsObject { get; set; }
        public bool Absorbable { get; set; }
        public bool IsWaterSprite { get; set; }
        public bool IsEnemySprite { get; set; }
        public List<byte> GroupId { get; set; } = new List<byte>();
        public List<byte> SubGroup0 { get; set; } = new List<byte>();
        public List<byte> SubGroup1 { get; set; } = new List<byte>();
        public List<byte> SubGroup2 { get; set; } = new List<byte>();
        public List<byte> SubGroup3 { get; set; } = new List<byte>();
        public byte? Parameters { get; set; }
        public bool SpecialGlitched { get; set; }

        List<int> ExcludedRooms = new List<int>();
        List<int> DontRandomizeRooms = new List<int>();
        List<int> SpawnableRooms = new List<int>();


        public SpriteRequirement(int SpriteId)
        {
            this.SpriteId = SpriteId;
            this.IsEnemySprite = true;
        }

        public static SpriteRequirement New(int spriteId)
        {
            return new SpriteRequirement(spriteId);
        }

        public SpriteRequirement SetBoss()
        {
            Boss = true;
            DoNotRandomize = true; // TODO: ???
            return this;
        }

        public SpriteRequirement SetNPC()
        {
            NPC = true;
            DoNotRandomize = true;
            IsEnemySprite = false;
            return this;
        }

        public SpriteRequirement SetNeverUse()
        {
            NeverUseDungeon = true;
            NeverUseOverworld = true;
            //DoNotRandomize = true;
            return this;
        }

        public SpriteRequirement SetNeverUseDungeon()
        {
            NeverUseDungeon = true;
            return this;
        }

        public SpriteRequirement SetNeverUseOverworld()
        {
            NeverUseOverworld = true;
            return this;
        }

        public SpriteRequirement SetDoNotRandomize()
        {
            DoNotRandomize = true;
            return this;
        }

        public SpriteRequirement SetIsEnemySprite()
        {
            IsEnemySprite = true;
            return this;
        }

        public SpriteRequirement SetNotEnemySprite()
        {
            IsEnemySprite = false;
            return this;
        }

        public SpriteRequirement SetWaterSprite()
        {
            IsWaterSprite = true;
            CannotHaveKey = true; // TODO: remove this after we fix water sprites only showing up on water areas
            return this;
        }

        public SpriteRequirement SetOverlord()
        {
            DoNotRandomize = true;
            Overlord = true;
            return this;
        }

        public SpriteRequirement SetAbsorbable()
        {
            Absorbable = true;
            CannotHaveKey = true;
            return this;
        }

        public SpriteRequirement SetCannotHaveKey()
        {
            CannotHaveKey = true;
            return this;
        }

        public SpriteRequirement SetIsObject()
        {
            IsObject = true;
            IsEnemySprite = false;
            return this;
        }

        public SpriteRequirement SetKillable()
        {
            Killable = true;
            return this;
        }

        public SpriteRequirement AddGroup(params byte[] groupId)
        {
            this.GroupId.AddRange(groupId);
            return this;
        }

        public SpriteRequirement AddSubgroup0(params byte[] subgroup0)
        {
            this.SubGroup0.AddRange(subgroup0);
            return this;
        }

        public SpriteRequirement AddSubgroup1(params byte[] subgroup1)
        {
            this.SubGroup1.AddRange(subgroup1);
            return this;
        }

        public SpriteRequirement AddSubgroup2(params byte[] subgroup2)
        {
            this.SubGroup2.AddRange(subgroup2);
            return this;
        }

        public SpriteRequirement AddSubgroup3(params byte[] subgroup3)
        {
            this.SubGroup3.AddRange(subgroup3);
            return this;
        }

        public SpriteRequirement SetParameters(byte parameters)
        {
            this.Parameters = parameters;
            return this;
        }

        public SpriteRequirement IsSpecialGlitched()
        {
            SpecialGlitched = true;
            return this;
        }

        public SpriteRequirement AddExcludedRooms(params int[] roomIds)
        {
            this.ExcludedRooms.AddRange(roomIds);
            return this;
        }

        public SpriteRequirement AddDontRandomizeRooms(params int[] roomIds)
        {
            this.DontRandomizeRooms.AddRange(roomIds);
            return this;
        }

        public SpriteRequirement AddSpawnableRooms(params int[] roomIds)
        {
            this.SpawnableRooms.AddRange(roomIds);
            return this;
        }

        public bool SpriteInGroup(SpriteGroup spriteGroup)
        {
            if(this.GroupId != null && this.GroupId.Count > 0 && this.GroupId.Contains((byte)spriteGroup.DungeonGroupId) == false)
            {
                return false;
            }
            if(this.SubGroup0 != null && this.SubGroup0.Count > 0 && this.SubGroup0.Contains((byte)spriteGroup.SubGroup0) == false)
            {
                return false;
            }
            if (this.SubGroup1 != null && this.SubGroup1.Count > 0 && this.SubGroup1.Contains((byte)spriteGroup.SubGroup1) == false)
            {
                return false;
            }
            if (this.SubGroup2 != null && this.SubGroup2.Count > 0 && this.SubGroup2.Contains((byte)spriteGroup.SubGroup2) == false)
            {
                return false;
            }
            if (this.SubGroup3 != null && this.SubGroup3.Count > 0 && this.SubGroup3.Contains((byte)spriteGroup.SubGroup3) == false)
            {
                return false;
            }

            return true;
        }

        public bool CanSpawnInRoom(Room room)
        {
            return ExcludedRooms.Contains(room.RoomId) == false
                && (SpawnableRooms.Count == 0 || SpawnableRooms.Contains(room.RoomId)
                && (this.SpriteId != SpriteConstants.WallmasterSprite || room.RoomId < 256)); // wall masters need an 'exit' set and only rooms 0-255 have one
        }

        public bool CanBeRandomizedInRoom(Room room)
        {
            return !DoNotRandomize && (DontRandomizeRooms.Count == 0 || DontRandomizeRooms.Contains(room.RoomId));
        }
    }

    public class SpriteRequirementCollection
    {
        public List<SpriteRequirement> SpriteRequirements { get; set; }

        public IEnumerable<SpriteRequirement> RandomizableSprites
        {
            get
            {
                return SpriteRequirements.Where(x => x.DoNotRandomize == false);
            }
        }

        public List<SpriteRequirement> DoNotRandomizeSprites
        {
            get
            {
                return SpriteRequirements.Where(x => x.DoNotRandomize).ToList();
            }
        }

        public IEnumerable<SpriteRequirement> UsableEnemySprites
        {
            get
            {
                return SpriteRequirements.Where(x => x.NPC == false 
                                                    && x.IsEnemySprite == true
                                                    && x.Boss == false
                                                    && x.Overlord == false
                                                    && x.IsObject == false);
            }
        }

        public IEnumerable<SpriteRequirement> GetUsableDungeonEnemySprites(bool absorbable = false)
        {
            return UsableEnemySprites.Where(x => x.NeverUseDungeon == false && (x.Absorbable == false || x.Absorbable == absorbable));
        }

        public IEnumerable<SpriteRequirement> GetUsableOverworldEnemySprites(bool absorbable = false)
        {
            // TODO: figure out why absorbables won't show up (red rupees, bombs, etc. won't show up...)
            return UsableEnemySprites.Where(x => x.NeverUseOverworld == false && (x.Absorbable == false || x.Absorbable == absorbable));
        }

        public IEnumerable<SpriteRequirement> KillableSprites
        {
            get
            {
                return SpriteRequirements.Where(x => x.Killable == true);
            }
        }

        public IEnumerable<SpriteRequirement> WaterSprites
        {
            get
            {
                return SpriteRequirements.Where(x => x.IsWaterSprite == true);
            }
        }

        public SpriteRequirementCollection()
        {
            SpriteRequirements = new List<SpriteRequirement>();

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RavenSprite).SetCannotHaveKey().AddSubgroup3(17, 25)
                .AddExcludedRooms(DontUseFlyingSprites));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VultureSprite).SetCannotHaveKey().AddSubgroup2(18)
                .AddExcludedRooms(DontUseFlyingSprites));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FlyingStalfosHeadSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EmptySprite).SetNeverUse());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullSwitch_GoodSprite).SetDoNotRandomize().SetIsObject().SetNeverUse().AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullSwitch_TrapSprite).SetDoNotRandomize().SetIsObject().SetNeverUse().AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_OneWaySprite).SetKillable().AddSubgroup2(12, 24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MoldormSprite).SetBoss().AddSubgroup2(48));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_FourWaySprite).SetKillable().AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ChickenSprite).AddSubgroup3(21, 80)
                .AddExcludedRooms(DontUseFlyingSprites));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_MaybeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BuzzblobSprite).SetCannotHaveKey().SetKillable().AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SnapdragonSprite).SetKillable().AddSubgroup0(22).AddSubgroup2(23));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OctoballoonSprite).SetCannotHaveKey().AddSubgroup2(12)
                .AddExcludedRooms(DontUseFlyingSprites));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OctoballoonHatchlingsSprite).SetNeverUse().AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HinoxSprite).SetKillable().AddSubgroup0(22));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MoblinSprite).SetKillable().AddSubgroup2(23));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MiniHelmasaurSprite).SetKillable().AddSubgroup1(30));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GargoylesDomainGateSprite).SetDoNotRandomize().SetIsObject().SetNeverUse());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AntifairySprite).AddSubgroup3(82, 83)
                .AddExcludedRooms(RoomIdConstants.R64_AgahnimsTower_FinalBridgeRoom)
                .AddExcludedRooms(DontUseFlyingSprites)); // can make it almost impossible to advance without powder

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SahasrahlaAginahSprite).SetNPC().AddSubgroup2(76));

            // if you remove their bush before killing them they won't drop a key, so exclude them from key mob pool
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BushHoarderSprite).SetCannotHaveKey().SetKillable().AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MiniMoldormSprite).SetKillable().AddSubgroup1(30));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PoeSprite).SetCannotHaveKey().AddSubgroup3(14, 21)
                .AddExcludedRooms(DontUseFlyingSprites));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DwarvesSprite).SetNPC().SetDoNotRandomize().AddSubgroup1(77).AddSubgroup3(21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowInWall_MaybeSprite).SetDoNotRandomize().SetNeverUse());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StatueSprite).SetDoNotRandomize().SetIsObject().AddSubgroup3(82, 83)
                .AddExcludedRooms(DontUseImmovableSpritesRooms)
                .AddExcludedRooms(RoomIdConstants.R63_IcePalace_MapChestRoom)); // statues break the pull switch in the second room

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WeathervaneSprite).SetDoNotRandomize().SetNeverUse());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CrystalSwitchSprite).SetDoNotRandomize().SetIsObject().SetNeverUse().AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BugCatchingKidSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(81));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SluggulaSprite).SetKillable().AddSubgroup2(37));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PushSwitchSprite).SetDoNotRandomize().SetIsObject().SetNeverUse().AddSubgroup3(83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RopaSprite).SetKillable().AddSubgroup0(22));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedBariSprite).SetKillable().SetCannotHaveKey().AddSubgroup0(31)
                .AddDontRandomizeRooms(RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueBariSprite).SetKillable().AddSubgroup0(31)
                .AddDontRandomizeRooms(RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TalkingTreeSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HardhatBeetleSprite).AddSubgroup1(30));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DeadrockSprite).AddSubgroup3(16)
                .AddExcludedRooms(RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StorytellersSprite).SetNPC().SetDoNotRandomize()); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlindHideoutAttendantSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(14, 79));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SweepingLadySprite).SetNPC().SetDoNotRandomize().AddGroup(6)); // TODO: add subs instead?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MultipurposeSpriteSprite).SetNeverUse().SetDoNotRandomize()); // TODO: what is this?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LumberjacksSprite).SetNPC().SetDoNotRandomize().AddSubgroup2(74));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TelepathicStones_NoIdeaWhatThisActuallyIsLikelyUnusedSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            // this uses sub2 for LW and sub3 for DW...
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FluteBoysNotesSprite).SetNeverUse().SetDoNotRandomize()); // TODO: does this use OAM2?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RaceHPNPCsSprite).SetNPC().SetDoNotRandomize().AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Person_MaybeSprite).SetNPC().SetDoNotRandomize().AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FortuneTellerSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(75));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AngryBrothersSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(79));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullForRupeesSpriteSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ScaredGirl2Sprite).SetNPC().SetDoNotRandomize().AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.InnkeeperSprite).SetNPC().SetDoNotRandomize()); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WitchSprite).SetNPC().SetDoNotRandomize().AddSubgroup2(76));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WaterfallSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowTargetSprite).SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AverageMiddleAgedManSprite).SetNPC().SetDoNotRandomize().AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HalfMagicBatSprite).SetNPC().SetDoNotRandomize().AddSubgroup3(29));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DashItemSprite).SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VillageKidSprite).SetNPC().SetDoNotRandomize().AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Signs_ChickenLadyAlsoShowedUp_ScaredLadiesOutsideHousesSprite).SetNPC().SetDoNotRandomize().AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RockHoarderSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TutorialSoldierSprite).SetNPC().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LightningLockSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup3(29));

            // probably needs 19 and 41 for sub 2 for falling animation
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueSwordSoldier_DetectPlayerSprite).SetKillable().AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSwordSoldierSprite).SetKillable().AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedSpearSoldierSprite).SetKillable().AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AssaultSwordSoldierSprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSpearSoldierSprite).SetKillable().AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueArcherSprite).SetKillable().AddSubgroup0(72).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenArcherSprite).SetKillable().AddSubgroup0(72).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedJavelinSoldierSprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedJavelinSoldier2Sprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedBombSoldiersSprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSoldierRecruits_HMKnightSprite).SetKillable().AddSubgroup1(73).AddSubgroup2(19));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GeldmanSprite).SetCannotHaveKey().SetKillable().AddSubgroup2(18));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RabbitSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PopoSprite).SetKillable().AddSubgroup1(44));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Popo2Sprite).SetKillable().AddSubgroup1(44));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CannonBallsSprite).SetDoNotRandomize().SetNeverUse().AddSubgroup2(46));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArmosSprite).SetKillable().AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GiantZoraSprite).SetNPC().SetDoNotRandomize().AddSubgroup3(68));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArmosKnightsSprite).SetBoss().AddSubgroup3(29));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LanmolasSprite).SetBoss().AddSubgroup3(49));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FireballZoraSprite).SetWaterSprite().AddSubgroup2(12, 24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WalkingZoraSprite).SetWaterSprite().SetCannotHaveKey().SetKillable().AddSubgroup2(12).AddSubgroup3(68));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DesertPalaceBarriersSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(18));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CrabSprite).SetKillable().AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BirdSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(55).AddSubgroup3(54)); // TODO: check 54
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SquirrelSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(55).AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Spark_LeftToRightSprite).AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Spark_RightToLeftSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_VerticalMovingSprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_VerticalMoving2Sprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RollerSprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_HorizontalMovingSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BeamosSprite).AddSubgroup1(44)
                .AddExcludedRooms(DontUseImmovableSpritesRooms));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MasterSwordSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup2(55).AddSubgroup3(54));

            // TODO: exclude these for now. figure out how to add them later (they overload the sprites and cause odd stuff to happen)
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Devalant_NonShooterSprite).SetNeverUseDungeon().SetNeverUseOverworld().SetKillable().AddSubgroup0(47));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Devalant_ShooterSprite).SetNeverUseDungeon().SetNeverUseOverworld().SetKillable().AddSubgroup0(47));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ShootingGalleryProprietorSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(75));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_RightSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup0(47));//.AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_LeftSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup0(47));//.AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_DownSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup0(47));//.AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_UpSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup0(47));//.AddSubgroup2(46));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BallNChainTrooperSprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73)); // TODO: check 73
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CannonSoldierSprite).SetKillable().AddSubgroup0(70).AddSubgroup1(73)); // TODO: verify because these don't exist in vanilla

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MirrorPortalSprite).SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RatSprite).SetKillable().AddSubgroup2(28, 36));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RopeSprite).SetKillable().AddSubgroup2(28, 36)); // 36 isn't used anywhere in vanilla beside a trap in TT I think

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KeeseSprite).SetKillable().AddSubgroup2(28, 36));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HelmasaurKingFireballSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LeeverSprite).SetKillable().AddSubgroup0(47));

            // this is for both type of big fairy. sprite picks based on room
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ActivatoForThePonds_WhereYouThrowInItemsSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.UnclePriestSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(71, 81));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RunningManSprite).SetNPC().SetDoNotRandomize().AddGroup(6));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BottleSalesmanSprite).SetNPC().SetDoNotRandomize().AddGroup(6));
            
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PrincessZeldaSprite).SetNPC().SetDoNotRandomize()); // zelda uses some special sprite

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Antifairy_AlternateSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VillageElderSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(75).AddSubgroup1(77).AddSubgroup2(74)); // TODO: check which is actually needed

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BeeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimSprite).SetNeverUse().SetBoss().AddSubgroup0(85).AddSubgroup1(26, 61).AddSubgroup2(66).AddSubgroup3(67)); // not sure what difference is for sub 1

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimEnergyBallSprite).SetNeverUse());

            // are these killable???
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FloatingStalfosHeadSprite).AddSubgroup0(31)
                .AddExcludedRooms(DontUseFlyingSprites)); // TODO: check this because it only shows up as stalfos head in game??

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigSpikeTrapSprite).AddSubgroup3(82, 83)
                .AddExcludedRooms(DontUseImmovableSpritesRooms));

            // TODO: any other rooms?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GuruguruBar_ClockwiseSprite).AddSubgroup0(31)
                .AddDontRandomizeRooms(RoomIdConstants.R181_TurtleRock_DarkMaze,
                                        RoomIdConstants.R150_GanonsTower_Torches1Room));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GuruguruBar_CounterClockwiseSprite).AddSubgroup0(31)
                .AddDontRandomizeRooms(RoomIdConstants.R181_TurtleRock_DarkMaze,
                                        RoomIdConstants.R150_GanonsTower_Torches1Room));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WinderSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WaterTektiteSprite).SetWaterSprite().AddSubgroup2(34)
                .AddDontRandomizeRooms(RoomIdConstants.R40_SwampPalace_EntranceRoom)
                .AddExcludedRooms(DontUseFlyingSprites));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AntifairyCircleSprite).SetNeverUse().AddSubgroup3(82, 83)); // lag city

            // TODO: mimic rooms are hard coded (4 rooms) rest are always eyegore
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenEyegoreSprite).SetKillable()/*.AddSubgroup1(44)*/.AddSubgroup2(46)); // one is mimic, one is eyegore...
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedEyegoreSprite)/*.AddSubgroup1(44)*/.AddSubgroup2(46));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.YellowStalfosSprite)); // TODO: add

            // just don't use them until we fix the asm
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KodongosSprite).SetNeverUse().AddSubgroup2(42));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FlamesSprite)); // Kodongo fireball

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MothulaSprite).SetBoss().AddSubgroup2(56));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MothulasBeamSprite).SetNeverUse().AddSubgroup2(56));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SpikeTrapSprite).AddSubgroup3(82, 83)
                .AddExcludedRooms(RoomIdConstants.R40_SwampPalace_EntranceRoom)
                .AddExcludedRooms(DontUseImmovableSpritesRooms)); // TODO: maybe we can have them? probably better not to

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GibdoSprite).SetKillable().AddSubgroup2(35));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrghusSprite).SetBoss().AddSubgroup2(57));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrghusSpawnSprite).SetBoss().AddSubgroup2(57));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TerrorpinSprite).AddSubgroup2(42));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SlimeSprite_JumpsOutOfTheFloor).AddSubgroup1(32));

            // these will never work right in the overworld
            // and only work in rooms with an exit (0-255), technically in 260 (link's house) as well
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WallmasterSprite).SetDoNotRandomize().AddSubgroup2(35));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalfosKnightSprite).AddSubgroup1(32));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HelmasaurKingSprite).SetBoss().AddSubgroup2(58).AddSubgroup3(62));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BumperSprite).SetNeverUse().SetIsObject().SetDoNotRandomize().AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SwimmersEvilSprite).SetWaterSprite().SetNeverUse().SetDoNotRandomize()); // TODO: add? what is this? 

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_RightSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_LeftSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_DownSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_UpSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PengatorSprite).SetKillable().AddSubgroup2(38));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KyameronWaterSplashSprite).SetWaterSprite().AddSubgroup2(34)
                    .AddDontRandomizeRooms(RoomIdConstants.R40_SwampPalace_EntranceRoom));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WizzrobeSprite).AddSubgroup2(37, 41)); // can't be killed with bombs so don't put them in key/shutter rooms

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VerminHorizontalSprite).SetDoNotRandomize().SetKillable().AddSubgroup1(32));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VerminVerticalSprite).SetDoNotRandomize().SetKillable().AddSubgroup1(32));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Ostrich_HauntedGroveSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(78));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FluteSprite).SetNeverUse().SetDoNotRandomize()); // TODO: where is this?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Birds_HauntedGroveSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(78));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FreezorSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup2(38));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KholdstareSprite).SetBoss().AddSubgroup2(60));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KholdstaresShellSprite).SetBoss()); // TODO: this is BG2
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FallingIceSprite).SetBoss().AddSubgroup2(60));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueZazakSprite).SetKillable().AddSubgroup2(40));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedZazakSprite).SetKillable().AddSubgroup2(40));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalfosSprite).SetKillable().AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BomberFlyingCreaturesFromDarkworldSprite).SetCannotHaveKey().AddSubgroup3(27)
                .AddExcludedRooms(DontUseFlyingSprites));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BomberFlyingCreaturesFromDarkworld2Sprite).SetCannotHaveKey().AddSubgroup3(27)
                .AddExcludedRooms(DontUseFlyingSprites));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PikitSprite).SetCannotHaveKey().SetKillable().AddSubgroup3(27));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MaidenSprite).SetNPC().SetDoNotRandomize()); // TODO: where is this?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AppleSprite).SetDoNotRandomize().SetAbsorbable());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LostOldManSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(70).AddSubgroup1(73).AddSubgroup2(28)); // TODO: figure out which is actually needed

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DownPipeSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.UpPipeSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RightPipeSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LeftPipeSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GoodBee_AgainMaybeSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup0(31)); // TOOD: really?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HylianInscriptionSprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ThiefsChestSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup3(21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombSalesmanSprite).SetNPC().SetDoNotRandomize().AddSubgroup1(77));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KikiSprite).SetNPC().SetDoNotRandomize().AddSubgroup3(25));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MaidenInBlindDungeonSprite).SetNPC().SetDoNotRandomize()); // TODO: special?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MimicSprite).AddSubgroup1(44));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenEyegoreSprite).AddSubgroup1(44)); // one is mimic, one is eyegore...
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedEyegoreSprite).AddSubgroup1(44));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FeudingFriendsOnDeathMountainSprite).SetNPC().SetDoNotRandomize().AddSubgroup3(20));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WhirlpoolSprite).SetNeverUse().SetDoNotRandomize());


            // TODO: What to do????????
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(75)
                                                            .AddSpawnableRooms(RoomIdConstants.R255_Cave0xFF,
                                                                                RoomIdConstants.R274_CaveShop0x112,
                                                                                RoomIdConstants.R287_Shop0x11F));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(75)
                                                            .AddSubgroup1(77)
                                                            .AddSubgroup3(90)
                                                            .AddSpawnableRooms(RoomIdConstants.R271_Shop0x10F,
                                                                                RoomIdConstants.R272_Shop0x110));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup1(77)
                                                            .AddSpawnableRooms(RoomIdConstants.R272_Shop0x110));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(79)
                                                            .AddSpawnableRooms(RoomIdConstants.R280_Shop0x118));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(14)
                                                            .AddSpawnableRooms(RoomIdConstants.R256_ShopInLostWoods0x100,
                                                                                RoomIdConstants.R291_MiniMoldormCave,
                                                                                RoomIdConstants.R292_UnknownCave_BonkCave));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(14)
                                                            .AddSubgroup2(74)
                                                            .AddSubgroup3(80)
                                                            .AddSpawnableRooms(RoomIdConstants.R293_Cave0x125)); // TODO: where is this???????
            //SpriteRequirements.Where(x => x.SpriteId == SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).ToList().ForEach((x) => { x.NeverUse = true; x.NPC = true; });
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite).SetNPC().SetDoNotRandomize() // TODO: figure out which are needed
                                                            .AddSubgroup0(21)
                                                            .AddSpawnableRooms(RoomIdConstants.R286_HypeCave));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DrunkInTheInnSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(79).AddSubgroup1(77).AddSubgroup2(74).AddSubgroup3(80)); // TODO: figure out which are needed

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Vitreous_LargeEyeballSprite).SetBoss().AddSubgroup3(61));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Vitreous_SmallEyeballSprite).SetBoss().AddSubgroup3(61));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VitreousLightningSprite).SetBoss().AddSubgroup3(61));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CatFish_QuakeMedallionSprite).SetNPC().SetDoNotRandomize().AddSubgroup2(24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimTeleportingZeldaToDarkworldSprite).SetBoss().AddSubgroup0(85).AddSubgroup1(61).AddSubgroup2(66).AddSubgroup3(67)); // all needed?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BouldersSprite).SetNeverUse().AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Gibo_FloatingBlobSprite).SetKillable().AddSubgroup2(40));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ThiefSprite).SetCannotHaveKey().AddSubgroup0(14, 21));

            // These are loaded into BG as objects
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MedusaSprite).SetDoNotRandomize().SetIsObject().SetNeverUse());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FourWayFireballSpittersSprite).SetDoNotRandomize().SetIsObject().SetNeverUse());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HokkuBokkuSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigFairyWhoHealsYouSprite).SetNPC().SetDoNotRandomize().AddSubgroup2(57).AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TektiteSprite).SetKillable().AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ChainChompSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TrinexxSprite).SetBoss().AddSubgroup0(64).AddSubgroup3(63));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AnotherPartOfTrinexxSprite).SetBoss().AddSubgroup0(64).AddSubgroup3(63));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.YetAnotherPartOfTrinexxSprite).SetBoss().AddSubgroup0(64).AddSubgroup3(63));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlindTheThiefSprite).SetBoss().AddSubgroup1(44).AddSubgroup2(59));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SwamolaSprite).SetWaterSprite().SetCannotHaveKey().AddSubgroup3(25));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LynelSprite).AddSubgroup3(20));

            // TODO: add never use LW and DW
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BunnyBeamSprite)/*.SetNeverUseOverworld()*/.SetNeverUse().SetDoNotRandomize()); // TODO: find
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FloppingFishSprite).SetNeverUseDungeon().SetDoNotRandomize()); // TODO: find
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalSprite).SetNeverUse().SetDoNotRandomize()); // TODO: why do these spawn so frequently in dungeons?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LandmineSprite).SetDoNotRandomize()
                .SetNeverUse() // TODO: maybe this is a good idea? can't get the right gfx to load because it's automatic and uses OW grahics in OAM0(1)
                .AddExcludedRooms(DontUseImmovableSpritesRooms)); // TODO: find

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DiggingGameProprietorSprite).SetNPC().SetDoNotRandomize().AddSubgroup1(42));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GanonSprite).SetNeverUse().SetBoss().AddSubgroup0(33).AddSubgroup1(65).AddSubgroup2(69).AddSubgroup3(51));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CopyOfGanon_ExceptInvincibleSprite).SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenRupeeSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueRupeeSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedRupeeSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill1Sprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill4Sprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill8Sprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SmallMagicRefillSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FullMagicRefillSprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowRefill5Sprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowRefill10Sprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FairySprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KeySprite).SetNeverUseOverworld().SetAbsorbable().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigKeySprite).SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ShieldEaterSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup3(27)); // TODO: check this is for pikit

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MushroomSprite).SetNeverUse().SetDoNotRandomize().AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FakeMasterSwordSprite).AddSubgroup3(17)); // TODO: check

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MagicShopDude_HisItemsIncludingTheMagicPowderSprite).SetNPC().SetDoNotRandomize().AddSubgroup0(75).AddSubgroup3(90));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartContainerSprite).SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartPieceSprite).SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BushesSprite).SetNeverUse().SetDoNotRandomize()); // bush thrown sprite

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatformSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup2(39)); // TODO: verify

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MantleSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup0(93));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused1Sprite).SetIsObject().SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused2Sprite).SetIsObject().SetNeverUse().SetDoNotRandomize());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused3Sprite).SetIsObject().SetNeverUse().SetDoNotRandomize());

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MedallionTabletSprite).SetIsObject().SetNeverUse().SetDoNotRandomize().AddSubgroup2(18));

            // turn these off for now outside DM. they can only spawn in large (1024x1024 areas)
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OW_OL_FallingRocks).SetOverlord().SetNeverUseDungeon().SetDoNotRandomize().AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OW_OL_WallMaster_ToHoulihan).SetOverlord().SetNeverUseDungeon().AddSubgroup2(35));

            // Overlords
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_CanonBalls_EP4Walls).SetNeverUse().SetOverlord().AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_CanonBalls_EPEntrance).SetNeverUse().SetOverlord().AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_StalfosHeadTrap).SetNeverUse().SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BombDrop_RopeTrap).SetNeverUse().SetOverlord().AddSubgroup2(28, 36));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_MovingFloor).SetNeverUse().SetOverlord());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_SlimeDropper).SetOverlord().AddSubgroup1(32));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_Wallmaster).SetOverlord().AddSubgroup2(35));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FloorDrop_Square).SetNeverUse().SetOverlord().AddSubgroup3(82));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FloorDrop_Path).SetNeverUse().SetOverlord().AddSubgroup3(82));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_RightEvil_PirogusuSpawner).SetNeverUse().SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_LeftEvil_PirogusuSpawner).SetNeverUse().SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_DownEvil_PirogusuSpawner).SetNeverUse().SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_UpEvil_PirogusuSpawner).SetNeverUse().SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FlyingFloorTileTrap).SetOverlord()); // TODO: is this special sprites?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_WizzrobeSpawner).SetOverlord().AddSubgroup2(37, 41));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BlackSpawn_Zoro_BombHole).SetNeverUse().SetOverlord().AddSubgroup1(32));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_4Skull_Trap_Pot).SetNeverUse().SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_Stalfos_Spawn_Trap_EP).SetNeverUse().SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_ArmosKnight_Trigger).SetNeverUse().SetOverlord());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BombDrop_BombTrap).SetNeverUse().SetOverlord());

            //// "Special" sprites
            //// rat-guard = green recruit (0x4B) with sub 1=73, sub 2=28
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSoldierRecruits_HMKnightSprite)
            //                                        .IsSpecialGlitched()
            //                                        .SetKillable()
            //                                        .AddSubgroup1(73)
            //                                        .AddSubgroup2(28));

            //// zombie-guard = green recruit (0x4B) with sub 1=73, sub 2=28
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSoldierRecruits_HMKnightSprite)
            //                                        .IsSpecialGlitched()
            //                                        .SetKillable()
            //                                        .AddSubgroup1(73)
            //                                        .AddSubgroup2(35));

            //// Palette glitch and invisible guard (need to see if this causes any other issues)
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueSwordSoldier_DetectPlayerSprite)
            //                                        .IsSpecialGlitched()
            //                                        .SetKillable()
            //                                        .SetParameters(0x18) // 11000 should cause very bad things
            //                                        .AddSubgroup1(73));

            // TODO: add beefy arms
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_RightSprite).SetIsObject().IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup0(22));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_LeftSprite).SetIsObject().IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup0(22));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_DownSprite).SetIsObject().IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup0(22));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_UpSprite).SetIsObject().IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup0(22));

            // make popos into dwarves
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PopoSprite).IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup1(77));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Popo2Sprite).IsSpecialGlitched().SetNeverUse().SetDoNotRandomize().AddSubgroup1(77));
        }

        //void AddSpriteRequirement(int SpriteId, bool Overlord, int? GroupId, int? SubGroup0, int? SubGroup1, int? SubGroup2, int? SubGroup3, byte? Parameters = null, bool Special = false)
        //{
        //    SpriteRequirements.Add(new SpriteRequirement(SpriteId, Overlord, GroupId, SubGroup0, SubGroup1, SubGroup2, SubGroup3, Parameters, Special));
        //}

        int[] DontUseImmovableSpritesRooms =
        {
            RoomIdConstants.R11_PalaceofDarkness_TurtleRoom, // TODO: test, probably the single turtle in the L section
            RoomIdConstants.R22_SwampPalace_SwimmingTreadmill,
            RoomIdConstants.R25_PalaceofDarkness_DarkMaze, // TODO: test, top mob will probably block maze
            RoomIdConstants.R30_IcePalace_BombFloor_BariRoom, // TODO: test
            RoomIdConstants.R38_SwampPalace_StatueRoom,
            RoomIdConstants.R39_TowerofHera_BigChest, // TODO: test, top left dodongo
            RoomIdConstants.R54_SwampPalace_BigChestRoom, // TODO: check bottom left waterbug
            RoomIdConstants.R63_IcePalace_MapChestRoom, // spikes block stuff
            RoomIdConstants.R66_HyruleCastle_6RopesRoom, // only if two stack, but why push it
            RoomIdConstants.R64_AgahnimsTower_FinalBridgeRoom, // spikes are bad in here
            RoomIdConstants.R70_SwampPalace_CompassChestRoom,
            RoomIdConstants.R75_PalaceofDarkness_Warps_SouthMimicsRoom, // TODO: test
            RoomIdConstants.R78_IcePalace_Bomb_JumpRoom,
            RoomIdConstants.R85_CastleSecretEntrance_UncleDeathRoom, // TODO: test
            RoomIdConstants.R87_SkullWoods_BigKeyRoom,
            RoomIdConstants.R95_IcePalace_HiddenChest_SpikeFloorRoom, // TODO: would this cause problem in OHKO since you can't hookshot if middle mob is beamos,etc?
            RoomIdConstants.R101_ThievesTown_EastAtticRoom, // only if both bottom rats
            RoomIdConstants.R106_PalaceofDarkness_RupeeRoom, // only if two turtles in row
            RoomIdConstants.R116_DesertPalace_MapChestRoom, // only if both antlions
            RoomIdConstants.R118_SwampPalace_WaterDrainRoom, // would need 3 mobs to be impassible to possibly softlock (what are the odds?)
            RoomIdConstants.R125_GanonsTower_Winder_WarpMazeRoom, // would need a lot of things exactly right, but better safe than sorry
            RoomIdConstants.R127_IcePalace_BigSpikeTrapsRoom, // TODO: what happens to beamos over a pit?
            RoomIdConstants.R131_DesertPalace_WestEntranceRoom, // TODO: test
            RoomIdConstants.R132_DesertPalace_MainEntranceRoom, // TODO: test
            RoomIdConstants.R133_DesertPalace_EastEntranceRoom, // TODO: test (only a problem for ER?)
            RoomIdConstants.R140_GanonsTower_EastandWestDownstairs_BigChestRoom, // TODO: test (probably safe?)
            RoomIdConstants.R141_GanonsTower_Tile_TorchPuzzleRoom, // TODO: test
            RoomIdConstants.R146_MiseryMire_DarkBombWall_SwitchesRoom, // TODO: test
            RoomIdConstants.R149_GanonsTower_FinalCollapsingBridgeRoom, // TODO: test, probably safe because of conveyor belts
            RoomIdConstants.R152_MiseryMire_EntranceRoom,
            RoomIdConstants.R155_GanonsTower_ManySpikes_WarpMazeRoom, // TODO: test, middle spike covers warp, are we randomizing those?
            RoomIdConstants.R156_GanonsTower_InvisibleFloorMazeRoom, // TODO: test
            RoomIdConstants.R157_GanonsTower_CompassChest_InvisibleFloorRoom, // TODO: test
            RoomIdConstants.R158_IcePalace_BigChestRoom, // big spikes will block
            RoomIdConstants.R160_MiseryMire_Pre_VitreousRoom, // TODO: test
            RoomIdConstants.R170_EasternPalace_MapChestRoom, // TODO: test
            RoomIdConstants.R175_IcePalace_IceBridgeRoom,
            RoomIdConstants.R179_MiseryMire_SpikeKeyChestRoom, // TODO: test lower stalfos blocking door
            RoomIdConstants.R186_EasternPalace_DarkAntifairy_KeyPotRoom,
            RoomIdConstants.R187_ThievesTown_Hellway, // TODO: test, should be ok but double check
            RoomIdConstants.R188_ThievesTown_ConveyorToilet, // TODO: test
            RoomIdConstants.R198_TurtleRock0xC6, // technically a door is blocked off, but who would ever go there?
            RoomIdConstants.R203_ThievesTown_NorthWestEntranceRoom,
            RoomIdConstants.R206_IcePalace_HoletoKholdstareRoom, // spikes block stuff
            RoomIdConstants.R208_AgahnimsTower_DarkMaze, // TODO: test
            RoomIdConstants.R210_MiseryMire_Mire02_WizzrobesRoom,
            RoomIdConstants.R213_TurtleRock_LaserKeyRoom,
            RoomIdConstants.R216_EasternPalace_PreArmosKnightsRoom,
            RoomIdConstants.R220_ThievesTown_SouthEastEntranceRoom, // TODO: test
            RoomIdConstants.R223_Cave_BackwardsDeathMountainTopFloor,
            RoomIdConstants.R228_Cave_LostOldManFinalCave, // who would go that way?
            RoomIdConstants.R231_Cave0xE7,
            RoomIdConstants.R238_Cave_SpiralCave,
            RoomIdConstants.R249_Cave0xF9, // TODO: test, probably can get past
            RoomIdConstants.R253_Cave0xFD,
            RoomIdConstants.R268_MimicCave,
        };

        int[] DontUseFlyingSprites =
        {
            RoomIdConstants.R210_MiseryMire_Mire02_WizzrobesRoom
        };
    }
}
