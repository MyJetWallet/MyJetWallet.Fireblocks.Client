using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks
{
    public enum FewWebhookType
    {
        [System.Runtime.Serialization.EnumMember(Value = @"TRANSACTION_CREATED")]
        TRANSACTION_CREATED = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"TRANSACTION_STATUS_UPDATED")]
        TRANSACTION_STATUS_UPDATED = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_TRANSACTION_STATUS_UPDATED")]
        NCW_TRANSACTION_STATUS_UPDATED = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_ADD_DEVICE_SETUP_REQUESTED")]
        NCW_ADD_DEVICE_SETUP_REQUESTED = 3,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_CREATED")]
        NCW_CREATED = 4,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_ACCOUNT_CREATED")]
        NCW_ACCOUNT_CREATED = 5,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_ASSET_CREATED")]
        NCW_ASSET_CREATED = 6,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_BALANCE_UPDATE")]
        NCW_BALANCE_UPDATE = 7
    }
}
