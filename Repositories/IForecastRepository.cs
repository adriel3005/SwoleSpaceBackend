using HealthApplication.Models;

namespace HealthApplication.Repositories
{
    public interface IForecastRepository
    {
        // Retrieve DB Weather Forecasts
        Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
    }
}
