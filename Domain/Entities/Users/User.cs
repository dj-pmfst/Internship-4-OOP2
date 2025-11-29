
using Domain.Common.Model;
using Domain.Common.Validation;
using Domain.Common.Validation.ValidationItems;
using Domain.Persistence.Users;

namespace Domain.Entities.Users
{
    public class User
    {
        public const int FirstNameMaxLength = 100, 
            WebsiteMaxLength = 100, SurnameMaxLength = 100, 
            StreetMaxLength = 150, CityMaxLength = 150;
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
        public async Task <Result<bool>> Create(IUserRepository userRepository)
        {
            var validationResult = await CreateOrUpdateValidation(userRepository);
            if (validationResult.HasError)
            {
                return new Result<bool> (false, validationResult);
            }

            await userRepository.InsertAsync(this);

            return new Result<bool> (true, validationResult);
        }
        public async Task <ValidationResult> CreateOrUpdateValidation(IUserRepository userRepository)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(Name))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Name"));

            if (string.IsNullOrWhiteSpace(Surname))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Surname"));

            if (string.IsNullOrWhiteSpace(Username))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Username"));

            if (string.IsNullOrWhiteSpace(Email))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Email"));

            if (string.IsNullOrWhiteSpace(Password))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Password"));

            if (string.IsNullOrWhiteSpace(adressStreet))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Address Street"));

            if (string.IsNullOrWhiteSpace(adressCity))
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Address City"));

            if (geoLat == 0)
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Latitude"));

            if (geoLng == 0)
                AddValidationResult(validationResult, ValidationErrors.FieldIsRequired("Longitude"));

            if (validationResult.HasError)
                return validationResult;

            if (Name?.Length > FirstNameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.FirstNameMaxLength); 
            }

            if (Surname?.Length > SurnameMaxLength)
            {
                validationResult.AddValidationItem(ValidationItems.User.SurnameMaxLength);
            }

            if (!await userRepository.IsUsernameUniqueAsync(Username, Id))
            {
                validationResult = ValidationErrors.AlreadyExists("Username");
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
            }

            if (!ValidationHelpers.IsValidUrl(website))
            {
                validationResult.AddValidationItem(ValidationItems.User.URLInvalid);
            }

            if (!ValidationHelpers.IsValidEmail(Email))
            {
                validationResult.AddValidationItem(ValidationItems.User.EmailInvalid);
            }

            if (!await userRepository.IsWithin3KmAsync(geoLat, geoLng))
            {
                validationResult.AddValidationItem(ValidationItems.User.UserTooFar);
            }


            if (!await userRepository.IsEmailUniqueAsync(Email, Id))
            {
                var emailError = ValidationErrors.AlreadyExists("Email");
                foreach (var item in emailError.ValidationItems)
                    validationResult.AddValidationItem(item);
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
