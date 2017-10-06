using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class Type1DungeonObject
    {
        public static Type1DungeonObject GetDungeonObjectFromBytes(byte[] bytes)
        {
            if(bytes.Length != 3)
            {
                throw new Exception("Dungeon Objects must be built from 3 byte chunks.");
            }
            if(bytes[2] >= 0xF8)
            {
                return new SubType2Object(bytes);
            }

            return new Type1DungeonObject(bytes);
        }
        protected byte[] _bytes;
        public byte[] Bytes
        {
            get { return _bytes; }
            set
            {
                if (value.Length != 3)
                {
                    throw new Exception("Type1DungeonObject must be composed from 3 bytes");
                }
                _bytes = value;
                BuildFromBytes();
            }
        }

        protected int _xCoord;
        public int XCoord
        {
            get { return _xCoord; }
            set
            {
                _xCoord = value;
                BuildByteArray();
            }
        }

        protected int _yCoord;
        public int YCoord
        {
            get { return _yCoord; }
            set
            {
                _yCoord = value;
                BuildByteArray();
            }
        }

        protected int _oid;
        public int OID
        {
            get { return _oid; }
            set
            {
                _oid = value;
                BuildByteArray();
            }
        }

        protected int _sizeX;
        public int SizeX
        {
            get { return _sizeX; }
            set
            {
                _sizeX = value;
                BuildByteArray();
            }
        }

        protected int _sizeY;
        public int SizeY
        {
            get { return _sizeY; }
            set
            {
                _sizeY = value;
                BuildByteArray();
            }
        }

        protected int _sizeXY;
        public int SizeXY
        {
            get { return _sizeXY; }
            set
            {
                _sizeXY = value;
                BuildByteArray();
            }
        }

        public Type1DungeonObject(byte[] bytes)
        {
            this.Bytes = bytes;

            //BuildFromBytes();
        }

        public Type1DungeonObject(int x, int y, int oid)
        {
            this._xCoord = x;
            this._yCoord = y;
            this.OID = oid;

            //BuildByteArray();
        }

        protected virtual void BuildByteArray()
        {
            // TODO: fix this
            var b1 = ((_xCoord << 2) & 0xFC) | (_oid & 0x03);
            var b2 = ((_yCoord << 2) & 0xFC) | ((_oid >> 2) & 0x03);
            var b3 = 0xF0 | ((_oid >> 4) & 0x0F);
            _bytes = new byte[] { (byte)b1, (byte)b2, (byte)b3 };
        }

        protected virtual void BuildFromBytes()
        {
            // TODO: fix this
            _oid = ((_bytes[2] << 4) | 0x80 | (((_bytes[1] & 0x03) << 2) | ((_bytes[0] & 0x03))));
            _xCoord = (byte)((_bytes[0] & 0xFC) >> 2);
            _yCoord = (byte)((_bytes[1] & 0xFC) >> 2);
            _sizeX = (byte)((_bytes[0] & 0x03));
            _sizeY = (byte)((_bytes[1] & 0x03));
            _sizeXY = (byte)(((_sizeX << 2) + _sizeY));
        }

    }
}
