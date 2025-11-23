namespace Domain.Entities.Company
{
    internal class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public async Task Create()
        {

        }
        public async Task CreateOrUpdateValidation()
        {
            if (Name?.Length > 150) { } //triba bit unique
        }
    }
}
