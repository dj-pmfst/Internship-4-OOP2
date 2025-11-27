using Domain.Common.Model;
using Domain.Common.Validation;
using Domain.Common.Validation.ValidationItems;
using Domain.Persistence.Companies;

namespace Domain.Entities.Companies
{
    public class Company
    {
        public const int NameMaxLength = 150;
        public int Id { get; set; }
        public string Name { get; set; }
        public async Task<Result<bool>> Create()
        {
            var validationResult = await CreateOrUpdateValidation(ICompanyRepository companyRepository);
            if (validationResult.HasError)
            {
                return new Result<bool>(false, validationResult);
            }

            await companyRepository.InsertAsync(this);

            return new Result<bool>(true, validationResult);
        }
        public async Task<ValidationResult> CreateOrUpdateValidation()
        {
            var validationResult = new ValidationResult();

            if (Name?.Length > NameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.FirstNameMaxLength);
            } // unikatno ime

            return validationResult;
        }
    }
}
