using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyJetWallet.Fireblocks.Client.Auth;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public sealed class JwtTokenGeneratorTests : CredentialsTestsBase, IDisposable
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public JwtTokenGeneratorTests(RsaKeyFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
            _jwtTokenGenerator = new JwtTokenGenerator(Configuration);
        }


        [Fact]
        public void JwtTokenShouldBeInRightFormat()
        {
            var message = new HttpRequestMessage { RequestUri = new Uri("https://test.com/test1/validate") };
            var messageBody = "this body is taken into account in the signature and payload";
            message.Content = new StringContent(messageBody, Encoding.UTF8);
            _jwtTokenGenerator.GenerateJwtToken(message);

            var jwt = _jwtTokenGenerator.GenerateJwtToken(message);
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(jwt, GetValidationParameters(), out _);
            var token = handler.ReadJwtToken(jwt);
            ValidatePayload(token, messageBody);
        }


        private void ValidatePayload(JwtSecurityToken retrievedToken, string messageBody)
        {
            var payload = retrievedToken.Payload;
            //Assert.Equal(Convert.ToInt32(_nonce.ToUnixTimeSeconds()), payload.Iat);
            //Assert.Equal(Convert.ToInt32(_nonce.ToUnixTimeSeconds() + 20), payload.Exp);
            Assert.Equal(Configuration.ApiPubKey, payload.Sub);

            Assert.True(payload.TryGetValue("nonce", out var retrievedNonce));
            //Assert.Equal(_nonce.ToUnixTimeMilliseconds(), retrievedNonce);
            Assert.True(payload.TryGetValue("bodyHash", out var retrievedBodyHash));
            using var sha256 = SHA256.Create();

            Assert.Equal(sha256.ComputeHash(Encoding.UTF8.GetBytes(messageBody)).ToHex(), retrievedBodyHash);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(RsaKey),
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            return validationParameters;
        }

        public void Dispose()
        {
            _jwtTokenGenerator.Dispose();
        }
    }
}