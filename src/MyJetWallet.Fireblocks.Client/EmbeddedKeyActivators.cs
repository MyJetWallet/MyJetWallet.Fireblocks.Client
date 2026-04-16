using System;
using System.Threading.Tasks;
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

        public async Task WaitForActivationAsync(int maxWaitSeconds = 10)
        {
            if (Admin.IsActivated && Signer.IsActivated)
                return;

            for (var i = 0; i < maxWaitSeconds && (!Admin.IsActivated || !Signer.IsActivated); i++)
                await Task.Delay(1000);

            if (!Admin.IsActivated || !Signer.IsActivated)
                throw new Exception($"Fireblocks keys not activated within {maxWaitSeconds}s");
        }
    }
}
