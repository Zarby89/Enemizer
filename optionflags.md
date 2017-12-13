# Enemizer Options File Format

```
{
	"RandomizeEnemies": true,
	"RandomizeEnemiesType": 3,
	"RandomizeBushEnemyChance": true,
	"RandomizeEnemyHealthRange": true,
	"RandomizeEnemyHealthType": 1,
	"RandomizeEnemyDamage": true,
	"AllowEnemyZeroDamage": true,
	"ShuffleEnemyDamageGroups": false,
	"EnemyDamageChaosMode": false,
	"EasyModeEscape": false,
	"EnemiesAbsorbable": false,
	"AbsorbableSpawnRate": 10,
	"AbsorbableTypes": {
		"FullMagic": true,
		"SmallMagic": true,
		"Bomb_1": true,
		"BlueRupee": true,
		"Heart": true,
		"BigKey": true,
		"Key": true,
		"Fairy": true,
		"Arrow_10": true,
		"Arrow_5": true,
		"Bomb_8": true,
		"Bomb_4": true,
		"GreenRupee": true,
		"RedRupee": true
	},
	"BossMadness": false,
	"RandomizeBosses": true,
	"RandomizeBossesType": 0,
	"RandomizeBossHealth": false,
	"RandomizeBossHealthMinAmount": 0,
	"RandomizeBossHealthMaxAmount": 300,
	"RandomizeBossDamage": false,
	"RandomizeBossDamageMinAmount": 0,
	"RandomizeBossDamageMaxAmount": 200,
	"RandomizeBossBehavior": false,
	"RandomizeDungeonPalettes": true,
	"SetBlackoutMode": false,
	"RandomizeOverworldPalettes": true,
	"RandomizeSpritePalettes": true,
	"SetAdvancedSpritePalettes": false,
	"PukeMode": false,
	"NegativeMode": false,
	"GrayscaleMode": false,
	"GenerateSpoilers": true,
	"RandomizeLinkSpritePalette": false,
	"RandomizePots": true,
	"ShuffleMusic": false,
	"BootlegMagic": true,
	"DebugMode": false,
	"CustomBosses": false,
	"AndyMode": false,
	"HeartBeepSpeed": 2,
	"AlternateGfx": false,
	"ShieldGraphics": "shield_gfx\\normal.gfx",
	"SwordGraphics": "sword_gfx\\normal.gfx",
	"BeeMizer": false,
	"BeesLevel": 3,
	"DebugForceEnemy": true,
	"DebugForceEnemyId": 196,
	"DebugForceBoss": false,
	"DebugForceBossId": 4,
	"DebugOpenShutterDoors": false,
	"DebugForceEnemyDamageZero": true,
	"DebugShowRoomIdInRupeeCounter": true,
	"OHKO": false,
	"RandomizeTileTrapPattern": true,
	"RandomizeTileTrapFloorTile": false,
	"AllowKillableThief": true,
	"RandomizeSpriteOnHit": true
}
```

### RandomizeEnemies (boolean)
Enable enemy randomization.

### RandomizeEnemiesType (enum/number)
Enemy randomization mode. Must be set to 3 (Chaos) because nothing else is implemented.
Possible values: 
* Basic (0)
* Normal (1)
* Hard (2)
* Chaos (3)
* Insanity (4)

### RandomizeBushEnemyChance (boolean)
Randomize bush/grass enemy to be one of the possible enemies for a given area. If disabled this will default to guards, but the graphics may be wrong.

### RandomizeEnemyHealthRange (boolean)
Enable randomization of enemy health range.

### RandomizeEnemyHealthType (enum/number)
Type of enemy health randomization.
Possible values:
* Easy (1-4 hp)
* Medium (2-15 hp)
* Hard (2-30 hp)
* Patty (4-50 hp)

### RandomizeEnemyDamage (boolean)
Enable randomization of enemy damage. Groups stay the same.

### AllowEnemyZeroDamage (boolean)
Allow enemies to do zero damage (this can cause a softlock with fake flippers).

### ShuffleEnemyDamageGroups (boolean)
Shuffles the enemy damage groups. Groups cannot be shuffled without also being randomized.

### EnemyDamageChaosMode (boolean)
Scrambles damage groups and mail upgrade damage reduction. Green mail could be stronger than red mail against certain enemies with this mode enabled. (FYI this is rather broken because it hasn't been tuned and usually makes most mobs OHKO)

### EasyModeEscape (boolean)
Currently not implemented.

### EnemiesAbsorbable (boolean)
Allow absorbables (from selected types) to spawn in place of enemies.

### AbsorbableSpawnRate (number)
The spawn rate for absorbables instead of enemies (%).

### AbsorbableTypes (collection)
Possible absorbable types. Self-explanitory.
```
{
    "FullMagic": true,
    "SmallMagic": true,
    "Bomb_1": true,
    "BlueRupee": true,
    "Heart": true,
    "BigKey": true,
    "Key": true,
    "Fairy": true,
    "Arrow_10": true,
    "Arrow_5": true,
    "Bomb_8": true,
    "Bomb_4": true,
    "GreenRupee": true,
    "RedRupee": true
}
```

### BossMadness (boolean)
Unused, depreciated.

### RandomizeBosses (boolean)
Enable boss randomization. Pool of possible bosses is created based on the RandomizeBossesType set.

### RandomizeBossesType (enum/number)
Possible values:
* Basic (0) - Vanilla bosses
* Normal (1) - GT bosses are randomized in pool
* Chaos (2) - Boss pool is completely random

### RandomizeBossHealth (boolean)
Currently not implemented.

### RandomizeBossHealthMinAmount (number)
currently not implemented.

### RandomizeBossHealthMaxAmount (number)
Currently not implemented.

### RandomizeBossDamage (boolean)
Currently not implemented.

### RandomizeBossDamageMinAmount (number)
Currently not implemented.

### RandomizeBossDamageMaxAmount (number)
Currently not implemented.

### RandomizeBossBehavior (boolean)
Currently not implemented.

### RandomizeDungeonPalettes (boolean)
Randomizes palettes used in dungeons.

### SetBlackoutMode (boolean)
Sets palettes to completely black in dungeons. Torches, chests, and pots are not blacked out.

### RandomizeOverworldPalettes (boolean)
Randomizes palettes used in the overworld.

### RandomizeSpritePalettes (boolean)
Randomizes palettes used by sprites (shuffles existing palettes).

### SetAdvancedSpritePalettes (boolean)
Randomizes palettes used by sprites (randomizes the colors in the palettes).

### PukeMode (boolean)
Random palettes... the name says it all.

### NegativeMode (boolean)
Inverted palette mode.

### GrayscaleMode (boolean)
Grayscale palette mode.

### GenerateSpoilers (boolean)
Generate spoilers. Should always be turned on for web.

### RandomizeLinkSpritePalette (boolean)
Randomizes Links sprite palette.

### RandomizePots (boolean)
Shuffles the contents of pots in a given super room.

### ShuffleMusic (boolean)
Shuffles music (can cause hard locks).

### BootlegMagic (boolean)
Bootleg Chinese inspired magic (currently just gives Moldorm random number of eyes). This should always be on...just because.

### DebugMode (boolean)
Enables debug options.

### CustomBosses (boolean)
Currently not implemented

### AndyMode (boolean)
Adds Andy inspired soundfx for chest opening. Forces pug sprite into sprite pool if RandomizeSpriteOnHit is enabled. Requires pug sprite to be in `sprites` directory.

### HeartBeepSpeed (enum/number)
Heart beep speed. This will override randomizer setting, so make sure they are the same.
Possible values:
* Default,
* Half,
* Quarter,
* Off

### AlternateGfx (boolean)
Swaps boss graphics with alternatives (currently only one set is implemented).

### ShieldGraphics (string)
Relative string path to the shield graphics file.
Should be set to `"shield_gfx\\normal.gfx"` for race roms.

### SwordGraphics (string)
Relative string path to the sword graphics file. 
Should be set to `"sword_gfx\\normal.gfx"` for race roms.

### BeeMizer (boolean)
Enable Beeeeeeeeeeeees!

### BeesLevel (enum/number)
Possible values:
* Bees?? (0) - 
* Bees! (1) - 
* Beeeeees!? (2) - 
* Beeeeeeeeeeeeeeeeeeeees (3) - replaces every item not needed to beat the game with bee trap

### DebugForceEnemy (boolean)
Enable forced enemy debug flag (requires DebugMode to be set).

### DebugForceEnemyId (number)
SpriteId. See enemizer source code, or z3 compendium for list of possible sprites. Do not allow overlord sprites to be selected.

### DebugForceBoss (boolean)
Enable forced boss debug flag (requires DebugMode to be set).

### DebugForceBossId (enum/number)
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

### DebugOpenShutterDoors (boolean)
Causes all shutter doors to be open (requires DebugMode to be set).

### DebugForceEnemyDamageZero (boolean)
Set all enemy damage to zero (requires DebugMode to be set).

### DebugShowRoomIdInRupeeCounter (boolean)
Shows the current roomId in the rupee counter (requires DebugMode to be set).

### OHKO (boolean)
Enables OHKO mode with no timer. This was added because OHKO wasn't available in ER for some new mode, but this seems to have been added, so this is probably no longer needed.

### RandomizeTileTrapPattern (boolean)
Enables random tile trap pattern. Pattens are stored in the `tiles` directory.

### RandomizeTileTrapFloorTile (boolean)
Enables "random" tile trap floor replacement tile. Currently this picks from a pool of 1 value (spike floor).

### AllowKillableThief (boolean)
Lets you kill those bastards.

### RandomizeSpriteOnHit (boolean)
Sets sprite randomization on hit mode on. This requires at least 32 sprite files in the `sprites` directory.
