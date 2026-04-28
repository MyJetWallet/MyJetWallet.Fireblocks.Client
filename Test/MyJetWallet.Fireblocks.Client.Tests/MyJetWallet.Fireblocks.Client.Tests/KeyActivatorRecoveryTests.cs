using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using MyJetWallet.Fireblocks.Client.Auth;
using Xunit;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public sealed class KeyActivatorRecoveryTests : IDisposable
    {
        private readonly RSA _rsa;
        private readonly string _pkcs8Base64;
        private readonly ClientConfigurator _emptyConfig;
        private readonly KeyActivator _keyActivator;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public KeyActivatorRecoveryTests()
        {
            _rsa = RSA.Create(2048);
            _pkcs8Base64 = Convert.ToBase64String(_rsa.ExportPkcs8PrivateKey());
            _emptyConfig = new ClientConfigurator
            {
                BaseUrl = "https://api.fireblocks.io/v1",
                ApiKey = null,
                ApiPrivateKey = null,
            };
            _keyActivator = new KeyActivator();
            _jwtTokenGenerator = new JwtTokenGenerator(_emptyConfig, _keyActivator);
        }

        [Fact]
        public void Activate_NullCallbackThenValid_RestoresIsActivated()
        {
            _keyActivator.ActivateKeys(null, null);

            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);

            Assert.True(_keyActivator.IsActivated, "IsActivated must recover after null bootstrap callback followed by valid keys");
            var jwt = _jwtTokenGenerator.GenerateJwtToken(BuildRequest());
            Assert.False(string.IsNullOrEmpty(jwt));
        }

        [Fact]
        public void Activate_EmptyStringCallbackThenValid_RestoresIsActivated()
        {
            _keyActivator.ActivateKeys(string.Empty, string.Empty);

            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);

            Assert.True(_keyActivator.IsActivated);
            var jwt = _jwtTokenGenerator.GenerateJwtToken(BuildRequest());
            Assert.False(string.IsNullOrEmpty(jwt));
        }

        [Fact]
        public void Activate_InvalidKeyThenValid_RecoversWithoutCorruptingState()
        {
            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);
            Assert.True(_keyActivator.IsActivated);

            _keyActivator.ActivateKeys("api-key-2", "not-a-valid-base64-pkcs8-key!!!");
            Assert.False(_keyActivator.IsActivated);
            Assert.Equal("api-key-1", _emptyConfig.ApiKey);
            Assert.Equal(_pkcs8Base64, _emptyConfig.ApiPrivateKey);

            using var newRsa = RSA.Create(2048);
            var newPkcs8 = Convert.ToBase64String(newRsa.ExportPkcs8PrivateKey());
            _keyActivator.ActivateKeys("api-key-3", newPkcs8);

            Assert.True(_keyActivator.IsActivated);
            Assert.Equal("api-key-3", _emptyConfig.ApiKey);
            var jwt = _jwtTokenGenerator.GenerateJwtToken(BuildRequest());
            Assert.False(string.IsNullOrEmpty(jwt));
        }

        [Fact]
        public void Activate_IdempotentRefire_KeepsIsActivatedTrue()
        {
            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);
            _keyActivator.IsActivated = false;

            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);

            Assert.True(_keyActivator.IsActivated, "Idempotent re-fire must restore IsActivated even when activation was previously cleared");
        }

        [Fact]
        public void GenerateJwtToken_AfterNullBootstrapAndValidActivation_DoesNotThrow()
        {
            _keyActivator.ActivateKeys(null, null);
            _keyActivator.ActivateKeys("api-key-1", _pkcs8Base64);

            var ex = Record.Exception(() => _jwtTokenGenerator.GenerateJwtToken(BuildRequest()));
            Assert.Null(ex);
        }

        private static HttpRequestMessage BuildRequest()
        {
            return new HttpRequestMessage
            {
                RequestUri = new Uri("https://api.fireblocks.io/v1/test"),
                Content = new StringContent("body", Encoding.UTF8),
            };
        }

        public void Dispose()
        {
            _jwtTokenGenerator.Dispose();
            _rsa.Dispose();
        }
    }
}
