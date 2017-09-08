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
        public int? GroupId { get; set; }
        public int? SubGroup0 { get; set; }
        public int? SubGroup1 { get; set; }
        public int? SubGroup2 { get; set; }
        public int? SubGroup3 { get; set; }
        public byte? Parmeters { get; set; }
        public bool Special { get; set; }

        public SpriteRequirement(int SpriteId, bool Overlord, int? GroupId, int? SubGroup0, int? SubGroup1, int? SubGroup2, int? SubGroup3, byte? Parameters = null, bool Special = false)
        {
            this.SpriteId = SpriteId;
            this.Overlord = Overlord;
            this.GroupId = GroupId;
            this.SubGroup0 = SubGroup0;
            this.SubGroup1 = SubGroup1;
            this.SubGroup2 = SubGroup2;
            this.SubGroup3 = SubGroup3;

            this.Parmeters = Parameters;
            this.Special = Special;
        }

        public SpriteRequirement(int SpriteId)
        {
            this.SpriteId = SpriteId;
        }

        public static SpriteRequirement New(int SpriteId)
        {
            return new SpriteRequirement(SpriteId);
        }

        public SpriteRequirement AddGroup(int GroupId)
        {
            return this;
        }
    }

    public class SpriteRequirementCollection
    {
        public List<SpriteRequirement> SpriteRequirements { get; set; }

        public SpriteRequirementCollection()
        {
            SpriteRequirements = new List<SpriteRequirement>();

            AddSpriteRequirement(SpriteConstants.RavenSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.VultureSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FlyingStalfosHeadSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PullSwitch_GoodSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PullSwitch_TrapSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Octorok_OneWaySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MoldormSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Octorok_FourWaySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ChickenSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Octorok_MaybeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BuzzblobSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SnapdragonSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OctoballoonSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OctoballoonHatchlingsSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HinoxSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MoblinSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MiniHelmasaurSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GargoylesDomainGateSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AntifairySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SahasrahlaAginahSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BushHoarderSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MiniMoldormSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PoeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DwarvesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrowInWall_MaybeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.StatueSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WeathervaneSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CrystalSwitchSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BugCatchingKidSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SluggulaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PushSwitchSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RopaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedBariSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlueBariSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TalkingTreeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HardhatBeetleSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DeadrockSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.StorytellersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlindHideoutAttendantSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SweepingLadySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MultipurposeSpriteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LumberjacksSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TelepathicStones_NoIdeaWhatThisActuallyIsLikelyUnusedSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FluteBoysNotesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RaceHPNPCsSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Person_MaybeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FortuneTellerSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AngryBrothersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PullForRupeesSpriteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ScaredGirl2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.InnkeeperSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WitchSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WaterfallSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrowTargetSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AverageMiddleAgedManSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HalfMagicBatSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DashItemSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.VillageKidSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Signs_ChickenLadyAlsoShowedUp_ScaredLadiesOutsideHousesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RockHoarderSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TutorialSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LightningLockSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlueSwordSoldier_DetectPlayerSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenSwordSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedSpearSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AssaultSwordSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenSpearSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlueArcherSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenArcherSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedJavelinSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedJavelinSoldier2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedBombSoldiersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenSoldierRecruits_HMKnightSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GeldmanSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RabbitSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PopoSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Popo2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CannonBallsSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArmosSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GiantZoraSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArmosKnightsSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LanmolasSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FireballZoraSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WalkingZoraSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DesertPalaceBarriersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CrabSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BirdSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SquirrelSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Spark_LeftToRightSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Spark_RightToLeftSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Roller_VerticalMovingSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Roller_VerticalMoving2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RollerSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Roller_HorizontalMovingSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BeamosSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MasterSwordSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Devalant_NonShooterSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Devalant_ShooterSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ShootingGalleryProprietorSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MovingCannonBallShooters_RightSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MovingCannonBallShooters_LeftSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MovingCannonBallShooters_DownSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MovingCannonBallShooters_UpSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BallNChainTrooperSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CannonSoldierSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MirrorPortalSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RatSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RopeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KeeseSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HelmasaurKingFireballSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LeeverSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ActivatoForThePonds_WhereYouThrowInItemsSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.UnclePriestSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RunningManSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BottleSalesmanSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PrincessZeldaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Antifairy_AlternateSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.VillageElderSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BeeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AgahnimSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AgahnimEnergyBallSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HyuSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BigSpikeTrapSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GuruguruBar_ClockwiseSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GuruguruBar_CounterClockwiseSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WinderSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WaterTektiteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AntifairyCircleSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenEyegoreSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedEyegoreSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.YellowStalfosSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KodongosSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FlamesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MothulaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MothulasBeamSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SpikeTrapSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GibdoSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrghusSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrghusSpawnSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TerrorpinSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SlimeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WallmasterSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.StalfosKnightSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HelmasaurKingSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BumperSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SwimmersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.EyeLaser_RightSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.EyeLaser_LeftSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.EyeLaser_DownSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.EyeLaser_UpSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PengatorSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KyameronSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WizzrobeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TadpolesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Tadpoles2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Ostrich_HauntedGroveSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FluteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Birds_HauntedGroveSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FreezorSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KholdstareSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KholdstaresShellSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FallingIceSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ZazakFireballSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedZazakSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.StalfosSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BomberFlyingCreaturesFromDarkworldSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BomberFlyingCreaturesFromDarkworld2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.PikitSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MaidenSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AppleSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LostOldManSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DownPipeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.UpPipeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RightPipeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LeftPipeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GoodBee_AgainMaybeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HylianInscriptionSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ThiefsChestSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BombSalesmanSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KikiSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MaidenInBlindDungeonSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MonologueTestingSpriteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FeudingFriendsOnDeathMountainSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.WhirlpoolSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SalesmanChestgameGuy300RupeeGiverGuyChestGameThiefSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DrunkInTheInnSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Vitreous_LargeEyeballSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.Vitreous_SmallEyeballSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.VitreousLightningSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CatFish_QuakeMedallionSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AgahnimTeleportingZeldaToDarkworldSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BouldersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GiboSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ThiefSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MedusaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FourWayFireballSpittersSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HokkuBokkuSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BigFairyWhoHealsYouSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TektiteSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ChainChompSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.TrinexxSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.AnotherPartOfTrinexxSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.YetAnotherPartOfTrinexxSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlindTheThiefSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SwamolaSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LynelSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BunnyBeamSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FloppingFishSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.StalSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.LandmineSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.DiggingGameProprietorSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GanonSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CopyOfGanon_ExceptInvincibleSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HeartSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.GreenRupeeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BlueRupeeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.RedRupeeSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BombRefill1Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BombRefill4Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BombRefill8Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.SmallMagicRefillSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FullMagicRefillSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrowRefill5Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ArrowRefill10Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FairySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.KeySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BigKeySprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.ShieldSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MushroomSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.FakeMasterSwordSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MagicShopDude_HisItemsIncludingTheMagicPowderSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HeartContainerSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.HeartPieceSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.BushesSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CaneOfSomariaPlatformSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MantleSprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CaneOfSomariaPlatform_Unused1Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CaneOfSomariaPlatform_Unused2Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.CaneOfSomariaPlatform_Unused3Sprite, false, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.MedallionTabletSprite, false, null, null, null, null, null);
            // Overlords
            AddSpriteRequirement(SpriteConstants.OL_CanonBalls_EP4Walls, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_CanonBalls_EPEntrance, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_StalfosHeadTrap, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_BombDrop_RopeTrap, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_MovingFloor, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_Transformer_BunnyBeam, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_Wallmaster, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_FloorDrop_Square, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_FloorDrop_Path, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_RightEvil_PirogusuSpawner, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_LeftEvil_PirogusuSpawner, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_DownEvil_PirogusuSpawner, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_UpEvil_PirogusuSpawner, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_FlyingFloorTileTrap, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_WizzrobeSpawner, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_BlackSpawn_Zoro_BombHole, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_4Skull_Trap_Pot, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_Stalfos_Spawn_Trap_EP, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_ArmosKnight_Trigger, true, null, null, null, null, null);
            AddSpriteRequirement(SpriteConstants.OL_BombDrop_BombTrap, true, null, null, null, null, null);

            // "Special" sprites
            // rat-guard = green recruit (0x4B) with sub 1=73, sub 2=28
            AddSpriteRequirement(SpriteConstants.GreenSoldierRecruits_HMKnightSprite, false, null, 73, 28, null, null, null, true);
        }

        void AddSpriteRequirement(int SpriteId, bool Overlord, int? GroupId, int? SubGroup0, int? SubGroup1, int? SubGroup2, int? SubGroup3, byte? Parameters = null, bool Special = false)
        {
            SpriteRequirements.Add(new SpriteRequirement(SpriteId, Overlord, GroupId, SubGroup0, SubGroup1, SubGroup2, SubGroup3, Parameters, Special));

        }
    }
}
