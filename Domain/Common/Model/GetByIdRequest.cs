namespace Domain.Common.Model
{
    public class GetByIdRequest
    {
        public int Id { get; init; }

        public GetByIdRequest(int id)
        {
            this.Id = id; 
        }
    }
}
