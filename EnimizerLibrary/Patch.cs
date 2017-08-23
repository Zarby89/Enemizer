using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace EnemizerLibrary
{
    public class Patch
    {
        readonly string patchFilename;
        public Patch(string patchFile)
        {
            patchFilename = patchFile;
        }

        public void PatchRom(RomData rom)
        {
            var patches = JsonConvert.DeserializeObject<List<PatchObject>>(File.ReadAllText(this.patchFilename));

            foreach(var patch in patches)
            {
                rom.PatchData(patch);
            }
        }
    }

    public class PatchObject
    {
        public int address { get; set; }
        public List<byte> patchData { get; set; }

        public PatchObject()
        {
            patchData = new List<byte>();
        }
    }
}
