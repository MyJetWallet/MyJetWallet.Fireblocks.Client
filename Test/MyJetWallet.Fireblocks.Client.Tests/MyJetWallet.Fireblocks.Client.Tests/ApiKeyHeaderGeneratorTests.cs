﻿using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Options;
using MyJetWallet.Fireblocks.Client;
using MyJetWallet.Fireblocks.Client.Auth;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public sealed class ApiKeyHeaderGeneratorTests : CredentialsTestsBase, IDisposable
    {
        private readonly ApiKeyHeaderGenerator _provider;
        private readonly JwtTokenGenerator _tokenGenerator;

        public ApiKeyHeaderGeneratorTests(RsaKeyFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
            _tokenGenerator = new JwtTokenGenerator(Configuration, new KeyActivator()
            {
                IsActivated = true,
            });
            _provider = new ApiKeyHeaderGenerator(Configuration, _tokenGenerator, new KeyActivator()
            {
                IsActivated = true,
            });
        }

        [Fact]
        public void AddCredentialsAuthHeaderShouldBeAdded()
        {
            var message = new HttpRequestMessage { RequestUri = new Uri("https://test.com/test1/validate1") };

            _provider.AddCredentials(message);

            ClassicAssert.Equal("pubKey", message.Headers.GetValues(ApiKeyHeaderGenerator.ApiKeyHeader).Single());
            ClassicAssert.Equal(ApiKeyHeaderGenerator.JwtScheme, message.Headers.Authorization!.Scheme);
            ClassicAssert.NotNull(message.Headers.Authorization!.Parameter);
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}
