using HealthApplication.Models;

namespace HealthApplication.Repositories
{
    public interface ISupabaseRepository
    {
        public Task UpsertUser(UserProfile user);

        public Task<IEnumerable<UserProfile>> GetUser(string id);
    }
}
