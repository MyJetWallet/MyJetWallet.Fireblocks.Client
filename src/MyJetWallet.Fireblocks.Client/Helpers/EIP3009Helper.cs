using Nethereum.ABI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using Nethereum.Hex.HexConvertors.Extensions;


namespace MyJetWallet.Fireblocks.Client.Helpers;

public static class EIP3009Helper
{
    public static string GenerateNonce()
    {
        byte[] randBytes = new byte[24];
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var timeNowBytes = BitConverter.GetBytes(now);

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randBytes);
        }

        byte[] combinedBytes = new byte[32];
        Buffer.BlockCopy(timeNowBytes, 0, combinedBytes, 0, timeNowBytes.Length);
        Buffer.BlockCopy(randBytes, 0, combinedBytes, timeNowBytes.Length, randBytes.Length);

        return "0x" + BitConverter.ToString(combinedBytes).Replace("-", "").ToLower();
    }

    public static string GenerateUsdcTransferWithAuthorizationMessage(string from, string to, decimal amount, double contractDecimals, string nonce, long validBeforeUnix)
    {
        amount = amount * (decimal)Math.Pow(10, contractDecimals);

        if (from?.StartsWith("0x") == false)
            from = "0x" + from;

        if (to?.StartsWith("0x") == false)
            to = "0x" + to;

        var eip712Data = new Dictionary<string, object>();

        var domain = new Dictionary<string, object>
        {
            { "name", "USD Coin" },
            { "version", "2" },
            { "chainId", 137 },
            { "verifyingContract", "0x3c499c542cEF5E3811e1192ce70d8cC03d5c3359" }
        };

        var types = new Dictionary<string, object>
        {
            { "EIP712Domain", new List<object>
                {
                    new Dictionary<string, string> { { "name", "name" }, { "type", "string" } },
                    new Dictionary<string, string> { { "name", "version" }, { "type", "string" } },
                    new Dictionary<string, string> { { "name", "chainId" }, { "type", "uint256" } },
                    new Dictionary<string, string> { { "name", "verifyingContract" }, { "type", "address" } }
                }
            },
            { "TransferWithAuthorization", new List<object>
                {
                    new Dictionary<string, string> { { "name", "from" }, { "type", "address" } },
                    new Dictionary<string, string> { { "name", "to" }, { "type", "address" } },
                    new Dictionary<string, string> { { "name", "value" }, { "type", "uint256" } },
                    new Dictionary<string, string> { { "name", "validAfter" }, { "type", "uint256" } },
                    new Dictionary<string, string> { { "name", "validBefore" }, { "type", "uint256" } },
                    new Dictionary<string, string> { { "name", "nonce" }, { "type", "bytes32" } }
                }
            }
        };

        var message = new Dictionary<string, object>{
            { "from", from },
            { "to", to },
            { "value", amount },
            { "validAfter", 0 },
            { "validBefore", validBeforeUnix },
            { "nonce", nonce }
        };

        eip712Data.Add("domain", domain);
        eip712Data.Add("types", types);
        eip712Data.Add("primaryType", "TransferWithAuthorization");
        eip712Data.Add("message", message);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(eip712Data, options);
    }

    public static TransactionRequest GenerateContractCallTransferWithAuthorization(
    string valutAccountId, string assetId, string smartContractAddress, string methodId, 
    string from, string to, string nonce, decimal amount, int contractDecimals, long validBeforeUnix, 
    string r, string s, int v, TransactionRequestFeeLevel feeLevel)
    {        
        long amountLong = (long)(amount * (long)Math.Pow(10, contractDecimals));

        if (string.IsNullOrEmpty(methodId))
            methodId = "0xe3ee160e"; //transferWithAuthorization

        if (from?.StartsWith("0x") == false)
            from = "0x" + from;

        if (to?.StartsWith("0x") == false)
            to = "0x" + to;

        var _v = v == 0 ? 27 : 28;

        var abiEncode = new ABIEncode();

        var data = methodId + abiEncode.GetABIEncoded(
            new ABIValue("address", from),
            new ABIValue("address", to),
            new ABIValue("uint256", amountLong),
            new ABIValue("uint256", 0),
            new ABIValue("uint256", validBeforeUnix),
            new ABIValue("bytes32", nonce.HexToByteArray()),
            new ABIValue("uint8", _v),
            new ABIValue("bytes32", r.HexToByteArray()),
            new ABIValue("bytes32", s.HexToByteArray())            
            ).ToHex();

        var resp = new TransactionRequest()
        {
            Source = new SourceTransferPeerPath()
            {
                Type = TransferPeerPathType.VAULT_ACCOUNT,
                Id = valutAccountId
            },
            Destination = new DestinationTransferPeerPath()
            {
                Type = TransferPeerPathType.ONE_TIME_ADDRESS,
                OneTimeAddress = new OneTimeAddress()
                {
                    Address = smartContractAddress,
                }
            },
            Amount = "0",
            AssetId = assetId,
            ExtraParameters = new ExtraParameters()
            {
                ContractCallData = data
            },
            Operation = TransactionOperation.CONTRACT_CALL,
            ExternalTxId = $"ContractCall_TWA_{Guid.NewGuid()}",
            FeeLevel = feeLevel
        };

        return resp;
    }
}
