using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ALS.Printify.Net
{
    public class Blueprint: AbstractPrintifyObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("images")]
        public Uri[] Images { get; set; }

        public async Task<List<Provider>> GetProviders(bool fullResponse = false)
        {
            return new List<Provider>(await PrintifyClient.GetProviders(this, fullResponse));
        }
    }
}
