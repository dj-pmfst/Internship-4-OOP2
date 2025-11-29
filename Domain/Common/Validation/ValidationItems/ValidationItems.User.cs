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

            public static readonly ValidationItem URLInvalid = new ValidationItem(
                code: $"{CodePrefix}6",
                message: $"URL nije ispravnog formata.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
             );

            public static readonly ValidationItem GeoLatRange = new ValidationItem(
                code: $"{CodePrefix}7",
                message: "Geografska širina mora biti između -90 i 90 stupnjeva.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem GeoLngRange = new ValidationItem(
                code: $"{CodePrefix}8",
                message: "Geografska dužina mora biti između -180 i 180 stupnjeva.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
            );

            public static readonly ValidationItem EmailInvalid = new ValidationItem(
                code: $"{CodePrefix}9",
                message: $"Email nije ispravnog formata.",
                severity: ValidationSeverity.Error,
                type: ValidationType.FormalValidation
             );
        }
    }
}
