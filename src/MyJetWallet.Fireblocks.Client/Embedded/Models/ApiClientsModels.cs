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
        [Newtonsoft.Json.JsonProperty("assetType")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public AssetTypeResponseType AssetType { get; set; }

        [Newtonsoft.Json.JsonProperty("replacedTxHash")]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string ReplacedTxHash { get; set; }

        [Newtonsoft.Json.JsonProperty("rewardsInfo")]
        public RewardInfo RewardsInfo { get; set; }

    }
}
