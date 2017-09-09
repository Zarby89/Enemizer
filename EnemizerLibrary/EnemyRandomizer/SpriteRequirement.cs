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
        public bool Overlord { get; set; }
        public List<byte> GroupId { get; set; } = new List<byte>();
        public List<byte> SubGroup0 { get; set; } = new List<byte>();
        public List<byte> SubGroup1 { get; set; } = new List<byte>();
        public List<byte> SubGroup2 { get; set; } = new List<byte>();
        public List<byte> SubGroup3 { get; set; } = new List<byte>();
        public byte? Parameters { get; set; }
        public bool SpecialGlitched { get; set; }

        public SpriteRequirement(int SpriteId, bool Overlord, byte? GroupId, byte? SubGroup0, byte? SubGroup1, byte? SubGroup2, byte? SubGroup3, byte? Parameters = null, bool Special = false)
        {
            this.SpriteId = SpriteId;
            this.Overlord = Overlord;
            if (GroupId != null)
            {
                this.GroupId.Add((byte)GroupId);
            }
            if (SubGroup0 != null)
            {
                this.SubGroup0.Add((byte)SubGroup0);
            }
            if (SubGroup1 != null)
            {
                this.SubGroup1.Add((byte)SubGroup1);
            }
            if (SubGroup2 != null)
            {
                this.SubGroup2.Add((byte)SubGroup2);
            }
            if (SubGroup3 != null)
            {
                this.SubGroup3.Add((byte)SubGroup3);
            }

            this.Parameters = Parameters;
            this.SpecialGlitched = Special;
        }

        public SpriteRequirement(int SpriteId)
        {
            this.SpriteId = SpriteId;
        }

        public static SpriteRequirement New(int SpriteId)
        {
            return new SpriteRequirement(SpriteId);
        }

        public SpriteRequirement SetOverlord()
        {
            Overlord = true;
            return this;
        }

        public SpriteRequirement AddGroup(params byte[] GroupId)
        {
            this.GroupId.AddRange(GroupId);
            return this;
        }

        public SpriteRequirement AddSubgroup0(params byte[] Subgroup0)
        {
            this.SubGroup0.AddRange(SubGroup0);
            return this;
        }

        public SpriteRequirement AddSubgroup1(params byte[] Subgroup1)
        {
            this.SubGroup1.AddRange(SubGroup1);
            return this;
        }

        public SpriteRequirement AddSubgroup2(params byte[] Subgroup2)
        {
            this.SubGroup2.AddRange(SubGroup2);
            return this;
        }

        public SpriteRequirement AddSubgroup3(params byte[] Subgroup3)
        {
            this.SubGroup3.AddRange(SubGroup3);
            return this;
        }

        public SpriteRequirement SetParameters(byte Parameters)
        {
            this.Parameters = Parameters;
            return this;
        }

        public SpriteRequirement IsSpecialGlitched()
        {
            SpecialGlitched = true;
            return this;
        }
    }

    public class SpriteRequirementCollection
    {
        public List<SpriteRequirement> SpriteRequirements { get; set; }

        public SpriteRequirementCollection()
        {
            SpriteRequirements = new List<SpriteRequirement>();

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RavenSprite).AddSubgroup3(17, 25));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VultureSprite).AddSubgroup2(18));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FlyingStalfosHeadSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullSwitch_GoodSprite).AddSubgroup3(82));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullSwitch_TrapSprite).AddSubgroup3(82));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_OneWaySprite).AddSubgroup2(12, 24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MoldormSprite).AddSubgroup2(48));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_FourWaySprite).AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ChickenSprite).AddSubgroup3(21, 80));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Octorok_MaybeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BuzzblobSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SnapdragonSprite).AddSubgroup0(22).AddSubgroup2(23));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OctoballoonSprite).AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OctoballoonHatchlingsSprite).AddSubgroup2(12)); // TODO: double check

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HinoxSprite).AddSubgroup0(22));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MoblinSprite).AddSubgroup2(23));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MiniHelmasaurSprite).AddSubgroup1(30));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GargoylesDomainGateSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AntifairySprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SahasrahlaAginahSprite).AddSubgroup2(76));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BushHoarderSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MiniMoldormSprite).AddSubgroup1(30));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PoeSprite).AddSubgroup3(14, 21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DwarvesSprite).AddSubgroup1(77).AddSubgroup3(21));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowInWall_MaybeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StatueSprite).AddSubgroup3(82, 83));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WeathervaneSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CrystalSwitchSprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BugCatchingKidSprite).AddSubgroup0(81));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SluggulaSprite).AddSubgroup2(37));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PushSwitchSprite).AddSubgroup3(83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RopaSprite).AddSubgroup0(22));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedBariSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueBariSprite).AddSubgroup0(31));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TalkingTreeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HardhatBeetleSprite).AddSubgroup1(30));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DeadrockSprite).AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StorytellersSprite)); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlindHideoutAttendantSprite).AddSubgroup0(14, 79));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SweepingLadySprite).AddGroup(6)); // TODO: add subs instead?

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MultipurposeSpriteSprite)); // TODO: what is this?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LumberjacksSprite).AddSubgroup2(74));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TelepathicStones_NoIdeaWhatThisActuallyIsLikelyUnusedSprite));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FluteBoysNotesSprite)); // TODO: does this use OAM2?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RaceHPNPCsSprite).AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Person_MaybeSprite).AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FortuneTellerSprite).AddSubgroup0(75));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AngryBrothersSprite).AddSubgroup0(79));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PullForRupeesSpriteSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ScaredGirl2Sprite).AddGroup(6));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.InnkeeperSprite)); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WitchSprite).AddSubgroup2(76));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WaterfallSprite));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowTargetSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AverageMiddleAgedManSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HalfMagicBatSprite).AddSubgroup3(29));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DashItemSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VillageKidSprite).AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Signs_ChickenLadyAlsoShowedUp_ScaredLadiesOutsideHousesSprite).AddGroup(6));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RockHoarderSprite).AddSubgroup3(17));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TutorialSoldierSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LightningLockSprite).AddSubgroup2(12).AddSubgroup3(29)); // TODO: check sub 2 is needed

            // probably needs 19 and 41 for sub 2 for falling animation
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueSwordSoldier_DetectPlayerSprite).AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSwordSoldierSprite).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedSpearSoldierSprite).AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AssaultSwordSoldierSprite).AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSpearSoldierSprite).AddSubgroup1(13, 73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueArcherSprite).AddSubgroup0(72).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenArcherSprite).AddSubgroup0(72).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedJavelinSoldierSprite).AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedJavelinSoldier2Sprite).AddSubgroup0(70).AddSubgroup1(73)); // TODO: double check 70 needed
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedBombSoldiersSprite).AddSubgroup0(70).AddSubgroup1(73));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSoldierRecruits_HMKnightSprite).AddSubgroup1(73).AddSubgroup2(19));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GeldmanSprite).AddSubgroup2(18));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RabbitSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PopoSprite).AddSubgroup1(44));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Popo2Sprite).AddSubgroup1(44));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CannonBallsSprite).AddSubgroup2(46));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArmosSprite).AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GiantZoraSprite).AddSubgroup3(68));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArmosKnightsSprite).AddSubgroup3(29));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LanmolasSprite).AddSubgroup3(49));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FireballZoraSprite).AddSubgroup2(12, 24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WalkingZoraSprite).AddSubgroup2(12).AddSubgroup3(68));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DesertPalaceBarriersSprite).AddSubgroup2(18));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CrabSprite).AddSubgroup2(12));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BirdSprite).AddSubgroup2(55).AddSubgroup3(54)); // TODO: check 54
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SquirrelSprite).AddSubgroup2(55).AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Spark_LeftToRightSprite).AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Spark_RightToLeftSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_VerticalMovingSprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_VerticalMoving2Sprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RollerSprite).AddSubgroup2(39));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Roller_HorizontalMovingSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BeamosSprite).AddSubgroup1(44));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MasterSwordSprite).AddSubgroup2(55).AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Devalant_NonShooterSprite).AddSubgroup0(47));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Devalant_ShooterSprite).AddSubgroup0(47));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ShootingGalleryProprietorSprite).AddSubgroup0(75));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_RightSprite).AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_LeftSprite).AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_DownSprite).AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MovingCannonBallShooters_UpSprite).AddSubgroup2(46));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BallNChainTrooperSprite).AddSubgroup0(70).AddSubgroup1(73)); // TODO: check 73
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CannonSoldierSprite).AddSubgroup0(70).AddSubgroup1(73)); // TODO: verify because these don't exist in vanilla

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MirrorPortalSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RatSprite).AddSubgroup2(28, 36));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RopeSprite).AddSubgroup2(28, 36)); // 36 isn't used anywhere in vanilla beside a trap in TT I think

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KeeseSprite).AddSubgroup2(28, 36));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HelmasaurKingFireballSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LeeverSprite).AddSubgroup0(47));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ActivatoForThePonds_WhereYouThrowInItemsSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.UnclePriestSprite).AddSubgroup0(71, 81));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RunningManSprite).AddGroup(6));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BottleSalesmanSprite).AddGroup(6));
            
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PrincessZeldaSprite)); // zelda uses some special sprite

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Antifairy_AlternateSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VillageElderSprite).AddSubgroup0(75).AddSubgroup1(77).AddSubgroup2(74)); // TODO: check which is actually needed

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BeeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimSprite).AddSubgroup0(85).AddSubgroup1(26, 61).AddSubgroup2(66).AddSubgroup3(67)); // not sure what difference is for sub 1

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimEnergyBallSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HyuSprite).AddSubgroup0(31)); // TODO: check this because it only shows up as stalfos head in game??

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigSpikeTrapSprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GuruguruBar_ClockwiseSprite).AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GuruguruBar_CounterClockwiseSprite).AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WinderSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WaterTektiteSprite).AddSubgroup2(34));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AntifairyCircleSprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenEyegoreSprite).AddSubgroup1(44).AddSubgroup2(46)); // one is mimic, one is eyegore...
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedEyegoreSprite).AddSubgroup1(44).AddSubgroup2(46));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.YellowStalfosSprite)); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KodongosSprite).AddSubgroup2(42));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FlamesSprite)); // TODO: add

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MothulaSprite).AddSubgroup2(56));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MothulasBeamSprite).AddSubgroup2(56));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SpikeTrapSprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GibdoSprite).AddSubgroup2(35));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrghusSprite).AddSubgroup2(57));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrghusSpawnSprite).AddSubgroup2(57));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TerrorpinSprite).AddSubgroup2(42));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SlimeSprite)); // TODO: add? is this special?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WallmasterSprite).AddSubgroup2(35));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalfosKnightSprite).AddSubgroup1(32));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HelmasaurKingSprite).AddSubgroup2(58).AddSubgroup3(62));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BumperSprite).AddSubgroup3(82, 83));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SwimmersSprite)); // TODO: add? what is this?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_RightSprite).AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_LeftSprite).AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_DownSprite).AddSubgroup3(82, 83));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.EyeLaser_UpSprite).AddSubgroup3(82, 83));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PengatorSprite).AddSubgroup2(38));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KyameronSprite).AddSubgroup2(34));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WizzrobeSprite).AddSubgroup2(37, 41));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TadpolesSprite).AddSubgroup1(32));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Tadpoles2Sprite).AddSubgroup1(32));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Ostrich_HauntedGroveSprite).AddSubgroup2(78));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FluteSprite)); // TODO: where is this?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Birds_HauntedGroveSprite).AddSubgroup2(78));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FreezorSprite).AddSubgroup2(38));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KholdstareSprite).AddSubgroup2(60));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KholdstaresShellSprite)); // TODO: this is BG2
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FallingIceSprite).AddSubgroup2(60));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ZazakFireballSprite)); // TODO: can't find this

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedZazakSprite).AddSubgroup2(40));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalfosSprite).AddSubgroup0(31));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BomberFlyingCreaturesFromDarkworldSprite).AddSubgroup3(27));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BomberFlyingCreaturesFromDarkworld2Sprite).AddSubgroup3(27));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.PikitSprite).AddSubgroup3(27));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MaidenSprite)); // TODO: where is this?
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AppleSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LostOldManSprite).AddSubgroup0(70).AddSubgroup1(73).AddSubgroup2(28)); // TODO: figure out which is actually needed

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DownPipeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.UpPipeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RightPipeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LeftPipeSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GoodBee_AgainMaybeSprite).AddSubgroup0(31)); // TOOD: really?

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HylianInscriptionSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ThiefsChestSprite).AddSubgroup3(21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombSalesmanSprite).AddSubgroup1(77));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KikiSprite).AddSubgroup3(25));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MaidenInBlindDungeonSprite)); // TODO: special?

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MonologueTestingSpriteSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FeudingFriendsOnDeathMountainSprite).AddSubgroup3(20));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.WhirlpoolSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite) // TODO: figure out which are needed
                                                            .AddSubgroup0(75, 79, 14, 21)
                                                            .AddSubgroup1(77)
                                                            .AddSubgroup2(74)
                                                            .AddSubgroup3(80));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DrunkInTheInnSprite).AddSubgroup0(79).AddSubgroup1(77).AddSubgroup2(74).AddSubgroup3(80)); // TODO: figure out which are needed

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Vitreous_LargeEyeballSprite).AddSubgroup3(61));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.Vitreous_SmallEyeballSprite).AddSubgroup3(61));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.VitreousLightningSprite).AddSubgroup3(61));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CatFish_QuakeMedallionSprite).AddSubgroup2(24));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AgahnimTeleportingZeldaToDarkworldSprite).AddSubgroup0(85).AddSubgroup1(61).AddSubgroup2(66).AddSubgroup3(67)); // all needed?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BouldersSprite).AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GiboSprite).AddSubgroup2(40));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ThiefSprite).AddSubgroup0(14, 21));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MedusaSprite).AddSubgroup3(14)); // TODO: this is only for graveyard fake stones to spawn poe

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FourWayFireballSpittersSprite).AddSubgroup3(82)); // TODO: really?

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HokkuBokkuSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigFairyWhoHealsYouSprite).AddSubgroup2(57).AddSubgroup3(54));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TektiteSprite).AddSubgroup3(16));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ChainChompSprite).AddSubgroup2(39));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.TrinexxSprite).AddSubgroup0(64).AddSubgroup3(63));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.AnotherPartOfTrinexxSprite).AddSubgroup0(64).AddSubgroup3(63));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.YetAnotherPartOfTrinexxSprite).AddSubgroup0(64).AddSubgroup3(63));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlindTheThiefSprite).AddSubgroup2(59));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SwamolaSprite).AddSubgroup3(25));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LynelSprite).AddSubgroup3(20));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BunnyBeamSprite)); // TODO: find
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FloppingFishSprite)); // TODO: find
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.StalSprite)); // TODO: find
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.LandmineSprite)); // TODO: find

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.DiggingGameProprietorSprite).AddSubgroup1(42));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GanonSprite).AddSubgroup0(33).AddSubgroup1(65).AddSubgroup2(69).AddSubgroup3(51));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CopyOfGanon_ExceptInvincibleSprite));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenRupeeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueRupeeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.RedRupeeSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill1Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill4Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BombRefill8Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.SmallMagicRefillSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FullMagicRefillSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowRefill5Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ArrowRefill10Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FairySprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.KeySprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BigKeySprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.ShieldSprite).AddSubgroup3(27)); // TODO: check this is for pikit

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MushroomSprite).AddSubgroup3(17));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.FakeMasterSwordSprite).AddSubgroup3(17)); // TODO: check

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MagicShopDude_HisItemsIncludingTheMagicPowderSprite).AddSubgroup0(75).AddSubgroup3(90));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartContainerSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.HeartPieceSprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BushesSprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatformSprite).AddSubgroup2(39)); // TODO: verify

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MantleSprite).AddSubgroup0(93));

            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused1Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused2Sprite));
            //SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.CaneOfSomariaPlatform_Unused3Sprite));

            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.MedallionTabletSprite).AddSubgroup2(18));

            // Overlords
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_CanonBalls_EP4Walls).SetOverlord().AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_CanonBalls_EPEntrance).SetOverlord().AddSubgroup2(46));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_StalfosHeadTrap).SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BombDrop_RopeTrap).SetOverlord().AddSubgroup2(28, 36));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_MovingFloor).SetOverlord());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_Transformer_BunnyBeam).SetOverlord());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_Wallmaster).SetOverlord().AddSubgroup2(35));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FloorDrop_Square).SetOverlord().AddSubgroup3(82));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FloorDrop_Path).SetOverlord().AddSubgroup3(82));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_RightEvil_PirogusuSpawner).SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_LeftEvil_PirogusuSpawner).SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_DownEvil_PirogusuSpawner).SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_UpEvil_PirogusuSpawner).SetOverlord().AddSubgroup2(34));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_FlyingFloorTileTrap).SetOverlord()); // TODO: is this special sprites?
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_WizzrobeSpawner).SetOverlord().AddSubgroup2(37, 41));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BlackSpawn_Zoro_BombHole).SetOverlord().AddSubgroup1(32));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_4Skull_Trap_Pot).SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_Stalfos_Spawn_Trap_EP).SetOverlord().AddSubgroup0(31));
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_ArmosKnight_Trigger).SetOverlord());
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.OL_BombDrop_BombTrap).SetOverlord());

            // "Special" sprites
            // rat-guard = green recruit (0x4B) with sub 1=73, sub 2=28
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.GreenSoldierRecruits_HMKnightSprite)
                                                    .IsSpecialGlitched()
                                                    .AddSubgroup0(73)
                                                    .AddSubgroup1(28));

            // Palette glitch and invisible guard (need to see if this causes any other issues)
            SpriteRequirements.Add(SpriteRequirement.New(SpriteConstants.BlueSwordSoldier_DetectPlayerSprite)
                                                    .IsSpecialGlitched()
                                                    .SetParameters(0x18) // 11000 should cause very bad things
                                                    .AddSubgroup1(73));
        }

        //void AddSpriteRequirement(int SpriteId, bool Overlord, int? GroupId, int? SubGroup0, int? SubGroup1, int? SubGroup2, int? SubGroup3, byte? Parameters = null, bool Special = false)
        //{
        //    SpriteRequirements.Add(new SpriteRequirement(SpriteId, Overlord, GroupId, SubGroup0, SubGroup1, SubGroup2, SubGroup3, Parameters, Special));
        //}
    }
}
