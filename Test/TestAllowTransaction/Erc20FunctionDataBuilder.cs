namespace TestAllowTransaction;

using System.Numerics;
using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;

public static class Erc20FunctionDataBuilder
{
    private const string UsdcPolygonApproveMethodId = "0x095ea7b3";

    public const string UsdcPolygonContractAddress = "0x3c499c542cef5e3811e1192ce70d8cc03d5c3359";   
    
    public static string BuildUsdcPolygonApproveData(string spender, BigInteger amount)
    {
        var abiEncode = new ABIEncode();
        var data = UsdcPolygonApproveMethodId + abiEncode.GetABIEncoded(new ABIValue("address", spender), new ABIValue("uint256", amount)).ToHex();
        return data;
    }
}