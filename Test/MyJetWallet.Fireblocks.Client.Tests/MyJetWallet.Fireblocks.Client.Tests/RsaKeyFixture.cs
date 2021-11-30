using System;
using System.Security.Cryptography;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public sealed class RsaKeyFixture : IDisposable
    {
        public RSA RsaKey { get; }
        public RsaKeyFixture()
        {
            RsaKey = RSA.Create(4096);
        }

        public void Dispose()
        {
            RsaKey.Dispose();
        }
    }
}
