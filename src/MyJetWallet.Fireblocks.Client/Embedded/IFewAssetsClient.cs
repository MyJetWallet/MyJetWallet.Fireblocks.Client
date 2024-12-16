using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IFewAssetsClient
    {
        Task<Response<FewAssetsGetListResponse>> GetAssetsListAsync(FewAssetsGetListRequest request, CancellationToken cancellationToken);
        
        Task<Response<FewAsset>> GetAssetByIdAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetBalance>> GetAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetAddressesGetListResponse>> GetAssetAddressesListAsync(FewAssetAddressesGetListRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetsGetListResponse>> GetSupportedAssetsListAsync(FewAssetGetSupportedAssetsListRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetAddress>> AddAssetAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetBalance>> RefreshAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken);
    }
}
