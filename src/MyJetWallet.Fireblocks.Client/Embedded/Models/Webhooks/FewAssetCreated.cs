using Autofac.Core;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks
{
    public class FewAssetCreated : FewWebhookWithData<FewAssetCreatedData>
    {
    }

    public class FewAssetCreatedData
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("accountId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("asset", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Asset { get; set; }
    }
}