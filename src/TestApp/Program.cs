using Autofac;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.DelegateHandlers;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestApp
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            var privateKey = await File.ReadAllTextAsync(@"C:\Users\O1\Desktop\fireblocks\fireblocks_secret.key");
            privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("-----END PRIVATE KEY-----", "");
            var publicKey = await File.ReadAllTextAsync(@"C:\Users\O1\Desktop\fireblocks\fireblocks_api_key");
            var container = new ContainerBuilder();
            var config = new ClientConfigurator()
            {
                ApiPrivateKey = privateKey,
                ApiKey = publicKey,
                BaseUrl = "https://api.fireblocks.io/v1",
            };
            container.RegisterFireblocksClient(config, new DelegateHandlerLogger());
            var provider = container.Build();
            var vaultClient = provider.Resolve<IVaultClient>();
            var accountsClient = provider.Resolve<IAccountsClient>();
            var client = provider.Resolve<IClient>();
            var transactionClient = provider.Resolve<ITransactionsClient>();

            var supportedAssets = await client.Supported_assetsAsync();

            foreach (var asset in supportedAssets.Result)
            {
                Console.WriteLine($"{asset.Id} {asset.Name}");
            }

            var ethTestAsset = supportedAssets.Result.First(x => x.Id == "ETH_TEST");

            //var vaultCreateResponse = await vaultClient.AccountsPostAsync(new Body { Name = Guid.NewGuid().ToString() });
            var allAccounts = await vaultClient.AccountsGetAsync();
            var vaultAccountId = "3";

            //var walletCreate = await vaultClient.AccountsPostAsync(vaultAccountId, ethTestAsset.Id, new Body5()
            //{
            //});

            var wallet = await vaultClient.AccountsGetAsync(vaultAccountId, ethTestAsset.Id, default);
            var address = await accountsClient.AddressesGetAsync(vaultAccountId, ethTestAsset.Id, default);

            //var createAddressRequestИес = await accountsClient.AddressesPostAsync(vaultAccountId, "BTC_TEST", new Body6
            //{
            //    Description = "Created for test",
            //});
            //var createAddressRequest = await accountsClient.AddressesPostAsync(vaultAccountId, ethTestAsset.Id, new Body6
            //{
            //    Description = "Created for test",
            //});

            double amount = 0;
            while (amount == 0)
            {
                var acc = await vaultClient.AccountsGetAsync(vaultAccountId, default);
                var available = acc.Result.Assets.FirstOrDefault(x => x.Id == ethTestAsset.Id)?.Available;

                if (string.IsNullOrEmpty(available))
                {
                    Task.Delay(1000).Wait();
                    continue;
                }

                amount = double.Parse(available);
                break;
            }

            var transaction = await client.TransactionsPostAsync(new TransactionRequest()
            {
                Amount = amount,
                AssetId = ethTestAsset.Id,
                Source = new TransferPeerPath()
                {
                    Id = vaultAccountId,
                    Type = TransferPeerPathType.VAULT_ACCOUNT
                },
                Destination = new DestinationTransferPeerPath()
                {
                    Type = TransferPeerPathType.ONE_TIME_ADDRESS,
                    OneTimeAddress = new OneTimeAddress()
                    {
                        Address = "0x1Eab7d412a25a5d00Ec3d04648aa54CeA4aB7e94"
                    },
                },
                FailOnLowFee = false,
                FeeLevel = TransactionRequestFeeLevel.MEDIUM,
                Operation = TransactionOperation.TRANSFER,
                TreatAsGrossAmount = true,

            });
            //transactionClient.

            //_client = new CircleClient(_accessToken);

            // await TestPublicKey();
            Console.ReadLine();
        }
    }
}