using Dapper;
using HealthApplication.Models;
using Npgsql;
using Npgsql.Internal.TypeHandlers;
using System.Data;

namespace HealthApplication.Repositories
{
    public class SupabaseRepository : ISupabaseRepository
    {
        private readonly string _connectionString;
        public SupabaseRepository()
        {
            _connectionString = GetBuilder();
        }

        /// <summary>
        /// Updates user if user exists
        /// </summary>
        /// <param name="user"></param>
        /// <see cref="https://www.postgresql.org/docs/current/sql-createfunction.html"/>
        /// <returns>whether user was succesfully updated</returns>
        public async Task<bool> UpdateUser(UserProfile user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Id = user.id,
                    Username = user.username,
                    FullName = user.full_name,
                    AvatarUrl = user.avatar_url,
                    Website = user.website
                };

                string sql = "select * from update_user(@Id, @Username, @FullName, @AvatarUrl, @Website)";

                var result = (await connection.QueryAsync<bool>(sql, parameters)).FirstOrDefault();

                return result;
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
