using Autofac;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Auth;
using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.DelegateHandlers;
using MyJetWallet.Fireblocks.Client.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MyJetWallet.Fireblocks.Client.Embedded;
using MyJetWallet.Fireblocks.Client.Embedded.Models;

namespace TestApp
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            //var body = "";
            //var transaction = (Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookWithData<TransactionResponse>>(body)).Data;

            var privateKey = await File.ReadAllTextAsync("api_test_secret.key");
            var apiKey = "b4fcd05f-0ebc-44ac-9e60-808c7d3d012e";

            // var privateKey = await File.ReadAllTextAsync("API_Editor_secret.key");
            // var apiKey = "d5a40f93-8cbf-49e3-aa2e-0a3d8e81b4e4";

            privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("-----END PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("\r\n", "");



            var container = new ContainerBuilder();
            var config = new ClientConfigurator()
            {
                BaseUrl = "https://sandbox-api.fireblocks.io/v1",
            };
            container.RegisterFireblocksClient(config, new DelegateHandlerLogger());
            var provider = container.Build();

            var vaultClient = provider.Resolve<IVaultClient>();
            var accountsClient = provider.Resolve<IAccountsClient>();
            var client = provider.Resolve<IClient>();
            var transactionClient = provider.Resolve<ITransactionsClient>();
            var gasStationClient = provider.Resolve<IGas_stationClient>();

            var embeddedClient = provider.Resolve<IEmbeddedWalletClient>();


            var activator = provider.Resolve<KeyActivator>();


            activator.ActivateKeys(apiKey, privateKey);

            // var accounts = await vaultClient.Accounts_pagedAsync();
            // Console.WriteLine($"Code: {accounts.StatusCode}; Count: {accounts.Result?.Accounts.Count}");

            Console.WriteLine();
            Console.WriteLine();

             var assetList = await embeddedClient.AssetsGetSupportedAssetsListAsync(
                 new FewAssetGetSupportedAssetsListRequest()
                 {
                     OnlyBaseAssets = true,
                     PageCursor = "",
                     PageSize = 100
                 }, CancellationToken.None);

            Console.WriteLine($"Code: {assetList.StatusCode}; Count: {assetList.Result?.Data?.Count}; Padding: {assetList.Result?.Paging?.Next}");

            Console.ReadLine();
        }
    }
}