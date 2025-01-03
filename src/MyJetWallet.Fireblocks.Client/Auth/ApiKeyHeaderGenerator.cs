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
        private readonly KeyActivator _keyActivator;
        private readonly CancellationTokenSource _tokenSource;

        public ApiKeyHeaderGenerator(ClientConfigurator configuration, JwtTokenGenerator jwtTokenGenerator, KeyActivator keyActivator)
        {
            _configuration = configuration;
            _jwtTokenGenerator = jwtTokenGenerator;
            _keyActivator = keyActivator;
            _tokenSource = new CancellationTokenSource();
        }

        public void AddCredentials(HttpRequestMessage msg)
        {
            if (!_keyActivator.IsActivated)
                throw new Exception("Api key is not activated!");
            
            var token = _jwtTokenGenerator.GenerateJwtToken(msg);
            
            msg.Headers.Authorization = new AuthenticationHeaderValue(JwtScheme, token);
            msg.Headers.Add(ApiKeyHeader, _configuration.ApiKey);
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