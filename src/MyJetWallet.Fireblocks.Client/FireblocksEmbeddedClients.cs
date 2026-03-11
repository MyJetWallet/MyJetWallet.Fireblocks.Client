using MyJetWallet.Fireblocks.Client.Autofac;
using MyJetWallet.Fireblocks.Client.Embedded;

namespace MyJetWallet.Fireblocks.Client
{
    public class FireblocksEmbeddedClients
    {
        public IVaultClientAdmin VaultAdmin { get; }
        public IVaultClientSigner VaultSigner { get; }
        public IAccountsClientAdmin AccountsAdmin { get; }
        public IAccountsClientSigner AccountsSigner { get; }
        public IAddressesClientAdmin AddressesAdmin { get; }
        public IAddressesClientSigner AddressesSigner { get; }
        public IClientAdmin ClientAdmin { get; }
        public IClientSigner ClientSigner { get; }
        public ITransactionsClientAdmin TransactionsAdmin { get; }
        public ITransactionsClientSigner TransactionsSigner { get; }
        public ITxHashClientAdmin TxHashAdmin { get; }
        public ITxHashClientSigner TxHashSigner { get; }
        public IWebhooksClientAdmin WebhooksAdmin { get; }
        public IWebhooksClientSigner WebhooksSigner { get; }
        public IEmbeddedWalletAdminClient EmbeddedWalletAdmin { get; }
        public IEmbeddedWalletSignerClient EmbeddedWalletSigner { get; }

        public FireblocksEmbeddedClients(ClientConfigurator adminConfig, ClientConfigurator signerConfig)
        {
            var baseUrlAdmin  = adminConfig.BaseUrl;
            var baseUrlSigner = signerConfig.BaseUrl;
            var httpAdmin  = AutofacHelper.CreateHttpClient(adminConfig);
            var httpSigner = AutofacHelper.CreateHttpClient(signerConfig);

            VaultAdmin           = new VaultClient(adminConfig, httpAdmin)           { BaseUrl = baseUrlAdmin };
            VaultSigner          = new VaultClient(signerConfig, httpSigner)         { BaseUrl = baseUrlSigner };
            AccountsAdmin        = new AccountsClient(adminConfig, httpAdmin)        { BaseUrl = baseUrlAdmin };
            AccountsSigner       = new AccountsClient(signerConfig, httpSigner)      { BaseUrl = baseUrlSigner };
            AddressesAdmin       = new AddressesClient(adminConfig, httpAdmin)       { BaseUrl = baseUrlAdmin };
            AddressesSigner      = new AddressesClient(signerConfig, httpSigner)     { BaseUrl = baseUrlSigner };
            ClientAdmin          = new Client(adminConfig, httpAdmin)                { BaseUrl = baseUrlAdmin };
            ClientSigner         = new Client(signerConfig, httpSigner)              { BaseUrl = baseUrlSigner };
            TransactionsAdmin    = new TransactionsClient(adminConfig, httpAdmin)    { BaseUrl = baseUrlAdmin };
            TransactionsSigner   = new TransactionsClient(signerConfig, httpSigner)  { BaseUrl = baseUrlSigner };
            TxHashAdmin          = new TxHashClient(adminConfig, httpAdmin)          { BaseUrl = baseUrlAdmin };
            TxHashSigner         = new TxHashClient(signerConfig, httpSigner)        { BaseUrl = baseUrlSigner };
            WebhooksAdmin        = new WebhooksClient(adminConfig, httpAdmin)        { BaseUrl = baseUrlAdmin };
            WebhooksSigner       = new WebhooksClient(signerConfig, httpSigner)      { BaseUrl = baseUrlSigner };
            EmbeddedWalletAdmin  = new EmbeddedWalletClient(adminConfig, httpAdmin)  { BaseUrl = baseUrlAdmin };
            EmbeddedWalletSigner = new EmbeddedWalletClient(signerConfig, httpSigner){ BaseUrl = baseUrlSigner };
        }
    }
}
