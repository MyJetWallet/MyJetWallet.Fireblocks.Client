using MyJetWallet.Fireblocks.Client.Auth;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.DelegateHandlers
{
    internal class AuthHandler : DelegatingHandler
    {
        private readonly ApiKeyHeaderGenerator _apiKeyCredentialsProvider;

        public AuthHandler(ApiKeyHeaderGenerator apiKeyCredentialsProvider)
        {
            this._apiKeyCredentialsProvider = apiKeyCredentialsProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            await _apiKeyCredentialsProvider.AddCredentialsAsync(request);
            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
