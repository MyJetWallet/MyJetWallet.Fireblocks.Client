using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewRpcResponse
    {
        [Newtonsoft.Json.JsonProperty("RpcResultDto")] //?
        public FewRpcResultDTO RpcResultDto { get; set; }

        [Newtonsoft.Json.JsonProperty("RpcErrorDto")] //?
        public FewRpcErrorDTO RpcErrorDto { get; set; }
    }

    public class FewRpcResultDTO
    {
        [Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public object Result { get; set; }
    }

    public class FewRpcErrorDTO
    {
        [Newtonsoft.Json.JsonProperty("error", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public FewRpcError Error { get; set; }
    }

    public class FewRpcError
    {
        [Newtonsoft.Json.JsonProperty("code")]
        public int Code { get; set; }

        [Newtonsoft.Json.JsonProperty("message", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
