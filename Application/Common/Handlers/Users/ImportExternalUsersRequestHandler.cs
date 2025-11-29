using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class ImportExternalUsersRequestHandler
        : RequestHandler<ImportExternalUsersRequest, SuccessResponse>
    {
        private readonly IUserRepository _users;
        private readonly IExternalUserApiClient _externalApi;
        private readonly ICacheService _cache;

        public ImportExternalUsersRequestHandler(
            IUserRepository users,
            IExternalUserApiClient externalApi,
            ICacheService cache)
        {
            _users = users;
            _externalApi = externalApi;
            _cache = cache;
        }

        protected override async Task<Result<SuccessResponse>> HandleRequest(
            ImportExternalUsersRequest request,
            Result<SuccessResponse> result)
        {

            var cachedUsers = await _cache.GetAsync<List<ExternalUserDto>>("external_users");

            var externalUsers = cachedUsers ??
                                await _externalApi.GetExternalUsersAsync();

            if (cachedUsers == null)
            {
                await _cache.SetAsync(
                    "external_users",
                    externalUsers,
                    TimeSpan.FromMinutes(10)
                );
            }

            foreach (var ext in externalUsers)
            {
                if (!await _users.IsEmailUniqueAsync(ext.Email))
                    continue;

                var user = new Domain.Entities.Users.User
                {
                    Name = ext.Name,
                    Surname = ext.Surname,
                    Email = ext.Email,
                    Username = ext.Username,
                    website = ext.Website,
                };

                await _users.InsertAsync(user);
            }

            result.SetResult(new SuccessResponse(true));
            return result;
        }

        protected override Task<bool> IsAuthorized()
            => Task.FromResult(true);
    }
}
