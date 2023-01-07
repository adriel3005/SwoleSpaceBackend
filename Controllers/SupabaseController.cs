using HealthApplication.Attributes;
using HealthApplication.Models;
using HealthApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupabaseController : Controller
    {

        private readonly ILogger<SupabaseController> _logger;
        private readonly ISupabaseRepository _supabaseRepository;

        public SupabaseController(ILogger<SupabaseController> logger, ISupabaseRepository supabaseRepository)
        {
            _logger = logger;
            _supabaseRepository = supabaseRepository;
        }

        [Route("~/UpdateUser")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> PostUpdateUser(UserProfile userProfile)
        {
            return await _supabaseRepository.UpdateUser(userProfile) ? Ok() : NotFound();
        }

        [Route("~/GetUser")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpGet]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var data = await _supabaseRepository.GetUser(id);
            return Ok(data);
        }
    }
}
