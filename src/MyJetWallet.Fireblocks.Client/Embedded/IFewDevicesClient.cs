using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IFewDevicesClient
    {
        Task<Response<List<FewDeviceResponse>>> GetRegisteredDevicesAsync(FewGetByWalletIdRequest request, CancellationToken cancellationToken);
        
        Task<Response<FewDevicesGetKeySetupStateResponse>> GetDeviceKeySetupStateAsync(FewDeviceRequest request, CancellationToken cancellationToken);

        Task<Response<bool>> ChangeDeviceIsEnableAsync(FewDevicesCnahgeEnabledRequest request, CancellationToken cancellationToken);
    }
}
