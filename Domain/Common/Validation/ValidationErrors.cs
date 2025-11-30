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

        public static ValidationResult InvalidCredentials()
        {
            var result = new ValidationResult();
            result.AddValidationItem(new ValidationItem( 
                code: "INVALID_CREDENTIALS",
                message: "Neispravan username ili lozinka. ",
                severity: ValidationSeverity.Error
            ));
            return result;
        }

        public static ValidationResult UserInactive()
        {
            var result = new ValidationResult();
            result.AddValidationItem(new ValidationItem( 
                code: "USER_INACTIVE",
                message: "User account is deactivated.",
                severity: ValidationSeverity.Error
            ));
            return result;
        }
    }
}
