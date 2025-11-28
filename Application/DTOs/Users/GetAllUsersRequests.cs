namespace Application.DTOs.Users
{
    public class GetAllUsersRequest
    {
        public GetAllUsersRequest() { }
        public GetAllUsersRequest(int? id)
        {
            Id = id;
        }
        public int? Id { get; set; } 
    }
}
