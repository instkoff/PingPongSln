using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PingApp.Models;
using PingPong.Shared.Models.Requests;
using PingPong.Shared.Models.Responses;

namespace PingApp.Utils
{
    public class PongAppClient
    {
        private readonly HttpClient _httpClient;
        public PongAppClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse> AddMessageCommand(AddMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/add", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<AddMessageResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();

        }

        public async Task<BaseResponse> ListCommand(GetMessageRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/list", request);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadFromJsonAsync<MessageListResponse>();
            }

            return await result.Content.ReadFromJsonAsync<ErrorResponse>();

        }
    }
}
