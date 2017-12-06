using EnemizerLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnemizerWebApi
{
    public class EnemizerController : ApiController
    {
        public async Task<string> Get(string randomizerOptions, string enemizerOptions) //OptionFlags optionFlags)
        {
            EntranceRandomizerOptions randoOptions = JsonConvert.DeserializeObject<EntranceRandomizerOptions>(randomizerOptions);
            OptionFlags optionFlags = JsonConvert.DeserializeObject<OptionFlags>(enemizerOptions);

            var rando = new RandomizerClient();

            var randoPatchJson = await rando.GetData(randoOptions);

            var randoPatch = JsonConvert.DeserializeObject<RandomizerPatch>(randoPatchJson);

            return randoPatchJson;
        }
    }
}