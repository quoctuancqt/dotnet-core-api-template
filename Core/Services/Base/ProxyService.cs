using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProxyService : IProxyService
    {
        private readonly HttpClient _client;

        public ProxyService(HttpClient client, IConfiguration config)
        {
            client.BaseAddress = new Uri(config.GetSection("ExternalApiAddress").Value);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client = client;
        }

        public async Task<HttpResponseMessage> DeteleAsync(string endPoint)
        {
            return await _client.DeleteAsync(endPoint);
        }

        public async Task<HttpResponseMessage> GetAsync(string endPoint)
        {
            return await _client.GetAsync(endPoint);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync(string endPoint, object obj)
        {
            return await _client.PostAsync(endPoint, ObjToHttpContent(obj));
        }

        public async Task<HttpResponseMessage> PostAsync(string endPoint, HttpContent content)
        {
            return await _client.PostAsync(endPoint, content);
        }

        public async Task<HttpResponseMessage> PutAsJsonAsync(string endPoint, object obj)
        {
            return await _client.PostAsync(endPoint, ObjToHttpContent(obj));
        }

        private StringContent ObjToHttpContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}
