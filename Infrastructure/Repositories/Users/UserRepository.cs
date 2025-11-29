using Domain.Common.Validation;
using Domain.Entities.Users;
using Domain.Persistence.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public sealed class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IDapperManager _dapperManager;
        public UserRepository(DbContext context, IDapperManager dapperManager) 
            : base(context)
        {
            _dapperManager = dapperManager;
        }
        public async Task<User> GetById(int id)
        {
            var sql = @"
                        SELECT
                            id AS Id,
                            name AS Name,
                            surname AS Surname,
                            username AS Username,
                            dob AS DoB,
                            email AS Email,
                            password AS Password,
                            adressstreet AS adressStreet,
                            adresscity AS adressCity,
                            geolat AS geoLat,
                            geolng AS geoLng,
                            website AS Website,
                            createdat AS createdAt,
                            updatedat AS updatedAt,
                            isactive AS isActive
                        FROM public.users
                        WHERE id=@Id
                    ";

            var parameters = new { Id = id };

            return await _dapperManager.QuerySingleAsync<User>(sql, parameters);
        }

        public async Task<bool> IsUsernameUniqueAsync(string username, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Users 
                WHERE Username = @Username AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapperManager.QuerySingleAsync<int>(sql, new { Username = username, Id = id });

            return count == 0;
        }
        public async Task<bool> IsEmailUniqueAsync(string email, int? id = null)
        {
            var sql = @"SELECT COUNT(*) 
                FROM Users 
                WHERE Email = @Email AND (@Id IS NULL OR Id <> @Id)";

            var count = await _dapperManager.QuerySingleAsync<int>(sql, new { Email = email, Id = id });

            return count == 0;
        }

        public async Task<User?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> IsWithin3KmAsync(decimal latitude, decimal longitude, int? excludeUserId = null)
        {
            var users = await _dbSet
                .Where(u => !excludeUserId.HasValue || u.Id != excludeUserId.Value)
                .ToListAsync();

            foreach (var user in users)
            {
                var distance = GeoHelper.DistanceInKm(
                    user.geoLat,
                    user.geoLng,
                    latitude,
                    longitude
                );

                if (distance <= 3) 
                    return true;
            }
            return false;
        }

    }
}
