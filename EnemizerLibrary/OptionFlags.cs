using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OptionFlags
    {
        public bool RandomizeEnemies { get; set; } = true;
        public RandomizeEnemiesType RandomizeEnemiesType { get; set; } = RandomizeEnemiesType.Chaos; // default to Chaos
        public bool RandomizeBushEnemyChance { get; set; } = true;

        public bool RandomizeEnemyHealthRange { get; set; }
        public RandomizeEnemyHPType RandomizeEnemyHealthType { get; set; }

        public bool RandomizeEnemyDamage { get; set; }
        public bool AllowEnemyZeroDamage { get; set; }
        public bool ShuffleEnemyDamageGroups { get; set; }
        public bool EnemyDamageChaosMode { get; set; }

        public bool EasyModeEscape { get; set; }

        public bool EnemiesAbsorbable { get; set; }
        public int AbsorbableSpawnRate { get; set; }
        public Dictionary<AbsorbableTypes, bool> AbsorbableTypes { get; set; } = new Dictionary<AbsorbableTypes, bool>();

        public bool BossMadness { get; set; }

        public bool RandomizeBosses { get; set; } = true;
        public RandomizeBossesType RandomizeBossesType { get; set; } = RandomizeBossesType.Chaos;

        public bool RandomizeBossHealth { get; set; }
        public int RandomizeBossHealthMinAmount { get; set; }
        public int RandomizeBossHealthMaxAmount { get; set; }

        public bool RandomizeBossDamage { get; set; }
        public int RandomizeBossDamageMinAmount { get; set; }
        public int RandomizeBossDamageMaxAmount { get; set; }

        public bool RandomizeBossBehavior { get; set; }

        public bool RandomizeDungeonPalettes { get; set; } = true;
        public bool SetBlackoutMode { get; set; }

        public bool RandomizeOverworldPalettes { get; set; } = true;

        public bool RandomizeSpritePalettes { get; set; } = true;
        public bool SetAdvancedSpritePalettes { get; set; }
        public bool PukeMode { get; set; }
        public bool NegativeMode { get; set; }
        public bool GrayscaleMode { get; set; }

        public bool GenerateSpoilers { get; set; } = true;
        public bool RandomizeLinkSpritePalette { get; set; }
        public bool RandomizePots { get; set; } = true;
        public bool ShuffleMusic { get; set; }
        public bool BootlegMagic { get; set; } = false;
        public bool DebugMode { get; set; } = false;
        public bool CustomBosses { get; set; }
        public bool AndyMode { get; set; } = false;
        public HeartBeepSpeed HeartBeepSpeed { get; set; } = HeartBeepSpeed.Half;
        public bool AlternateGfx { get; set; }
        public string ShieldGraphics { get; set; } = "shield_gfx\\normal.gfx";
        public string SwordGraphics { get; set; } = "sword_gfx\\normal.gfx";
        public bool BeeMizer { get; set; }
        public BeeLevel BeesLevel { get; set; }
        public bool DebugForceEnemy { get; set; }
        public int DebugForceEnemyId { get; set; }
        public bool DebugForceBoss { get; set; }
        public BossType DebugForceBossId { get; set; }
        public bool DebugOpenShutterDoors { get; set; }
        public bool DebugForceEnemyDamageZero { get; set; }
        public bool DebugShowRoomIdInRupeeCounter { get; set; }
        public bool OHKO { get; set; }
        public bool RandomizeTileTrapPattern { get; set; }
        public bool RandomizeTileTrapFloorTile { get; set; }
        public bool AllowKillableThief { get; set; }
        public bool RandomizeSpriteOnHit { get; set; }
        public bool HeroMode { get; set; }
        public bool IncreaseBrightness { get; set; }
        public bool MuteMusicEnableMSU1 { get; set; }

        public bool UseManualBosses { get; set; } = false; // used by randomizer web
        public ManualBosses ManualBosses { get; set; }

        public OptionFlags()
        {

        }

        public OptionFlags(byte[] optionBytes)
        {
            int i = 0;
            this.RandomizeEnemies = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeEnemiesType = (RandomizeEnemiesType)optionBytes[i++];
            this.RandomizeBushEnemyChance = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeEnemyHealthRange = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeEnemyHealthType = (RandomizeEnemyHPType)optionBytes[i++];
            this.RandomizeEnemyDamage = Convert.ToBoolean(optionBytes[i++]);
            this.AllowEnemyZeroDamage = Convert.ToBoolean(optionBytes[i++]);
            this.EasyModeEscape = Convert.ToBoolean(optionBytes[i++]);
            this.EnemiesAbsorbable = Convert.ToBoolean(optionBytes[i++]);
            this.AbsorbableSpawnRate = optionBytes[i++];

            foreach(var e in Enum.GetValues(typeof(AbsorbableTypes)))
            {
                AbsorbableTypes[(AbsorbableTypes)e] = false;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Heart] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.GreenRupee] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.BlueRupee] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.RedRupee] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Bomb_1] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Bomb_4] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Bomb_8] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.SmallMagic] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.FullMagic] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Arrow_5] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Arrow_10] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Fairy] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.Key] = true;
            }
            if (optionBytes[i++] != 0)
            {
                AbsorbableTypes[EnemizerLibrary.AbsorbableTypes.BigKey] = true;
            }

            this.BossMadness = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeBosses = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeBossesType = (EnemizerLibrary.RandomizeBossesType)optionBytes[i++];
            this.RandomizeBossHealth = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeBossHealthMinAmount = optionBytes[i++];
            this.RandomizeBossHealthMaxAmount = optionBytes[i++];
            this.RandomizeBossDamage = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeBossDamageMinAmount = optionBytes[i++];
            this.RandomizeBossDamageMaxAmount = optionBytes[i++];
            this.RandomizeBossBehavior = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeDungeonPalettes = Convert.ToBoolean(optionBytes[i++]);
            this.SetBlackoutMode = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeOverworldPalettes = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeSpritePalettes = Convert.ToBoolean(optionBytes[i++]);
            this.SetAdvancedSpritePalettes = Convert.ToBoolean(optionBytes[i++]);
            this.PukeMode = Convert.ToBoolean(optionBytes[i++]);
            this.NegativeMode = Convert.ToBoolean(optionBytes[i++]);
            this.GrayscaleMode = Convert.ToBoolean(optionBytes[i++]);
            this.GenerateSpoilers = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeLinkSpritePalette = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizePots = Convert.ToBoolean(optionBytes[i++]);
            this.ShuffleMusic = Convert.ToBoolean(optionBytes[i++]);
            this.BootlegMagic = Convert.ToBoolean(optionBytes[i++]);
            this.DebugMode = Convert.ToBoolean(optionBytes[i++]);
            this.CustomBosses = Convert.ToBoolean(optionBytes[i++]);
            this.AndyMode = Convert.ToBoolean(optionBytes[i++]);
            this.HeartBeepSpeed = (EnemizerLibrary.HeartBeepSpeed)optionBytes[i++];
            this.AlternateGfx = Convert.ToBoolean(optionBytes[i++]);
            //this.ShieldGraphics = optionBytes[i++];
            i++;
            this.ShuffleEnemyDamageGroups = Convert.ToBoolean(optionBytes[i++]);
            this.EnemyDamageChaosMode = Convert.ToBoolean(optionBytes[i++]);
            //this.SwordGraphics = optionBytes[i++];
            i++;
            this.BeeMizer = Convert.ToBoolean(optionBytes[i++]);
            this.BeesLevel = (BeeLevel)optionBytes[i++];

            this.DebugForceEnemy = Convert.ToBoolean(optionBytes[i++]);
            this.DebugForceEnemyId = optionBytes[i++];
            this.DebugForceBoss = Convert.ToBoolean(optionBytes[i++]);
            this.DebugForceBossId = (BossType)optionBytes[i++];
            this.DebugOpenShutterDoors = Convert.ToBoolean(optionBytes[i++]);
            this.DebugForceEnemyDamageZero = Convert.ToBoolean(optionBytes[i++]);
            this.DebugShowRoomIdInRupeeCounter = Convert.ToBoolean(optionBytes[i++]);
            this.OHKO = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeTileTrapPattern = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeTileTrapFloorTile = Convert.ToBoolean(optionBytes[i++]);
            this.AllowKillableThief = Convert.ToBoolean(optionBytes[i++]);
            this.RandomizeSpriteOnHit = Convert.ToBoolean(optionBytes[i++]);
            this.HeroMode = Convert.ToBoolean(optionBytes[i++]);
            this.IncreaseBrightness = Convert.ToBoolean(optionBytes[i++]);
            this.MuteMusicEnableMSU1 = Convert.ToBoolean(optionBytes[i++]);
        }

        public byte[] ToByteArray()
        {
            var ret = new byte[RomData.EnemizerInfoFlagsLength];
            int i = 0;
            ret[i++] = Convert.ToByte(this.RandomizeEnemies);
            ret[i++] = (byte)this.RandomizeEnemiesType;
            ret[i++] = Convert.ToByte(this.RandomizeBushEnemyChance);
            ret[i++] = Convert.ToByte(this.RandomizeEnemyHealthRange);
            ret[i++] = (byte)this.RandomizeEnemyHealthType;
            ret[i++] = Convert.ToByte(this.RandomizeEnemyDamage);
            ret[i++] = Convert.ToByte(this.AllowEnemyZeroDamage);
            ret[i++] = Convert.ToByte(this.EasyModeEscape);
            ret[i++] = Convert.ToByte(this.EnemiesAbsorbable);
            ret[i++] = (byte)this.AbsorbableSpawnRate;

            var absorbableType = false;
            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Heart, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.GreenRupee, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.BlueRupee, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.RedRupee, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_1, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_4, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_8, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.SmallMagic, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.FullMagic, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Arrow_5, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Arrow_10, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Fairy, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Key, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.BigKey, out absorbableType);
            ret[i++] = Convert.ToByte(absorbableType);

            ret[i++] = Convert.ToByte(this.BossMadness);
            ret[i++] = Convert.ToByte(this.RandomizeBosses);
            ret[i++] = (byte)this.RandomizeBossesType;
            ret[i++] = Convert.ToByte(this.RandomizeBossHealth);
            ret[i++] = (byte)this.RandomizeBossHealthMinAmount;
            ret[i++] = (byte)this.RandomizeBossHealthMaxAmount;
            ret[i++] = Convert.ToByte(this.RandomizeBossDamage);
            ret[i++] = (byte)this.RandomizeBossDamageMinAmount;
            ret[i++] = (byte)this.RandomizeBossDamageMaxAmount;
            ret[i++] = Convert.ToByte(this.RandomizeBossBehavior);
            ret[i++] = Convert.ToByte(this.RandomizeDungeonPalettes);
            ret[i++] = Convert.ToByte(this.SetBlackoutMode);
            ret[i++] = Convert.ToByte(this.RandomizeOverworldPalettes);
            ret[i++] = Convert.ToByte(this.RandomizeSpritePalettes);
            ret[i++] = Convert.ToByte(this.SetAdvancedSpritePalettes);
            ret[i++] = Convert.ToByte(this.PukeMode);
            ret[i++] = Convert.ToByte(this.NegativeMode);
            ret[i++] = Convert.ToByte(this.GrayscaleMode);
            ret[i++] = Convert.ToByte(this.GenerateSpoilers);
            ret[i++] = Convert.ToByte(this.RandomizeLinkSpritePalette);
            ret[i++] = Convert.ToByte(this.RandomizePots);
            ret[i++] = Convert.ToByte(this.ShuffleMusic);
            ret[i++] = Convert.ToByte(this.BootlegMagic);
            ret[i++] = Convert.ToByte(this.DebugMode);
            ret[i++] = Convert.ToByte(this.CustomBosses);
            ret[i++] = Convert.ToByte(this.AndyMode);
            ret[i++] = (byte)this.HeartBeepSpeed;
            ret[i++] = Convert.ToByte(this.AlternateGfx);
            ret[i++] = 0; // (byte)this.ShieldGraphics;
            ret[i++] = Convert.ToByte(this.ShuffleEnemyDamageGroups);
            ret[i++] = Convert.ToByte(this.EnemyDamageChaosMode);
            ret[i++] = 0; // (byte)this.SwordGraphics;
            ret[i++] = Convert.ToByte(this.BeeMizer);
            ret[i++] = Convert.ToByte(this.BeesLevel);

            ret[i++] = Convert.ToByte(this.DebugForceEnemy);
            ret[i++] = Convert.ToByte(this.DebugForceEnemyId);
            ret[i++] = Convert.ToByte(this.DebugForceBoss);
            ret[i++] = Convert.ToByte(this.DebugForceBossId);
            ret[i++] = Convert.ToByte(this.DebugOpenShutterDoors);
            ret[i++] = Convert.ToByte(this.DebugForceEnemyDamageZero);
            ret[i++] = Convert.ToByte(this.DebugShowRoomIdInRupeeCounter);
            ret[i++] = Convert.ToByte(this.OHKO);
            ret[i++] = Convert.ToByte(this.RandomizeTileTrapPattern);
            ret[i++] = Convert.ToByte(this.RandomizeTileTrapFloorTile);
            ret[i++] = Convert.ToByte(this.AllowKillableThief);
            ret[i++] = Convert.ToByte(this.RandomizeSpriteOnHit);
            ret[i++] = Convert.ToByte(this.HeroMode);
            ret[i++] = Convert.ToByte(this.IncreaseBrightness);
            ret[i++] = Convert.ToByte(this.MuteMusicEnableMSU1);
            return ret;
        }
    }

    public class ManualBosses
    {
        public string EasternPalace { get; set; }
        public string DesertPalace { get; set; }
        public string TowerOfHera { get; set; }
        public string AgahnimsTower { get; set; }
        public string PalaceOfDarkness { get; set; }
        public string SwampPalace { get; set; }
        public string SkullWoods { get; set; }
        public string ThievesTown { get; set; }
        public string IcePalace { get; set; }
        public string MiseryMire { get; set; }
        public string TurtleRock { get; set; }
        public string GanonsTower1 { get; set; }
        public string GanonsTower2 { get; set; }
        public string GanonsTower3 { get; set; }
        public string GanonsTower4 { get; set; }
        public string Ganon { get; set; }
    }

    public enum RandomizeEnemiesType
    {
        Basic,
        Normal,
        Hard,
        Chaos,
        Insanity
    }

    public enum RandomizeEnemyHPType
    {
        Easy,
        Medium,
        Hard,
        Patty
    }

    public enum RandomizeBossesType
    {
        Basic,
        Normal,
        Chaos
    }

    public enum SwordTypes
    {
        [Description("Normal Sword")]
        Normal,
    }

    public enum ShieldTypes
    {
        [Description("Normal Shield")]
        Normal,
        //[Description("Skull Shield")]
        //SkullShield,
        //[Description("Square Shield")]
        //SquareShield
    }

    public enum AbsorbableTypes
    {
        [Description("Heart")]
        Heart,
        [Description("Green Rupee")]
        GreenRupee,
        [Description("Blue Rupee")]
        BlueRupee,
        [Description("Red Rupee")]
        RedRupee,
        [Description("Bomb (1)")]
        Bomb_1,
        [Description("Bomb (4)")]
        Bomb_4,
        [Description("Bomb (8)")]
        Bomb_8,
        [Description("Small Magic")]
        SmallMagic,
        [Description("Full Magic")]
        FullMagic,
        [Description("Arrow (5)")]
        Arrow_5,
        [Description("Arrow (10)")]
        Arrow_10,
        [Description("Fairy")]
        Fairy,
        [Description("Key")]
        Key,
        [Description("Big Key")]
        BigKey
    }

    public enum HeartBeepSpeed
    {
        Default,
        Half,
        Quarter,
        Off
    }

    public enum BeeLevel
    {
        [Description("Bees??")]
        Level1,
        [Description("Bees!")]
        Level2,
        [Description("Beeeeees!?")]
        Level3,
        [Description("Beeeeeeeeeeeeeeeeeeeees")]
        Level4
    }
}
