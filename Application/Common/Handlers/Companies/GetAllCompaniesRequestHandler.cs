using Application.Common.Model;
using Application.DTOs.Companies;
using Domain.Common.Validation;
using Domain.Entities.Companies;
using Domain.Persistence.Companies;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Companies
{
    public class GetAllCompaniesRequestHandler : RequestHandler<GetAllCompaniesRequest, GetResponse<Company>>
    {
        private readonly ICompanyUnitOfWork _companyUnitOfWork;
        private readonly IUserRepository _userRepository;

        public GetAllCompaniesRequestHandler(
            ICompanyUnitOfWork companyUnitOfWork,
            IUserRepository userRepository)
        {
            _companyUnitOfWork = companyUnitOfWork;
            _userRepository = userRepository;
        }

        protected override async Task<Result<GetResponse<Company>>> HandleRequest(
            GetAllCompaniesRequest request,
            Result<GetResponse<Company>> result)
        {

            var user = await _userRepository.GetByUsernameAndPasswordAsync(request.Username, request.Password);
            if (user == null || !user.isActive)
            {
                result.SetValidationResult(
                    ValidationErrors.FieldIsRequired("username/password"));
                return result;
            }

            var response = await _companyUnitOfWork.Repository.GetAll();
            var companies = response.Values.ToList();

            result.SetResult(new GetResponse<Company> { Items = companies });
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
