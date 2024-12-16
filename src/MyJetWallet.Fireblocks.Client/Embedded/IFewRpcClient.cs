using MyJetWallet.Fireblocks.Client.Embedded.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Embedded
{
    public interface IFewRpcClient
    {
        Task<Response<FewRpcResponse>> InvokeRpcAsync(FewRpcRequest request, CancellationToken cancellationToken);
    }
}
