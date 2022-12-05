using HealthApplication.Attributes;
using HealthApplication.Models;
using HealthApplication.Repositories;
using HealthApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

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
        private readonly ISupaAuthService _authService;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastRepository forecastRepository, ISupaAuthService authService)
        {
            _logger = logger;
            _forecastRepository = forecastRepository;
            _authService = authService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>All forecast DB entries</returns>
        [ServiceFilter(typeof (AuthenticationFilterAttribute))]
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromHeader] string Authorization)
        {
            var data = await _forecastRepository.GetWeatherForecasts();
            return Ok(data);
        }
    }
}