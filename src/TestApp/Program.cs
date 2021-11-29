using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestApp
{
    static class Program
    {
        private static string _accessToken;
        //private static ICircleClient _client;

        static async Task Main(string[] args)
        {
            _accessToken = Environment.GetEnvironmentVariable("AccessToken");

            if (string.IsNullOrEmpty(_accessToken))
            {
                Console.WriteLine("AccessToken is empty. Please setup env variable");
                return;
            }

            //_client = new CircleClient(_accessToken);

            // await TestPublicKey();
        }
    }
}