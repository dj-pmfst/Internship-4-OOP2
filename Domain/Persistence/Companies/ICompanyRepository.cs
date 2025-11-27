using Domain.Entities.Company;

namespace Domain.Persistence.Companies
{
    public interface ICompanyRepository
    {
        Task<Company> GetById(int id);
    }
}
