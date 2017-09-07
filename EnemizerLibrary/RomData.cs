using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public int Length
        {
            get
            {
                return romData.Length;
            }
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

        public void WriteRom(FileStream fs)
        {
            fs.Write(this.romData, 0, this.romData.Length);
        }

        public void PatchData(int address, byte[] patchData)
        {
            Array.Copy(patchData, 0, romData, address, patchData.Length);
        }

        public void PatchData(PatchObject patch)
        {
            Array.Copy(patch.patchData.ToArray(), 0, romData, patch.address, patch.patchData.ToArray().Length);
        }
    }
}
