using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class OverworldArea
    {
        public const int OverworldAreaGraphicsBlockBaseAddress = 0x007A81;
        public const int OverworldSpritePointerTableBaseAddress = 0x04C901;
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

        RomData romData;

        public OverworldArea(RomData romData, int AreaId)
        {
            this.romData = romData;
            this.AreaId = AreaId;

            int spriteTableBaseSnesAddress = (09 << 16) + (romData[OverworldSpritePointerTableBaseAddress + (AreaId * 2) + 1] << 8) + (romData[OverworldSpritePointerTableBaseAddress + (AreaId * 2)]);
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

            GraphicsBlockAddress = OverworldAreaGraphicsBlockBaseAddress + AreaId;

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
    }

    public class OverworldAreaCollection
    {
        public List<OverworldArea> OverworldAreas { get; set; } = new List<OverworldArea>();
        RomData romData;
        public OverworldAreaCollection(RomData romData)
        {
            this.romData = romData;

            LoadAreas();
        }

        void LoadAreas()
        {
            for (int i = 0; i < 0x120; i++)
            {
                var owArea = new OverworldArea(romData, i);
                OverworldAreas.Add(owArea);
            }
        }

        public void SaveAreas()
        {
            foreach(var a in OverworldAreas)
            {
                
            }
        }
    }
}
