namespace HxcApiClient.Http;

public interface IhttpFactory
{
    Task<HttpClient> GetClient();
}