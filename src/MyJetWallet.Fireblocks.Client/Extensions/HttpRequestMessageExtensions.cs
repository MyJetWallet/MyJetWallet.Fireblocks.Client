using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyJetWallet.Fireblocks.Client.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static void AddFireblocksIdempotencyKeyHeader(this HttpRequestMessage request, string idempotencyKey)
        {
            request.Headers.Add("Idempotency-Key", idempotencyKey);
        }
    }
}