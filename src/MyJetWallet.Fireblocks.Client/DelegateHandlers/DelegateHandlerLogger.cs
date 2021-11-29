using System.Net.Http;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.DelegateHandlers
{
    //TODO: Complete logging and telemetry
    internal class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

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
