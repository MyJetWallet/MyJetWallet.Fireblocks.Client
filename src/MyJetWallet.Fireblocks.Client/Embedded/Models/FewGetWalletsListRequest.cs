using System;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewGetWalletsListRequest
    {
        [Newtonsoft.Json.JsonProperty("pageCursor")]
        public string PageCursor { get; set; }

        [Newtonsoft.Json.JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [Newtonsoft.Json.JsonProperty("sort")]
        public string Sort { get; set; } //Sort field, for example "createdAt"       

        [Newtonsoft.Json.JsonProperty("order")]
        public string Order { get; set; } //Defaults to ASC

        [Newtonsoft.Json.JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
