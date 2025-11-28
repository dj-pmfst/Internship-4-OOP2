using Application.Common.Model;
using Domain.Common.Validation;
using Domain.Persistence.Companies;
using static Application.DTOs.Companies.AddCompanyRequest;

namespace Application.Common.Companies.Handlers
{
    public class UpdateCompanyRequestHandler : RequestHandler<UpdateCompanyRequest, SuccessResponse>
    {
        private readonly ICompanyUnitOfWork _unitOfWork;

        public UpdateCompanyRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result<SuccessResponse>> HandleRequest(UpdateCompanyRequest request, Result<SuccessResponse> result)
        {
            var company = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            if (company == null)
            {
                result.SetValidationResult(ValidationSeverity.); //ovo popravit
                return result;
            }

            if (!string.IsNullOrEmpty(request.Name)) company.Name = request.Name;

            var validation = await company.CreateOrUpdateValidation(_unitOfWork.Repository);
            result.SetValidationResult(validation);
            if (result.HasError) return result;

            await _unitOfWork.Repository.UpdateAsync(company);
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessResponse(true));
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
