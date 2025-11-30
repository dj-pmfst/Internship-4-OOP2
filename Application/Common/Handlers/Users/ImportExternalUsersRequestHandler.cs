using Application.Common.Interfaces;
using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Common.Validation;
using Domain.Common.Validation.ValidationItems;
using Domain.Persistence.Users;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Application.Common.Handlers.Users
{
    public class ImportExternalUsersRequest { }

    public class ImportExternalUsersRequestHandler : RequestHandler<ImportExternalUsersRequest, ImportExternalUsersResponse>
    {
        private readonly IExternalUserApiClient _externalUserApiClient;
        private readonly IUserUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "external_users_cache";

        public ImportExternalUsersRequestHandler(
            IExternalUserApiClient externalUserApiClient,
            IUserUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _externalUserApiClient = externalUserApiClient;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        protected override async Task<Result<ImportExternalUsersResponse>> HandleRequest(
            ImportExternalUsersRequest request,
            Result<ImportExternalUsersResponse> result)
        {
            var cachedUsers = _cacheService.Get<List<ExternalUserDTO>>(CacheKey);
            List<ExternalUserDTO>? externalUsers;

            if (cachedUsers != null && cachedUsers.Any())
            {
                externalUsers = cachedUsers;
            }
            else
            {
                externalUsers = await _externalUserApiClient.GetExternalUsersAsync();

                if (externalUsers == null)
                {
                    result.SetValidationResult(ValidationErrors.NotFound("Vanjski API nije dostupan."));
                    return result;
                }

                _cacheService.Set(CacheKey, externalUsers);
            }

            int importedCount = 0;
            int skippedCount = 0;
            var errors = new List<string>();
            var validationResult = new Domain.Common.Validation.ValidationResult();

            foreach (var externalUser in externalUsers)
            {
                try
                {
                    var existingUsers = await _unitOfWork.Repository.GetAll();
                    var userExists = existingUsers.Values.Any(u =>
                        u.Username == externalUser.Username ||
                        u.Email == externalUser.Email);

                    if (userExists)
                    {
                        skippedCount++;
                        continue;
                    }

                    if (!decimal.TryParse(externalUser.Address.Geo.Lat,
                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out var lat) || lat < -90 || lat > 90)
                    {
                        validationResult.AddValidationItem(ValidationItems.User.GeoLatRange);
                        skippedCount++;
                        continue;
                    }

                    if (!decimal.TryParse(externalUser.Address.Geo.Lng,
                        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                        CultureInfo.InvariantCulture,
                        out var lng) || lng < -180 || lng > 180)
                    {
                        validationResult.AddValidationItem(ValidationItems.User.GeoLngRange);
                        skippedCount++;
                        continue;
                    }


                    var nameParts = externalUser.Name.Split(' ', 2);
                    var firstName = nameParts.Length > 0 ? nameParts[0] : externalUser.Name;
                    var lastName = nameParts.Length > 1 ? nameParts[1] : "External";


                    var user = new Domain.Entities.Users.User
                    {
                        Name = firstName,
                        Surname = lastName,
                        Username = externalUser.Username,
                        Email = externalUser.Email,
                        Password = "ExternalUser123!",
                        AddressStreet = externalUser.Address.Street,
                        AddressCity = externalUser.Address.City,
                        GeoLat = lat,
                        GeoLng = lng,
                        website = $"https://{firstName}.com",
                        DoB = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-25)),
                        createdAt = DateTime.UtcNow,
                        updatedAt = DateTime.UtcNow,
                        isActive = true,
                        IsExternalUser = true
                    };

                    var domainResult = await user.Create(_unitOfWork.Repository);

                    if (domainResult.ValidationResult.HasError)
                    {
                        errors.Add($"User '{externalUser.Username}': {string.Join(", ", domainResult.ValidationResult.ValidationItems.Select(v => v.Message))}");
                        skippedCount++;
                        continue;
                    }

                    importedCount++;
                }
                catch (Exception ex)
                {
                    errors.Add($"User '{externalUser.Username}': {ex.Message}");
                    skippedCount++;
                }
            }


            if (importedCount > 0)
            {
                await _unitOfWork.SaveAsync();
            }

            var response = new ImportExternalUsersResponse
            {
                TotalFetched = externalUsers.Count,
                ImportedCount = importedCount,
                SkippedCount = skippedCount,
                Errors = errors,
                Message = $"Dodano {importedCount} korisnika, preskoceno {skippedCount} korisnika."
            };

            result.SetResult(response);
            return result;
        }

        protected override Task<bool> IsAuthorized()
        {
            return Task.FromResult(true);
        }
        private bool IsValidCoordinate(decimal lat, decimal lng)
        {
            return lat >= -90 && lat <= 90 && lng >= -180 && lng <= 180;
        }

        private const decimal referenceLat = 40.15m;
        private const decimal referenceLng = 20.102m;
    }

    public class ImportExternalUsersResponse
    {
        public int TotalFetched { get; set; }
        public int ImportedCount { get; set; }
        public int SkippedCount { get; set; }
        public List<string> Errors { get; set; } = new();
        public string Message { get; set; }
    }
}