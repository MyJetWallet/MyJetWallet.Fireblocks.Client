﻿using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    
    public class FewWalletsGetKeySetupStateResponse
    {
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Status { get; set; }
        
        [Newtonsoft.Json.JsonProperty("deviceSetupStatus", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<FewWalletsDeviceSetupStatus> DeviceSetupStatus { get; set; }
    }

    public class FewWalletsDeviceSetupStatus
    {
        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Status { get; set; }

        [Newtonsoft.Json.JsonProperty("deviceId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DeviceId { get; set; }

        [Newtonsoft.Json.JsonProperty("setupStatus", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<FewWalletsAlgorithmSetupStatus> SetupStatus { get; set; }
    }
}
