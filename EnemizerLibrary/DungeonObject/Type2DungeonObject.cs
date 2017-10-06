using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Type2DungeonObject
    {
        public static Type2DungeonObject GetDungeonObjectFromBytes(byte[] bytes)
        {
            if(bytes.Length != 2)
            {
                throw new Exception("Type 2 Dungeon Objects must be built from 2 byte chunks.");
            }

            return new Type2DungeonObject(bytes);
        }
        protected byte[] _bytes;
        public byte[] Bytes
        {
            get { return _bytes; }
            set
            {
                if (value.Length != 2)
                {
                    throw new Exception("Type2DungeonObject must be composed from 2 bytes");
                }
                _bytes = value;
            }
        }

        public Type2DungeonObject(byte[] bytes)
        {
            this.Bytes = bytes;
        }
    }
}
