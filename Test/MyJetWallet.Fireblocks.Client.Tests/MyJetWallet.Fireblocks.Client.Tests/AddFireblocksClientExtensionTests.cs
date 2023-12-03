using Autofac;
using MyJetWallet.Fireblocks.Client.Autofac;
using Xunit;
using Xunit.Abstractions;

namespace MyJetWallet.Fireblocks.Client.Tests
{
    public class AddFireblocksClientExtensionTests : CredentialsTestsBase
    {
        public AddFireblocksClientExtensionTests(RsaKeyFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        { }

        [Fact]
        public void ServiceShouldBeResolved()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterFireblocksClient(Configuration);

            var container = containerBuilder.Build();
            var client = container.Resolve<IClient>();
            Assert.NotNull(client);
        }
    }
}
