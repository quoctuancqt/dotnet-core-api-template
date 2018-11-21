using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProxyService
    {
        Task<HttpResponseMessage> PostAsync(string endPoint, HttpContent content);

        Task<HttpResponseMessage> PostAsJsonAsync(string endPoint, object obj);

        Task<HttpResponseMessage> PutAsJsonAsync(string endPoint, object obj);

        Task<HttpResponseMessage> GetAsync(string endPoint);

        Task<HttpResponseMessage> DeteleAsync(string endPoint);
    }
}
