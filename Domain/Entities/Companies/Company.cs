using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Company
{
    internal class Company
    {
        //konstante definirat
        public int Id { get; set; }
        public string Name { get; set; }
        public async Task <Result<int?>> Create()
        {

        }
        public async Task <ValidationResult> CreateOrUpdateValidation()
        {
            if (Name?.Length > 150) { } //triba bit unique
        }
    }
}
