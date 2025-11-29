namespace Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class User
        {
            public static string CodePrefix = nameof(User);

            public static readonly ValidationItem FirstNameMaxLength = new ValidationItem(
                code: $"{CodePrefix}1",
                message: $"Ime ne smije biti duže od {Entities.Users.User.FirstNameMaxLength} znakova.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem SurnameMaxLength = new ValidationItem(
                code: $"{CodePrefix}2",
                message: $"Prezime ne smije biti duže od {Entities.Users.User.SurnameMaxLength} znakova.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem StreetMaxLength = new ValidationItem(
                code: $"{CodePrefix}3",
                message: $"Ime ulice ne smije biti duže od {Entities.Users.User.StreetMaxLength} znakova.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem CityMaxLength = new ValidationItem(
                code: $"{CodePrefix}4",
                message: $"Ime grada ne smije biti duže od {Entities.Users.User.CityMaxLength} znakova.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem WebsiteMaxLength = new ValidationItem(
                code: $"{CodePrefix}5",
                message: $"Ime web stranice ne smije biti duže od {Entities.Users.User.WebsiteMaxLength} znakova.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );
        }
    }
}
