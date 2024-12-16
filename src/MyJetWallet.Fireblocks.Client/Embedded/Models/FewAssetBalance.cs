using System;
using System.Collections.Generic;

namespace MyJetWallet.Fireblocks.Client.Embedded.Models
{
    public class FewAssetBalance
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("total")]
        public string Total { get; set; }

        [Newtonsoft.Json.JsonProperty("balance")]
        public string Balance { get; set; }

        [Newtonsoft.Json.JsonProperty("available")]
        public string Available { get; set; }

        [Newtonsoft.Json.JsonProperty("pending")]
        public string Pending { get; set; }

        [Newtonsoft.Json.JsonProperty("frozen")]
        public string Frozen { get; set; }

        [Newtonsoft.Json.JsonProperty("lockedAmount")]
        public string LockedAmount { get; set; }

        [Newtonsoft.Json.JsonProperty("totalStakedCPU")]
        public string TotalStakedCPU { get; set; }

        [Newtonsoft.Json.JsonProperty("totalStakedNetwork")]
        public string TotalStakedNetwork { get; set; }

        [Newtonsoft.Json.JsonProperty("selfStakedCPU")]
        public string SelfStakedCPU { get; set; }

        [Newtonsoft.Json.JsonProperty("selfStakedNetwork")]
        public string SelfStakedNetwork { get; set; }

        [Newtonsoft.Json.JsonProperty("pendingRefundCPU")]
        public string PendingRefundCPU { get; set; }

        [Newtonsoft.Json.JsonProperty("pendingRefundNetwork")]
        public string PendingRefundNetwork { get; set; }

        [Newtonsoft.Json.JsonProperty("rewardInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public FewAssetRewardInfo RewardInfo { get; set; }

        [Newtonsoft.Json.JsonProperty("blockHeight")]
        public string BlockHeight { get; set; }

        [Newtonsoft.Json.JsonProperty("allocatedBalances")]
        public List<FewAssetAllocatedBalance> AllocatedBalances { get; set; }
    }

    public class FewAssetRewardInfo
    {
        [Newtonsoft.Json.JsonProperty("pendingRewards", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PendingRewards { get; set; }
    }

    public class FewAssetAllocatedBalance
    {
        [Newtonsoft.Json.JsonProperty("allocationId")]
        public string AllocationId { get; set; }

        [Newtonsoft.Json.JsonProperty("total")]
        public string Total { get; set; }

        [Newtonsoft.Json.JsonProperty("available")]
        public string Available { get; set; }

        [Newtonsoft.Json.JsonProperty("pending")]
        public string Pending { get; set; }

        [Newtonsoft.Json.JsonProperty("frozen")]
        public string Frozen { get; set; }

        [Newtonsoft.Json.JsonProperty("locked")]
        public string Locked { get; set; }

    }
}
