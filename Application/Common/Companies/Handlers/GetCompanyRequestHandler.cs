using Application.Common.Model;
using Application.DTOs.Companies;
using Domain.Entities.Companies;
using Domain.Persistence.Companies;

namespace Application.Common.Companies.Handlers
{
    public class GetCompanyRequestHandler
    {
        private readonly ICompanyUnitOfWork _unitOfWork;

        public GetCompanyRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result<GetResponse<Company>>> HandleRequest(GetCompanyRequest request, Result<GetResponse<Company>> result)
        {
            var company = await _unitOfWork.Repository.GetByIdAsync(request.Id);
            var items = company != null ? new List<Company> { company } : new List<Company>();

            result.SetResult(new GetResponse<Company> { Items = items });
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
