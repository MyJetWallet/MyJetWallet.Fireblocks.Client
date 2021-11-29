using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Auth
{
    public class ApiKeyHeaderGenerator : IDisposable
    {
        internal const string ApiKeyHeader = "X-API-Key";
        internal const string JwtScheme = "Bearer";

        private readonly ClientConfigurator _configuration;
        private readonly JwtTokenGenerator _bearerCredentialsProvider;
        private readonly CancellationTokenSource _tokenSource;

        public ApiKeyHeaderGenerator(ClientConfigurator configuration, JwtTokenGenerator bearerCredentialsProvider)
        {
            _configuration = configuration;
            _bearerCredentialsProvider = bearerCredentialsProvider;

            _tokenSource = new CancellationTokenSource();
        }


        #region Implementation of ICredentialsProvider

        /// <inheritdoc />
        public void AddCredentials(HttpRequestMessage msg)
        {
            var token = _bearerCredentialsProvider.GenerateJwtToken(msg);
            msg.Headers.Authorization = new AuthenticationHeaderValue(JwtScheme, token);
            msg.Headers.Add(ApiKeyHeader, _configuration.ApiPubKey);
        }

        public Task AddCredentialsAsync(HttpRequestMessage msg)
        {
            AddCredentials(msg);
            return Task.CompletedTask;
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}