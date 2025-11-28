using Domain.Common.Validation;

namespace Application.Common.Model
{
    public class Result<TValue> where TValue : class
    {
        private List<ValidationResultItem> _info = new List<ValidationResultItem>();
        private List<ValidationResultItem> _warning = new List<ValidationResultItem>();
        private List<ValidationResultItem> _error = new List<ValidationResultItem>();

        public TValue? Value { get; set; }
        public Guid RequestId { get; set; }
        public bool isAuthorized { get; set; }

        public IReadOnlyList <ValidationResultItem> Infos
        {
            get => _info.AsReadOnly();
            init => _info.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Warnings
        {
            get => _warning.AsReadOnly();
            init => _warning.AddRange(value);
        }
        public IReadOnlyList<ValidationResultItem> Errors
        {
            get => _error.AsReadOnly();
            init => _error.AddRange(value);
        }

        public bool HasError => Errors.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Error);
        public bool HasWarning => Warnings.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Warning);
        public bool HasInfo => Infos.Any(validationResult => validationResult.ValidationSeverity == ValidationSeverity.Info);

        public void SetResult(TValue value)
        {
            Value = value;
        }
        public void SetValidationResult(ValidationResult validationResult)
        {
            _error?.AddRange(validationResult.ValidationItems.Where(v => v.ValidationSeverity == ValidationSeverity.Error).Select(v => ValidationResultItem.FormValidationResultItem(v)));
        }

        public void SetUnauthorizedResult()
        {
            Value = null;
            isAuthorized = false;

        }
    }
}
