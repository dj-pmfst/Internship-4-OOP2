using Application.Common.Model;
using Application.DTOs.Users;
using Domain.Persistence.Users;

namespace Application.Common.Handlers.Users
{
    public class ImportExternalUsersRequestHandler : RequestHandler<ImportExternalUsersRequest, SuccessResponse>
    {
        private readonly IUserUnitOfWork _unitOfWork;
        public ImportExternalUsersRequestHandler(IUserUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        protected override async Task<Result<SuccessResponse>> HandleRequest(ImportExternalUsersRequest request, Result<SuccessResponse> result)
        {
            var externalUsers = await _unitOfWork.Repository.ImportFromExternalAsync(); 

            foreach (var ext in externalUsers)
            {
                var existing = await _unitOfWork.Repository.FindByExternalIdAsync(ext.ExternalId); 
                if (existing == null) await _unitOfWork.Repository.InsertAsync(ext);
                else
                {
                    existing.Name = ext.Name;
                    existing.Surname = ext.Surname;
                    existing.adressCity = ext.adressCity;
                    existing.adressStreet = ext.adressStreet;
                    existing.geoLat = ext.geoLat;
                    existing.geoLng = ext.geoLng;
                    await _unitOfWork.Repository.UpdateAsync(existing);
                }
            }

            await _unitOfWork.SaveAsync();
            result.SetResult(new SuccessResponse(true));
            return result;
        }

        protected override Task<bool> IsAuthorized() => Task.FromResult(true);
    }
}
