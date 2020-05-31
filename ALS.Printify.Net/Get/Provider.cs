using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ALS.Printify.Net.Get;
using Newtonsoft.Json;

namespace ALS.Printify.Net
{
    public class Provider: AbstractPrintifyObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("location")]
        public ProviderAddress Location { get; set; }

        internal Blueprint Blueprint { get; set; }

        public async Task<List<Variant>> GetVariants()
        {
            return new List<Variant>(await Blueprint.PrintifyClient.GetVariants(Blueprint.Id, Id));
        }
    }

    public class ProviderAddress
    {
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public object Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
}
