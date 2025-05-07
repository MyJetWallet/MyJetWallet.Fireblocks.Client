// See https://aka.ms/new-console-template for more information

using Autofac;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.Embedded;
using MyJetWallet.Fireblocks.Client.Embedded.Models;
using Nethereum.ABI;
using Nethereum.Hex.HexConvertors.Extensions;
using Newtonsoft.Json;
using TestAllowTransaction;


var admimPrivateKey = await File.ReadAllTextAsync("test_Key_EW_Admin_secret.key");
var adminApiKey = "30--2f";

var signerPrivateKey = await File.ReadAllTextAsync("test_Key_EW_Signer_secret.key");
var signerApiKey = "27--91c";


admimPrivateKey = admimPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
admimPrivateKey = admimPrivateKey.Replace("-----END PRIVATE KEY-----", "");
admimPrivateKey = admimPrivateKey.Replace("\r\n", "");
admimPrivateKey = admimPrivateKey.Replace("\n", "");

signerPrivateKey = signerPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
signerPrivateKey = signerPrivateKey.Replace("-----END PRIVATE KEY-----", "");
signerPrivateKey = signerPrivateKey.Replace("\r\n", "");
signerPrivateKey = signerPrivateKey.Replace("\n", "");


var container = new ContainerBuilder();
var adminConfig = new ClientConfigurator()
{
    //BaseUrl = "https://sandbox-api.fireblocks.io/v1",
    BaseUrl = "https://api.fireblocks.io/v1",
    ApiKey = adminApiKey,
    ApiPrivateKey = admimPrivateKey
};
var signerConfig = new ClientConfigurator()
{
    //BaseUrl = "https://sandbox-api.fireblocks.io/v1",
    BaseUrl = "https://api.fireblocks.io/v1",
    ApiKey = signerApiKey,
    ApiPrivateKey = signerPrivateKey
};

container.RegisterEmbeddedFireblocksClient(adminConfig, signerConfig); //, new DelegateHandlerLogger());
var provider = container.Build();

var vaultClient = provider.Resolve<IVaultClientAdmin>();
var accountsClient = provider.Resolve<IAccountsClientAdmin>();
//var client = provider.Resolve<IClient>();
var transactionAdminClient = provider.Resolve<ITransactionsClientAdmin>();
var transactionSignerClient = provider.Resolve<ITransactionsClientSigner>();

var embeddedAdminClient = provider.Resolve<IEmbeddedWalletAdminClient>();
var embeddedSignerClient = provider.Resolve<IEmbeddedWalletSignerClient>();
var clienAdmin = provider.Resolve<IClientAdmin>();
var clientSigner = provider.Resolve<IClientSigner>();


var walletId = "6e379c98-3640-4860-a715-f41198f07421";
            
var walletResp = await embeddedAdminClient.WalletsGetByIdAsync(new FewWalletsGetByIdRequest()
{
    WalletId = walletId
});

Console.WriteLine(walletResp.StatusCode);
Console.WriteLine(walletResp.Result?.WalletId);
Console.WriteLine(walletResp.Result?.Enabled);
Console.WriteLine();

var addressResp = await embeddedAdminClient.AssetsGetAssetAddressesListAsync(new FewAssetAddressesGetListRequest()
{
    WalletId = walletId,
    AccountId = "0",
    AssetId = "MATIC_POLYGON",
    PageCursor = "",
    PageSize = 10
});


Console.WriteLine(addressResp.StatusCode);
Console.WriteLine(JsonConvert.SerializeObject(addressResp.Result));
Console.WriteLine();

var key = $"test_alex_001_{DateTime.UtcNow.TimeOfDay.TotalSeconds}";
Console.WriteLine($"Key: {key}");
Console.WriteLine();


var data = Erc20FunctionDataBuilder.BuildUsdcPolygonApproveData("0x06Ee0E909F0279CFe2794A6590527ca3E6b73FFB", 1000_000000);

var transactionResp = await clientSigner.TransactionsPostAsync(new TransactionRequest()
{
    Source = new SourceTransferPeerPath()
    {
        Type = TransferPeerPathType.END_USER_WALLET,
        WalletId = Guid.Parse(walletId),
        Id = "0"
    },
    Destination = new DestinationTransferPeerPath()
    {
        Type = TransferPeerPathType.ONE_TIME_ADDRESS,
        OneTimeAddress = new OneTimeAddress()
        {
            Address = Erc20FunctionDataBuilder.UsdcPolygonContractAddress
        }
    },
    Amount = "0",
    AssetId = "MATIC_POLYGON",
    ExtraParameters = new ExtraParameters()
    {
        ContractCallData = data,
    }, 
    Operation = TransactionOperation.CONTRACT_CALL,
    ExternalTxId = key,
    FeeLevel = TransactionRequestFeeLevel.HIGH
}, Guid.Parse(walletId), key);

Console.WriteLine("Transaction:");
Console.WriteLine(transactionResp.StatusCode);
Console.WriteLine(transactionResp.Result?.Id);
Console.WriteLine(transactionResp.Result?.Status);
Console.WriteLine();


Console.WriteLine("Press enter to cancel");
Console.ReadLine();


// var cancelResp = await transactionSignerClient.CancelAsync(transactionResp.Result?.Id, Guid.Parse(walletId));
//
// //var cancelResp = await transactionSignerClient.CancelAsync("3ec21661-700e-4955-9ee1-25de7aa68512", Guid.Parse(walletId));
// Console.WriteLine("Cancel:");
// Console.WriteLine(cancelResp.StatusCode);
// Console.WriteLine(cancelResp.Result?.Success);
// Console.WriteLine();

