namespace Application.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateOnly DoB { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; 
        public string AdressStreet { get; set; } = null!;
        public string AdressCity { get; set; } = null!;
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
        public string? Website { get; set; }
    }
}
