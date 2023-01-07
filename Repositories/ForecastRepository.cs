using HealthApplication.Models;
using Dapper;
using Npgsql;
using HealthApplication.Utilities;

namespace HealthApplication.Repositories
{
    public class ForecastRepository : IForecastRepository
    {
        private readonly string _connectionString;
        public ForecastRepository() 
        {
            _connectionString = Utility.GetBuilder(Environment.GetEnvironmentVariable("DATABASE_URL"));
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

        
    }
}
