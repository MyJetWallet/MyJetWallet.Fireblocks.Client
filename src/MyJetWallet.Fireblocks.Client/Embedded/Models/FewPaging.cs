using System;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewPaging
    {
        [Newtonsoft.Json.JsonProperty("next", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Next { get; set; }
    }
}
