using Dapper;
using HealthApplication.Models;
using HealthApplication.Utilities;
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
            _connectionString = Utility.GetBuilder(Environment.GetEnvironmentVariable("DATABASE_URL"));
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
    }
}
