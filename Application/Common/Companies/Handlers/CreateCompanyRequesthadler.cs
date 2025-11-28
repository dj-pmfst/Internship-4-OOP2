using Application.Common.Model;
using Domain.Persistence.Companies;

namespace Application.Common.Companies.Handlers
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
    }
    internal class CreateCompanyRequestHandler : RequestHandler<CreateCompanyRequest, SuccessPostResponse>
    {
        private readonly ICompanyUnitOfWork _unitOfWork;
        public CreateCompanyRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateCompanyRequest request, Result<SuccessPostResponse> result)
        {
            var user = new Domain.Entities.Companies.Company
            {
                Name = request.Name,
            };

            var validation = await user.Create(_unitOfWork.Repository);
            result.SetValidationResult(validation);

            if (result.HasError)
            {
                return result;
            }
            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessPostResponse(user.Id));
            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
