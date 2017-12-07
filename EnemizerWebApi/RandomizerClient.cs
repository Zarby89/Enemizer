using EnemizerLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EnemizerWebApi
{
    public class RandomizerClient
    {
        public async Task<string> GetData(RandomizerOptions options)
        {
            return await GetData(options, "seed");
        }

        public async Task<string> GetData(EntranceRandomizerOptions options)
        {
            if (options.shuffle == "off")
            {
                return await GetData(options as RandomizerOptions);
            }
            else
            {
                return await GetData(options, "entrance/seed");
            }
        }

        async Task<string> GetData(RandomizerOptions options, string endpoint)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://vt.alttp.run");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));

            HttpResponseMessage response = await client.PostAsJsonAsync(endpoint, options);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                Debug.Write(result);

                return result;
            }
            else
            {
                // call failed
                return null;
            }
        }

        public async Task<string> GetJsonAsString(string endpoint)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://vt.alttp.run");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/html"));

            HttpResponseMessage response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                Debug.Write(result);

                return result;
            }
            else
            {
                // call failed
                return null;
            }
        }
    }

    public class RandomizerOptions
    {
        public string logic { get; set; } = "NoMajorGlitches";
        public string difficulty { get; set; } = "normal";
        public string variation { get; set; } = "none";
        public string mode { get; set; } = "open";
        public string goal { get; set; } = "ganon";
        // shuffle
        public string heart_speed { get; set; } = "quarter";
        public string sram_trace { get; set; } = "false";
        public string menu_fast { get; set; } = "false";
        public string debug { get; set; } = "false";
        public string tournament { get; set; } = "false";
    }

    public class EntranceRandomizerOptions : RandomizerOptions
    {
        public string shuffle { get; set; } = "full";
    }

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

            foreach(var p in rawBase)
            {
                foreach(var prop in p)
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
                                //Patches[address] = v.Select(x => (byte)x).ToList();
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

            var rawPatch = JObject.Parse(json);

            Seed = (string)rawPatch["seed"];
            Logic = (string)rawPatch["logic"];
            Difficulty = (string)rawPatch["difficulty"];

            foreach(var p in rawPatch["patch"])
            {
                foreach(var prop in p)
                {
                    var property = prop as JProperty;

                    if(property != null)
                    {
                        var v = property.Value as JArray;
                        if(v != null)
                        {
                            int address;
                            if (Int32.TryParse(property.Name, out address))
                            {
                                Patches.Add(new PatchObject() { address = address, patchData = v.Select(x => (byte)x).ToList() });
                                //Patches[address] = v.Select(x => (byte)x).ToList();
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
            foreach(var p in Patches)
            {
                Array.Copy(p.patchData.ToArray(), 0, rom, p.address, p.patchData.Count);
            }
        }
    }
}