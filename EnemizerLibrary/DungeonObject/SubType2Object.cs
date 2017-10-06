using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SubType2Object : LayerDungeonObject
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
            var b1 = 0xFC | ((_xCoord >> 4) & 0x03);
            var b2 = ((_xCoord << 4) & 0xF0) | ((_yCoord >> 2) & 0x0F);
            var b3 = ((_oid - 0x100) & 0x3F) | ((_yCoord << 6) & 0xC0);
            _bytes = new byte[] { (byte)b1, (byte)b2, (byte)b3 };
        }

        protected override void BuildFromBytes()
        {
            _oid = ((_bytes[2] & 0x3F) + 0x100); // ((_bytes[2] << 4) | 0x80 | (((_bytes[1] & 0x03) << 2) | ((_bytes[0] & 0x03))));
            _xCoord = (byte)((_bytes[1] & 0xF0) >> 4) | ((_bytes[0] & 0x03) << 4);
            _yCoord = (byte)((_bytes[1] & 0x0F) << 2) | ((_bytes[2] & 0xC0) >> 6);
            _sizeX = 0;
            _sizeY = 0;
            _sizeXY = 0;
        }
    }
}
