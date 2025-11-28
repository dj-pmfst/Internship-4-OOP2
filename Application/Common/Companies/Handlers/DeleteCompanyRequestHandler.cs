using Application.Common.Model;
using Application.DTOs.Companies;
using Domain.Persistence.Companies;

namespace Application.Common.Companies.Handlers
{
    public class DeleteCompanyRequestHandler : RequestHandler<DeleteCompanyRequest, SuccessResponse>
    {
        private readonly ICompanyUnitOfWork _unitOfWork;

        public DeleteCompanyRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result<SuccessResponse>> HandleRequest(DeleteCompanyRequest request, Result<SuccessResponse> result)
        {
            var company = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            if (company == null)
            {
                result.SetValidationResult(Domain.Common.Validation.ValidationItems.Common.NotFound);
                return result;
            }

            await _unitOfWork.Repository.DeleteAsync(request.Id);
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessResponse(true));
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
