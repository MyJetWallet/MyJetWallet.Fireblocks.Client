using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace MyJetWallet.Fireblocks.Client.Auth
{
    public sealed class JwtTokenGenerator : IDisposable
    {
        private readonly ClientConfigurator _fireblocksConfiguration;
        private readonly KeyActivator _keyActivator;
        private volatile SigningCredentials _signingCredentials;
        private RSA _rsa;

        public JwtTokenGenerator(ClientConfigurator configuration, KeyActivator keyActivator)
        {
            _fireblocksConfiguration = configuration;
            _keyActivator = keyActivator;
            _keyActivator.KeyActivatedEvent += Activate;

            if (!string.IsNullOrEmpty(_fireblocksConfiguration.ApiPrivateKey))
            {
                Activate(this, _fireblocksConfiguration.ApiKey, _fireblocksConfiguration.ApiPrivateKey);
            }
        }

        private JwtPayload GetPayload(HttpRequestMessage msg)
        {
            var now = DateTimeOffset.UtcNow;
            var nonce = now.Ticks;
            
            var issuedTimestamp = now.ToUnixTimeSeconds();
            var expirationTimestamp = issuedTimestamp + 20;
            var body = msg.Content?.ReadAsStringAsync().GetAwaiter().GetResult() ?? string.Empty;
            var hashBody = GetSignature(body);
            var jwt = new JwtPayload
            {
                {"uri", msg.RequestUri!.PathAndQuery},
                {"nonce", nonce},
                {"iat", issuedTimestamp},
                {"exp", expirationTimestamp},
                {"sub", _fireblocksConfiguration.ApiKey}
            };
            
            if (!string.IsNullOrEmpty(body))
            {
                jwt.Add("bodyHash", GetSignature(body));
            }

            return jwt;
        }

        internal void Activate(object sender, string apiKey, string privateKey)
        {
            try
            {
                if (_signingCredentials != null
                    && _fireblocksConfiguration.ApiKey == apiKey
                    && _fireblocksConfiguration.ApiPrivateKey == privateKey)
                    return;

                _fireblocksConfiguration.ApiKey = apiKey;
                _fireblocksConfiguration.ApiPrivateKey = privateKey;

                var newRsa = RSA.Create();
                newRsa.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKey), out _);
                var newCredentials = new SigningCredentials(new RsaSecurityKey(newRsa), SecurityAlgorithms.RsaSha256);

                var oldRsa = _rsa;
                _rsa = newRsa;
                _signingCredentials = newCredentials;

                if (oldRsa != null)
                    Task.Run(async () => { await Task.Delay(30_000); oldRsa.Dispose(); });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during api key activation {e.Message}");
                _keyActivator.IsActivated = false;
            }
        }

        public string GenerateJwtToken(HttpRequestMessage msg)
        {
            if (!_keyActivator.IsActivated)
                throw new Exception("Private key is not activated");
            
            var payload = GetPayload(msg);
            
            var token = new JwtSecurityToken(new JwtHeader(_signingCredentials), payload);

            var sTokenHandler = new JwtSecurityTokenHandler();
            return sTokenHandler.WriteToken(token);
        }

        private string GetSignature(string preHash)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(preHash)).ToHex();
        }

        public void Dispose()
        {
            _rsa.Dispose();
            _keyActivator.KeyActivatedEvent -= Activate;
        }
    }
}
