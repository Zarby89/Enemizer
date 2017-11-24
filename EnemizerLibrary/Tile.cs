using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Tile
    {
        public byte x, y;

        public Tile(byte x, byte y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            var t = obj as Tile;

            if(Object.ReferenceEquals(null, t))
            {
                return false;
            }

            return this.x == t.x && this.y == t.y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ x.GetHashCode();
                hash = (hash * HashingMultiplier) ^ y.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Tile t1, Tile t2)
        {
            if(Object.ReferenceEquals(null, t1) || Object.ReferenceEquals(null, t2))
            {
                return false;
            }
            if(Object.ReferenceEquals(t1, t2))
            {
                return true;
            }

            return t1.Equals(t2);
        }

        public static bool operator !=(Tile t1, Tile t2)
        {
            return !(t1 == t2);
        }
    }

    public class TileCollection
    {
        public byte Speed { get; set; } = 0xE0;

        public List<Tile> Items { get; set; } = new List<Tile>();

        public void UpdateRom(RomData romData)
        {
            romData[0x4BA21] = Speed;

            romData[0x04BA1D] = (byte)this.Items.Count;

            for (int i = 0; i < this.Items.Count; i++)
            {
                // x
                romData[0x4BA2A + i] = (byte)((this.Items[i].x + 3) * 16);

                // y
                romData[0x4BA2A + 0x16 + i] = (byte)((this.Items[i].y + 4) * 16);
            }
        }
    }
}
