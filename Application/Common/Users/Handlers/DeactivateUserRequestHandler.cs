using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;

namespace Application.Common.Users.Handlers
{
    public class DeactivateUserRequestHandler 
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public DeactivateUserRequestHandler(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        protected override async Task<Result<SuccessResponse>> HandleRequest(DeactivateUserRequest request, Result<SuccessResponse> result)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            if (user == null)
            {
                result.SetValidationResult(Domain.Common.Validation.ValidationItems.Common.NotFound);
                return result;
            }

            user.isActive = false;
            await _unitOfWork.Repository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessResponse(true));
            return result;
        }

    }
}
