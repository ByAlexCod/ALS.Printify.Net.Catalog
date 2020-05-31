using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ALS.Printify.Net
{
    public class Shop: AbstractPrintifyObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("sales_channel")]
        public string SalesChannel { get; set; }
    }
}
