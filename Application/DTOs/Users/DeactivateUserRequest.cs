namespace Application.DTOs.Users
{
    internal class DeactivateUserRequest
    {
        public DeactivateUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
