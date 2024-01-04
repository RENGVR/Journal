using System.Text.Json.Serialization;

namespace Journal.Data
{
    public class Workout
    {
        public DateOnly ExerciseDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public List<Exercise> ExerciseList { get; set; } = default!;

        public Workout() 
        {

        }
    }

}
