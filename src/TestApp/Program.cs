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
using Newtonsoft.Json;

namespace TestApp
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            //var body = "";
            //var transaction = (Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookWithData<TransactionResponse>>(body)).Data;

            var admimPrivateKey = await File.ReadAllTextAsync("test_API_EW_Admin_secret.key");
            var adminApiKey = "b4fcd05f-0ebc-44ac-9e60-808c7d3d012e";

            var signerPrivateKey = await File.ReadAllTextAsync("test_API_EW_Signer_secret.key");
            var signerApiKey = "ea2bed5d-ca4c-4ff1-981b-89d422bf8fb0";
            

            admimPrivateKey = admimPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            admimPrivateKey = admimPrivateKey.Replace("-----END PRIVATE KEY-----", "");
            admimPrivateKey = admimPrivateKey.Replace("\r\n", "");
            
            signerPrivateKey = signerPrivateKey.Replace("-----BEGIN PRIVATE KEY-----", "");
            signerPrivateKey = signerPrivateKey.Replace("-----END PRIVATE KEY-----", "");
            signerPrivateKey = signerPrivateKey.Replace("\r\n", "");

            //await Console.Out.WriteLineAsync("Admin key:");
            //await Console.Out.WriteLineAsync(admimPrivateKey);
            //await Console.Out.WriteLineAsync();
            //await Console.Out.WriteLineAsync("Signer key:");
            //await Console.Out.WriteLineAsync(signerPrivateKey);

            var container = new ContainerBuilder();
            var adminConfig = new ClientConfigurator()
            {
                BaseUrl = "https://sandbox-api.fireblocks.io/v1",
                ApiKey = adminApiKey,
                ApiPrivateKey = admimPrivateKey
            };
            var signerConfig = new ClientConfigurator()
            {
                BaseUrl = "https://sandbox-api.fireblocks.io/v1",
                ApiKey = signerApiKey,
                ApiPrivateKey = signerPrivateKey
            };

            container.RegisterEmbeddedFireblocksClient(adminConfig, signerConfig); //, new DelegateHandlerLogger());
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

            Console.WriteLine("WalletsGetByIdAsync:");
            var wallet = await embeddedAdminClient.WalletsGetByIdAsync(new FewWalletsGetByIdRequest()
            {
                WalletId = "da24b489-dc68-460b-b0e8-0fcbfe5624fd"
            }, default);
            
            Console.WriteLine(JsonConvert.SerializeObject(wallet.Result, Formatting.Indented));

            Console.WriteLine("WalletsGetWalletKeySetupStateAsync:");
            var walletSetupKeyState = await embeddedAdminClient.WalletsGetWalletKeySetupStateAsync(new FewWalletsGetByIdRequest()
            {
                WalletId = "da24b489-dc68-460b-b0e8-0fcbfe5624fd"
            }, default);
            
            Console.WriteLine(JsonConvert.SerializeObject(walletSetupKeyState.Result, Formatting.Indented));

            // Console.WriteLine("CreateAccountAsync:");
            // var acountCreateResp = await embeddedAdminClient.CreateAccountAsync(new FewGetByWalletIdRequest()
            // {
            //     WalletId = "da24b489-dc68-460b-b0e8-0fcbfe5624fd"
            // }, default);
            //
            // Console.WriteLine(JsonConvert.SerializeObject(acountCreateResp, Formatting.Indented));
            
            
            Console.WriteLine("GetAccountsListAsync:");
            var accountList = await embeddedAdminClient.GetAccountByIdAsync(new FewAccountRequest()
            {
                WalletId = "da24b489-dc68-460b-b0e8-0fcbfe5624fd",
                AccountId = "1"
            }, default);
            
            Console.WriteLine(JsonConvert.SerializeObject(accountList.Result, Formatting.Indented));


            var payload1 = @"{""method"":""get_service_certificates"",""params"":[{""names"":[""policy_service"",""signing_service"",""zona_service"",""nckms"",""ncw-service""]}],""headers"":{""physicalDeviceId"":""f88b3a5b-631a-4e42-9b68-39bef0fce0f8"",""mpcVersion"":""6"",""platformType"":""iOS"",""sdkVersion"":""2.9.1""}}";
            
            Console.WriteLine($"RPC 1\n{payload1}");

            var recCall1 = await embeddedSignerClient.RpcInvokeAsync(
                walletId: "da24b489-dc68-460b-b0e8-0fcbfe5624fd",
                deviceId: "b75d679b-6583-4f8f-8048-d0da40f92738",
                request: new FewRpcRequest()
                {
                    Payload = payload1
                }); 
            
            Console.WriteLine(JsonConvert.SerializeObject(recCall1, Formatting.Indented));

            // var resp = await embeddedAdminClient.WalletsGetWalletKeySetupStateAsync(new FewWalletsGetByIdRequest()
            // {
            //     WalletId = "cfb579d4-16e5-4724-8613-e8c8147bd2ec"
            // }, default);
            //
            // var data = resp.Result;
            //
            // Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            //  var assetListAdmin = await embeddedAdminClient.AssetsGetSupportedAssetsListAsync(
            //      new FewAssetGetSupportedAssetsListRequest()
            //      {
            //          OnlyBaseAssets = true,
            //          PageCursor = "",
            //          PageSize = 100
            //      }, CancellationToken.None);
            //
            // var assetListSigner = await embeddedSignerClient.AssetsGetSupportedAssetsListAsync(
            //     new FewAssetGetSupportedAssetsListRequest()
            //     {
            //         OnlyBaseAssets = true,
            //         PageCursor = "",
            //         PageSize = 100
            //     }, CancellationToken.None);
            //
            // Console.WriteLine($"AssetListAdmin Code: {assetListAdmin.StatusCode}; Count: {assetListAdmin.Result?.Data?.Count}; Padding: {assetListAdmin.Result?.Paging?.Next}");
            // Console.WriteLine($"AssetListSigner Code: {assetListSigner.StatusCode}; Count: {assetListSigner.Result?.Data?.Count}; Padding: {assetListSigner.Result?.Paging?.Next}");

            Console.ReadLine();
        }
    }
}