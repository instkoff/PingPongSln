using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PingApp.Models;
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

        public async Task<BaseResponse> AddMessageCommand(AddMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/Message/add", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<AddMessageResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();

        }

        public async Task<BaseResponse> ListCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/Message/list", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<MessageListResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();

        }

        public async Task<BaseResponse> DeleteMessageCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/Message/delete", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<BaseResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();
        }

        public async Task<BaseResponse> GetServiceStatus()
        {
            var result = await _httpClient.GetAsync("/health");

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<HealthResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();
        }
    }
}
