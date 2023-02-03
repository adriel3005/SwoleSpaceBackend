using Npgsql.Internal.TypeHandlers;

namespace HealthApplication.Models
{
    public class UserProfile
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public DateTime updated_at { get; set; }
        public string? full_name { get; set; }
        public string? avatar_url { get; set; }
        public string? website { get; set; }
    }
}
