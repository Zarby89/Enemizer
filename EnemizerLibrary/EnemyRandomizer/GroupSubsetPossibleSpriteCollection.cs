using System.Collections.Generic;

namespace EnemizerLibrary
{
    /*
    public class GroupSubsetPossibleSpriteCollection
    {
        public List<GroupSubsetPossibleSprite> Sprites { get; set; }

        public GroupSubsetPossibleSpriteCollection()
        {
            this.Sprites = new List<GroupSubsetPossibleSprite>();

            AddPossibleSprites();
        }

        void AddPossibleSprites()
        {
            //ghini,thief?
            Sprites.Add(new GroupSubsetPossibleSprite(14, SpriteConstants.PoeSprite));

            // (require 23)
            Sprites.Add(new GroupSubsetPossibleSprite(22, SpriteConstants.RopaSprite, new[] { 23 }));
            Sprites.Add(new GroupSubsetPossibleSprite(22, SpriteConstants.HinoxSprite, new[] { 23 }));

            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.RedBariSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.BlueBariSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.YellowStalfosSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.StalfosSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.FlyingStalfosHeadSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.GuruguruBar_ClockwiseSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.GuruguruBar_CounterClockwiseSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(31, SpriteConstants.WinderSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(47, SpriteConstants.LeeverSprite));

            // TODO: 19 not needed for all? 73 could be 13?
            //need to be combined with 73 and 19 all guards
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.BlueSwordSoldier_DetectPlayerSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.GreenSwordSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.RedSpearSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.AssaultSwordSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.GreenSpearSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.RedJavelinSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.RedJavelinSoldier2Sprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.RedBombSoldiersSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.GreenSoldierRecruits_HMKnightSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.BallNChainTrooperSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(70, SpriteConstants.CannonSoldierSprite, new[] { 73, 19 }));

            // TODO: 19 not needed for all? 73 could be 13?
            //need to be combined with 73 and 19 all guards archers
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.BlueSwordSoldier_DetectPlayerSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.GreenSwordSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.RedSpearSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.GreenSpearSoldierSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.BlueArcherSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.GreenArcherSprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.RedJavelinSoldier2Sprite, new[] { 73, 19 }));
            Sprites.Add(new GroupSubsetPossibleSprite(72, SpriteConstants.GreenSoldierRecruits_HMKnightSprite, new[] { 73, 19 }));

            // --- Subset 1 ---
            //Sprites.Add(new GroupSubsetPossibleSprite(13, SpriteConstants)); // guards
            //13 contains guards same as 73
            //subset_gfx_sprites[13] = { }; //guards
            //subset_gfx_sprites[19] = { }; //guards

            Sprites.Add(new GroupSubsetPossibleSprite(30, SpriteConstants.MiniHelmasaurSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(30, SpriteConstants.MiniMoldormSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(30, SpriteConstants.HardhatBeetleSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(32, SpriteConstants.SlimeSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(32, SpriteConstants.StalfosKnightSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(32, SpriteConstants.TadpolesSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(32, SpriteConstants.Tadpoles2Sprite));

            Sprites.Add(new GroupSubsetPossibleSprite(44, SpriteConstants.PopoSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(44, SpriteConstants.Popo2Sprite));
            Sprites.Add(new GroupSubsetPossibleSprite(44, SpriteConstants.BeamosSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(73, SpriteConstants.BlueSwordSoldier_DetectPlayerSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(73, SpriteConstants.GreenSwordSoldierSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(73, SpriteConstants.GreenSpearSoldierSprite));

            // --- Subset 2 ---
            Sprites.Add(new GroupSubsetPossibleSprite(12, SpriteConstants.Octorok_OneWaySprite));
            Sprites.Add(new GroupSubsetPossibleSprite(12, SpriteConstants.OctoballoonSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(12, SpriteConstants.CrabSprite));

            //vulture, jazzhand, (Also contain tablets / rock in front of desert)
            Sprites.Add(new GroupSubsetPossibleSprite(18, SpriteConstants.VultureSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(18, SpriteConstants.GeldmanSprite));

            // TODO: Don't think octorock is in here? also doesn't require 22 (see group 18)?
            //pigmanspear, snapdragon (require 22) 
            //Sprites.Add(new GroupSubsetPossibleSprite(23, SpriteConstants.Octorok_OneWaySprite, new[] { 22 })); // octorok is not in this group...

            Sprites.Add(new GroupSubsetPossibleSprite(24, SpriteConstants.Octorok_OneWaySprite));

            //also the oldman
            Sprites.Add(new GroupSubsetPossibleSprite(28, SpriteConstants.RatSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(28, SpriteConstants.RopeSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(28, SpriteConstants.KeeseSprite));

            //subset_gfx_sprites[29] = { };

            Sprites.Add(new GroupSubsetPossibleSprite(34, SpriteConstants.WaterTektiteSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(34, SpriteConstants.KyameronSprite));

            //wallmaster,gibdo
            Sprites.Add(new GroupSubsetPossibleSprite(35, SpriteConstants.GibdoSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(36, SpriteConstants.RatSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(36, SpriteConstants.RopeSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(36, SpriteConstants.KeeseSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(37, SpriteConstants.SluggulaSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(37, SpriteConstants.WizzrobeSprite));

            //(A1) iceman,penguin
            Sprites.Add(new GroupSubsetPossibleSprite(38, SpriteConstants.PengatorSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.Roller_VerticalMovingSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.Roller_VerticalMoving2Sprite));
            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.RollerSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.Roller_HorizontalMovingSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.HokkuBokkuSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(39, SpriteConstants.ChainChompSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(40, SpriteConstants.ZazakFireballSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(40, SpriteConstants.RedZazakSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(40, SpriteConstants.GiboSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(41, SpriteConstants.WizzrobeSprite));

            //turtle,kondongo ,also the digging game guy //0x86 kodongo problem // KodongosSprite
            Sprites.Add(new GroupSubsetPossibleSprite(42, SpriteConstants.TerrorpinSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(42, SpriteConstants.Spark_LeftToRightSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(42, SpriteConstants.Spark_RightToLeftSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(46, SpriteConstants.GreenEyegoreSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(46, SpriteConstants.RedEyegoreSprite));
            //74 = lumberjack

            // --- Subset 3 ---
            Sprites.Add(new GroupSubsetPossibleSprite(16, SpriteConstants.ArmosSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(16, SpriteConstants.DeadrockSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(16, SpriteConstants.TektiteSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(17, SpriteConstants.RavenSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(17, SpriteConstants.BuzzblobSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(20, SpriteConstants.LynelSprite));

            Sprites.Add(new GroupSubsetPossibleSprite(27, SpriteConstants.BomberFlyingCreaturesFromDarkworldSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(27, SpriteConstants.BomberFlyingCreaturesFromDarkworld2Sprite));
            Sprites.Add(new GroupSubsetPossibleSprite(27, SpriteConstants.PikitSprite));

            //subset_gfx_sprites[74] = { };//wizzrobe

            //wizzrobe?
            Sprites.Add(new GroupSubsetPossibleSprite(80, SpriteConstants.ChickenSprite));

            //subset_gfx_sprites[81] = { };//switches

            //0x82 };//switches
            Sprites.Add(new GroupSubsetPossibleSprite(82, SpriteConstants.SpikeTrapSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(82, SpriteConstants.StatueSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(82, SpriteConstants.AntifairySprite));
            //Sprites.Add(new GroupSubsetPossibleSprite(82, SpriteConstants.AntifairyCircleSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(82, SpriteConstants.BigSpikeTrapSprite));

            //0x82 };//switches
            Sprites.Add(new GroupSubsetPossibleSprite(83, SpriteConstants.SpikeTrapSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(83, SpriteConstants.StatueSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(83, SpriteConstants.AntifairySprite));
            //Sprites.Add(new GroupSubsetPossibleSprite(83, SpriteConstants.AntifairyCircleSprite));
            Sprites.Add(new GroupSubsetPossibleSprite(83, SpriteConstants.BigSpikeTrapSprite));

            //subset_gfx_sprites[93] = { };//sanctuary mantle
        }
    }
    //*/
}