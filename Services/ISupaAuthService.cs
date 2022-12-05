namespace HealthApplication.Services
{
    public interface ISupaAuthService
    {
        public Task<bool> VerifyUser(string jwt);
    }
}
