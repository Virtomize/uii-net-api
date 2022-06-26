using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UII
{
    /// <summary>
    /// Client for interacting with Virtomize UII.
    /// </summary>
    public class Client
    {
        private readonly string Token;

        private static readonly string BaseUrl = "https://api.virtomize.com/uii/";

        public Client(string token)
        {
            this.Token = token;
        }

        /// <summary>
        /// Reads a list of all available operating systems. 
        /// </summary>
        /// <returns>The list.</returns>
        public async Task<List<OperatingSystem>> ReadOsList()
        {
            using var client = CreateNewClient();
            var response = await client.GetAsync("oslist");

            if (response.IsSuccessStatusCode)
            {
                await using var bodyStream = await response.Content.ReadAsStreamAsync();
                using var bodyReader = new StreamReader(bodyStream);
                // Parse the response body.
                var body = await bodyReader.ReadToEndAsync();
                var result = JsonSerializer.Deserialize<Embedded<List<OperatingSystem>>>(body);
                return result?.Inner ?? new List<OperatingSystem>();
            }

            return new List<OperatingSystem>();
        }

        public async Task<PackageList> ReadPackageList(string distribution, string version, string architecture)
        {
            using var client = CreateNewClient();
            var payload = new PackageRequest
            {
                arch = architecture,
                dist = distribution,
                version = version
            };

            var response = await client.PostAsJsonAsync("packages", payload);

            if (response.IsSuccessStatusCode)
            {
                await using var bodyStream = await response.Content.ReadAsStreamAsync();
                using var bodyReader = new StreamReader(bodyStream);
                // Parse the response body.
                var body = await bodyReader.ReadToEndAsync();
                var res = JsonSerializer.Deserialize<Embedded<PackageList>>(body);
                return res?.Inner ?? new PackageList();
            }

            return new PackageList();
        }

        public async Task Build(string distribution, string version, string architecture, string hostname,
            List<Network> networks)
        {
            using var client = CreateNewClient();
            var payload = new BuildRequest
            {
                arch = architecture,
                dist = distribution,
                version = version,
                hostname = hostname,
                networks = networks.Select(BuildRequestNetwork.FromNetwork)
            };

            var response = await client.PostAsJsonAsync("images", payload);

            if (response.IsSuccessStatusCode)
            {
                await using var bodyStream = await response.Content.ReadAsStreamAsync();
                await using FileStream fs = File.OpenWrite("foo.iso");
                await bodyStream.CopyToAsync(fs);
            }
            else
            {
                await ProcessError(response);
            }
        }

        private async Task ProcessError(HttpResponseMessage response)
        {
            await using var bodyStream = await response.Content.ReadAsStreamAsync();
            using var bodyReader = new StreamReader(bodyStream);
            var body = await bodyReader.ReadToEndAsync();
            var result = JsonSerializer.Deserialize<ErrorResponse>(body);
            throw new RequestException(result?.errors ?? new List<string>(){ "Could not deserialize response"});
        }

        private HttpClient CreateNewClient()
        {
            var client = new HttpClient( /*new MyHandler()*/);
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("client", "foo");
            return client;
        }
    }

    internal class MyHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            await using var bodyStream = await request.Content.ReadAsStreamAsync();
            using var bodyReader = new StreamReader(bodyStream);
            var body = await bodyReader.ReadToEndAsync();

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Accepted,
                Content = new StringContent("hello")
            };
        }
    }
}