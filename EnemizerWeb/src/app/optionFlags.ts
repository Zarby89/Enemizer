
export class OptionFlags
{
    RandomizeEnemies: boolean = true;
    RandomizeEnemiesType: RandomizeEnemiesType = RandomizeEnemiesType.Chaos;
    RandomizeBushEnemyChance: boolean = true;

    RandomizeEnemyHealthRange: boolean = false;
    RandomizeEnemyHealthType: RandomizeEnemyHPType = RandomizeEnemyHPType.Hard;

    RandomizeEnemyDamage: boolean = false;
    AllowEnemyZeroDamage: boolean = false;
    ShuffleEnemyDamageGroups: boolean = false;
    EnemyDamageChaosMode: boolean = false;

    EasyModeEscape: boolean = false;

    EnemiesAbsorbable: boolean = false;
    AbsorbableSpawnRate: number = 0;
    AbsorbableTypes: AbsorbableTypesDictionary;

    BossMadness: boolean = false; // unused

    RandomizeBosses: boolean = false;
    RandomizeBossesType: RandomizeBossesType = RandomizeBossesType.Chaos;

    RandomizeBossHealth: boolean = false;
    RandomizeBossHealthMinAmount: number = 0;
    RandomizeBossHealthMaxAmount: number = 0;

    RandomizeBossDamage: boolean = false;
    RandomizeBossDamageMinAmount: number = 0;
    RandomizeBossDamageMaxAmount: number = 0;

    RandomizeBossBehavior: boolean = false;

    RandomizeDungeonPalettes: boolean = true;
    SetBlackoutMode: boolean = false;

    RandomizeOverworldPalettes: boolean = true;

    RandomizeSpritePalettes: boolean = true;
    SetAdvancedSpritePalettes: boolean = false;
    PukeMode: boolean = false;
    NegativeMode: boolean = false;
    GrayscaleMode: boolean = false;

    GenerateSpoilers: boolean = true;
    RandomizeLinkSpritePalette: boolean = false;
    RandomizePots: boolean = true;
    ShuffleMusic: boolean = false;
    BootlegMagic: boolean = false;
    DebugMode: boolean = false;
    CustomBosses: boolean = false;
    AndyMode: boolean = false;
    HeartBeepSpeed: HeartBeepSpeed = HeartBeepSpeed.Half;
    AlternateGfx: boolean = false;
    ShieldGraphics: string;
    SwordGraphics: string;
    BeeMizer: boolean = false;
    BeesLevel: BeeLevel = BeeLevel.Level1;
    DebugForceEnemy: boolean = false;
    DebugForceEnemyId: number = 0;
    DebugForceBoss: boolean = false;
    DebugForceBossId: BossType = BossType.Kholdstare;
    DebugOpenShutterDoors: boolean = false;
    DebugForceEnemyDamageZero: boolean = false;
    DebugShowRoomIdInRupeeCounter: boolean = false;
    OHKO: boolean = false;
    RandomizeTileTrapPattern: boolean = false;
    RandomizeTileTrapFloorTile: boolean = false;
    AllowKillableThief: boolean = false;
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

export class RandomizerOptions
{
    logic: string = "NoMajorGlitches";
    difficulty: string = "normal";
    variation: string = "none";
    mode: string = "open";
    goal: string = "ganon";
    heart_speed: string = "quarter";
    sram_trace: boolean = false;
    menu_fast: boolean = false;
    debug: boolean = false;
    tournament: boolean = false;
    shuffle: string = "full";
}
