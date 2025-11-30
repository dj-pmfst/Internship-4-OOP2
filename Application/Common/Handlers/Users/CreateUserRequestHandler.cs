using Application.Common.Model;
using Domain.Common.Validation;
using Domain.Common.Validators;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class CreateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public DateOnly DoB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
        public string? website { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
    public class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public CreateUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateUserRequest request, Result<SuccessPostResponse> result)
        {
            var passwordValidation = ValidationPassword.Validate(request.Password);
            if (!passwordValidation.HasError)
            {
                result.SetValidationResult(passwordValidation);
                return result;
            }

            var user = new Domain.Entities.Users.User
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                DoB = request.DoB,
                Email = request.Email,
                Password = request.Password,
                AddressStreet = request.AddressStreet,
                AddressCity = request.AddressCity,
                GeoLat = request.GeoLat,
                GeoLng = request.GeoLng,
                website = request.website,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };

            var domainResult = await user.Create(_unitOfWork.Repository);

            result.SetValidationResult(domainResult.ValidationResult);

            if (result.HasError)
                return result;

            await _unitOfWork.SaveAsync();

            result.SetResult(new SuccessPostResponse(user.Id));
            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
    }
}
