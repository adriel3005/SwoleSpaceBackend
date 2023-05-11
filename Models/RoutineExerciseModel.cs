namespace HealthApplication.Models
{
    public class RoutineExerciseModel
    {
        public Guid routine_exercise_id { get; set; }
        public Guid user_routine_id { get; set; }
        public int exercise_id { get; set; }
        public int repetition { get; set; }
        public int sets { get; set; }
        public int default_weight { get; set; }
        public DateTime? created_at { get; set; }
        public Guid user_id { get; set; }
        public int exercise_order { get; set; }
    }
}
