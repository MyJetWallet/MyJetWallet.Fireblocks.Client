using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetAddress
    {
        [Newtonsoft.Json.JsonProperty("accountName", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountName { get; set; }

        [Newtonsoft.Json.JsonProperty("accountId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("asset", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Asset { get; set; }

        [Newtonsoft.Json.JsonProperty("address", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Address { get; set; }

        [Newtonsoft.Json.JsonProperty("addressType", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AddressType { get; set; }

        [Newtonsoft.Json.JsonProperty("addressDescription", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AddressDescription { get; set; }

        [Newtonsoft.Json.JsonProperty("tag", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Tag { get; set; }

        [Newtonsoft.Json.JsonProperty("addressIndex")]
        public int AddressIndex { get; set; }

        [Newtonsoft.Json.JsonProperty("change")]
        public decimal Change { get; set; }

        [Newtonsoft.Json.JsonProperty("coinType")]
        public int CoinType { get; set; }

        [Newtonsoft.Json.JsonProperty("customerRefId")]
        public string CustomerRefId { get; set; }

        [Newtonsoft.Json.JsonProperty("addressFormat")]
        public string AddressFormat { get; set; }

        [Newtonsoft.Json.JsonProperty("legacyAddress")]
        public string LegacyAddress { get; set; }

        [Newtonsoft.Json.JsonProperty("paymentAddress")]
        public string PaymentAddress { get; set; }

        [Newtonsoft.Json.JsonProperty("userDefined")]
        public bool UserDefined { get; set; }

    }
}
