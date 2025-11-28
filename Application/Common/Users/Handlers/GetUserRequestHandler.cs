using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;
using Domain.Entities.Users;

namespace Application.Common.Users.Handlers
{
    public class GetUserRequestHandler : RequestHandler<GetUserRequest, GetResponse<User>>
    {
        private readonly IUserUnitOfWork _unitOfWork;

        public GetUserRequestHandler(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        protected override async Task<Result<GetResponse<User>>> HandleRequest(GetUserRequest request, Result<GetResponse<User>> result)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            var users = user != null ? new List<User> { user } : new List<User>();
            result.SetResult(new GetResponse<User> { Items = users });
            return result;
        }
        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
