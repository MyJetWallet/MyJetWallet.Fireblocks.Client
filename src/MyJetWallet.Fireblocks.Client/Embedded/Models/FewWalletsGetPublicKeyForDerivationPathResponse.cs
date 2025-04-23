using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewWalletsGetPublicKeyForDerivationPathResponse
    {
        [Newtonsoft.Json.JsonProperty("algorithm", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Algorithm { get; set; }

        [Newtonsoft.Json.JsonProperty("derivationPath", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<int> DerivationPath { get; set; }

        [Newtonsoft.Json.JsonProperty("publicKey", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PublicKey { get; set; }
    }
}