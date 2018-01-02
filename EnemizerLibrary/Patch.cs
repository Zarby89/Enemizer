using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
//using System.Web.Hosting;

namespace EnemizerLibrary
{
    public class Patch
    {
        readonly string patchFilename;

        public List<PatchObject> Patches { get; private set; }

        public Patch(string patchFile)
        {
            patchFilename = patchFile;

            patchFilename = Path.Combine(EnemizerBasePath.Instance.BasePath, patchFilename);

            Patches = JsonConvert.DeserializeObject<List<PatchObject>>(File.ReadAllText(this.patchFilename));
        }

        public void PatchRom(RomData rom)
        {
            foreach(var patch in Patches)
            {
                rom.PatchData(patch);
            }
        }

        public void AddPatch(PatchObject patch)
        {
            Patches.Add(patch);
        }

        public void AddPatches(List<PatchObject> patches)
        {
            Patches.AddRange(patches);
        }

        public string ExportJson()
        {
            return JsonConvert.SerializeObject(Patches);
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
