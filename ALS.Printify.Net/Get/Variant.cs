using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ALS.Printify.Net.Get
{
    public class Variant: AbstractPrintifyObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("options")]
        public Options Options { get; set; }

        [JsonProperty("placeholders")]
        public Placeholder[] Placeholders { get; set; }
    }

    public class Options
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }

    public class Placeholder
    {
        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }
}
