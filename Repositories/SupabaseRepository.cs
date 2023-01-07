using Dapper;
using HealthApplication.Models;
using Npgsql;
using Npgsql.Internal.TypeHandlers;
using System.Data;

namespace HealthApplication.Repositories
{
    public class SupabaseRepository : ISupabaseRepository
    {
        // TODO: add similar setup to Forecast Repository.

        // Also TODO: Have SupabaseController referencing this Repository

        private readonly string _connectionString;
        public SupabaseRepository()
        {
            _connectionString = GetBuilder();
        }

        public async Task UpsertUser(UserProfile user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {

                var parameters = new {
                    Id = user.id,
                    Username = user.username,
                    FullName = user.full_name,
                    AvatarUrl = user.avatar_url,
                    Website = user.website
                };

                // TODO: replace with stored procedure that does insert

                // Supabase Upsert equivalent
                string sql = "insert into public.profiles(id, username, full_name, avatar_url, website) " +
                    "values(@Id, @Username, @FullName, @AvatarUrl, @Website) " +
                    "ON CONFLICT (id) DO UPDATE SET " +
                    "username = EXCLUDED.username, " +
                    "full_name = EXCLUDED.full_name, " +
                    "avatar_url = EXCLUDED.avatar_url, " +
                    "website = EXCLUDED.website ";

                var results = await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<UserProfile> GetUser(Guid id) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "select * from get_user(@user_id)";


                var result = (await connection.QueryAsync<UserProfile>(sql, new { user_id = id })).FirstOrDefault();
                
                return result;
            }
        }

        // TODO: Methods like GetBuilder are repeated in Repositories. There should be a better way to reference this.

        /// <summary>
        /// Create connection string for DB connection
        /// </summary>
        /// <returns>connection string</returns>
        public static string GetBuilder()
        {
            // TODO: set env variables in deployment pipeline
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl, true);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }
    }
}
