using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IFewAccountsClient
    {
        Task<Response<FewAccountResponse>> CreateAccountAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewAccountResponse>> GetAccountByIdAsync(FewAccountRequest request, CancellationToken cancellationToken);

        Task<Response<FewAccountsGetListResponse>> GetAccountsListAsync(FewGetListRequest request, string walletId, CancellationToken cancellationToken);
    }
}
