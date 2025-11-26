namespace Domain.Common.Validation
{
    public class ValidationItem
    {
        public ValidationSeverity ValidationSeverity {  get; set; }
        public ValidationType ValidationType { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
