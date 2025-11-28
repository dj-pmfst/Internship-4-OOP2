namespace Application.DTOs.Companies
{
    public class GetAllCompaniesRequest
    {
        public GetAllCompaniesRequest() { }
        public GetAllCompaniesRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
