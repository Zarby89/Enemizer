using EnemizerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasePatchGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("zelda.sfc", FileMode.Open, FileAccess.Read);
            byte[] rom_data = new byte[fs.Length];
            fs.Read(rom_data, 0, (int)fs.Length);
            fs.Close();

            Array.Resize(ref rom_data, 4 * 1024 * 1024);

            RomData rom = new RomData(rom_data);

            Patch patch = new Patch("patchData.json");
            patch.PatchRom(rom);

            GeneralPatches.MoveRoomHeaders(rom);

            var patches = rom.GeneratePatch();

            patch.AddPatches(patches);

            var patchJson = patch.ExportJson();

            File.WriteAllText("enemizerBasePatch.json", patchJson);
        }
    }
}
