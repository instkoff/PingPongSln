using System;
using System.Net;
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

           throw await ProcessError(result);
        }

        public async Task<MessageListResponse> ListCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Message/list", request);

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<MessageListResponse>();

            throw await ProcessError(result);
        }

        public async Task<BaseResponse> DeleteMessageCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Message/delete", request);

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<BaseResponse>();

            throw await ProcessError(result);
        }

        public async Task<HealthResponse> GetServiceStatus()
        {
            var result = await _httpClient.GetAsync("/health");

            if (result.IsSuccessStatusCode) return await result.Content.ReadFromJsonAsync<HealthResponse>();

            return new HealthResponse { Status = "Unhealthy" };
        }

        private async Task<HttpRequestException> ProcessError(HttpResponseMessage result)
        {
            if (result.StatusCode is HttpStatusCode.BadRequest or HttpStatusCode.InternalServerError)
            {
                var errorResponse = await result.Content.ReadFromJsonAsync<ErrorResponse>();
                return new HttpRequestException($"{errorResponse?.ErrorMessage}", null, result.StatusCode);
            }

            var error = await result.Content.ReadAsStringAsync();
            return new HttpRequestException($"{error}", null, result.StatusCode);
        }
    }
}