using System;
using System.Security.Cryptography;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    [Collection(nameof(CredentialsTestCollection))]
    public class CredentialsTestsBase
    {
        protected string RsaPrivatePcs8PrivateKey { get; }
        protected RSA RsaKey { get; }
        protected ClientConfigurator Configuration { get; }

        protected CredentialsTestsBase(RsaKeyFixture fixture, ITestOutputHelper output)
        {
            RsaKey = fixture.RsaKey;
            RsaPrivatePcs8PrivateKey = Convert.ToBase64String(RsaKey.ExportPkcs8PrivateKey());
            Configuration = new ClientConfigurator
            {
                BaseUrl = "https://api.fireblocks.io/v1",
                ApiPrivateKey = RsaPrivatePcs8PrivateKey,
                ApiPubKey = "pubKey"
            };
        }
    }
}
