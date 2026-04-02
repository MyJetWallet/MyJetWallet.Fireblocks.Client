using MyJetWallet.Fireblocks.Client.Auth;

namespace MyJetWallet.Fireblocks.Client
{
    public class EmbeddedKeyActivators
    {
        public KeyActivator Admin { get; }
        public KeyActivator Signer { get; }

        public EmbeddedKeyActivators(KeyActivator admin, KeyActivator signer)
        {
            Admin = admin;
            Signer = signer;
        }
    }
}
