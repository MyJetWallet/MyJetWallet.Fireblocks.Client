using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetGetSupportedAssetsListRequest
    {
        [Newtonsoft.Json.JsonProperty("pageCursor")]
        public string PageCursor { get; set; }

        [Newtonsoft.Json.JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [Newtonsoft.Json.JsonProperty("onlyBaseAssets")]
        public bool OnlyBaseAssets { get; set; }
    }
}
