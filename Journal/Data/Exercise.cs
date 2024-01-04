using System.Text.Json.Serialization;

namespace Journal.Data
{
    public class Exercise
    {
        public string ExerciseName { get; set; } = default!;
        public int Repetitions { get; set; } = default!;
        public int Weight { get; set; } = default!;
        public int Sets { get; set; } = default!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MuscleType MusclesTrained { get; set; }
        public enum MuscleType
        {
            Brust = 0,
            Rücken = 1,
            Beine = 2,
            Bizeps = 3,
            Trizeps = 4,
            Schultern = 5,
            Unterarme = 6,
            Bauch = 7
        }

        public Exercise()
        {
            
        }
    }
}
