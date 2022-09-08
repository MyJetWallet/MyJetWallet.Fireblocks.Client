using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.ContractCalls
{
    public class ExtraParamsForContractCall
    {
        [Newtonsoft.Json.JsonProperty("contractCallData",
            Required = Newtonsoft.Json.Required.DisallowNull,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ContractCallData { get; set; }
    }
}
