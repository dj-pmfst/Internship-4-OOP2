namespace Application.DTOs.Users
{
    public class ActivateUserRequest
    {
        public ActivateUserRequest(int id) => Id = id;
        public int Id { get; set; }
    }
}
