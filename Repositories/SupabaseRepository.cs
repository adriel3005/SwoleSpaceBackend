using Dapper;
using HealthApplication.Models;
using HealthApplication.Utilities;
using Npgsql;
using Npgsql.Internal.TypeHandlers;
using System.Data;
using System.Net.WebSockets;

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

        public async Task AddRoutineExercise(RoutineExerciseModel re)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new
                {
                    Rei = re.routine_exercise_id,
                    UrID = re.user_routine_id,
                    Ei = re.exercise_id,
                    Repetition = re.repetition,
                    Sets = re.sets,
                    DW = re.default_weight,
                    UserID = re.user_id,
                    Eo = re.exercise_order
                };
                string sql = "select * from insert_routine_exercise(@Rei, @UrID, @Ei, @Repetition, @Sets, @DW, @UserID, @Eo)";

                // TODO: We need to have a method of verifying if the data was correctly inserted. Otherwise we may be missing user
                // routines.
                var result = (await connection.QueryAsync(sql, parameters));
            }
        }
        public async Task<IEnumerable<RoutineExerciseModel>> GetRoutineExercises(Guid urID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new
                {
                    ur_ID = urID,
                };
                string sql = "select * from get_routine_exercises(@ur_ID)";


                return await connection.QueryAsync<RoutineExerciseModel>(sql, parameters);
            }
        }

        /// <summary>
        /// Adds user routine to DB. This can be used to reference associated Routine Exercises
        /// </summary>
        /// <param name="ur"></param>
        /// <returns></returns>
        public async Task AddUserRoutine(UserRoutineModel ur)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new
                {
                    UrID = ur.user_routine_id,
                    uID = ur.user_id,
                    rName = ur.routine_name,
                    rDescription = ur.routine_description
                };
                string sql = "select * from insert_user_routine(@UrID, @uID, @rName, @rDescription)";

                // TODO: We need to have a method of verifying if the data was correctly inserted. Otherwise we may be missing user
                // routines.
                var result = (await connection.QueryAsync(sql, parameters));
            }
        }

        public async Task<IEnumerable<UserRoutineModel>> GetUserRoutines(Guid id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var parameters = new
                {
                    uID = id,
                };
                string sql = "select * from get_user_routines(@uID)";

                return await connection.QueryAsync<UserRoutineModel>(sql, parameters);
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

        /// <summary>
        /// Returns all Exercises
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExerciseModel>> GetExercises()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                string sql = "select * from get_exercises()";
                var result = await connection.QueryAsync<ExerciseModel>(sql);

                return result;
            }
        }
    }
}
