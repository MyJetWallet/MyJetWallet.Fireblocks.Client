using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IFewWalletsClient
    {
        Task<Response<bool>> ChangeWalletIsEnableAsync(FewWalletsCnahgeEnabledRequest request, CancellationToken cancellationToken);
     
        Task<Response<FewWalletsWalletResponse>> CreateWalletAsync(CancellationToken cancellationToken);

        Task<Response<FewWalletsWalletResponse>> GetWalletByIdAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewWalletsGetListResponse>> GetWalletsListAsync(FewGetListRequest request, CancellationToken cancellationToken);

        Task<Response<FewWalletsGetLatestBackupDetailsResponse>> GetWalletLatestBackupDetailsAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewWalletsGetKeySetupStateResponse>> GetWalletKeySetupStateAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);

    }
}
