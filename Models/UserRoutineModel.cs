namespace HealthApplication.Models
{
    public class UserRoutineModel
    {
        public Guid user_routine_id { get; set; }
        public Guid user_id { get; set; }
        public string routine_name { get; set; }
        public string routine_description { get; set; }
        public DateTime? created_at { get; set; }
    }
}
