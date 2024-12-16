using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetsGetListResponse
    {
        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<FewAsset> Data { get; set; }

        [Newtonsoft.Json.JsonProperty("paging")]
        public FewPaging Paging { get; set; }
    }
}
