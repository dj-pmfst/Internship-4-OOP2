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
        public async Task<Result<bool>> Create(ICompanyRepository companyRepository)
        {
            var validationResult = await CreateOrUpdateValidation(companyRepository);
            if (validationResult.HasError)
            {
                return new Result<bool>(false, validationResult);
            }

            await companyRepository.InsertAsync(this);

            return new Result<bool>(true, validationResult);
        }
        public async Task<ValidationResult> CreateOrUpdateValidation(ICompanyRepository companyRepository)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(Name))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Name"));

            if (validationResult.HasError)
                return validationResult;

            if (Name?.Length > NameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.Company.NameMaxLength);
            }

            if (!await companyRepository.IsNameUniqueAsync(Name, Id))
            {
                validationResult = ValidationErrors.AlreadyExists("Name");
            }

            return validationResult;
        }

        private void AddValidationResult(ValidationResult main, ValidationResult toAdd)
        {
            foreach (var item in toAdd.ValidationItems)
                main.AddValidationItem(item);
        }
    }
}
