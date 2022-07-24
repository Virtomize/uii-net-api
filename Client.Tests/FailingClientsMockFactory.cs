using System.Net;
using Moq;
using Moq.Protected;
using UII;

namespace Client.Tests;

public class ClientsMockFactory : IHttpClientFactory
{
    private readonly string JsonBody;

    public ClientsMockFactory(string jsonBody)
    {
        this.JsonBody = jsonBody;
    }

    public HttpClient BuildClient()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(this.JsonBody)
            });


        var client = new HttpClient(handlerMock.Object);
        client.BaseAddress = new Uri("https://unit.test");
        return client;
    }
}
