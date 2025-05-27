using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MyJetWallet.Fireblocks.Client.Auth;

namespace MyJetWallet.Fireblocks.Client.DelegateHandlers;

internal class NonceErrorHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    {
        var index = 0;
        do
        {
            index++;


            request.Headers.Authorization = null;
            request.Headers.Remove(ApiKeyHeaderGenerator.ApiKeyHeader);
            
            var response = await base.SendAsync(request, cancellationToken);
            
            var needRetry = false;
            
            if (index < 100 && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                if (content.Contains("This nonce was already used in a previous request"))
                {
                    needRetry = true;
                }
            }
            
            if (!needRetry)
            {
                return response;
            }
            
        } while (true);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
    }
}