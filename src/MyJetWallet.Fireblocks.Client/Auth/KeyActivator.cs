using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Auth
{
    public class KeyActivator
    {
        public bool IsActivated { get; set; } = false;
        public DateTime StartedAt { get; } = DateTime.UtcNow;
        public KeyActivator()
        {
        }

        public void ActivateKeys(string apiKey, string privateKey)
        {
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(privateKey))
                return;
            IsActivated = true;
            KeyActivatedEvent.Invoke(this, apiKey, privateKey);
        }

        public delegate void KeyActivationEventntHandler(object sender, string apiKey, string privateKey);
        
        public event KeyActivationEventntHandler KeyActivatedEvent = delegate { };
    }
}
