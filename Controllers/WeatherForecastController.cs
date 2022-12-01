using HealthApplication.Models;
using HealthApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "LukeWarm", "Balmy", "Fiery", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IForecastRepository _forecastRepository;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastRepository forecastRepository)
        {
            _logger = logger;
            _forecastRepository = forecastRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All forecast DB entries</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public Task<IEnumerable<WeatherForecast>> Get()
        {
            return _forecastRepository.GetWeatherForecasts();
        }
    }
}