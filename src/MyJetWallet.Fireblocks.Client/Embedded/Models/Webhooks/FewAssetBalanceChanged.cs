using Autofac.Core;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks
{
    public class FewAssetBalanceChanged : FewWebhookWithData<FewAssetBalanceChangedData>
    {
    }

    public class FewAssetBalanceChangedData
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("accountId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("assetId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AssetId { get; set; }

        [Newtonsoft.Json.JsonProperty("total", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Total { get; set; }

        [Newtonsoft.Json.JsonProperty("pending", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Pending { get; set; }

        [Newtonsoft.Json.JsonProperty("staked", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Staked { get; set; }

        [Newtonsoft.Json.JsonProperty("frozen", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Frozen { get; set; }

        [Newtonsoft.Json.JsonProperty("lockedAmount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string LockedAmount { get; set; }

        [Newtonsoft.Json.JsonProperty("blockHeight", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockHeight { get; set; }

        [Newtonsoft.Json.JsonProperty("blockHash", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockHash { get; set; }
    }
}