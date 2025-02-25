using MyJetWallet.Fireblocks.Client.Embedded;
using System.Linq;

namespace MyJetWallet.Fireblocks.Client
{
    public partial class Client : IClientAdmin, IClientSigner
    {
    }

    public interface IClientAdmin : IClient { }
    public interface IClientSigner : IClient { }

    public partial class VaultClient : IVaultClientAdmin, IVaultClientSigner
    {
    }

    public interface IVaultClientAdmin : IVaultClient { }
    public interface IVaultClientSigner : IVaultClient { }

    public partial class AccountsClient : IAccountsClientAdmin, IAccountsClientSigner
    {
    }

    public interface IAccountsClientAdmin : IAccountsClient { }
    public interface IAccountsClientSigner : IAccountsClient { }

    public partial class AddressesClient : IAddressesClientAdmin, IAddressesClientSigner
    {
    }

    public interface IAddressesClientAdmin : IAddressesClient { }
    public interface IAddressesClientSigner : IAddressesClient { }

    public partial class TransactionsClient : ITransactionsClientAdmin, ITransactionsClientSigner
    {
    }

    public interface ITransactionsClientAdmin : ITransactionsClient { }
    public interface ITransactionsClientSigner : ITransactionsClient { }

    public partial class TxHashClient : ITxHashClientAdmin, ITxHashClientSigner
    {
    }

    public interface ITxHashClientAdmin : ITxHashClient { }
    public interface ITxHashClientSigner : ITxHashClient { }

    public partial class WebhooksClient : IWebhooksClientAdmin, IWebhooksClientSigner
    {
    }

    public interface IWebhooksClientAdmin : IWebhooksClient { }
    public interface IWebhooksClientSigner : IWebhooksClient { }


    public partial class ApiException<TResult> : ApiException
    {
        public override string ToString()
        {
            return string.Format("{0}\n\n{1}", Result.ToString(), base.ToString());
        }
    }

    public partial class ErrorSchema
    {
        public string ErrorCode
        {
            get
            {
                if (AdditionalProperties?.ContainsKey("error") ?? false)
                    return AdditionalProperties["error"].ToString();

                return null;
            }
        }
        public override
            string ToString()
        {
            return $"ErrorSchema: {{ Message: {Message}, Code: {Code}, ErrorCode: {ErrorCode} }}";
        }
    }
}

