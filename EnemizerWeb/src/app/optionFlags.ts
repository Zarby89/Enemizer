
export class OptionFlags
{
    RandomizeEnemies: boolean = true;
    RandomizeEnemiesType: RandomizeEnemiesType = RandomizeEnemiesType.Chaos;
    RandomizeBushEnemyChance: boolean = true;

    RandomizeEnemyHealthRange: boolean;
    RandomizeEnemyHealthType: RandomizeEnemyHPType;

    RandomizeEnemyDamage: boolean;
    AllowEnemyZeroDamage: boolean;
    ShuffleEnemyDamageGroups: boolean;
    EnemyDamageChaosMode: boolean;

    EasyModeEscape: boolean;

    EnemiesAbsorbable: boolean;
    AbsorbableSpawnRate: number;
    AbsorbableTypes: AbsorbableTypesDictionary;

    BossMadness: boolean; // unused

    RandomizeBosses: boolean;
    RandomizeBossesType: RandomizeBossesType = RandomizeBossesType.Chaos;

    RandomizeBossHealth: boolean;
    RandomizeBossHealthMinAmount: number;
    RandomizeBossHealthMaxAmount: number;

    RandomizeBossDamage: boolean;
    RandomizeBossDamageMinAmount: number;
    RandomizeBossDamageMaxAmount: number;

    RandomizeBossBehavior: boolean;

    RandomizeDungeonPalettes: boolean = true;
    SetBlackoutMode: boolean;

    RandomizeOverworldPalettes: boolean = true;

    RandomizeSpritePalettes: boolean = true;
    SetAdvancedSpritePalettes: boolean;
    PukeMode: boolean;
    NegativeMode: boolean;
    GrayscaleMode: boolean;

    GenerateSpoilers: boolean = true;
    RandomizeLinkSpritePalette: boolean;
    RandomizePots: boolean = true;
    ShuffleMusic: boolean;
    BootlegMagic: boolean = false;
    DebugMode: boolean = false;
    CustomBosses: boolean;
    AndyMode: boolean = false;
    HeartBeepSpeed: HeartBeepSpeed = HeartBeepSpeed.Half;
    AlternateGfx: boolean;
    ShieldGraphics: string;
    SwordGraphics: string;
    BeeMizer: boolean;
    BeesLevel: BeeLevel;
    DebugForceEnemy: boolean;
    DebugForceEnemyId: number;
    DebugForceBoss: boolean;
    DebugForceBossId: BossType;
    DebugOpenShutterDoors: boolean;
    DebugForceEnemyDamageZero: boolean;
    DebugShowRoomIdInRupeeCounter: boolean;
    OHKO: boolean;
    RandomizeTileTrapPattern: boolean;
    RandomizeTileTrapFloorTile: boolean;
    AllowKillableThief: boolean;
}

export interface AbsorbableTypesDictionary
{
    Heart: boolean;
    GreenRupee: boolean;
    BlueRupee: boolean;
    RedRupee: boolean;
    Bomb_1: boolean;
    Bomb_4: boolean;
    Bomb_8: boolean;
    SmallMagic: boolean;
    FullMagic: boolean;
    Arrow_5: boolean;
    Arrow_10: boolean;
    Fairy: boolean;
    Key: boolean;
    BigKey: boolean;
}

export enum RandomizeEnemiesType
{
    Basic,
    Normal,
    Hard,
    Chaos,
    Insanity
}

export enum RandomizeEnemyHPType
{
    Easy,
    Medium,
    Hard,
    Patty
}

export enum RandomizeBossesType
{
    Basic,
    Normal,
    Chaos
}

export enum SwordTypes
{
    //[Description("Normal Sword")]
    Normal,
}

export enum ShieldTypes
{
    //[Description("Normal Shield")]
    Normal,
    //[Description("Skull Shield")]
    //SkullShield,
    //[Description("Square Shield")]
    //SquareShield
}

export enum AbsorbableTypes
{
    //[Description("Heart")]
    Heart,
    //[Description("Green Rupee")]
    GreenRupee,
    //[Description("Blue Rupee")]
    BlueRupee,
    //[Description("Red Rupee")]
    RedRupee,
    //[Description("Bomb (1)")]
    Bomb_1,
    //[Description("Bomb (4)")]
    Bomb_4,
    //[Description("Bomb (8)")]
    Bomb_8,
    //[Description("Small Magic")]
    SmallMagic,
    //[Description("Full Magic")]
    FullMagic,
    //[Description("Arrow (5)")]
    Arrow_5,
    //[Description("Arrow (10)")]
    Arrow_10,
    //[Description("Fairy")]
    Fairy,
    //[Description("Key")]
    Key,
    //[Description("Big Key")]
    BigKey
}

export enum HeartBeepSpeed
{
    Default,
    Half,
    Quarter,
    Off
}

export enum BeeLevel
{
    //[Description("Bees??")]
    Level1,
    //[Description("Bees!")]
    Level2,
    //[Description("Beeeeees!?")]
    Level3,
    //[Description("Beeeeeeeeeeeeeeeeeeeees")]
    Level4
}

export enum BossType
{
    Kholdstare = 0,
    Moldorm = 1,
    Mothula = 2,
    Vitreous = 3,
    Helmasaur = 4,
    Armos = 5,
    Lanmola = 6,
    Blind = 7,
    Arrghus = 8,
    Trinexx = 9,
    NoBoss = 255
}
