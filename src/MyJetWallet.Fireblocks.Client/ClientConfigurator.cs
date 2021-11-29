namespace MyJetWallet.Fireblocks.Client
{
    public class ClientConfigurator
    {
        public string BaseUrl { get; set; }
        public string ApiPubKey { get; init; }
        public string ApiPrivateKey { get; init; }
    }
}