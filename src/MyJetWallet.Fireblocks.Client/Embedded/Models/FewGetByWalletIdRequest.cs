﻿using System;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewGetByWalletIdRequest
    {
        [Newtonsoft.Json.JsonProperty("walletId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string WalletId { get; set; }
    }
}