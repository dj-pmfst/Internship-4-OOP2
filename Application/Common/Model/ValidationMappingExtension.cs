using Application.Common.Model;

public static class ValidationMappingExtensions
{
    public static ValidationResultItem ToAppItem(this Domain.Common.Validation.ValidationItem item)
    {
        return new ValidationResultItem
        {
            Code = item.Code,
            Message = item.Message,
            ValidationSeverity = item.ValidationSeverity
        };
    }
}