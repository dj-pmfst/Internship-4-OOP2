namespace Domain.Common.Validation.ValidationItems
{
    public static partial class ValidationItems
    {
        public static class Company
        {
            public static string CodePrefix = nameof(Company);

            public static readonly ValidationItem NameMaxLength = new ValidationItem(
                $"{CodePrefix}1",
                $"Ime kompanije ne smije biti duže od {Entities.Companies.Company.NameMaxLength} znakova.",
                ValidationSeverity.Error,
                ValidationType.FormalValidation
            );
        }
    }
}
