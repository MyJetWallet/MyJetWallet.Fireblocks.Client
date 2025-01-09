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

            var privateKey1 = await File.ReadAllTextAsync("API_Editor_secret.key");
            var apiKey1 = "d5a40f93-8cbf-49e3-aa2e-0a3d8e81b4e4";

            privateKey = privateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("-----END PRIVATE KEY-----", "");
            privateKey = privateKey.Replace("\r\n", "");



            var container = new ContainerBuilder();
            var config = new ClientConfigurator()
            {
                BaseUrl = "https://sandbox-api.fireblocks.io/v1",
                ApiKey = apiKey,
                ApiPrivateKey = privateKey
            };
            var config1 = new ClientConfigurator()
            {
                BaseUrl = "https://sandbox-api.fireblocks.io/v1",
                ApiKey = apiKey1,
                ApiPrivateKey = privateKey1
            };
            container.RegisterEmbeddedFireblocksClient(config, config1);//, new DelegateHandlerLogger());
            var provider = container.Build();

            var vaultClient = provider.Resolve<IVaultClient>();
            var accountsClient = provider.Resolve<IAccountsClient>();
            var client = provider.Resolve<IClient>();
            var transactionAdminClient = provider.Resolve<ITransactionsAdminClient>();
            var transactionSignerClient = provider.Resolve<ITransactionsSignerClient>();
            var gasStationClient = provider.Resolve<IGas_stationClient>();

            var embeddedAdminClient = provider.Resolve<IEmbeddedWalletAdminClient>();
            var embeddedSignerClient = provider.Resolve<IEmbeddedWalletSignerClient>();


            //var activator = provider.Resolve<KeyActivator>();
            //activator.ActivateKeys(apiKey, privateKey);

            // var accounts = await vaultClient.Accounts_pagedAsync();
            // Console.WriteLine($"Code: {accounts.StatusCode}; Count: {accounts.Result?.Accounts.Count}");

            Console.WriteLine();
            Console.WriteLine();

             var assetListAdmin = await embeddedAdminClient.AssetsGetSupportedAssetsListAsync(
                 new FewAssetGetSupportedAssetsListRequest()
                 {
                     OnlyBaseAssets = true,
                     PageCursor = "",
                     PageSize = 100
                 }, CancellationToken.None);

            var assetListSigner = await embeddedSignerClient.AssetsGetSupportedAssetsListAsync(
                new FewAssetGetSupportedAssetsListRequest()
                {
                    OnlyBaseAssets = true,
                    PageCursor = "",
                    PageSize = 100
                }, CancellationToken.None);

            Console.WriteLine($"AssetListAdmin Code: {assetListAdmin.StatusCode}; Count: {assetListAdmin.Result?.Data?.Count}; Padding: {assetListAdmin.Result?.Paging?.Next}");
            Console.WriteLine($"AssetListSigner Code: {assetListSigner.StatusCode}; Count: {assetListSigner.Result?.Data?.Count}; Padding: {assetListSigner.Result?.Paging?.Next}");

            Console.ReadLine();
        }
    }
}