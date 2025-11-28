namespace Application.DTOs.Users
{
    internal class ActivateUserRequest
    {
        public ActivateUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
