using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class SubType1Object : LayerDungeonObject
    {
        public SubType1Object(byte[] bytes)
            : base(bytes)
        {
            this.Bytes = bytes;

            //BuildFromBytes();
        }

        public SubType1Object(int x, int y, int oid)
            : base(x, y, oid)
        {
            this._xCoord = x;
            this._yCoord = y;
            this.OID = oid;

            //BuildByteArray();
        }

        protected override void BuildByteArray()
        {
            var b1 = ((_xCoord << 2) & 0xFC) | (_sizeX & 0x03);
            var b2 = ((_yCoord << 2) & 0xFC) | (_sizeY & 0x03);
            var b3 = _oid;
            _bytes = new byte[] { (byte)b1, (byte)b2, (byte)b3 };
        }

        protected override void BuildFromBytes()
        {
            _oid = _bytes[2];
            _xCoord = (byte)((_bytes[0] & 0xFC) >> 2);
            _yCoord = (byte)((_bytes[1] & 0xFC) >> 2);
            _sizeX = (byte)((_bytes[0] & 0x03));
            _sizeY = (byte)((_bytes[1] & 0x03));
            _sizeXY = (byte)(((_sizeX << 2) + _sizeY));
        }
    }
}
