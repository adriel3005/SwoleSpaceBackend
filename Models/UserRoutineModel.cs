namespace HealthApplication.Models
{
    public class UserRoutineModel
    {
        public Guid user_routine_id { get; set; }
        public Guid user_id { get; set; }
        public DateTime? created_at { get; set; }
    }
}
