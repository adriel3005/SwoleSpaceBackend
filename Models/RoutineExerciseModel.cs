﻿namespace HealthApplication.Models
{
    public class RoutineExerciseModel
    {
        public Guid routine_exercise_id { get; set; }
        public int exercise_id { get; set; }
        public int repetition { get; set; }
        public int sets { get; set; }
        public DateTime? created_at { get; set; }
    }
}