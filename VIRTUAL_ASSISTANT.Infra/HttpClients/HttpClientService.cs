using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VIRTUAL_ASSISTANT.Domain.Interfaces;

namespace VIRTUAL_ASSISTANT.Infra.HttpClients
{
    public class HttpClientService: IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<TResponse> GetAsync<TResponse>(string clientName, string endpoint)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var response = await client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Error: {response.StatusCode} - {response.ReasonPhrase}. " +
                    $"Endpoint: {endpoint}. Response content: {errorContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content)!;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string clientName, string endpoint, TRequest body)
        {
            var client = _httpClientFactory.CreateClient(clientName);
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Error: {response.StatusCode} - {response.ReasonPhrase}. " +
                    $"Endpoint: {endpoint}. Response content: {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent)!;
        }
    }
}
