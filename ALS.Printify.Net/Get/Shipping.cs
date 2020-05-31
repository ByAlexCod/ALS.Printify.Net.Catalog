using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ALS.Printify.Net.Get
{
    public class Shipping
    {
        [JsonProperty("handling_time")]
        public HandlingTime HandlingTime { get; set; }

        [JsonProperty("profiles")]
        public Profile[] Profiles { get; set; }
    }

    public class HandlingTime
    {
        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Profile
    {
        [JsonProperty("variant_ids")]
        public long[] VariantIds { get; set; }

        [JsonProperty("first_item")]
        public AdditionalItems FirstItem { get; set; }

        [JsonProperty("additional_items")]
        public AdditionalItems AdditionalItems { get; set; }

        [JsonProperty("countries")]
        public string[] Countries { get; set; }
    }

    public class AdditionalItems
    {
        [JsonProperty("cost")]
        public long Cost { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
