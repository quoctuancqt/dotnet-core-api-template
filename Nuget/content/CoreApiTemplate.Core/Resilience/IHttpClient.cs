using System.Net.Http;
using System.Threading.Tasks;

namespace CoreApiTemplate.Core.Resilience
{
    public interface IHttpClient
    {
        HttpClient Inst { get; }
        Task<string> GetStringAsync(string uri);
        Task<HttpResponseMessage> PostAsync<T>(string uri, T item);
        Task<HttpResponseMessage> DeleteAsync(string uri);
    }
}
