namespace Application.Common.Model
{
    public class SuccessResponse
    {
        public bool isSuccess {  get; init; }
        public SuccessResponse(bool issuccess) { isSuccess = issuccess; }
    }
}
