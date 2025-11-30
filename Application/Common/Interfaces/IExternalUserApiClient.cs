using Application.DTOs.Users;

public interface IExternalUserApiClient
{
    Task<List<ExternalUserDTO>> GetExternalUsersAsync();
}
