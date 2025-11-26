using Domain.Common.Validation;

namespace Application.Common.Model
{
    public class ValidationResultItem
    {
        public ValidationSeverity ValidationSeverity { get; set; }
        public ValidationType ValidationType { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public static ValidationResultItem FormValidationResultItem(ValidationResultItem item)
        {
            return new ValidationResultItem
            {
                ValidationSeverity = item.ValidationSeverity,
                ValidationType = item.ValidationType,
                Message = item.Message,
                Code = item.Code
            };
        }
    }
}
