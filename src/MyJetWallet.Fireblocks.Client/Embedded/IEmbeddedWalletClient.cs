using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IEmbeddedWalletClient
    {
        #region Wallets

        Task<Response<bool>> WalletsChangeIsEnableAsync(FewWalletsCnahgeEnabledRequest request, CancellationToken cancellationToken = default);
     
        Task<Response<FewWalletsWalletResponse>> WalletsCreateWalletAsync(CancellationToken cancellationToken = default);

        Task<Response<FewWalletsWalletResponse>> WalletsGetByIdAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewWalletsGetListResponse>> WalletsGetWalletsListAsync(FewGetWalletsListRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewWalletsGetKeySetupStateResponse>> WalletsGetWalletKeySetupStateAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken = default);
        
        Task<Response<FewWalletsGetLatestBackupDetailsResponse>> WalletsGetWalletLatestBackupDetailsAsync(FewWalletsGetByIdRequest request, CancellationToken cancellationToken = default);

        #endregion

        #region Accounts

        Task<Response<FewAccountResponse>> CreateAccountAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAccountResponse>> GetAccountByIdAsync(FewAccountRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAccountsGetListResponse>> GetAccountsListAsync(FewGetAccountsListRequest request, CancellationToken cancellationToken = default);

        #endregion

        #region Assets

        Task<Response<FewAssetsGetListResponse>> AssetsGetAssetsListAsync(FewAssetsGetListRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAsset>> AssetsGetAssetByIdAsync(FewAssetRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAssetBalance>> AssetsGetAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAssetAddressesGetListResponse>> AssetsGetAssetAddressesListAsync(FewAssetAddressesGetListRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAssetsGetListResponse>> AssetsGetSupportedAssetsListAsync(FewAssetGetSupportedAssetsListRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAssetAddress>> AssetsAddAssetAsync(FewAssetRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewAssetBalance>> AssetsRefreshAssetBalanceAsync(FewAssetRequest request, CancellationToken cancellationToken = default);

        #endregion

        #region Devices

        Task<Response<List<FewDeviceResponse>>> DevicesGetRegisteredDevicesAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken = default);

        Task<Response<FewDevicesGetKeySetupStateResponse>> DevicesGetDeviceKeySetupStateAsync(FewDeviceRequest request, CancellationToken cancellationToken = default);

        Task<Response<bool>> DevicesChangeDeviceIsEnableAsync(FewDevicesCnahgeEnabledRequest request, CancellationToken cancellationToken = default);

        #endregion

        #region RPC

        Task<Response<string>> RpcInvokeAsync(string walletId, string deviceId, FewRpcRequest request, CancellationToken cancellationToken = default);

        #endregion
    }
}
