using System.Net.Http;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.DelegateHandlers
{
    //TODO: Complete logging and telemetry
    public class DelegateHandlerLogger : DelegatingHandler
    {
        public DelegateHandlerLogger()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            //System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));

            var contentAsStr = request.Content?.ReadAsStringAsync().Result;
            System.Console.WriteLine(contentAsStr);
            var response = await base.SendAsync(request, cancellationToken);

            //System.Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(request));

            contentAsStr = await response.Content.ReadAsStringAsync();
            System.Console.WriteLine(contentAsStr);

            return response;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}
