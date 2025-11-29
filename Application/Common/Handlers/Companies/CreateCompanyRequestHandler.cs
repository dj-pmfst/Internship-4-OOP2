using Application.Common.Model;
using Domain.Persistence.Companies;

namespace Application.Common.Handlers.Companies
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
    }
    public class CreateCompanyRequestHandler : RequestHandler<CreateCompanyRequest, SuccessPostResponse>
    {
        private readonly ICompanyUnitOfWork _unitOfWork;
        public CreateCompanyRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateCompanyRequest request, Result<SuccessPostResponse> result)
        {
            var company = new Domain.Entities.Companies.Company
            {
                Name = request.Name,
            };

            var domainResult = await company.Create(_unitOfWork.Repository);

            result.SetValidationResult(domainResult.ValidationResult);

            if (result.HasError)
                return result;

            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessPostResponse(company.Id));
            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
