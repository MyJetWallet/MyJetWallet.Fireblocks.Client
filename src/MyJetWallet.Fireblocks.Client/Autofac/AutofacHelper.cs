using System;
using Autofac;
using MyJetWallet.Fireblocks.Client.Auth;
using MyJetWallet.Fireblocks.Client.DelegateHandlers;
using MyJetWallet.Fireblocks.Client.Embedded;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MyJetWallet.Fireblocks.Client.Autofac
{
    public static class AutofacHelper
    {
        public static void RegisterFireblocksClient(this ContainerBuilder builder,
            ClientConfigurator clientConfigurator,
            params DelegatingHandler[] handlers)
        {
            var baseUrl = clientConfigurator.BaseUrl;
            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("clientConfigurator.BaseUrl cannot be empty");

            if (!baseUrl.Contains("/v1"))
                throw new Exception("clientConfigurator.BaseUrl should be with /v1");

            var keyActivator = new KeyActivator();

            var auth = new ApiKeyHeaderGenerator(clientConfigurator, new JwtTokenGenerator(clientConfigurator, keyActivator), keyActivator);
            var handlersWithAuth = new List<DelegatingHandler> { new AuthHandler(auth) };

            if (string.IsNullOrEmpty(clientConfigurator.ApiPrivateKey) &&
                string.IsNullOrEmpty(clientConfigurator.ApiKey))
                keyActivator.IsActivated = false;
            else
            {
                keyActivator.ActivateKeys(clientConfigurator.ApiKey, clientConfigurator.ApiPrivateKey);
            }

            if (handlers != null && handlers.Any())
            {
                handlersWithAuth.AddRange(handlers);
            }

            var httpClient = HttpClientFactory.Create(handlersWithAuth.ToArray());

            builder.RegisterInstance(keyActivator).SingleInstance();

            builder
                .RegisterInstance(new VaultClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IVaultClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AccountsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IAccountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AddressesClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IAddressesClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Client(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Internal_walletsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IInternal_walletsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new External_walletsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IExternal_walletsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Exchange_accountsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IExchange_accountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Fiat_accountsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IFiat_accountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TransactionsClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<ITransactionsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TxHashClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<ITxHashClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Gas_stationClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IGas_stationClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new WebhooksClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IWebhooksClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Off_exchangeClient(clientConfigurator, httpClient) { BaseUrl = baseUrl })
                .As<IOff_exchangeClient>()
                .AutoActivate()
                .SingleInstance();
        }

        public static void RegisterEmbeddedFireblocksClient(this ContainerBuilder builder,
            ClientConfigurator clientConfiguratorAdmin,
            ClientConfigurator clientConfiguratorSigner,
            params DelegatingHandler[] handlers)
        {
            var baseUrlAdmin = clientConfiguratorAdmin.BaseUrl;
            var baseUrlSigner = clientConfiguratorSigner.BaseUrl;

            var httpClientAdmin = CreateHttpClient(clientConfiguratorAdmin, handlers);
            var httpClientSigner = CreateHttpClient(clientConfiguratorSigner, handlers);

            builder
                .RegisterInstance(new VaultClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<IVaultClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new VaultClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<IVaultClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AccountsClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<IAccountsClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AccountsClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<IAccountsClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AddressesClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<IAddressesClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AddressesClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<IAddressesClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
              .RegisterInstance(new Client(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
              .As<IClientAdmin>()
              .AutoActivate()
              .SingleInstance();

            builder
              .RegisterInstance(new Client(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
              .As<IClientSigner>()
              .AutoActivate()
              .SingleInstance();

            builder
                .RegisterInstance(new TransactionsClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<ITransactionsClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TransactionsClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<ITransactionsClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TxHashClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<ITxHashClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TxHashClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<ITxHashClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new WebhooksClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<IWebhooksClientAdmin>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new WebhooksClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<IWebhooksClientSigner>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new EmbeddedWalletClient(clientConfiguratorAdmin, httpClientAdmin) { BaseUrl = baseUrlAdmin })
                .As<IEmbeddedWalletAdminClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new EmbeddedWalletClient(clientConfiguratorSigner, httpClientSigner) { BaseUrl = baseUrlSigner })
                .As<IEmbeddedWalletSignerClient>()
                .AutoActivate()
                .SingleInstance();
        }

        public static HttpClient CreateHttpClient(ClientConfigurator clientConfigurator, params DelegatingHandler[] handlers)
        {
            var baseUrl = clientConfigurator.BaseUrl;

            if (string.IsNullOrEmpty(baseUrl))
                throw new Exception("clientConfigurator.BaseUrl cannot be empty");

            if (!baseUrl.Contains("/v1"))
                throw new Exception("clientConfigurator.BaseUrl should be with /v1");

            var keyActivator = new KeyActivator();

            var auth = new ApiKeyHeaderGenerator(clientConfigurator, new JwtTokenGenerator(clientConfigurator, keyActivator), keyActivator);
            var handlersWithAuth = new List<DelegatingHandler> { new NonceErrorHandler(), new AuthHandler(auth) };

            keyActivator.ActivateKeys(clientConfigurator.ApiKey, clientConfigurator.ApiPrivateKey);

            if (handlers != null && handlers.Any())
            {
                handlersWithAuth.AddRange(handlers);
            }

            return HttpClientFactory.Create(handlersWithAuth.ToArray());
        }
    }
}
