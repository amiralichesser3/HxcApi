using Newtonsoft.Json;

namespace HxcApiClient.Http;

internal abstract class BaseClient
{
    protected async Task<HxcHttpResponse<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        HxcHttpResponse<T> result = new HxcHttpResponse<T>();
        result.StatusCode = response.StatusCode;
        result.IsSuccess = response.IsSuccessStatusCode;
        
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            result.Data = JsonConvert.DeserializeObject<T>(content);
        }
        else
        {
            result.ErrorMessage = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";
        }

        return result;
    }
}