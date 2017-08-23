using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public class RomData
    {
        private byte[] romData;
        public RomData(byte[] romData)
        {
            this.romData = romData;
        }

        public byte this[int i]
        {
            get
            {
                return romData[i];
            }
            set
            {
                if(i == 0)
                {
                    Debugger.Break();
                }
                romData[i] = value;
            }
        }
    }
}
