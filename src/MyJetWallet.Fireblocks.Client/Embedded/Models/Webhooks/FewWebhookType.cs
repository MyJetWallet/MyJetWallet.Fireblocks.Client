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
        NCW_BALANCE_UPDATE = 7,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_DEVICE_MESSAGE")]
        NCW_DEVICE_MESSAGE = 8,

        [System.Runtime.Serialization.EnumMember(Value = @"NCW_STATUS_UPDATED")]
        NCW_STATUS_UPDATED = 9,

        [System.Runtime.Serialization.EnumMember(Value = @"ON_NEW_EXTERNAL_TRANSACTION")]
        ON_NEW_EXTERNAL_TRANSACTION = 10,

        [System.Runtime.Serialization.EnumMember(Value = @"VAULT_ACCOUNT_ADDED")]
        VAULT_ACCOUNT_ADDED = 11,

        [System.Runtime.Serialization.EnumMember(Value = @"VAULT_WALLET_READY")]
        VAULT_WALLET_READY = 12,

        [System.Runtime.Serialization.EnumMember(Value = @"UNMANAGED_WALLET_ADDED")]
        UNMANAGED_WALLET_ADDED = 13,

        [System.Runtime.Serialization.EnumMember(Value = @"UNMANAGED_WALLET_REMOVED")]
        UNMANAGED_WALLET_REMOVED = 14,

        [System.Runtime.Serialization.EnumMember(Value = @"THIRD_PARTY_ACCOUNT_ADDED")]
        THIRD_PARTY_ACCOUNT_ADDED = 15,

        [System.Runtime.Serialization.EnumMember(Value = @"NETWORK_CONNECTION_ADDED")]
        NETWORK_CONNECTION_ADDED = 16,

        [System.Runtime.Serialization.EnumMember(Value = @"NETWORK_CONNECTION_REMOVED")]
        NETWORK_CONNECTION_REMOVED = 17,

        [System.Runtime.Serialization.EnumMember(Value = @"CONFIG_CHANGE_REQUEST_STATUS")]
        CONFIG_CHANGE_REQUEST_STATUS = 18,

        [System.Runtime.Serialization.EnumMember(Value = @"TRANSACTION_APPROVAL_STATUS_UPDATED")]
        TRANSACTION_APPROVAL_STATUS_UPDATED = 19,

        [System.Runtime.Serialization.EnumMember(Value = @"VAULT_ACCOUNT_ASSET_ADDED")]
        VAULT_ACCOUNT_ASSET_ADDED = 20,

        [System.Runtime.Serialization.EnumMember(Value = @"EXTERNAL_WALLET_ASSET_ADDED")]
        EXTERNAL_WALLET_ASSET_ADDED = 21,

        [System.Runtime.Serialization.EnumMember(Value = @"INTERNAL_WALLET_ASSET_ADDED")]
        INTERNAL_WALLET_ASSET_ADDED = 22,
    }
}
