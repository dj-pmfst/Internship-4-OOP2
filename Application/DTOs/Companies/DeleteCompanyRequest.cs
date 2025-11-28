namespace Application.DTOs.Companies
{
    public class DeleteCompanyRequest
    {
        public DeleteCompanyRequest(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
