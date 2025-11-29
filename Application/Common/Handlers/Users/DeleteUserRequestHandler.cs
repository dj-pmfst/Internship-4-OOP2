using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class DeleteUserRequestHandler : RequestHandler<DeleteUserRequest, SuccessResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public DeleteUserRequestHandler(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        protected override async Task<Result<SuccessResponse>> HandleRequest(DeleteUserRequest request, Result<SuccessResponse> result)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                result.SetValidationResult(error); //popravit
                return result;
            }

            await _unitOfWork.Repository.DeleteByIdAsync(request.Id);
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessResponse(true));
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
