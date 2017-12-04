using EnemizerLibrary;
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
        public async Task<string> Get() //OptionFlags optionFlags)
        {
            OptionFlags optionFlags;
            var rando = new RandomizerClient();

            return await rando.GetData(new EntranceRandomizerOptions());
        }
    }
}