using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client
{
    //Non-Custodial
    public partial class TransactionResponse
    {
        [Newtonsoft.Json.JsonProperty("assetType", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AssetTypeResponseType AssetType { get; set; }

        [Newtonsoft.Json.JsonProperty("replacedTxHash", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string ReplacedTxHash { get; set; }

        [Newtonsoft.Json.JsonProperty("rewardsInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public RewardInfo RewardsInfo { get; set; }



        [Newtonsoft.Json.JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [Newtonsoft.Json.JsonProperty("accountId")]
        public string AccountId { get; set; }

        [Newtonsoft.Json.JsonProperty("asset")]
        public string Asset { get; set; }
    }
}
