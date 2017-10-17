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
        public Dictionary<AbsorbableTypes, bool> AbsorbableTypes { get; set; } = new Dictionary<EnemizerLibrary.AbsorbableTypes, bool>();

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
        [Description("Full Maigc")]
        FullMaigc,
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
