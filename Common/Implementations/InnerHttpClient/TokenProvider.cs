using Common.Domain.Options;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Implementations.InnerHttpClient
{
    public sealed class TokenProvider
    {
        private readonly HttpClient _httpClient;
        private TokenResponse _token;
        private string _tokenEndpoint;
        private DateTime _expiresAt;
        private TokenProviderOptions _options;

        private bool IsExpired
        {
            get
            {
                return _expiresAt <= DateTime.UtcNow;
            }
        }

        public TokenProvider(HttpClient httpClient, IOptions<TokenProviderOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        private async Task<string> GetTokenEndpointAsync()
        {
            if (_tokenEndpoint == null)
            {
                var discoveryResponse = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
                {
                    Address = _httpClient.BaseAddress.ToString(),
                    Policy = {
                        RequireHttps = false
                    }
                });
                if (discoveryResponse.IsError)
                {
                    throw new Exception(discoveryResponse.Error);
                }
                _tokenEndpoint = discoveryResponse.TokenEndpoint;
            }
            return _tokenEndpoint;
        }

        private async Task RequestTokenAsync()
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = await GetTokenEndpointAsync(),
                ClientId = _options.ClientId,
                ClientSecret = _options.ClientSecret,
                Scope = _options.Scope,

            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            _expiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
            _token = tokenResponse;
        }

        public async Task<string> GetTokenAsync()
        {
            if (_token == null)
            {
                await RequestTokenAsync();
            }
            if (IsExpired)
            {
                await RequestTokenAsync();
            }

            return _token.AccessToken;
        }
    }
}
