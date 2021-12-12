﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MyJetWallet.Fireblocks.Client.Auth
{
    public sealed class JwtTokenGenerator : IDisposable
    {
        private readonly ClientConfigurator _fireblocksConfiguration;
        private readonly SigningCredentials _signingCredentials;
        private readonly RSA _rsa;

        public JwtTokenGenerator(ClientConfigurator configuration)
        {
            _fireblocksConfiguration = configuration;
            _rsa = RSA.Create();
            _rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(_fireblocksConfiguration.ApiPrivateKey), out _);
            var securityKey = new RsaSecurityKey(_rsa);
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256);
        }

        private JwtPayload GetPayload(HttpRequestMessage msg)
        {
            var now = DateTimeOffset.UtcNow;
            var nonce = now.ToUnixTimeMilliseconds();
            var issuedTimestamp = now.ToUnixTimeSeconds();
            var expirationTimestamp = issuedTimestamp + 20;
            var body = msg.Content?.ReadAsStringAsync().GetAwaiter().GetResult() ?? string.Empty;
            var hashBody = GetSignature(body);
            return new JwtPayload
            {
                {"uri", msg.RequestUri!.PathAndQuery},
                {"nonce", nonce},
                {"iat", issuedTimestamp},
                {"exp", expirationTimestamp},
                {"sub", _fireblocksConfiguration.ApiKey},
                {"bodyHash", hashBody}
            };
        }


        public string GenerateJwtToken(HttpRequestMessage msg)
        {
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
        }
    }
}
