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

        #region Transactions

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Create a new transaction
        /// </summary>
        /// <remarks>
        /// Creates a new transaction. This endpoint can be used for regular Transfers, Contract Calls, Raw &amp; Typed message signing.
        /// <br/>- For Transfers, the required parameters are: `assetId`, `source`, `destination` and `amount`.
        /// <br/>
        /// <br/>- For Contract Calls, the required parameters are: `operation.CONTRACT_CALL`, `assetId` (Base Asset), `source`, `destination`, `amount` (usually 0) and `extraParameters` object with `contractCallData` string.
        /// <br/>
        /// <br/>- For RAW and Typed messages signing, the required parameters are: `operation.RAW/TYPED_MESSAGE`, `assetId` or `derivationPath`, `source` or `derivationPath`, `extraParameters` with [rawMessageData object](https://developers.fireblocks.com/reference/raw-signing-objects).
        /// <br/>
        /// <br/>- Typed Message Signing is supported for the following asset IDs: 'ETH', 'BTC' and 'TRX'. [Typed Message Signing Guide](https://developers.fireblocks.com/docs/typed-message-signing-overview).
        /// <br/>
        /// <br/>- For MEV Protection configuration the required parameters are:
        /// <br/>  `extraParameters` with the [`nodeControls` object](https://developers.fireblocks.com/reference/transaction-objects#nodecontrols)
        /// <br/>  Note: MEV Protection is a premium feature. Please contact your Customer Success Manager or the Fireblocks Support team for more information.
        /// </remarks>
        /// <param name="x_End_User_Wallet_Id">Unique ID of the End-User wallet to the API request. Required for end-user wallet operations.</param>
        /// <param name="idempotency_Key">A unique identifier for the request. If the request is sent multiple times with the same idempotency key, the server will return the same response as the first request. The idempotency key is valid for 24 hours.</param>
        /// <returns>A transaction object</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        Task<Response<CreateTransactionResponse>> TransactionsPostAsync(TransactionRequest body, System.Guid? x_End_User_Wallet_Id = null, string idempotency_Key = null, CancellationToken cancellationToken = default);

        #endregion

        #region RPC

        Task<Response<string>> RpcInvokeAsync(string walletId, string deviceId, FewRpcRequest request, CancellationToken cancellationToken = default);

        #endregion
    }
}
