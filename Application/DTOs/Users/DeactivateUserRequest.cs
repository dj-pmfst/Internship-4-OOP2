namespace Application.DTOs.Users
{
    public class DeactivateUserRequest
    {
        public DeactivateUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
