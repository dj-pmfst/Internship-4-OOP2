using Application.Common.Model;
using Domain.Common.Validation;
using Domain.Common.Validators;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class UpdateUserRequest
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
    public class UpdateUserRequestHandler : RequestHandler<UpdateUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public UpdateUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(UpdateUserRequest request, Result<SuccessPostResponse> result)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                result.SetValidationResult(ValidationErrors.NotFound($"User with ID {request.Id}"));
                return result;
            }

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Username = request.Username;
            user.DoB = request.DoB;
            user.Email = request.Email;
            user.Password = request.Password;
            user.AddressStreet = request.AddressStreet;
            user.AddressCity = request.AddressCity;
            user.GeoLat = request.GeoLat;
            user.GeoLng = request.GeoLng;
            user.website = request.website;
            user.updatedAt = DateTime.UtcNow;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var passwordValidation = ValidationPassword.Validate(request.Password);
                if (passwordValidation.HasError)
                {
                    result.SetValidationResult(passwordValidation);
                    return result;
                }
            }

            var validation = await user.CreateOrUpdateValidation(_unitOfWork.Repository);
            result.SetValidationResult(validation);

            if (result.HasError) 
                return result;

            _unitOfWork.Repository.Update(user);
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
