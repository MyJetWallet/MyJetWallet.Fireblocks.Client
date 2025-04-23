namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewWalletsGetPublicKeyForDerivationPathRequest
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("derivationPath", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DerivationPath { get; set; }

        [Newtonsoft.Json.JsonProperty("algorithm", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Algorithm { get; set; }
    
        [Newtonsoft.Json.JsonProperty("compressed")]
        public bool Compressed { get; set; }
    }
}