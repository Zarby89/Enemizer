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
        public int Address { get; set; }
        public bool IsOverlord { get; set; }
        public bool HasAKey { get; set; }

        public DungeonSprite(RomData romData, int address)
        {
            Address = address;

            byte0 = romData[address];
            byte1 = romData[address + 1];
            SpriteId = romData[address + 2];

            IsOverlord = (byte1 & SpriteConstants.StatisMask) != 0;

            if (romData[address + 3] != 0xFF && (romData[address + 5] == SpriteConstants.KeySprite || romData[address + 5] == SpriteConstants.BigKeySprite))
            {
                HasAKey = true;
            }
        }
    }
}
