namespace UII;

/// <summary>
/// Creates a HttpClient to use for request
/// </summary>
public interface IHttpClientFactory
{
    /// <summary>
    /// Create a http client for use in requests
    /// </summary>
    /// <returns>The client</returns>
    public HttpClient BuildClient();
}