namespace Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class User
        {
            public static string CodePrefix = nameof(User);

            public static readonly ValidationItem FirstNameMaxLength = new ValidationItem
            {
                Code = $"{CodePrefix}1",
                Message = $"Ime ne smije biti duže od {Entities.Users.User.FirstNameMaxLength} znakova.",
                ValidationSeverity = ValidationSeverity.Error,
                ValidationType = ValidationType.FormalValidation
            };

            public static readonly ValidationItem SurnameMaxLength = new ValidationItem
            {
                Code = $"{CodePrefix}2",
                Message = $"Prezime ne smije biti duže od {Entities.Users.User.SurnameMaxLength} znakova.",
                ValidationSeverity = ValidationSeverity.Error,
                ValidationType = ValidationType.FormalValidation
            };

            public static readonly ValidationItem StreetMaxLength = new ValidationItem
            {
                Code = $"{CodePrefix}3",
                Message = $"Ime ulice ne smije biti duže od {Entities.Users.User.StreetMaxLength} znakova.",
                ValidationSeverity = ValidationSeverity.Error,
                ValidationType = ValidationType.FormalValidation
            };

            public static readonly ValidationItem CityMaxLength = new ValidationItem
            {
                Code = $"{CodePrefix}4",
                Message = $"Ime grada ne smije biti duže od {Entities.Users.User.CityMaxLength} znakova.",
                ValidationSeverity = ValidationSeverity.Error,
                ValidationType = ValidationType.FormalValidation
            };

            public static readonly ValidationItem WebsiteMaxLength = new ValidationItem
            {
                Code = $"{CodePrefix}5",
                Message = $"Ime web stranice ne smije biti duže od {Entities.Users.User.WebsiteMaxLength} znakova.",
                ValidationSeverity = ValidationSeverity.Error,
                ValidationType = ValidationType.FormalValidation
            };
        }
    }
}
