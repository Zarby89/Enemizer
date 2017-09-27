using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerLibrary
{
    public static class Utilities
    {
        public static int SnesToPCAddress(int addr)
        {
            int temp = (addr & 0x7FFF) + ((addr / 2) & 0xFF8000);
            return (temp);
        }

        public static int PCToSnesAddress(int addr)
        {
            byte[] b = BitConverter.GetBytes(addr);
            b[2] = (byte)(b[2] * 2);
            if (b[1] >= 0x80)
                b[2] += 1;
            else
                b[1] += 0x80;

            return BitConverter.ToInt32(b, 0);
        }

        public static byte[] PCAddressToSnesByteArray(int pos)
        {
            int addr = PCToSnesAddress(pos);

            return new byte[] { (byte)(addr >> 16), ((byte)(addr >> 8)), ((byte)addr) };
        }
    }
}
