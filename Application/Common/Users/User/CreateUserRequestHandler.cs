using Application.Common.Model;

namespace Application.Common.Users.User
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateOnly DoB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string adressStreet { get; set; }
        public string adressCity { get; set; }
        public decimal geoLat { get; set; }
        public decimal geoLng { get; set; }
        public string? website { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
    internal class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessPostResponse>
    {
    }
}
