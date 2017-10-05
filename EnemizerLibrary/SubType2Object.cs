using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SubType2Object
    {
        byte[] _bytes;
        public byte[] Bytes
        {
            get { return _bytes; }
            set
            {
                if(value.Length != 3)
                {
                    throw new Exception("SubType2Object must be composed for 3 bytes");
                }
                _bytes = value;
                BuildFromBytes();
            }
        }

        int _xCoord;
        public int XCoord
        {
            get { return _xCoord; }
            set
            {
                _xCoord = value;
                BuildByteArray();
            }
        }

        int _yCoord;
        public int YCoord
        {
            get { return _yCoord; }
            set
            {
                _yCoord = value;
                BuildByteArray();
            }
        }

        int _oid;
        public int OID
        {
            get { return _oid; }
            set
            {
                _oid = value;
                BuildByteArray();
            }
        }

        public SubType2Object(byte[] bytes)
        {
            this._bytes = bytes;

            BuildFromBytes();
        }

        public SubType2Object(int x, int y, int oid)
        {
            this._xCoord = x;
            this._yCoord = y;
            this._oid = oid;

            BuildByteArray();
        }

        void BuildByteArray()
        {
            var b1 = ((_xCoord << 2) & 0xFC) | (_oid & 0x03);
            var b2 = ((_yCoord << 2) & 0xFC) | ((_oid >> 2) & 0x03);
            var b3 = 0xF0 | ((_oid >> 4) & 0x0F);
            _bytes = new byte[] { (byte)b1, (byte)b2, (byte)b3 };
        }

        void BuildFromBytes()
        {
            _oid = ((_bytes[2] << 4) | 0x80 | (((_bytes[1] & 0x03) << 2) | ((_bytes[0] & 0x03))));
            _xCoord = (byte)((_bytes[0] & 0xFC) >> 2);
            _yCoord = (byte)((_bytes[1] & 0xFC) >> 2);
        }
    }
}
