using MyJetWallet.Fireblocks.Client;
using Xunit;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    [CollectionDefinition(nameof(CredentialsTestCollection))]
    public class CredentialsTestCollection : ICollectionFixture<RsaKeyFixture>
    {
    }
}