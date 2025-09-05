using Nethereum.ABI;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using Nethereum.Hex.HexConvertors.Extensions;


namespace MyJetWallet.Fireblocks.Client.Helpers;

public static class EIP712Helper
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

    public static string GenerateUsdcTransferWithAuthorizationMessage(string from, string to, decimal amount, int contractDecimals, string nonce, long validBeforeUnix)
    {
        amount = amount * (decimal)Math.Pow(10, contractDecimals);
        //var validBeforeUnix = validBefore.ToUnixTimeSeconds();

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
            { "verifyingContract", "0x3c499c542cEF5E3811e1192cE70d8cC03d5c3359" }
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

        return System.Text.Json.JsonSerializer.Serialize(eip712Data, options);
    }

    public static TransactionRequest GenerateContractCallFromVaultAccount(string valutAccountId, string smartContractAddress, string methodId,
        int v, string r, string s, string from, string to, string nonce,
        decimal amount, decimal contractDecimals, long validBeforeUnix)
    {

        amount = amount * contractDecimals;

        if (string.IsNullOrEmpty(methodId))
            methodId = "0xe3ee160e"; //transferWithAuthorization

        if (from?.StartsWith("0x") == false)
            from = "0x" + from;

        if (to?.StartsWith("0x") == false)
            to = "0x" + to;

        var abiEncode = new ABIEncode();
        var data = methodId + abiEncode.GetABIEncoded(
            new ABIValue("address", from),
            new ABIValue("address", to),
            new ABIValue("uint256", amount),
            new ABIValue("uint256", 0),
            new ABIValue("uint256", validBeforeUnix),
            new ABIValue("bytes32", nonce.HexToByteArray()),
            new ABIValue("bytes32", r.HexToByteArray()),
            new ABIValue("bytes32", s.HexToByteArray()),
            new ABIValue("uint8", v)
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
                //Type = TransferPeerPathType.CONTRACT,
                //Id = smartContractAddress,


                //Type = TransferPeerPathType.ONE_TIME_ADDRESS,
                //OneTimeAddress = new OneTimeAddress()
                //{
                //    Address = "0x1864bC2C7E69A7530d414684eDcC9EBC7b23a38E"//smartContractAddress
                //}
            },
            Amount = amount.ToString(),
            AssetId = "USDC",
            ExtraParameters = new ExtraParameters()
            {
                ContractCallData = data,


            },
            Operation = TransactionOperation.CONTRACT_CALL,
            ExternalTxId = $"TEST_{validBeforeUnix}",
            FeeLevel = TransactionRequestFeeLevel.HIGH
        };

        return resp;
    }



    // Create the ABI for the 'transferWithAuthorization' function from EIP-2612.

    //var transferWithAuthAbi = new List<Abi>
    //{
    //    new Abi
    //    {
    //        Name = "transferWithAuthorization",
    //        Type = "function",
    //        Inputs = new List<AbiInput>
    //        {
    //            new AbiInput { Name = "from", Type = "address" },
    //            new AbiInput { Name = "to", Type = "address" },
    //            new AbiInput { Name = "value", Type = "uint256" },
    //            new AbiInput { Name = "validAfter", Type = "uint256" },
    //            new AbiInput { Name = "validBefore", Type = "uint256" },
    //            new AbiInput { Name = "nonce", Type = "bytes32" },
    //            new AbiInput { Name = "v", Type = "uint8" },
    //            new AbiInput { Name = "r", Type = "bytes32" },
    //            new AbiInput { Name = "s", Type = "bytes32" }
    //        },
    //        Outputs = new List<AbiOutput>
    //        {
    //            new AbiOutput { Name = "", Type = "bool" }
    //        },
    //        StateMutability = "nonpayable"
    //    }
    //};


    //// Convert the ABI object to a JSON string.
    //string abiJson = JsonConvert.SerializeObject(transferWithAuthAbi);


    //var contractCall = new ContractCall
    //{
    //    Abi = abiJson,
    //    FunctionCall = data,
    //    Amount = "0",
    //    Retryable = false
    //};

    // A simple example of an ABI for a single 'transferWithAuthorization' function.
    //public class Abi
    //{
    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }

    //    [JsonProperty("inputs")]
    //    public List<AbiInput> Inputs { get; set; }

    //    [JsonProperty("outputs")]
    //    public List<AbiOutput> Outputs { get; set; }

    //    [JsonProperty("stateMutability")]
    //    public string StateMutability { get; set; }
    //}

    //// Represents an input parameter in the ABI.
    //public class AbiInput
    //{
    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }
    //}

    //// Represents an output parameter in the ABI.
    //public class AbiOutput
    //{
    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    [JsonProperty("type")]
    //    public string Type { get; set; }
    //}
    //// Represents the contract call details.
    //public class ContractCall
    //{
    //    // The ABI (Application Binary Interface) for the contract.
    //    // This is a JSON string of the contract's ABI.
    //    [JsonProperty("abi")]
    //    public string Abi { get; set; }

    //    // The encoded function call. This is typically a hex string
    //    // representing the function signature and encoded parameters.
    //    // A library like Nethereum would be used to generate this value.
    //    [JsonProperty("functionCall")]
    //    public string FunctionCall { get; set; }

    //    // The optional amount of native currency (e.g., ETH) to send with the call.
    //    [JsonProperty("amount")]
    //    public string Amount { get; set; }

    //    // Optional: whether to retry the transaction if it fails.
    //    [JsonProperty("retryable")]
    //    public bool Retryable { get; set; }
    //}
}
