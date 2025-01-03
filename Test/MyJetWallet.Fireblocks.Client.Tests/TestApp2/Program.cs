using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        //string apiKey = "b4fcd05f-0ebc-44ac-9e60-808c7d3d012e";
        //string privateKey = System.IO.File.ReadAllText("api_test_secret.key");
        //string baseUrl = "https://sandbox-api.fireblocks.io"; // For sandbox: https://sandbox-api.fireblocks.io
        
        string apiKey = "d5a40f93-8cbf-49e3-aa2e-0a3d8e81b4e4";
        string privateKey = System.IO.File.ReadAllText("API_Editor_secret.key");
        string baseUrl = "https://sandbox-api.fireblocks.io"; // For sandbox: https://sandbox-api.fireblocks.io

        
        //var AssetsResponse = await GetSupportedAssetPaged(apiKey, privateKey, baseUrl);
        //Console.WriteLine("ASSETS Response: " + AssetsResponse);
        
        // Call GET request
        string getResponse = await GetAccountsPaged(apiKey, privateKey, baseUrl);
        Console.WriteLine("GET Response: " + getResponse);
        
        // Call POST request
        string postResponse = await CreateAccount(apiKey, privateKey, baseUrl);
        Console.WriteLine("POST Response: " + postResponse);
    }
    
    static async Task<string> GetSupportedAssetPaged(string apiKey, string privateKey, string baseUrl)
    {
        string path = "/v1/wallets/supported_assets?pageSize=100&onlyBaseAssets=true";
        ApiTokenProvider provider = new ApiTokenProvider(privateKey, apiKey);

        string token = provider.SignJwt(path, "");
        
        Console.WriteLine($"Token: {token}");

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            HttpResponseMessage response = await client.GetAsync(baseUrl + path);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "GET Request failed with status: " + response.StatusCode;
            }
        }
    }

    static async Task<string> GetAccountsPaged(string apiKey, string privateKey, string baseUrl)
    {
        string path = "/v1/vault/accounts_paged";
        ApiTokenProvider provider = new ApiTokenProvider(privateKey, apiKey);

        string token = provider.SignJwt(path, "");

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            HttpResponseMessage response = await client.GetAsync(baseUrl + path);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "GET Request failed with status: " + response.StatusCode;
            }
        }
    }

    static async Task<string> CreateAccount(string apiKey, string privateKey, string baseUrl)
    {
        string path = "/v1/vault/accounts";
        ApiTokenProvider provider = new ApiTokenProvider(privateKey, apiKey);

        var body = new
        {
            name = "MyCSharpVault",
            hiddenOnUI = true
        };

        string token = provider.SignJwt(path, JsonConvert.SerializeObject(body));

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("X-API-Key", apiKey);

            string jsonBody = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(baseUrl+ path, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "POST Request failed with status: " + response.StatusCode;
            }
        }
    }
}

class ApiTokenProvider
{
    private RSA privateKey;
    private string apiKey;

    public ApiTokenProvider(string privateKeyPem, string apiKey)
    {
        this.privateKey = RSA.Create();
        this.privateKey.ImportFromPem(privateKeyPem);
        this.apiKey = apiKey;
    }

    public string SignJwt(string path, string bodyJson)
    {
        JwtPayload payload = new JwtPayload
        {
            { "uri", path },
            { "nonce", Guid.NewGuid().ToString() },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "exp", DateTimeOffset.UtcNow.AddSeconds(55).ToUnixTimeSeconds() },
            { "sub", apiKey }
        };

        if (!string.IsNullOrEmpty(bodyJson))
        {
            Console.WriteLine($"Body: {bodyJson}");
            payload.Add("bodyHash", CalculateBodyHash(bodyJson));
        }

        Console.WriteLine($"Payload 22: {JsonConvert.SerializeObject(payload, Formatting.Indented)}");
        
        var rsaKey = new RsaSecurityKey(privateKey);

        var header = new JwtHeader(new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256));
        var secToken = new JwtSecurityToken(header, payload);
        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(secToken);
    }

    private string CalculateBodyHash(string bodyJson)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(bodyJson));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}