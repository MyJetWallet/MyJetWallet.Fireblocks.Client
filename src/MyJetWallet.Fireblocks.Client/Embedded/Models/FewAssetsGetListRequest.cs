using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetsGetListRequest
    {
        [Newtonsoft.Json.JsonProperty("accountId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("pageCursor")]
        public string PageCursor { get; set; }

        [Newtonsoft.Json.JsonProperty("pageSize")]
        public int PageSize { get; set; }

        //Defaults to assetId
        [Newtonsoft.Json.JsonProperty("sort")]
        public List<string> Sort { get; set; }

        //Defaults to ASC
        [Newtonsoft.Json.JsonProperty("order")]
        public string Order { get; set; }

        [Newtonsoft.Json.JsonProperty("enabled")]
        public bool Enabled { get; set; }

    }
}
