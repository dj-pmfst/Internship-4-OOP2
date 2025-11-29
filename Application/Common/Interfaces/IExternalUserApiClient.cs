public interface IExternalUserApiClient
{
    Task<List<ExternalUserDto>> GetExternalUsersAsync();
}
