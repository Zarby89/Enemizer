using EnemizerLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemizerCLI
{
    public class RandomizerPatch
    {
        public string Seed { get; set; }

        public string Logic { get; set; }

        public string Difficulty { get; set; }

        public List<PatchObject> Patches { get; set; } = new List<PatchObject>();

        public object Spoilers { get; set; }

        public string Hash { get; set; }

        public RandomizerPatch(string basePatchJson, string json)
        {
            var rawBase = JArray.Parse(basePatchJson);
            ConvertPatch(rawBase);

            var rawPatch = JObject.Parse(json);
            Seed = (string)rawPatch["seed"];
            Logic = (string)rawPatch["logic"];
            Difficulty = (string)rawPatch["difficulty"];
            ConvertPatch(rawPatch["patch"] as JArray);
            Spoilers = rawPatch["spoilers"];
            Hash = (string)rawPatch["hash"];
        }

        void ConvertPatch(JArray patchBase)
        {
            foreach (var p in patchBase)
            {
                foreach (var prop in p)
                {
                    var property = prop as JProperty;

                    if (property != null)
                    {
                        var v = property.Value as JArray;
                        if (v != null)
                        {
                            int address;
                            if (Int32.TryParse(property.Name, out address))
                            {
                                Patches.Add(new PatchObject() { address = address, patchData = v.Select(x => (byte)x).ToList() });
                            }
                            else
                            {
                                throw new Exception($"invalid address: {property.Name}");
                            }
                        }
                        else
                        {
                            throw new Exception($"invalid patch property value: {property.Value.ToString()}");
                        }
                    }
                    else
                    {
                        throw new Exception($"invalid patch property: {prop.ToString()}");
                    }
                }
            }
        }

        public void PatchRom(ref byte[] rom)
        {
            foreach (var p in Patches)
            {
                Array.Copy(p.patchData.ToArray(), 0, rom, p.address, p.patchData.Count);
            }
        }
    }
}
