using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerTests
{
    public static class Utilities
    {
        public static byte[] LoadRom(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            return rom_data;
        }
    }
}
