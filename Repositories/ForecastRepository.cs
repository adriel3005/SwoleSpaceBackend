using HealthApplication.Models;
using Dapper;
using Npgsql;

namespace HealthApplication.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly string _connectionString;
        public ForecastRepository() 
        {
            _connectionString = GetBuilder();
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                // Note: Table name seems to be converted to lowercase.
                var results = await connection.QueryAsync<WeatherForecast>("select * from public.forecasts");

                return results;

            }
        }

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
