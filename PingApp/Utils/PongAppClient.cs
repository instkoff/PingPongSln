using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PingApp.Models.Settings;
using PingPong.Shared.Models.Requests;
using PingPong.Shared.Models.Responses;

namespace PingApp.Utils
{
    public class PongAppClient
    {
        private readonly HttpClient _httpClient;

        public PongAppClient(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ApiEndpoint);
        }

        public async Task<AddMessageResponse> AddMessageCommand(AddMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Message/add", request);

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<AddMessageResponse>();

            var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();

            throw new HttpRequestException($"{errorResponse?.ErrorMessage}", null, result.StatusCode);
        }

        public async Task<MessageListResponse> ListCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Message/list", request);

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<MessageListResponse>();

            var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();

            throw new HttpRequestException($"{errorResponse?.ErrorMessage}", null, result.StatusCode);
        }

        public async Task<BaseResponse> DeleteMessageCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Message/delete", request);

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<BaseResponse>();

            var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();

            throw new HttpRequestException($"{errorResponse?.ErrorMessage}", null, result.StatusCode);
        }

        public async Task<HealthResponse> GetServiceStatus()
        {
            var result = await _httpClient.GetAsync("/health");

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<HealthResponse>();

            return new HealthResponse { Status = "Unhealthy" };
        }
    }
}