using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IEmbeddedWalletClient
    {
        #region Wallets

        Task<Response<bool>> WalletsChangeIsEnableAsync(FewWalletsCnahgeEnabledRequest request, CancellationToken cancellationToken);
     
        Task<Response<FewWalletsWalletResponse>> WalletsCreateWalletAsync(CancellationToken cancellationToken);

        Task<Response<FewWalletsWalletResponse>> WalletsGetByIdAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewWalletsGetListResponse>> WalletsGetWalletsListAsync(FewGetWalletsListRequest request, CancellationToken cancellationToken);

        Task<Response<FewWalletsGetKeySetupStateResponse>> WalletsGetWalletKeySetupStateAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);
        
        Task<Response<FewWalletsGetLatestBackupDetailsResponse>> WalletsGetWalletLatestBackupDetailsAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken);

        #endregion

        #region Accounts

        Task<Response<FewAccountResponse>> CreateAccountAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewAccountResponse>> GetAccountByIdAsync(FewAccountRequest request, CancellationToken cancellationToken);

        Task<Response<FewAccountsGetListResponse>> GetAccountsListAsync(FewGetAccountsListRequest request, CancellationToken cancellationToken);

        #endregion

        #region Assets

        Task<Response<FewAssetsGetListResponse>> AssetsGetAssetsListAsync(FewAssetsGetListRequest request, CancellationToken cancellationToken);

        Task<Response<FewAsset>> AssetsGetAssetByIdAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetBalance>> AssetsGetAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetAddressesGetListResponse>> AssetsGetAssetAddressesListAsync(FewAssetAddressesGetListRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetsGetListResponse>> AssetsGetSupportedAssetsListAsync(FewAssetGetSupportedAssetsListRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetAddress>> AssetsAddAssetAsync(FewAssetRequest request, CancellationToken cancellationToken);

        Task<Response<FewAssetBalance>> AssetsRefreshAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken);

        #endregion

        #region Devices

        Task<Response<List<FewDeviceResponse>>> DevicesGetRegisteredDevicesAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken);

        Task<Response<FewDevicesGetKeySetupStateResponse>> DevicesGetDeviceKeySetupStateAsync(FewDeviceRequest request, CancellationToken cancellationToken);

        Task<Response<bool>> DevicesChangeDeviceIsEnableAsync(FewDevicesCnahgeEnabledRequest request, CancellationToken cancellationToken);

        #endregion

        #region RPC

        Task<Response<string>> RpcInvokeAsync(string walletId, string deviceId, FewRpcRequest request, CancellationToken cancellationToken = default);

        #endregion
    }
}
