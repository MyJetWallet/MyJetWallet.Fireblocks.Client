namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewWalletsGetPublicKeyOfAssetRequest
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("accountId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("assetId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AssetId { get; set; }
    
        [Newtonsoft.Json.JsonProperty("change", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Change { get; set; }

        [Newtonsoft.Json.JsonProperty("addressIndex", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AddressIndex { get; set; }

        [Newtonsoft.Json.JsonProperty("compressed")]
        public bool Compressed { get; set; }
    }
}