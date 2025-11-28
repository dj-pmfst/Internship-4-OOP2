namespace Application.DTOs.Users
{
    public class DeleteUserRequest
    {
        public DeleteUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
