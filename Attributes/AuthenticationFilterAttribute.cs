using HealthApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HealthApplication.Attributes
{
    public class AuthenticationFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ISupaAuthService _authService;

        public AuthenticationFilterAttribute(ISupaAuthService authService)
        {
            _authService= authService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IHeaderDictionary headers = context.HttpContext.Request.Headers;

            // Get call Authorization header
            headers.TryGetValue("Authorization", out var authorizationHeader);
            string? jwt = authorizationHeader.FirstOrDefault();

            if (jwt != null)
            {
                try
                {
                    // Not authenticated
                    if (!await _authService.VerifyUser(jwt))
                    {
                        context.Result = new ObjectResult(new { message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                        return;
                    }
                }
                catch
                {
                    context.Result = new ObjectResult(new { message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                    return;
                }
            }
            else
            {
                // Missing Authorization header
                context.Result = new ObjectResult(new {message = "You need to sign in or sign up before continuing." }) { StatusCode = 401 };
                return;
            }
        }
    }
}
