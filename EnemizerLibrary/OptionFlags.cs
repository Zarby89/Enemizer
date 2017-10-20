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
        public bool RandomizeEnemies { get; set; }
        public RandomizeEnemiesType RandomizeEnemiesType { get; set; } = RandomizeEnemiesType.Chaos; // default to Chaos
        public bool RandomizeBushEnemyChance { get; set; }

        public bool RandomizeEnemyHealthRange { get; set; }
        public int RandomizeEnemyHealthRangeAmount { get; set; }

        public bool RandomizeEnemyDamage { get; set; }
        public bool AllowEnemyZeroDamage { get; set; }

        public bool EasyModeEscape { get; set; }

        public bool EnemiesAbsorbable { get; set; }
        public int AbsorbableSpawnRate { get; set; }
        public Dictionary<AbsorbableTypes, bool> AbsorbableTypes { get; set; } = new Dictionary<AbsorbableTypes, bool>();

        public bool BossMadness { get; set; }

        public bool RandomizeBosses { get; set; }
        public RandomizeBossesType RandomizeBossesType { get; set; }

        public bool RandomizeBossHealth { get; set; }
        public int RandomizeBossHealthMinAmount { get; set; }
        public int RandomizeBossHealthMaxAmount { get; set; }

        public bool RandomizeBossDamage { get; set; }
        public int RandomizeBossDamageMinAmount { get; set; }
        public int RandomizeBossDamageMaxAmount { get; set; }

        public bool RandomizeBossBehavior { get; set; }

        public bool RandomizeDungeonPalettes { get; set; }
        public bool SetBlackoutMode { get; set; }

        public bool RandomizeOverworldPalettes { get; set; }

        public bool RandomizeSpritePalettes { get; set; }
        public bool SetAdvancedSpritePalettes { get; set; }
        public bool PukeMode { get; set; }
        public bool NegativeMode { get; set; }
        public bool GrayscaleMode { get; set; }

        public bool GenerateSpoilers { get; set; }
        public bool RandomizeLinkSpritePalette { get; set; }
        public bool RandomizePots { get; set; }
        public bool ShuffleMusic { get; set; }
        public bool BootlegMagic { get; set; }
        public bool DebugMode { get; set; }
        public bool CustomBosses { get; set; }
        public bool AndyMode { get; set; }
        public HeartBeepSpeed HeartBeepSpeed { get; set; } = HeartBeepSpeed.Half;
        public bool AlternateGfx { get; set; }
        public ShieldTypes ShieldGraphics { get; set; } = ShieldTypes.Normal;


        public byte[] ToByteArray()
        {
            var ret = new byte[0x50];
            int i = 0;
            ret[i++] = Convert.ToByte(this.RandomizeEnemies);
            ret[i++] = (byte)this.RandomizeEnemiesType;
            ret[i++] = Convert.ToByte(this.RandomizeBushEnemyChance);
            ret[i++] = Convert.ToByte(this.RandomizeEnemyHealthRange);
            ret[i++] = (byte)this.RandomizeEnemyHealthRangeAmount;
            ret[i++] = Convert.ToByte(this.RandomizeEnemyDamage);
            ret[i++] = Convert.ToByte(this.AllowEnemyZeroDamage);
            ret[i++] = Convert.ToByte(this.EasyModeEscape);
            ret[i++] = Convert.ToByte(this.EnemiesAbsorbable);
            ret[i++] = (byte)this.AbsorbableSpawnRate;

            var absorbableType = false;
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Heart, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.GreenRupee, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.BlueRupee, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.RedRupee, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_1, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_4, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Bomb_8, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.SmallMagic, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.FullMagic, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Arrow_5, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Arrow_10, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Fairy, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.Key, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }
            if (AbsorbableTypes.TryGetValue(EnemizerLibrary.AbsorbableTypes.BigKey, out absorbableType))
            {
                ret[i++] = Convert.ToByte(absorbableType);
            }

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
            ret[i++] = (byte)this.ShieldGraphics;

            return ret;
        }
    }

    public enum RandomizeEnemiesType
    {
        Basic,
        Normal,
        Hard,
        Chaos,
        Insanity
    }

    public enum RandomizeBossesType
    {
        Basic,
        Normal,
        Chaos
    }

    public enum ShieldTypes
    {
        [Description("Normal Shield")]
        Normal,
        [Description("Skull Shield")]
        SkullShield,
        [Description("Square Shield")]
        SquareShield
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
        [Description("Big Key(Test)")]
        BigKey
    }

    public enum HeartBeepSpeed
    {
        Default,
        Half,
        Quarter,
        Off
    }
}
