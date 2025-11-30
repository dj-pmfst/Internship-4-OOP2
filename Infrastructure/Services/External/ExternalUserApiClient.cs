using Application.DTOs.Users;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Infrastructure.Services.External
{
    public class ExternalUserApiClient : IExternalUserApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _usersEndpoint;

        public ExternalUserApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ExternalApi:BaseUrl"]
                ?? throw new InvalidOperationException("ExternalApi:BaseUrl configuration missing");
            _usersEndpoint = configuration["ExternalApi:UsersEndpoint"] ?? "/users";
        }

        public async Task<List<ExternalUserDTO>?> GetExternalUsersAsync()
        {
            try
            {
                var url = $"{_baseUrl}{_usersEndpoint}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var users = await response.Content.ReadFromJsonAsync<List<ExternalUserDTO>>();
                return users;
            }
            catch (HttpRequestException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}