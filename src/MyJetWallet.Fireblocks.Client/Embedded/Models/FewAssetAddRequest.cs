using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetAddRequest
    {
        [Newtonsoft.Json.JsonProperty("id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("symbol", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [Newtonsoft.Json.JsonProperty("name", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("decimals", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public decimal Decimals { get; set; }

        [Newtonsoft.Json.JsonProperty("networkProtocol", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NetworkProtocol { get; set; }

        [Newtonsoft.Json.JsonProperty("testnet", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Testnet { get; set; }

        [Newtonsoft.Json.JsonProperty("hasFee", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool HasFee { get; set; }

        [Newtonsoft.Json.JsonProperty("type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("baseAsset", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BaseAsset { get; set; }

        [Newtonsoft.Json.JsonProperty("ethNetwork", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string EthNetwork { get; set; }

        [Newtonsoft.Json.JsonProperty("ethContractAddress", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string EthContractAddress { get; set; }

        [Newtonsoft.Json.JsonProperty("issuerAddress", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IssuerAddress { get; set; }

        [Newtonsoft.Json.JsonProperty("blockchainSymbol", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockchainSymbol { get; set; }

        [Newtonsoft.Json.JsonProperty("deprecated", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Deprecated { get; set; }

        [Newtonsoft.Json.JsonProperty("coinType", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int CoinType { get; set; }

        [Newtonsoft.Json.JsonProperty("tenantIds", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> TenantIds { get; set; }

        [Newtonsoft.Json.JsonProperty("blockchain", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Blockchain { get; set; }

        [Newtonsoft.Json.JsonProperty("configuration", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public object Configuration { get; set; }

        [Newtonsoft.Json.JsonProperty("assetInfo", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public object AssetInfo { get; set; }

        [Newtonsoft.Json.JsonProperty("blockchainDisplayName", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockchainDisplayName { get; set; }

        [Newtonsoft.Json.JsonProperty("blockchainId", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string BlockchainId { get; set; }
    }
}
