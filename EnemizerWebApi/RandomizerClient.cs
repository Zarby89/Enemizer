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
            return await GetData(options, "entrance/seed");
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
}