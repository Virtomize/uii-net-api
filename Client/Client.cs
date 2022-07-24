using System.Net.Http.Headers;
using System.Text.Json;

namespace UII
{
    /// <summary>
    /// Client for interacting with Virtomize UII.
    /// </summary>
    public class Client
    {
        private readonly IHttpClientFactory ClientFactory;

        /// <summary>
        /// Construct a new UII client by providing an official authorization token
        /// </summary>
        /// <param name="token">The authorization token.</param>
        public Client(string token) : this(new DefaultHttpClientFactory(token))
        {
        }

        /// <summary>
        /// Construct a new UII client by providing a factory containing the logic of how to create
        /// a HttpClient.
        /// </summary>
        /// <param name="factory">A factory for HTTP clients.</param>
        public Client(IHttpClientFactory factory)
        {
            this.ClientFactory = factory;
        }

        /// <summary>
        /// Reads a list of all available operating systems. 
        /// </summary>
        /// <returns>The list of available operating systems.</returns>
        public async Task<List<OperatingSystem>> ReadOsList()
        {
            using var client = this.ClientFactory.BuildClient();
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

        /// <summary>
        /// Read a list of all available packages for a given distribution
        /// </summary>
        /// <param name="distribution">Name of the distribution, i.e. "debian"</param>
        /// <param name="version">Version of the distribution, i.e. "10"</param>
        /// <param name="architecture">Architecture of the distribution, i.e. "x86_64"</param>
        /// <returns>The list of packages</returns>
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

        /// <summary>
        /// Build an ISO file
        /// </summary>
        /// <param name="path">Target path, where the ISO file should be saved to.</param>
        /// <param name="distribution">Name of the distribution, i.e. "debian"</param>
        /// <param name="version">Version of the distribution, i.e. "10"</param>
        /// <param name="architecture">Architecture of the distribution, i.e. "x86_64"</param>
        /// <param name="hostname">The host name of the installed operation system, i.e. "DemoPC"</param>
        /// <param name="networks">The desired network configuration.</param>
        public async Task Build(string path, string distribution, string version, string architecture, string hostname,
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
                await using FileStream fs = File.OpenWrite(path);
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
            throw new RequestException(result?.errors ?? new List<string>() { "Could not deserialize response" });
        }

        private HttpClient CreateNewClient()
        {
            return this.ClientFactory.BuildClient();
        }
    }
}