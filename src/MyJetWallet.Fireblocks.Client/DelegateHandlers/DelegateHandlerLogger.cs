using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.DelegateHandlers
{
    //TODO: Complete logging and telemetry
    public class DelegateHandlerLogger : DelegatingHandler
    {
        public DelegateHandlerLogger()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var uuid = Guid.NewGuid().ToString();
            var path = request.RequestUri?.ToString();
            var contentAsStr = request.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"[{uuid}] Request path: {path}, content: {contentAsStr}");
            var response = await base.SendAsync(request, cancellationToken);

            contentAsStr = response.Content?.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine($"[{uuid}] Response path: {path}, Code: {response.StatusCode}, content: {contentAsStr}");

            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}
