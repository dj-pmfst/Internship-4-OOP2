using System.Net.Http.Json;

public class ExternalUserApiClient : IExternalUserApiClient
{
    private readonly HttpClient _http;

    public ExternalUserApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ExternalUserDto>> GetExternalUsersAsync()
    {
        var response = await _http.GetFromJsonAsync<List<ExternalUserDto>>("https://external-api.com/users");

        return response ?? new List<ExternalUserDto>();
    }
}