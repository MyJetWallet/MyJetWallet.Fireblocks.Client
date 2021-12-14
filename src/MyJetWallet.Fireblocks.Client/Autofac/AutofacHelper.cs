using Autofac;
using MyJetWallet.Fireblocks.Client.Auth;
using MyJetWallet.Fireblocks.Client.DelegateHandlers;
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
            var keyActivator = new KeyActivator();

            var auth = new ApiKeyHeaderGenerator(clientConfigurator, new JwtTokenGenerator(clientConfigurator, keyActivator), keyActivator);
            var handlersWithAuth = new List<DelegatingHandler> { new AuthHandler(auth) };

            if (string.IsNullOrEmpty(clientConfigurator.ApiPrivateKey) &&
                string.IsNullOrEmpty(clientConfigurator.ApiKey))
                keyActivator.IsActivated = false;

            if (handlers != null && handlers.Any())
            {
                handlersWithAuth.AddRange(handlers);
            }

            var httpClient = HttpClientFactory.Create(handlersWithAuth.ToArray());

            builder.RegisterInstance(keyActivator);

            builder
                .RegisterInstance(new VaultClient(clientConfigurator, httpClient))
                .As<IVaultClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AccountsClient(clientConfigurator, httpClient))
                .As<IAccountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new AddressesClient(clientConfigurator, httpClient))
                .As<IAddressesClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Client(clientConfigurator, httpClient))
                .As<IClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Internal_walletsClient(clientConfigurator, httpClient))
                .As<IInternal_walletsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new External_walletsClient(clientConfigurator, httpClient))
                .As<IExternal_walletsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Exchange_accountsClient(clientConfigurator, httpClient))
                .As<IExchange_accountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Fiat_accountsClient(clientConfigurator, httpClient))
                .As<IFiat_accountsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TransactionsClient(clientConfigurator, httpClient))
                .As<ITransactionsClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new TxHashClient(clientConfigurator, httpClient))
                .As<ITxHashClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Gas_stationClient(clientConfigurator, httpClient))
                .As<IGas_stationClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new WebhooksClient(clientConfigurator, httpClient))
                .As<IWebhooksClient>()
                .AutoActivate()
                .SingleInstance();

            builder
                .RegisterInstance(new Off_exchange_accountsClient(clientConfigurator, httpClient))
                .As<IOff_exchange_accountsClient>()
                .AutoActivate()
                .SingleInstance();
        }
    }
}
