using System.Net.Http.Headers;

namespace UII;

internal class DefaultHttpClientFactory : IHttpClientFactory
{
    private static readonly Uri BaseUrl = new Uri("https://api.virtomize.com/uii/");

    private readonly string Token;

    public DefaultHttpClientFactory(string token)
    {
        this.Token = token;
    }

    /// <inheritdoc />
    public HttpClient BuildClient()
    {
        var client = new HttpClient( /*new MyHandler()*/);
        client.BaseAddress = BaseUrl;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("client", "foo");
        return client;
    }
}