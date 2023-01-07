using HealthApplication.Models;

namespace HealthApplication.Repositories
{
    public interface ISupabaseRepository
    {
        public Task<bool> UpdateUser(UserProfile user);

        public Task<UserProfile> GetUser(Guid id);
    }
}
