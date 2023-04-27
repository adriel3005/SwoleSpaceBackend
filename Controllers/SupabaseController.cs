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

        [Route("~/AddRoutineExercise")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> AddRoutineExercise(RoutineExerciseModel routineExercise)
        {
            try
            {
                await _supabaseRepository.AddRoutineExercise(routineExercise);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }

        [Route("~/AddUserRoutine")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> AddUserRoutine(UserRoutineModel routineExercise)
        {
            try
            {
                await _supabaseRepository.AddUserRoutine(routineExercise);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Ok();
        }

        [Route("~/GetUserRoutines")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpGet]
        public async Task<IActionResult> GetUserRoutine([FromQuery] Guid userID)
        {
            try
            {
                var data = await _supabaseRepository.GetUserRoutines(userID);
                return Ok(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Route("~/GetExercises")]
        [ServiceFilter(typeof(AuthenticationFilterAttribute))]
        [HttpGet]
        public async Task<IActionResult> GetExercises()
        {
            var data = await _supabaseRepository.GetExercises();
            return Ok(data);
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
