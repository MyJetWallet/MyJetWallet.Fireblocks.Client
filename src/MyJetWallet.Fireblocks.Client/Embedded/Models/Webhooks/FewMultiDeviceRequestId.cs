using Autofac.Core;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks
{
    public class FewMultiDeviceRequestId : FewWebhookWithData<FewMultiDeviceRequestIdData>
    {
    }

    public class FewMultiDeviceRequestIdData
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }

        [Newtonsoft.Json.JsonProperty("deviceId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DeviceId { get; set; }

        [Newtonsoft.Json.JsonProperty("requestId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RequestId { get; set; }
    }
}