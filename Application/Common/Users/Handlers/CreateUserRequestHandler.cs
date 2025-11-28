using Application.Common.Model;
using Domain.Common.Validation;
using Domain.Persistence.Users;

namespace Application.Common.Users.Handlers
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
    public class CreateUserRequestHandler : RequestHandler<CreateUserRequest, SuccessPostResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public CreateUserRequestHandler(IUserUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async override Task<Result<SuccessPostResponse>> HandleRequest(CreateUserRequest request, Result<SuccessPostResponse> result)
        {
            var user = new Domain.Entities.Users.User
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                DoB = request.DoB,
                Email = request.Email,
                Password = request.Password,
                adressStreet = request.adressStreet,
                adressCity = request.adressCity,
                geoLat = request.geoLat,
                geoLng = request.geoLng,
                website = request.website,
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
