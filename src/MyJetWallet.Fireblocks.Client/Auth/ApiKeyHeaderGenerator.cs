using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Auth
{
    public class ApiKeyHeaderGenerator : IDisposable
    {
        public const string ApiKeyHeader = "X-API-Key";
        public const string JwtScheme = "Bearer";

        private readonly ClientConfigurator _configuration;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly CancellationTokenSource _tokenSource;

        public ApiKeyHeaderGenerator(ClientConfigurator configuration, JwtTokenGenerator jwtTokenGenerator)
        {
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;

            _tokenSource = new CancellationTokenSource();
        }

        public void AddCredentials(HttpRequestMessage msg)
        {
            var token = _jwtTokenGenerator.GenerateJwtToken(msg);
            msg.Headers.Authorization = new AuthenticationHeaderValue(JwtScheme, token);
            msg.Headers.Add(ApiKeyHeader, _configuration.ApiPubKey);
        }

        public Task AddCredentialsAsync(HttpRequestMessage msg)
        {
            AddCredentials(msg);
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}