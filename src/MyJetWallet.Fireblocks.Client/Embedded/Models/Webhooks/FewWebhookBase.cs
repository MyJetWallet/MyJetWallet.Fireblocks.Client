using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks
{
    public class FewWebhookBase
    {
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public FewWebhookType Type { get; set; }

        [Newtonsoft.Json.JsonProperty("tenantId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string TenantId { get; set; }

        [Newtonsoft.Json.JsonProperty("timestamp", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long Timestamp { get; set; }
    }

    public class FewWebhookWithData<T> : FewWebhookBase
    {
        [Newtonsoft.Json.JsonProperty("data", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public T Data { get; set; }
    }
}

