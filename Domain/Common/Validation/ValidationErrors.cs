namespace Domain.Common.Validation
{
    public static class ValidationErrors
    {
        public static ValidationResult NotFound(string entityName)
        {
            var result = new ValidationResult();

            result.AddValidationItem(new ValidationItem(
                code: $"{entityName}NotFound",
                message: $"{entityName} nije pronadeno.",
                severity: ValidationSeverity.Error
            ));

            return result;
        }

        public static ValidationResult AlreadyExists(string entityName)
        {
            var result = new ValidationResult();

            result.AddValidationItem(new ValidationItem(
                code: $"{entityName}AlreadyExists",
                message: $"{entityName} vec postoji.",
                severity: ValidationSeverity.Error
            ));

            return result;
        }

        public static ValidationResult FieldIsRequired(string fieldName)
        {
            var result = new ValidationResult();

            result.AddValidationItem(new ValidationItem(
                code: $"{fieldName}Required",
                message: $"{fieldName} je obavezno.",
                severity: ValidationSeverity.Error
            ));

            return result;
        }
    }
}
