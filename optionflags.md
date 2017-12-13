# Enemizer Options File Format

<big><pre>
{
&emsp;"[RandomizeEnemies](#RandomizeEnemies)": true,
&emsp;"[RandomizeEnemiesType](#RandomizeEnemiesType)": 3,
&emsp;"[RandomizeBushEnemyChance](#RandomizeBushEnemyChance)": true,
&emsp;"[RandomizeEnemyHealthRange](#RandomizeEnemyHealthRange)": true,
&emsp;"[RandomizeEnemyHealthType](#RandomizeEnemyHealthType)": 1,
&emsp;"[OHKO](#OHKO)": false,
&emsp;"[RandomizeEnemyDamage](#RandomizeEnemyDamage)": true,
&emsp;"[AllowEnemyZeroDamage](#AllowEnemyZeroDamage)": true,
&emsp;"[ShuffleEnemyDamageGroups](#ShuffleEnemyDamageGroups)": false,
&emsp;"[EnemyDamageChaosMode](#EnemyDamageChaosMode)": false,
&emsp;"[EasyModeEscape](#EasyModeEscape)": false,
&emsp;"[EnemiesAbsorbable](#EnemiesAbsorbable)": false,
&emsp;"[AbsorbableSpawnRate](#AbsorbableSpawnRate)": 10,
&emsp;"[AbsorbableTypes](#AbsorbableTypes)": {
&emsp;&emsp;&emsp;"FullMagic": true,
&emsp;&emsp;&emsp;"SmallMagic": true,
&emsp;&emsp;&emsp;"Bomb_1": true,
&emsp;&emsp;&emsp;"BlueRupee": true,
&emsp;&emsp;&emsp;"Heart": true,
&emsp;&emsp;&emsp;"BigKey": true,
&emsp;&emsp;&emsp;"Key": true,
&emsp;&emsp;&emsp;"Fairy": true,
&emsp;&emsp;&emsp;"Arrow_10": true,
&emsp;&emsp;&emsp;"Arrow_5": true,
&emsp;&emsp;&emsp;"Bomb_8": true,
&emsp;&emsp;&emsp;"Bomb_4": true,
&emsp;&emsp;&emsp;"GreenRupee": true,
&emsp;&emsp;&emsp;"RedRupee": true
&emsp;},
&emsp;"[BossMadness](#BossMadness)": false,
&emsp;"[RandomizeBosses](#RandomizeBosses)": true,
&emsp;"[RandomizeBossesType](#RandomizeBossesType)": 0,
&emsp;"[RandomizeBossHealth](#RandomizeBossHealth)": false,
&emsp;"[RandomizeBossHealthMinAmount](#RandomizeBossHealthMinAmount)": 0,
&emsp;"[RandomizeBossHealthMaxAmount](#RandomizeBossHealthMaxAmount)": 300,
&emsp;"[RandomizeBossDamage](#RandomizeBossDamage)": false,
&emsp;"[RandomizeBossDamageMinAmount](#RandomizeBossDamageMinAmount)": 0,
&emsp;"[RandomizeBossDamageMaxAmount](#RandomizeBossDamageMaxAmount)": 200,
&emsp;"[RandomizeBossBehavior](#RandomizeBossBehavior)": false,
&emsp;"[RandomizeDungeonPalettes](#RandomizeDungeonPalettes)": true,
&emsp;"[SetBlackoutMode](#SetBlackoutMode)": false,
&emsp;"[RandomizeOverworldPalettes](#RandomizeOverworldPalettes)": true,
&emsp;"[RandomizeSpritePalettes](#RandomizeSpritePalettes)": true,
&emsp;"[SetAdvancedSpritePalettes](#SetAdvancedSpritePalettes)": false,
&emsp;"[PukeMode](#PukeMode)": false,
&emsp;"[NegativeMode](#NegativeMode)": false,
&emsp;"[GrayscaleMode](#GrayscaleMode)": false,
&emsp;"[GenerateSpoilers](#GenerateSpoilers)": true,
&emsp;"[RandomizeLinkSpritePalette](#RandomizeLinkSpritePalette)": false,
&emsp;"[RandomizePots](#RandomizePots)": true,
&emsp;"[ShuffleMusic](#ShuffleMusic)": false,
&emsp;"[BootlegMagic](#BootlegMagic)": true,
&emsp;"[CustomBosses](#CustomBosses)": false,
&emsp;"[AndyMode](#AndyMode)": false,
&emsp;"[HeartBeepSpeed](#HeartBeepSpeed)": 2,
&emsp;"[AlternateGfx](#AlternateGfx)": false,
&emsp;"[ShieldGraphics](#ShieldGraphics)": "shield_gfx\\normal.gfx",
&emsp;"[SwordGraphics](#SwordGraphics)": "sword_gfx\\normal.gfx",
&emsp;"[BeeMizer](#BeeMizer)": false,
&emsp;"[BeesLevel](#BeesLevel)": 3,
&emsp;"[RandomizeTileTrapPattern](#RandomizeTileTrapPattern)": true,
&emsp;"[RandomizeTileTrapFloorTile](#RandomizeTileTrapFloorTile)": false,
&emsp;"[AllowKillableThief](#AllowKillableThief)": true,
&emsp;"[RandomizeSpriteOnHit](#RandomizeSpriteOnHit)": true,
&emsp;"[DebugMode](#DebugMode)": false,
&emsp;"[DebugForceEnemy](#DebugForceEnemy)": true,
&emsp;"[DebugForceEnemyId](#DebugForceEnemyId)": 196,
&emsp;"[DebugForceBoss](#DebugForceBoss)": false,
&emsp;"[DebugForceBossId](#DebugForceBossId)": 4,
&emsp;"[DebugOpenShutterDoors](#DebugOpenShutterDoors)": false,
&emsp;"[DebugForceEnemyDamageZero](#DebugForceEnemyDamageZero)": true,
&emsp;"[DebugShowRoomIdInRupeeCounter](#DebugShowRoomIdInRupeeCounter)": true
}
</pre></big>

### <a name="RandomizeEnemies"></a>RandomizeEnemies (boolean)
Enable enemy randomization.

### <a name="RandomizeEnemiesType"></a>RandomizeEnemiesType (enum/number)
Requires: [_RandomizeEnemies_](#RandomizeEnemies)<br/>
Enemy randomization mode. Must be set to 3 (Chaos) because nothing else is implemented.
Possible values: 
* Basic (0)
* Normal (1)
* Hard (2)
* Chaos (3)
* Insanity (4)

### <a name="RandomizeBushEnemyChance"></a>RandomizeBushEnemyChance (boolean)
Requires: [_RandomizeEnemies_](#RandomizeEnemies)<br/>
Randomize bush/grass enemy to be one of the possible enemies for a given area. If disabled this will default to guards, but the graphics may be wrong.

### <a name="RandomizeEnemyHealthRange"></a>RandomizeEnemyHealthRange (boolean)
Enable randomization of enemy health range.

### <a name="RandomizeEnemyHealthType"></a>RandomizeEnemyHealthType (enum/number)
Requires: [_RandomizeEnemyHealthRange_](#RandomizeEnemyHealthRange)<br/>
Type of enemy health randomization.
Possible values:
* Easy (1-4 hp)
* Medium (2-15 hp)
* Hard (2-30 hp)
* Patty (4-50 hp)

### <a name="OHKO"></a>OHKO (boolean)
Enables OHKO mode with no timer. This was added because OHKO wasn't available in ER for some new mode, but this seems to have been added, so this is probably no longer needed. Note that this should disable [_RandomizeEnemyDamage_](#RandomizeEnemyDamage) and its children.

### <a name="RandomizeEnemyDamage"></a>RandomizeEnemyDamage (boolean)
Enable randomization of enemy damage. Groups stay the same.

### <a name="AllowEnemyZeroDamage"></a>AllowEnemyZeroDamage (boolean)
Requires: [_RandomizeEnemyDamage_](#RandomizeEnemyDamage)<br/>
Allow enemies to do zero damage (this can cause a softlock with fake flippers).

### <a name="ShuffleEnemyDamageGroups"></a>ShuffleEnemyDamageGroups (boolean)
Requires: [_RandomizeEnemyDamage_](#RandomizeEnemyDamage)<br/>
Shuffles the enemy damage groups. Groups cannot be shuffled without also being randomized.

### <a name="EnemyDamageChaosMode"></a>EnemyDamageChaosMode (boolean)
Requires: [_ShuffleEnemyDamageGroups_](#ShuffleEnemyDamageGroups)<br/>
Scrambles damage groups and mail upgrade damage reduction. Green mail could be stronger than red mail against certain enemies with this mode enabled. (FYI this is rather broken because it hasn't been tuned and usually makes most mobs OHKO)

### <a name="EasyModeEscape"></a>EasyModeEscape (boolean)
Currently not implemented.

### <a name="EnemiesAbsorbable"></a>EnemiesAbsorbable (boolean)
Allow absorbables (from selected types) to spawn in place of enemies.

### <a name="AbsorbableSpawnRate"></a>AbsorbableSpawnRate (number)
Requires: [_EnemiesAbsorbable_](#EnemiesAbsorbable)<br/>
The spawn rate for absorbables instead of enemies (%).

### <a name="AbsorbableTypes"></a>AbsorbableTypes (collection)
Requires: [_EnemiesAbsorbable_](#EnemiesAbsorbable)<br/>
Possible absorbable types. Self-explanitory.
```
{
    "Heart": true,
    "GreenRupee": true,
    "BlueRupee": true,
    "RedRupee": true,
    "Bomb_1": true,
    "Bomb_4": true,
    "Bomb_8": true,
    "SmallMagic": true,
    "FullMagic": true,
    "Arrow_5": true,
    "Arrow_10": true,
    "Fairy": true,
    "Key": true,
    "BigKey": true
}
```

### <a name="BossMadness"></a>BossMadness (boolean)
Unused, depreciated.

### <a name="RandomizeBosses"></a>RandomizeBosses (boolean)
Enable boss randomization. Pool of possible bosses is created based on the RandomizeBossesType set.

### <a name="RandomizeBossesType"></a>RandomizeBossesType (enum/number)
Requires: [_RandomizeBosses_](#RandomizeBosses)<br/>
Possible values:
* Basic (0) - Vanilla bosses
* Normal (1) - GT bosses are randomized in pool
* Chaos (2) - Boss pool is completely random

### <a name="RandomizeBossHealth"></a>RandomizeBossHealth (boolean)
Currently not implemented.

### <a name="RandomizeBossHealthMinAmount"></a>RandomizeBossHealthMinAmount (number)
currently not implemented.

### <a name="RandomizeBossHealthMaxAmount"></a>RandomizeBossHealthMaxAmount (number)
Currently not implemented.

### <a name="RandomizeBossDamage"></a>RandomizeBossDamage (boolean)
Currently not implemented.

### <a name="RandomizeBossDamageMinAmount"></a>RandomizeBossDamageMinAmount (number)
Currently not implemented.

### <a name="RandomizeBossDamageMaxAmount"></a>RandomizeBossDamageMaxAmount (number)
Currently not implemented.

### <a name="RandomizeBossBehavior"></a>RandomizeBossBehavior (boolean)
Currently not implemented.

### <a name="RandomizeDungeonPalettes"></a>RandomizeDungeonPalettes (boolean)
Randomizes palettes used in dungeons.

### <a name="SetBlackoutMode"></a>SetBlackoutMode (boolean)
Requires: [_RandomizeDungeonPalettes_](#RandomizeDungeonPalettes)<br/>
Sets palettes to completely black in dungeons. Torches, chests, and pots are not blacked out.

### <a name="RandomizeOverworldPalettes"></a>RandomizeOverworldPalettes (boolean)
Randomizes palettes used in the overworld.

### <a name="RandomizeSpritePalettes"></a>RandomizeSpritePalettes (boolean)
Randomizes palettes used by sprites (shuffles existing palettes).

### <a name="SetAdvancedSpritePalettes"></a>SetAdvancedSpritePalettes (boolean)
Requires: [_RandomizeSpritePalettes_](#RandomizeSpritePalettes)<br/>
Randomizes palettes used by sprites (randomizes the colors in the palettes).

### <a name="PukeMode"></a>PukeMode (boolean)
Random palettes... the name says it all.

### <a name="NegativeMode"></a>NegativeMode (boolean)
Inverted palette mode.

### <a name="GrayscaleMode"></a>GrayscaleMode (boolean)
Grayscale palette mode.

### <a name="GenerateSpoilers"></a>GenerateSpoilers (boolean)
Generate spoilers. Should always be turned on for web.

### <a name="RandomizeLinkSpritePalette"></a>RandomizeLinkSpritePalette (boolean)
Randomizes Links sprite palette.

### <a name="RandomizePots"></a>RandomizePots (boolean)
Shuffles the contents of pots in a given super room.

### <a name="ShuffleMusic"></a>ShuffleMusic (boolean)
Shuffles music (can cause hard locks).

### <a name="BootlegMagic"></a>BootlegMagic (boolean)
Bootleg Chinese inspired magic (currently just gives Moldorm random number of eyes). This should always be on...just because.

### <a name="CustomBosses"></a>CustomBosses (boolean)
Currently not implemented

### <a name="AndyMode"></a>AndyMode (boolean)
Adds Andy inspired soundfx for chest opening. Forces pug sprite into sprite pool if RandomizeSpriteOnHit is enabled. Requires pug sprite to be in `sprites` directory.

### <a name="HeartBeepSpeed"></a>HeartBeepSpeed (enum/number)
Heart beep speed. This will override randomizer setting, so make sure they are the same.
Possible values:
* Default (0)
* Half (1)
* Quarter (2)
* Off (3)

### <a name="AlternateGfx"></a>AlternateGfx (boolean)
Swaps boss graphics with alternatives (currently only one set is implemented).

### <a name="ShieldGraphics"></a>ShieldGraphics (string)
Relative string path to the shield graphics file.
Should be set to `"shield_gfx\\normal.gfx"` for race roms.

### <a name="SwordGraphics"></a>SwordGraphics (string)
Relative string path to the sword graphics file. 
Should be set to `"sword_gfx\\normal.gfx"` for race roms.

### <a name="BeeMizer"></a>BeeMizer (boolean)
Enable Beeeeeeeeeeeees!

### <a name="BeesLevel"></a>BeesLevel (enum/number)
Requires: [_BeesLevel_](#BeesLevel)<br/>
Possible values:
* Bees?? (0) - Replaces Arrows, Bombs, Rupees, Compass, Maps, Arrow Upgrade, and Bomb Upgrades with bee traps
* Bees! (1) - Same as 0, but also introduces single bees and increases chance of trap.
* Beeeeees!? (2) - Randomly replaces items not needed to beat the game with bee trap (including: bug net, boomerangs, heart container and heart pieces, magic, shield and armor upgrades).
* Beeeeeeeeeeeeeeeeeeeees (3) - Always replaces items not needed to beat the game with bee trap (including: bug net, boomerangs, heart container and heart pieces, magic, shield and armor upgrades).

### <a name="RandomizeTileTrapPattern"></a>RandomizeTileTrapPattern (boolean)
Enables random tile trap pattern. Pattens are stored in the `tiles` directory.

### <a name="RandomizeTileTrapFloorTile"></a>RandomizeTileTrapFloorTile (boolean)
Enables "random" tile trap floor replacement tile. Currently this picks from a pool of 1 value (spike floor).

### <a name="AllowKillableThief"></a>AllowKillableThief (boolean)
Lets you kill those bastards.

### <a name="RandomizeSpriteOnHit"></a>RandomizeSpriteOnHit (boolean)
Sets sprite randomization on hit mode on. This requires at least 32 sprite files in the `sprites` directory.

### <a name="DebugMode"></a>DebugMode (boolean)
Enables debug options.

### <a name="DebugForceEnemy"></a>DebugForceEnemy (boolean)
Requires: [_DebugMode_](#DebugMode)<br/>
Enable forced enemy debug flag (requires DebugMode to be set).

### <a name="DebugForceEnemyId"></a>DebugForceEnemyId (number)
Requires: [_DebugMode_](#DebugMode)<br/>
SpriteId. See enemizer source code, or z3 compendium for list of possible sprites. Do not allow overlord sprites to be selected.

### <a name="DebugForceBoss"></a>DebugForceBoss (boolean)
Requires: [_DebugMode_](#DebugMode)<br/>
Enable forced boss debug flag (requires DebugMode to be set).

### <a name="DebugForceBossId"></a>DebugForceBossId (enum/number)
Requires: [_DebugMode_](#DebugMode)<br/>
Possible values:
* Kholdstare (0)
* Moldorm (1)
* Mothula (2)
* Vitreous (3)
* Helmasaur (4)
* Armos (5)
* Lanmola (6)
* Blind (7)
* Arrghus (8)
* Trinexx (9)

### <a name="DebugOpenShutterDoors"></a>DebugOpenShutterDoors (boolean)
Requires: [_DebugMode_](#DebugMode)<br/>
Causes all shutter doors to be open (requires DebugMode to be set).

### <a name="DebugForceEnemyDamageZero"></a>DebugForceEnemyDamageZero (boolean)
Requires: [_DebugMode_](#DebugMode)<br/>
Set all enemy damage to zero (requires DebugMode to be set).

### <a name="DebugShowRoomIdInRupeeCounter"></a>DebugShowRoomIdInRupeeCounter (boolean)
Requires: [_DebugMode_](#DebugMode)<br/>
Shows the current roomId in the rupee counter (requires DebugMode to be set).
