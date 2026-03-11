namespace MyJetWallet.Fireblocks.Client
{
    public class EmbeddedFireblocksStorageOptions
    {
        public string BaseUrl { get; set; }
        public string AdminKeyId { get; set; }
        public string SignerKeyId { get; set; }

        public string FallbackAdminApiKey { get; set; }
        public string FallbackAdminPrivateKey { get; set; }
        public string FallbackSignerApiKey { get; set; }
        public string FallbackSignerPrivateKey { get; set; }
    }
}
