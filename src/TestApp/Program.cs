﻿using Autofac;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Auth;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.DelegateHandlers;
using MyJetWallet.Fireblocks.Client.Extensions;
using System;
using System.Globalization;
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
            //var body = "";
            //var transaction = (Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookWithData<TransactionResponse>>(body)).Data;
            var privateKey = await File.ReadAllTextAsync(@"C:\Git\fireblocks-uat\fireblocks_secret.key");
            //var privateKey = await File.ReadAllTextAsync(@"D:\fireblocks uat\fireblocks_secret.key");
            privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("-----END PRIVATE KEY-----", "");
            //var publicKey = await File.ReadAllTextAsync(@"D:\fireblocks uat\fireblocks_api_key");
            var publicKey = await File.ReadAllTextAsync(@"C:\Git\fireblocks-uat\fireblocks_api_key");
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
            var gasStationClient = provider.Resolve<IGas_stationClient>();
            var activator = provider.Resolve<KeyActivator>();

            activator.ActivateKeys(publicKey, privateKey);

            var x = await accountsClient.AddressesGetAsync("11", "MATIC_POLYGON_MUMBAI", default);

            //var internalWallets = await client.Internal_walletsGetAsync();

            //foreach (var wallet in internalWallets.Result)
            //{
            //    //Console.WriteLine($"{asset.Id} {asset.Name}");
            //    var x = Newtonsoft.Json.JsonConvert.SerializeObject(wallet);
            //    Console.WriteLine($"{x}");
            //}

            //var gasStation = await client.Gas_stationAsync();

            //var z = Newtonsoft.Json.JsonConvert.SerializeObject(gasStation);
            //Console.WriteLine($"{z}");

            //1644657961499
            //1644493762755
            //var currentUnixTime = DateTimeOffset.UtcNow.AddDays(-40).ToUnixTimeMilliseconds();

            //var currentUnixTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            //do
            //{
            //    var transactions = await client.TransactionsGetAsync(
            //    after: currentUnixTime.ToString(),
            //    before: currentUnixTime.ToString(),
            //    orderBy: OrderBy.CreatedAt,
            //    status: "COMPLETED",
            //    limit: 200);

            //    currentUnixTime = transactions.Result.Any() ? transactions.Result.Last().CreatedAt - 1 : long.MaxValue;

            //    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(transactions));
            //} while (currentUnixTime != long.MaxValue);

            //var setGas = await gasStationClient.ConfigurationAsync(new()
            //{
            //    GasCap = "0.01",
            //    GasThreshold = "0.005",
            //    MaxGasPrice = "0,000004"
            //});

            var supportedAssets = await client.Supported_assetsAsync();

            foreach (var asset in supportedAssets.Result)
            {
                //Console.WriteLine($"{asset.Id} {asset.Name}");
                var r = Newtonsoft.Json.JsonConvert.SerializeObject(asset);
                Console.WriteLine($"{r}");
                try
                {
                    var estimateAsset1 = await client.Estimate_network_feeAsync(asset.Id);
                    {
                        var z = Newtonsoft.Json.JsonConvert.SerializeObject(asset.Id);
                        Console.WriteLine($"{z}");
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var ethTestAsset = supportedAssets.Result.First(x => x.Id == "ETH_TEST");

            //var vaultCreateResponse = await vaultClient.AccountsPostAsync(new Body { Name = Guid.NewGuid().ToString() });
            //var allAccounts = await vaultClient.AccountsGetAsync();
            var vaultAccountId = "11";

            //var walletCreate = await vaultClient.AccountsPostAsync("1", vaultAccountId, ethTestAsset.Id, new Body5()
            //{
            //});

            //var wallet = await vaultClient.AccountsGetAsync(vaultAccountId, ethTestAsset.Id, default);
            //var address = await accountsClient.AddressesGetAsync(vaultAccountId, ethTestAsset.Id, default);

            //var createAddressRequestИес = await accountsClient.AddressesPostAsync(vaultAccountId, "BTC_TEST", new Body6
            //{
            //    Description = "Created for test",
            //});
            //var createAddressRequest = await accountsClient.AddressesPostAsync(vaultAccountId, ethTestAsset.Id, new Body6
            //{
            //    Description = "Created for test",
            //});

            decimal amount = 0;
            //while (amount == 0)
            //{
            //    var acc = await vaultClient.AccountsGetAsync(vaultAccountId, default);
            //    var available = acc.Result.Assets.FirstOrDefault(x => x.Id == ethTestAsset.Id)?.Available;

            //    if (string.IsNullOrEmpty(available))
            //    {
            //        Task.Delay(1000).Wait();
            //        continue;
            //    }

            //    amount = decimal.Parse(available);
            //    break;
            //}
            Console.WriteLine();
            var guid = Guid.NewGuid();
            var estimateAsset = await client.Estimate_network_feeAsync(ethTestAsset.Id);

            {
                var z = Newtonsoft.Json.JsonConvert.SerializeObject(estimateAsset);
                Console.WriteLine($"{z}");
            }

            var transactionRequest = new TransactionRequest()
            {
                Amount = 0.01m.TryConvertToString(),
                AssetId = ethTestAsset.Id,
                Source = new SourceTransferPeerPath()
                {
                    Id = vaultAccountId,
                    Type = TransferPeerPathType.VAULT_ACCOUNT
                },
                Destination = new DestinationTransferPeerPath()
                {
                    Type = TransferPeerPathType.ONE_TIME_ADDRESS,
                    //Id = "4",
                    OneTimeAddress = new OneTimeAddress()
                    {
                        Address = "0x83ceAC6A4b7060348d8Ebf4996817962Db7e3758",
                        Tag = ""
                    },
                },
                ExternalTxId = guid.ToString(),
                FailOnLowFee = false,
                FeeLevel = TransactionRequestFeeLevel.MEDIUM,
                Operation = TransactionOperation.TRANSFER,
                TreatAsGrossAmount = true,
            };
            var response = await transactionClient.Estimate_feeAsync(null, transactionRequest);
            
            var x1 = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            Console.WriteLine($"{x}");
            
            var transaction = await client.TransactionsPostAsync(transactionRequest, guid);
            //transactionClient.

            //_client = new CircleClient(_accessToken);

            // await TestPublicKey();
            Console.ReadLine();
        }
    }
}