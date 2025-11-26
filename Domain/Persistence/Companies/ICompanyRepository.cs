using Domain.Entities.Company;

namespace Domain.Persistence.Companies
{
    internal interface ICompanyRepository
    {
        Task<Company> GetById(int id);
    }
}
