namespace Application.DTOs.Users
{
    public class GetUserRequest
    {
        public GetUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
