using Autofac;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Auth;
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
            var body = "{"type":"TRANSACTION_STATUS_UPDATED","tenantId":"ab589064 - 56fc - 5c4a - aa95 - 1f30a6d2a83a","timestamp":1665066451990,"data":{"id":"ba536d52 - 1661 - 47a5 - 83e3 - 0db0b3243f81","createdAt":1665066259564,"lastUpdated":1665066441416,"assetId":"MATIC_POLYGON_MUMBAI","source":{"id":"396","type":"VAULT_ACCOUNT","name":"client_1dd81a7ec9d24cc9b285d17f5cf0aa68MATIC_POLYGON_MUMBAI","subType":""},"destination":{"id":null,"type":"ONE_TIME_ADDRESS","name":"N / A","subType":""},"amount":0,"networkFee":0.000163278019109403,"netAmount":0,"sourceAddress":"","destinationAddress":"0x294c79f1e431923917eece75283f473a6cabcc40","destinationAddressDescription":"","destinationTag":"","status":"COMPLETED","txHash":"0xe2cccfe024ab87e220d9b139f9cd59a53c6a830f5c5eebfd9b5d046948c7a34b","subStatus":"CONFIRMED","signedBy":[],"createdBy":"2e2f8d16 - 3bea - 52f5 - 8538 - cabda05d657c","rejectedBy":"","amountUSD":0,"addressType":"","note":"","exchangeTxId":"","requestedAmount":0,"feeCurrency":"MATIC_POLYGON_MUMBAI","operation":"CONTRACT_CALL","numOfConfirmations":3,"amountInfo":{"amount":"0","requestedAmount":"0","netAmount":"0","amountUSD":null},"feeInfo":{"networkFee":"0.000163278019109403"},"externalTxId":"acsettl_135_396_11_0","blockInfo":{"blockHeight":"28477718","blockHash":"0xf04a725d7e24fdb57cab13d5de6018f28375464257d6d3c09a62d44f9ba78258"},"networkRecords":[{"source":{"id":"396","type":"VAULT_ACCOUNT","name":"client_1dd81a7ec9d24cc9b285d17f5cf0aa68MATIC_POLYGON_MUMBAI","subType":""},"destination":{"id":null,"type":"ONE_TIME_ADDRESS","name":"N / A","subType":""},"txHash":"0xe2cccfe024ab87e220d9b139f9cd59a53c6a830f5c5eebfd9b5d046948c7a34b","networkFee":"0.000163278019109403","assetId":"SMPLT1_POLYGON_TEST","netAmount":"0","isDropped":false,"type":"CONTRACT_CALL","destinationAddress":"0x0000000000000000000000000000000000000000","amountUSD":null},{"source":{"id":"396","type":"VAULT_ACCOUNT","name":"client_1dd81a7ec9d24cc9b285d17f5cf0aa68MATIC_POLYGON_MUMBAI","subType":""},"destination":{"id":null,"type":"ONE_TIME_ADDRESS","name":"N / A","subType":""},"txHash":"0xe2cccfe024ab87e220d9b139f9cd59a53c6a830f5c5eebfd9b5d046948c7a34b","networkFee":"0.000163278019109403","assetId":"MATIC_POLYGON_MUMBAI","netAmount":"0.000000000000000000","isDropped":false,"type":"CONTRACT_CALL","destinationAddress":"0x294c79f1e431923917eece75283f473a6cabcc40","amountUSD":"0.00"}],"signedMessages":[],"extraParameters":{"contractCallData":"0x42842e0e000000000000000000000000b74369a799c8ec8664e6744d7f191f58f8a32d6600000000000000000000000052a6434213a99b03fe1f0c59b33d8ec088a9fb8d0000000000000000000000000000000000000000000000000000000000000009"}}}"
            var transaction = (Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookWithData<TransactionResponse>>(body)).Data;
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
            var allAccounts = await vaultClient.AccountsGetAsync();
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
            var guid = Guid.NewGuid().ToString();
            var estimateAsset = await client.Estimate_network_feeAsync(ethTestAsset.Id);

            {
                var z = Newtonsoft.Json.JsonConvert.SerializeObject(estimateAsset);
                Console.WriteLine($"{z}");
            }

            var transactionRequest = new TransactionRequest()
            {
                Amount = 0.01m,
                AssetId = ethTestAsset.Id,
                Source = new TransferPeerPath()
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
                ExternalTxId = guid,
                FailOnLowFee = false,
                FeeLevel = TransactionRequestFeeLevel.MEDIUM,
                Operation = TransactionOperation.TRANSFER,
                TreatAsGrossAmount = true,
            };
            var response = await transactionClient.Estimate_feeAsync(transactionRequest);
            
            var x1 = Newtonsoft.Json.JsonConvert.SerializeObject(response);
            Console.WriteLine($"{x}");
            
            var transaction = await client.TransactionsPostAsync(guid, transactionRequest);
            //transactionClient.

            //_client = new CircleClient(_accessToken);

            // await TestPublicKey();
            Console.ReadLine();
        }
    }
}