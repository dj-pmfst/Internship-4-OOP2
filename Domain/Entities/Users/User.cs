using Domain.Common.Model;
using Domain.Common.Validation;
using Domain.Common.Validation.ValidationItems;
using Domain.Persistence.Users;

namespace Domain.Entities.Users
{
    public class User
    {
        public const int FirstNameMaxLength = 100, WebsiteMaxLength = 100, SurnameMaxLength = 100, StreetMaxLength = 150, CityMaxLength = 150;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateOnly DoB {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string adressStreet { get; set; }
        public string adressCity { get; set; }
        public decimal geoLat { get; set; }
        public decimal geoLng { get; set; }
        public string? website { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool isActive { get; set; } = true;
        public async Task <Result<bool>> Create()
        {
            var validationResult = await CreateOrUpdateValidation(IUserRepository userRepository);
            if (validationResult.HasError)
            {
                return new Result<bool> (false, validationResult);
            }

            await userRepository.InsertAsync(this);

            return new Result<bool> (true, validationResult);
        }
        public async Task <ValidationResult> CreateOrUpdateValidation()
        {
            var validationResult = new ValidationResult();

            if (Name?.Length > FirstNameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.FirstNameMaxLength); 
            }
            if (Surname?.Length > SurnameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.SurnameMaxLength);
            }
            if (adressStreet?.Length > StreetMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.StreetMaxLength);
            }
            if (adressCity?.Length > CityMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.CityMaxLength);
            }
            if (website?.Length > WebsiteMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.WebsiteMaxLength);
            } //valid url pattern 

            return validationResult;
        } 

    }
}
