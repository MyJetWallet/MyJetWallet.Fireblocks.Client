using System;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewWalletsDeviceSetupStatus
    {
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("algorithmName", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AlgorithmName { get; set; }

        [Newtonsoft.Json.JsonProperty("confirmed", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Confirmed { get; set; }

        [Newtonsoft.Json.JsonProperty("backedUp", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool BackedUp { get; set; }
    }
}
