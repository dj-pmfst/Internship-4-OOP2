namespace Application.DTOs.Users
{
    internal class DeleteUserRequest
    {
        public DeleteUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
