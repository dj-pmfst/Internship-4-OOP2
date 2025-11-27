
namespace Api.Common
{
    public class Response<TValue> where TValue : class
    {
        public IReadOnlyList<ValidationResultItem> Infos { get; set; }
        public IReadOnlyList<ValidationResultItem> Warnings { get; set; }
        public IReadOnlyList<ValidationResultItem> Errors { get; set; }

        public TValue? Value { get; private set; }
        public Guid RequestId {  get; private set; }

        public Response(Result<TValue> result)
        {
            Infos = result.Infos;
            Warnings = result.Warnings;
            Errors = result.Errors;
            Value = result.Value;
            RequestId = result.RequestId;
        }
    }
}
