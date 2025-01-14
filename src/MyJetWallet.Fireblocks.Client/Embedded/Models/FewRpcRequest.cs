using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewRpcRequest
    {
        // [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        // public string WalletId { get; set; }
        //
        // [Newtonsoft.Json.JsonProperty("deviceId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        // public string DeviceId { get; set; }

        //example: payload": "{\"method\":\"request_mpc_setup\",\"params\":[{\"algorithms\":[\"MPC_CMP_ECDSA_SECP256K1\",\"MPC_EDDSA_ED25519\"]}],\"headers\":{\"sdkVersion\":1}}"
        [Newtonsoft.Json.JsonProperty("payload", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Payload { get; set; }
    }
}
