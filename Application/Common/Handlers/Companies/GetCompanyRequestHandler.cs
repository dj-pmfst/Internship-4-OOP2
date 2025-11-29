using Application.Common.Model;
using Application.DTOs.Companies;
using Domain.Common.Validation;
using Domain.Entities.Companies;
using Domain.Persistence.Companies;
using Domain.Persistence.Users; 

namespace Application.Common.Handlers.Companies
{
    public class GetCompanyRequestHandler : RequestHandler<GetCompanyRequest, GetResponse<Company>>
    {
        private readonly ICompanyUnitOfWork _companyUnitOfWork;
        private readonly IUserRepository _userRepository;

        public GetCompanyRequestHandler(
            ICompanyUnitOfWork companyUnitOfWork,
            IUserRepository userRepository)
        {
            _companyUnitOfWork = companyUnitOfWork;
            _userRepository = userRepository;
        }

        protected override async Task<Result<GetResponse<Company>>> HandleRequest(
            GetCompanyRequest request,
            Result<GetResponse<Company>> result)
        {

            var user = await _userRepository.GetByUsernameAndPasswordAsync(request.Username, request.Password);
            if (user == null || !user.isActive)
            {
                result.SetValidationResult(
                    ValidationErrors.FieldIsRequired("Valid active username/password required"));
                return result;
            }

            var company = await _companyUnitOfWork.Repository.GetByIdAsync(request.Id);
            var items = company != null ? new List<Company> { company } : new List<Company>();

            result.SetResult(new GetResponse<Company> { Items = items });
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
