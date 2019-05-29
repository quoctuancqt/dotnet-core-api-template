using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Resilience
{
    public class StandardHttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private ILogger<StandardHttpClient> _logger;
        public HttpClient Inst => _httpClientFactory.CreateClient();
        public StandardHttpClient(IHttpClientFactory httpClientFactory, ILogger<StandardHttpClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public Task<string> GetStringAsync(string uri) =>
            Inst.GetStringAsync(uri);

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T item)
        {
            var contentString = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
            return Inst.PostAsync(uri, contentString);
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri) =>
            Inst.DeleteAsync(uri);
    }
}

