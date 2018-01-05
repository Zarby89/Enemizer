using EnemizerLibrary;
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
        public static RomData LoadRom(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            RomData romData = new RomData(rom_data);
            romData.ExpandRom();

            Patch patch = new Patch("enemizerBasePatch.json");
            patch.PatchRom(romData);

            return romData;
        }
    }
}
