using System;
using System.Collections.Generic;
using System.Linq;

namespace EnemizerLibrary
{
    public class OverworldArea
    {
        public int AreaId { get; set; }
        public int SpriteTableBaseAddress { get; set; }
        public int GraphicsBlockAddress { get; set; }
        public byte GraphicsBlockId { get; set; }
        public string AreaName
        {
            get
            {
                return OverworldAreaConstants.GetAreaName(AreaId);
            }
        }
        public List<OverworldSprite> Sprites { get; set; } = new List<OverworldSprite>();
        public int BushSpriteId
        {
            get
            {
                return romData[SpriteConstants.RandomizedBushEnemyTableBaseAddress + this.AreaId];
            }
            set
            {
                romData[SpriteConstants.RandomizedBushEnemyTableBaseAddress + this.AreaId] = (byte)value;
            }
        }
        public string BushSpriteName
        {
            get
            {
                return SpriteConstants.GetSpriteName(this.BushSpriteId);
            }
        }
        public SpriteGroup SpriteGroup
        {
            get
            {
                return spriteGroupCollection.SpriteGroups.First(x => x.GroupId == this.GraphicsBlockId);
            }
        }

        RomData romData;
        SpriteGroupCollection spriteGroupCollection;
        SpriteRequirementCollection spriteRequirementCollection;
        Random rand;

        public OverworldArea(RomData romData, int AreaId, Random rand, SpriteGroupCollection spriteGroupCollection, SpriteRequirementCollection spriteRequirementCollection)
        {
            this.romData = romData;
            this.AreaId = AreaId;
            this.spriteGroupCollection = spriteGroupCollection;
            this.spriteRequirementCollection = spriteRequirementCollection;
            this.rand = rand;

            int spriteTableBaseSnesAddress = (09 << 16) // bank 9
                + (romData[AddressConstants.OverworldSpritePointerTableBaseAddress + (AreaId * 2) + 1] << 8) 
                + (romData[AddressConstants.OverworldSpritePointerTableBaseAddress + (AreaId * 2)]);
            SpriteTableBaseAddress = Utilities.SnesToPCAddress(spriteTableBaseSnesAddress);

            LoadGraphicsBlock();
            LoadSprites();
        }

        void LoadSprites()
        {
            int i = 0;
            while(romData[SpriteTableBaseAddress + i] != 0xFF)
            {
                var sprite = new OverworldSprite(romData, SpriteTableBaseAddress + i);
                Sprites.Add(sprite);

                // sprites are in 3 byte chunks
                i += 3;
            }
        }

        void SetGraphicsBlockAddress()
        {
            if (AreaId == 0x80 || AreaId == 0x81)
            {
                GraphicsBlockAddress = 0x016576 + (AreaId - 0x80);
                return;
            }
            if (AreaId == 0x110 || AreaId == 0x111) // not sure if these are ever actually used?
            {
                GraphicsBlockAddress = 0x016576 + (AreaId - 0x110);
                return;
            }

            GraphicsBlockAddress = AddressConstants.OverworldAreaGraphicsBlockBaseAddress + AreaId;

            if (AreaId >= 0x40 && AreaId < 0x80)
            {
                GraphicsBlockAddress += 0x40;
            }
            if (AreaId >= 0x90 && AreaId < 0x110)
            {
                GraphicsBlockAddress -= 0x50;
            }
        }

        void LoadGraphicsBlock()
        {
            SetGraphicsBlockAddress();

            GraphicsBlockId = romData[GraphicsBlockAddress];
        }

        public void UpdateRom()
        {
            WriteGraphicsBlock();

            WriteSprites();
        }

        void WriteGraphicsBlock()
        {
            romData[GraphicsBlockAddress] = GraphicsBlockId;
        }

        void WriteSprites()
        {
            foreach(var s in Sprites)
            {
                s.UpdateRom();
            }
        }

        public void RandomizeSprites(OptionFlags optionFlags)
        {
            var spriteGroup = spriteGroupCollection.SpriteGroups.First(x => x.GroupId == this.GraphicsBlockId);

            var possibleSprites = spriteGroup.GetPossibleEnemySprites(this, optionFlags).Select(x => x.SpriteId).ToArray();

            if (possibleSprites.Length > 0)
            {
                var spritesToUpdate = this.Sprites.Where(x => spriteRequirementCollection.RandomizableSprites.Select(y => y.SpriteId).Contains(x.SpriteId))
                    .ToList();

                spritesToUpdate/*.Where(x => x.SpriteId != SpriteConstants.RavenSprite)*/.ToList()
                    .ForEach(x => x.SpriteId = possibleSprites[rand.Next(possibleSprites.Length)]);

                // Kodongo are not allowed in overworld for now, until ASM can be fixed, then this won't be needed at all.
                /*
                // Kodongo in Raven place will crash the game
                possibleSprites = possibleSprites.Where(x => x != SpriteConstants.KodongosSprite).ToArray();

                if (possibleSprites.Length == 0)
                {
                    // TODO: should throw an error but let's just leave it for now
                    return;
                }
                spritesToUpdate.Where(x => x.SpriteId == SpriteConstants.RavenSprite).ToList()
                    .ForEach(x => x.SpriteId = possibleSprites[rand.Next(possibleSprites.Length)]);
                //*/
            }
        }

        public void RandomizeBushSprite()
        {
            var spriteGroup = spriteGroupCollection.SpriteGroups.First(x => x.GroupId == this.GraphicsBlockId);

            var possibleSprites = spriteGroup.GetPossibleEnemySprites(this).Where(x => x.Overlord == false).Select(x => x.SpriteId).ToArray();
            if(possibleSprites.Length > 0)
            {
                romData[SpriteConstants.RandomizedBushEnemyTableBaseAddress + this.AreaId] = (byte)possibleSprites[rand.Next(possibleSprites.Length)];
            }
        }
    }
}
