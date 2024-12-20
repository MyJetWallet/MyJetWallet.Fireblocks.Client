using Autofac;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.Embedded.Models.Webhooks;
using MyJetWallet.Fireblocks.Client.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public class DeserializationTests
    {
        [Theory]
        [InlineData(@"{""id"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""createdAt"":1733847785497,""lastUpdated"":1733847787460,""assetId"":""ETH_TEST5"",""source"":{""id"":"""",""type"":""UNKNOWN"",""name"":""External"",""subType"":""""},""destination"":{""id"":""982"",""type"":""VAULT_ACCOUNT"",""name"":""client_81f116418ecc42e49640d9cc3da1f716ETH_TEST5"",""subType"":""""},""amount"":0.001,""networkFee"":0.003456222207732,""netAmount"":0.001,""sourceAddress"":""0xfA30C8C92E670fBD80cf31Ef06B9bE6C650270fd"",""destinationAddress"":""0x6c34770A3BFCdF8FD358D51b6A15b893737Ef556"",""destinationAddressDescription"":"""",""destinationTag"":"""",""status"":""COMPLETED"",""txHash"":""0x3de53770dedba0daea12d910c38b91d6e36c90e38588ef38d90807062b7afbb6"",""subStatus"":""CONFIRMED"",""signedBy"":[],""createdBy"":"""",""rejectedBy"":"""",""amountUSD"":3.57,""addressType"":"""",""note"":"""",""exchangeTxId"":"""",""requestedAmount"":0.001,""feeCurrency"":""ETH_TEST5"",""operation"":""TRANSFER"",""customerRefId"":null,""numOfConfirmations"":1,""amountInfo"":{""amount"":""0.001"",""requestedAmount"":""0.001"",""netAmount"":""0.001"",""amountUSD"":""3.57""},""feeInfo"":{""networkFee"":""0.003456222207732"",""gasPrice"":""164.582009892""},""destinations"":[],""externalTxId"":null,""blockInfo"":{""blockHeight"":""7251337"",""blockHash"":""0x8dcfeb026ff7e6a4aa207a8077123821f02b087168f7d1ee3ef4a4474ce1ab3a""},""signedMessages"":[],""index"":0,""assetType"":""BASE_ASSET""}")]
        [InlineData(@"{""id"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""createdAt"":1733847785497,""lastUpdated"":1733847786631,""assetId"":""ETH_TEST5"",""source"":{""id"":"""",""type"":""UNKNOWN"",""name"":""External"",""subType"":""""},""destination"":{""id"":""982"",""type"":""VAULT_ACCOUNT"",""name"":""client_81f116418ecc42e49640d9cc3da1f716ETH_TEST5"",""subType"":""""},""amount"":0.001,""networkFee"":0.003456222207732,""netAmount"":0.001,""sourceAddress"":""0xfA30C8C92E670fBD80cf31Ef06B9bE6C650270fd"",""destinationAddress"":""0x6c34770A3BFCdF8FD358D51b6A15b893737Ef556"",""destinationAddressDescription"":"""",""destinationTag"":"""",""status"":""CONFIRMING"",""txHash"":""0x3de53770dedba0daea12d910c38b91d6e36c90e38588ef38d90807062b7afbb6"",""subStatus"":""PENDING_BLOCKCHAIN_CONFIRMATIONS"",""signedBy"":[],""createdBy"":"""",""rejectedBy"":"""",""amountUSD"":3.57,""addressType"":"""",""note"":"""",""exchangeTxId"":"""",""requestedAmount"":0.001,""feeCurrency"":""ETH_TEST5"",""operation"":""TRANSFER"",""customerRefId"":null,""numOfConfirmations"":1,""amountInfo"":{""amount"":""0.001"",""requestedAmount"":""0.001"",""netAmount"":""0.001"",""amountUSD"":""3.57""},""feeInfo"":{""networkFee"":""0.003456222207732"",""gasPrice"":""164.582009892""},""destinations"":[],""externalTxId"":null,""blockInfo"":{""blockHeight"":""7251337"",""blockHash"":""0x8dcfeb026ff7e6a4aa207a8077123821f02b087168f7d1ee3ef4a4474ce1ab3a""},""signedMessages"":[],""index"":0,""assetType"":""BASE_ASSET""}")]
        public void ValidTransactionValueTryStringToConvertTest(string value)
        {
            var deserilized = Newtonsoft.Json.JsonConvert.DeserializeObject<TransactionResponse>(value);

            Assert.NotNull(deserilized);
        }

        [Theory]
        [InlineData(@"{""walletId"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""deviceId"":""9831e582-c78b-425f-add5-a4af7a27cd24"",""physicalDeviceId"":""9831e582-c78b-425f-add5-a4af7a27cd25"",""data"":""text""}")]
        [InlineData(@"{""walletId"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""deviceId"":""9831e582-c78b-425f-add5-a4af7a27cd24"",""physicalDeviceId"":""9831e582-c78b-425f-add5-a4af7a27cd25"",""data"":{""id"":""111""}}")]
        [InlineData(@"{""walletId"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""deviceId"":""9831e582-c78b-425f-add5-a4af7a27cd24"",""physicalDeviceId"":""9831e582-c78b-425f-add5-a4af7a27cd25"",""data"":123}")]
        [InlineData(@"{""walletId"":""9831e582-c78b-425f-add5-a4af7a27cd23"",""deviceId"":""9831e582-c78b-425f-add5-a4af7a27cd24"",""physicalDeviceId"":""9831e582-c78b-425f-add5-a4af7a27cd25"",""data"":false}")]
        public void ValidDeviceMessageValueTryStringToConvertTest(string value)
        {
            var deserilized = Newtonsoft.Json.JsonConvert.DeserializeObject<FewDeviceMessageData>(value);

            Assert.NotNull(deserilized);
        }
    }
}
