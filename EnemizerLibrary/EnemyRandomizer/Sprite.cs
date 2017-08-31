using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class DungeonSprite
    {
        public byte byte0 { get; set; }
        public byte byte1 { get; set; }
        public byte SpriteId { get; set; }
        public bool IsOverlord { get; set; }
        public bool HasAKey { get; set; }

        public DungeonSprite(RomData romData, int address)
        {
            byte0 = romData[address];
            byte1 = romData[address + 1];
            SpriteId = romData[address + 2];

            IsOverlord = (byte1 & SpriteConstants.StatisMask) != 0;

            if (romData[address + 3] != 0xFF && romData[address + 5] == SpriteConstants.KeySprite)
            {
                HasAKey = true;
            }
        }
    }

    public class PossibleSpriteGroupCollection
    {

    }
    /*
            subset_gfx_sprites[22] = new byte[] { 0x22, 0x11 };//DW Popo, Hinox, Snapdragon (require 23)
            subset_gfx_sprites[31] = new byte[] { 0x23, 0x24, 0x85, 0xA7, 0x02, 0x7E, 0x7F, 0x80 };//bari,stalfos,firebars
            subset_gfx_sprites[47] = new byte[] { 0x71 };//delalant,leever //0x64,0x63
            subset_gfx_sprites[14] = new byte[] { 0x19 };//ghini,thief
            subset_gfx_sprites[70] = new byte[] { 0x6A, 0x6B, 0x49, 0x43, 0x41, 0x42, 0x45, 0x48, 0x44, 0x4A, 0x4B };//need to be combined with 73 and 19 all guards
            subset_gfx_sprites[72] = new byte[] { 0x41, 0x42, 0x43, 0x45, 0x46, 0x47, 0x4B, 0x49 };//need to be combined with 73 and 19 all guards archers
            //subset1
            subset_gfx_sprites[44] = new byte[] { 0x4F, 0x4E, 0x61 }; //popo, beamos
            //13 contains guards same as 73
            subset_gfx_sprites[13] = new byte[] { }; //guards
            subset_gfx_sprites[73] = new byte[] {0x42,0x41,0x45 }; //guards
            subset_gfx_sprites[19] = new byte[] { }; //guards
            subset_gfx_sprites[30] = new byte[] { 0x26, 0x13, 0x18 }; //minihelma,minimoldorm,beetle
            subset_gfx_sprites[32] = new byte[] { 0x9C, 0x9D, 0x91, 0x8F }; //stalfos knight, shadow, blob
            //subset2
            subset_gfx_sprites[12] = new byte[] { 0x08, 0x58, 0x0F }; //octorock,crab,octobaloon
            subset_gfx_sprites[18] = new byte[] { 0x01, 0x4C }; //vulture, jazzhand, (Also contain tablets / rock in front of desert)
            subset_gfx_sprites[23] = new byte[] { 0x12 }; //pigmanspear, snapdragon (require 22) 
            subset_gfx_sprites[24] = new byte[] { 0x08 }; //dwoctorok
            subset_gfx_sprites[28] = new byte[] { 0x6D, 0x6E, 0x6F }; //rat,rope,keese, also the oldman
            subset_gfx_sprites[29] = new byte[] { };
            subset_gfx_sprites[46] = new byte[] { 0x84, 0x83 }; //green/red eyegores
            subset_gfx_sprites[34] = new byte[] { 0x9A, 0x81 }; //water sprites
            subset_gfx_sprites[35] = new byte[] { 0x8B }; //wallmaster,gibdo
            subset_gfx_sprites[39] = new byte[] { 0xC7, 0xCA, 0x5D, 0x5E, 0x5F, 0x60 };//chain chomp,pokey,rollers
            subset_gfx_sprites[40] = new byte[] { 0xA5, 0xA6, 0xC3 };//zazak,gibo (patrick star)
            subset_gfx_sprites[38] = new byte[] { 0x99 };//(A1) iceman,penguin
            subset_gfx_sprites[37] = new byte[] { 0x9B,0x20 };//wizzrobe,sluggula
            subset_gfx_sprites[41] = new byte[] { 0x9B };//wizzrobe
            subset_gfx_sprites[36] = new byte[] { 0x6D, 0x6E, 0x6F };//rat,rope,keese
            subset_gfx_sprites[42] = new byte[] { 0x8E, 0x5B, 0x5C };//turtle,kondongo ,also the digging game guy //0x86 kodongo problem
            //74 = lumberjack
            //subset3
            subset_gfx_sprites[16] = new byte[] { 0x51, 0x27, 0xC9 };//armos,deadrock,tektite
            subset_gfx_sprites[17] = new byte[] { 0x00, 0x0D };//raven,buzzblob
            subset_gfx_sprites[27] = new byte[] { 0xA8, 0xA9, 0xAA };//dw bomber/likelike
            subset_gfx_sprites[20] = new byte[] { 0xD0 };//lynel
            subset_gfx_sprites[74] = new byte[] { };//wizzrobe
            subset_gfx_sprites[80] = new byte[] {0x0B };//wizzrobe
            subset_gfx_sprites[81] = new byte[] { };//switches
            subset_gfx_sprites[93] = new byte[] { };//sanctuary mantle
            subset_gfx_sprites[82] = new byte[] { 0x8A, 0x1C, 0x15, 0x7D, }; //0x82 };//switches
            subset_gfx_sprites[83] = new byte[] { 0x8A, 0x1C, 0x15, 0x7D, }; //0x82 };//switches
     */

    //public class Sprite
    //{
    //    public int SpriteId { get; set; }
    //    public string Name { get; set; }
    //    public byte DefaultSpriteGroup { get; set; }
    //    public List<SubGroupPosition> SubGroupsUsed { get; set; }
    //    public List<byte> DefaultSubGroups { get; set; }

    //    public Sprite(int spriteId, string name, byte defaultSpriteGroup, SubGroupPosition[] subGroupPositions, byte[] defaultSubGroups)
    //        :this(spriteId, name, defaultSpriteGroup, subGroupPositions.ToList(), defaultSubGroups.ToList())
    //    {
    //    }

    //    public Sprite(int spriteId, string name, byte defaultSpriteGroup, List<SubGroupPosition> subGroupPositions, List<byte> defaultSubGroups)
    //    {
    //        SubGroupsUsed = new List<SubGroupPosition>();
    //        DefaultSubGroups = new List<byte>();

    //        this.SpriteId = spriteId;
    //        this.Name = name;
    //        this.DefaultSpriteGroup = defaultSpriteGroup;
    //        this.SubGroupsUsed = subGroupPositions;
    //        this.DefaultSubGroups = defaultSubGroups;
    //    }
    //}

    //public enum SubGroupPosition
    //{
    //    SubGroup0,
    //    SubGroup1,
    //    SubGroup2,
    //    SubGroup3
    //}

    //public class PossibleSpriteCollection
    //{
    //    public List<Sprite> PossibleSprites { get; set; }

    //    public PossibleSpriteCollection()
    //    {
    //        PossibleSprites = new List<Sprite>();

    //        //PossibleSprites.Add(new Sprite(0x00, "Raven", 0x0, SubGroupPosition.SubGroup0, 0x0));
    //        // TODO: add them all

    //        //$01 	Vulture
    //        //$02 	Flying Stalfos Head
    //        //$03 	Empty
    //        //$04 	Pull Switch (Good)
    //        //$05 	Pull Switch (Unused)
    //        //$06 	Pull Switch (Bad)
    //        //$07 	Pull Switch (Unused)
    //        //$08 	Octorok (One Way)
    //        //$09 	Moldorm (Boss)
    //        //$0A 	Octorok (Four Way)
    //        //$0B 	Chicken
    //        //$0C 	Octorok (?)
    //        //$0D 	Buzzblob
    //        //$0E 	Snapdragon
    //        //$0F 	Octoballoon
    //        //$10 	Octoballoon Hatchlings
    //        //$11 	Hinox
    //        //$12 	Moblin
    //        //$13 	Mini Helmasaur
    //        //$14 	Gargoyle's Domain Gate
    //        //$15 	Antifairy
    //        //$16 	Sahasrahla / Aginah
    //        //$17 	Bush Hoarder
    //        //$18 	Mini Moldorm
    //        //$19 	Poe
    //        //$1A 	Dwarves
    //        //$1B 	Arrow in wall?
    //        //$1C 	Statue
    //        //$1D 	Weathervane
    //        //$1E 	Crystal Switch
    //        //$1F 	Bug-Catching Kid
    //        //$20 	Sluggula
    //        //$21 	Push Switch
    //        //$22 	Ropa
    //        //$23 	Red Bari
    //        //$24 	Blue Bari
    //        //$25 	Talking Tree
    //        //$26 	Hardhat Beetle
    //        //$27 	Deadrock
    //        //$28 	Storytellers
    //        //$29 	Blind Hideout attendant
    //        //$2A 	Sweeping Lady
    //        //$2B 	Multipurpose Sprite
    //        //$2C 	Lumberjacks
    //        //$2D 	Telepathic stones? (No idea what this actually is, likely unused)
    //        //$2E 	Flute Boy's Notes
    //        //$2F 	Race HP NPCs
    //        //$30 	Person?
    //        //$31 	Fortune Teller
    //        //$32 	Angry Brothers
    //        //$33 	Pull For Rupees Sprite
    //        //$34 	Scared Girl 2
    //        //$35 	Innkeeper
    //        //$36 	Witch
    //        //$37 	Waterfall
    //        //$38 	Arrow Target
    //        //$39 	Average Middle-Aged Man
    //        //$3A 	Half Magic Bat
    //        //$3B 	Dash Item
    //        //$3C 	Village Kid
    //        //$3D 	Signs? Chicken lady also showed up / Scared ladies outside houses.
    //        //$3E 	Rock Hoarder
    //        //$3F 	Tutorial Soldier
    //        //$40 	Lightning Lock
    //        //$41 	Blue Sword Soldier / Used by guards to detect player
    //        //$42 	Green Sword Soldier
    //        //$43 	Red Spear Soldier
    //        //$44 	Assault Sword Soldier
    //        //$45 	Green Spear Soldier
    //        //$46 	Blue Archer
    //        //$47 	Green Archer
    //        //$48 	Red Javelin Soldier
    //        //$49 	Red Javelin Soldier 2
    //        //$4A 	Red Bomb Soldiers

    //        // HM says Enemy Block 13...
    //        //77, ??
    //        //$4B 	Green Soldier Recruits (HM = Knight)
    //        // sg1=73, sg2=13
    //        PossibleSprites.Add(new Sprite(0x4B, "Green Soldier Recruit", 77, new[] { SubGroupPosition.SubGroup1, SubGroupPosition.SubGroup2 }, new byte[] { 73, 13 }));

    //        //$4C 	Geldman
    //        //$4D 	Rabbit
    //        //$4E 	Popo
    //        //$4F 	Popo 2
    //        //$50 	Cannon Balls
    //        //$51 	Armos
    //        //$52 	Giant Zora
    //        //$53 	Armos Knights (Boss)
    //        //$54 	Lanmolas (Boss)
    //        //$55 	Fireball Zora
    //        //$56 	Walking Zora
    //        //$57 	Desert Palace Barriers
    //        //$58 	Crab
    //        //$59 	Bird
    //        //$5A 	Squirrel
    //        //$5B 	Spark (Left to Right)
    //        //$5C 	Spark (Right to Left)
    //        //$5D 	Roller (vertical moving)
    //        //$5E 	Roller (vertical moving)
    //        //$5F 	Roller
    //        //$60 	Roller (horizontal moving)
    //        //$61 	Beamos
    //        //$62 	Master Sword
    //        //$63 	Devalant (Non-shooter)
    //        //$64 	Devalant (Shooter)
    //        //$65 	Shooting Gallery Proprietor
    //        //$66 	Moving Cannon Ball Shooters (Right)
    //        //$67 	Moving Cannon Ball Shooters (Left)
    //        //$68 	Moving Cannon Ball Shooters (Down)
    //        //$69 	Moving Cannon Ball Shooters (Up)
    //        //$6A 	Ball N' Chain Trooper
    //        //$6B 	Cannon Soldier
    //        //$6C 	Mirror Portal
    //        //$6D 	Rat
    //        //$6E 	Rope
    //        //$6F 	Keese
    //        //$70 	Helmasaur King Fireball
    //        //$71 	Leever
    //        //$72 	Activator for the ponds (where you throw in items)

    //        // HM says Enemy Block 13...
    //        //43, 77
    //        PossibleSprites.Add(new Sprite(0x73, "Uncle / Priest", 77, new[] { SubGroupPosition.SubGroup0 }, new byte[] { 81 }));

    //        //$74 	Running Man
    //        //$75 	Bottle Salesman
    //        //$76 	Princess Zelda
    //        //$77 	Antifairy (Alternate)
    //        //$78 	Village Elder
    //        //$79 	Bee
    //        //$7A 	Agahnim
    //        //$7B 	Agahnim Energy Ball
    //        //$7C 	Hyu
    //        //$7D 	Big Spike Trap
    //        //$7E 	Guruguru Bar (Clockwise)
    //        //$7F 	Guruguru Bar (Counter Clockwise)
    //        //$80 	Winder
    //        //$81 	Water Tektite
    //        //$82 	Antifairy Circle
    //        //$83 	Green Eyegore
    //        //$84 	Red Eyegore
    //        //$85 	Yellow Stalfos
    //        //$86 	Kodongos
    //        //$87 	Flames
    //        //$88 	Mothula (Boss)
    //        //$89 	Mothula's Beam
    //        //$8A 	Spike Trap
    //        //$8B 	Gibdo
    //        //$8C 	Arrghus (Boss)
    //        //$8D 	Arrghus spawn
    //        //$8E 	Terrorpin
    //        //$8F 	Slime
    //        //$90 	Wallmaster
    //        //$91 	Stalfos Knight
    //        //$92 	Helmasaur King
    //        //$93 	Bumper
    //        //$94 	Swimmers
    //        //$95 	Eye Laser (Right)
    //        //$96 	Eye Laser (Left)
    //        //$97 	Eye Laser (Down)
    //        //$98 	Eye Laser (Up)
    //        //$99 	Pengator
    //        //$9A 	Kyameron
    //        //$9B 	Wizzrobe
    //        //$9C 	Tadpoles
    //        //$9D 	Tadpoles
    //        //$9E 	Ostrich (Haunted Grove)
    //        //$9F 	Flute
    //        //$A0 	Birds (Haunted Grove)
    //        //$A1 	Freezor
    //        //$A2 	Kholdstare (Boss)
    //        //$A3 	Kholdstare's Shell
    //        //$A4 	Falling Ice
    //        //$A5 	Zazak Fireball
    //        //$A6 	Red Zazak
    //        //$A7 	Stalfos
    //        //$A8 	Bomber Flying Creatures from Darkworld
    //        //$A9 	Bomber Flying Creatures from Darkworld
    //        //$AA 	Pikit
    //        //$AB 	Maiden
    //        //$AC 	Apple
    //        //$AD 	Lost Old Man
    //        //$AE 	Down Pipe
    //        //$AF 	Up Pipe
    //        //$B0 	Right Pip
    //        //$B1 	Left Pipe
    //        //$B2 	Good bee again?
    //        //$B3 	Hylian Inscription
    //        //$B4 	Thief's chest (not the one that follows you, the one that you grab from the DW smithy house)
    //        //$B5 	Bomb Salesman
    //        //$B6 	Kiki
    //        //$B7 	Maiden following you in Blind Dungeon
    //        //$B8 	Monologue Testing Sprite
    //        //$B9 	Feuding Friends on Death Mountain
    //        //$BA 	Whirlpool
    //        //$BB 	Salesman / chestgame guy / 300 rupee giver guy / Chest game thief
    //        //$BC 	Drunk in the inn
    //        //$BD 	Vitreous (Large Eyeball)
    //        //$BE 	Vitreous (Small Eyeball)
    //        //$BF 	Vitreous' Lightning
    //        //$C0 	Monster in Lake of Ill Omen / Quake Medallion
    //        //$C1 	Agahnim teleporting Zelda to dark world
    //        //$C2 	Boulders
    //        //$C3 	Gibo
    //        //$C4 	Thief
    //        //$C5 	Medusa
    //        //$C6 	Four Way Fireball Spitters (spit when you use your sword)
    //        //$C7 	Hokku-Bokku
    //        //$C8 	Big Fairy who heals you
    //        //$C9 	Tektite
    //        //$CA 	Chain Chomp
    //        //$CB 	Trinexx
    //        //$CC 	Another part of trinexx
    //        //$CD 	Yet another part of trinexx
    //        //$CE 	Blind The Thief (Boss)
    //        //$CF 	Swamola
    //        //$D0 	Lynel
    //        //$D1 	Bunny Beam
    //        //$D2 	Flopping fish
    //        //$D3 	Stal
    //        //$D4 	Landmine
    //        //$D5 	Digging Game Proprietor
    //        //$D6 	Ganon
    //        //$D7 	Copy of Ganon, except invincible?
    //        //$D8 	Heart
    //        //$D9 	Green Rupee
    //        //$DA 	Blue Rupee
    //        //$DB 	Red Rupee
    //        //$DC 	Bomb Refill (1)
    //        //$DD 	Bomb Refill (4)
    //        //$DE 	Bomb Refill (8)
    //        //$DF 	Small Magic Refill
    //        //$E0 	Full Magic Refill
    //        //$E1 	Arrow Refill (5)
    //        //$E2 	Arrow Refill (10)
    //        //$E3 	Fairy
    //        //$E4 	Key
    //        //$E5 	Big Key
    //        //$E6 	Shield
    //        //$E7 	Mushroom
    //        //$E8 	Fake Master Sword
    //        //$E9 	Magic Shop dude / His items, including the magic powder
    //        //$EA 	Heart Container
    //        //$EB 	Heart Piece
    //        //$EC 	Bushes
    //        //$ED 	Cane Of Somaria Platform
    //        //$EE 	Mantle
    //        //$EF 	Cane of Somaria Platform (Unused)
    //        //$F0 	Cane of Somaria Platform (Unused)
    //        //$F1 	Cane of Somaria Platform (Unused)
    //        //$F2 	Medallion Tablet
    //        //*/
    //    }
    //}
}
