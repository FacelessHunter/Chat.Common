using Common.Domain.Enums;
using Common.Implementations.ExceptionImplementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Implementations.InnerHttpClient
{
    public class ChatHttpService : HttpService
    {
        public ChatHttpService(HttpClient httpClient, TokenProvider token)
            : base(httpClient, token)
        {
            _httpClient = httpClient;
        }

        private readonly HttpClient _httpClient;

        public async Task<bool> IsAccessToChatAsync(long chatId, string username)
        {
            await SetTokenAsync();

            HttpResponseMessage response = await _httpClient.GetAsync($"api/chats/{chatId}/users/{username}/access");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsExistChatAsync(long chatId)
        {
            await SetTokenAsync();

            HttpResponseMessage response = await _httpClient.GetAsync($"api/chats/{chatId}/exist");

            return response.IsSuccessStatusCode;
        }
    }
}
