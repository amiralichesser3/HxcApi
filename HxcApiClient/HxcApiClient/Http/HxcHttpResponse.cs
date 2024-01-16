using System.Net;

namespace HxcApiClient.Http;

public class HxcHttpResponse<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; } 
    public string? ErrorMessage { get; set; }
}