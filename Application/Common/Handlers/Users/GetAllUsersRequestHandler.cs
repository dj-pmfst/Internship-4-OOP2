using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;
using Domain.Entities.Users;

namespace Application.Common.Handlers.Users
{
    public class GetAllUsersRequestHandler : RequestHandler<GetAllUsersRequest, GetResponse<User>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetAllUsersRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result<GetResponse<User>>> HandleRequest(GetAllUsersRequest request, Result<GetResponse<User>> result)
        {

            if (request.Id.HasValue)
            {
                var item = await _unitOfWork.Repository.GetByIdAsync(request.Id.Value);
                var response = new GetResponse<User>
                {
                    Items = item != null ? new List<User> { item } : new List<User>()
                };
                result.SetResult(response);
                return result;
            }

            var repoResult = await _unitOfWork.Repository.GetAll();
            var getResponse = new GetResponse<User>
            {
                Items = repoResult.Values.ToList()
            };

            result.SetResult(getResponse);
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
