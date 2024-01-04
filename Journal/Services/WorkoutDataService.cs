using Journal.Data;
using System.Text.Json;

namespace Journal.Services
{
    public class WorkoutDataService
    {
        private readonly string _filePath = "workouts.json";

        // Ich verwende für diese Anwendung nur Json-Dateien als "Datenbank" da es nur für mich persönlich ist
        // und Sie somit auch meine Einträge sehen und testen können.
        public async Task CreateWorkoutAsync(Workout workout)
        {
            var workouts = await GetAllWorkoutsAsync();

            workouts.Add(workout);

            await WriteWorkoutsToFileAsync(workouts);
        }

        public async Task<Workout?> GetWorkoutByDateAsync(DateOnly dateOnly)
        {
            var workouts = await GetAllWorkoutsAsync();

            if (workouts == null) return null;

            var workout = workouts.FirstOrDefault(w => w.ExerciseDate == dateOnly);

            return workout;
        }

        public async Task<List<Workout>?> GetAllWorkoutsAsync()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath);
            }

            var fileEmptyCheck = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(fileEmptyCheck))
            {
                File.AppendAllText(_filePath, "[]");
            }

            using var usersStream = File.OpenRead(_filePath);

            var workouts = await JsonSerializer.DeserializeAsync<List<Workout>>(usersStream);

            return workouts;
        }

        public async Task WriteWorkoutsToFileAsync(List<Workout> workouts)
        {
            using var workoutsStream = File.Open(_filePath, FileMode.Create, FileAccess.Write);

            await JsonSerializer.SerializeAsync(workoutsStream, workouts);
        }
    }
}
