using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary.EnemyRandomizer
{
    public class OverworldSprite
    {
        public int SpriteAddress { get; set; }
        public int SpriteId { get; set; }
        public int SpriteX { get; set; }
        public int SpriteY { get; set; }

        RomData romData;
        public OverworldSprite(RomData romData, int SpriteAddress)
        {
            this.romData = romData;
            this.SpriteAddress = SpriteAddress;
            this.SpriteY = romData[SpriteAddress];
            this.SpriteX = romData[SpriteAddress + 1];
            this.SpriteId = romData[SpriteAddress + 2];
        }
    }
}
