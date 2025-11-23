using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Users
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateOnly DoB {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string adressStreet { get; set; }
        public string adressCity { get; set; }
        public decimal geoLat { get; set; }
        public decimal geoLng { get; set; }
        public string? website { get; set; }
        public TimestampAttribute createdAt { get; set; }
        public TimestampAttribute updatedAt { get; set; }
        public bool isActive { get; set; } = true;
        public async Task Create()
        {

        }
        public async Task CreateOrUpdateValidation()
        {
            if (Name?.Length > 100) { }
            if (Surname?.Length > 100) { }
            if (adressStreet?.Length > 150) { }
            if (adressCity?.Length > 150) { }
            if (website?.Length > 100) { } //valid url pattern 

        } 

    }
}
