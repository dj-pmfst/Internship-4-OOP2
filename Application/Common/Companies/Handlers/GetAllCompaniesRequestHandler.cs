using Application.Common.Model;
using Application.DTOs.Companies;
using Domain.Entities.Companies;
using Domain.Persistence.Companies;

namespace Application.Common.Companies.Handlers
{
    public class GetAllCompaniesRequestHandler : RequestHandler<GetAllCompaniesRequest, GetResponse<Company>>
    {
        private readonly ICompanyUnitOfWork _unitOfWork;

        public GetAllCompaniesRequestHandler(ICompanyUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result<GetResponse<Company>>> HandleRequest(GetAllCompaniesRequest request, Result<GetResponse<Company>> result)
        {
           ///sifra i username myb???
            var companies = (await _unitOfWork.Repository.GetAllAsync()).ToList();

            result.SetResult(new GetResponse<Company> { Items = companies });
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true); 
    }
}
