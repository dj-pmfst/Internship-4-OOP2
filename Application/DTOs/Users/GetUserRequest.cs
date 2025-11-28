namespace Application.DTOs.Users
{
    internal class GetUserRequest
    {
        public GetUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
