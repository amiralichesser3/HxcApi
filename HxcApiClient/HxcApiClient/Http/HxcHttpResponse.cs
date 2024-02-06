using System.Net;

namespace HxcApiClient.Http;

public class HxcHttpResponse
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; } 
    public string? ErrorMessage { get; set; }
}

public class HxcHttpResponse<T> : HxcHttpResponse
{
    public T? Data { get; set; }
}