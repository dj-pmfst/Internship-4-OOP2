using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Common.Validation;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class ActivateUserRequestHandler : RequestHandler<ActivateUserRequest, SuccessResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public ActivateUserRequestHandler(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        protected override async Task<Result<SuccessResponse>> HandleRequest(ActivateUserRequest request, Result<SuccessResponse> result)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                result.SetValidationResult(ValidationErrors.NotFound("User"));
                return result;
            }

            user.isActive = true;
            await _unitOfWork.Repository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessResponse(true));
            return result; 
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
