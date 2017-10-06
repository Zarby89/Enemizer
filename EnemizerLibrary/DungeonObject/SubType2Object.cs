using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SubType2Object : Type1DungeonObject
    {
        public SubType2Object(byte[] bytes)
            : base(bytes)
        {
            this.Bytes = bytes;

            //BuildFromBytes();
        }

        public SubType2Object(int x, int y, int oid)
            : base(x, y, oid)
        {
            this._xCoord = x;
            this._yCoord = y;
            this.OID = oid;

            //BuildByteArray();
        }

        protected override void BuildByteArray()
        {
            var b1 = ((_xCoord << 2) & 0xFC) | (_oid & 0x03);
            var b2 = ((_yCoord << 2) & 0xFC) | ((_oid >> 2) & 0x03);
            var b3 = 0xF0 | ((_oid >> 4) & 0x0F);
            _bytes = new byte[] { (byte)b1, (byte)b2, (byte)b3 };
        }

        protected override void BuildFromBytes()
        {
            _oid = ((_bytes[2] << 4) | 0x80 | (((_bytes[1] & 0x03) << 2) | ((_bytes[0] & 0x03))));
            _xCoord = (byte)((_bytes[0] & 0xFC) >> 2);
            _yCoord = (byte)((_bytes[1] & 0xFC) >> 2);
            _sizeX = (byte)((_bytes[0] & 0x03));
            _sizeY = (byte)((_bytes[1] & 0x03));
            _sizeXY = (byte)(((_sizeX << 2) + _sizeY));
        }
    }
}
