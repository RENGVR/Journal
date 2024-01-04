using System.Net;
using System.Text.Json;
using Journal.Data;

namespace Journal.Services
{
    public class UserManager
    {
        // Ich verwende für diese Anwendung nur Json-Dateien als "Datenbank" da es nur für mich persönlich ist
        // und Sie somit auch meine Einträge sehen und testen können.
        private readonly string _filePath = "users.json";

        public async Task CreateUserAsync(User user)
        {
            var existingUser = await GetUserByUsernameOrEmailAsync(user.Username, user.Email);

            if (existingUser != null)
            {
                throw new Exception("Der Benutzername oder die E-Mail ist bereits vergeben.");
            }

            var users = await GetAllUsersAsync();

            users.Add(user);

            await WriteUsersToFileAsync(users);
        }

        public async Task<User?> GetUserByUsernameOrEmailAsync(string username, string email)
        {
            var users = await GetAllUsersAsync();

            if (users == null) return null;

            var user = users.FirstOrDefault(u => u.Username == username || u.Email == email);

            return user;
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var user = await GetUserByUsernameOrEmailAsync(username, string.Empty);

            return user != null && user.Password == password;
        }

        private async Task<List<User>?> GetAllUsersAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var fileEmptyCheck = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(fileEmptyCheck))
            {
                File.AppendAllText(_filePath, "[]");
            }

            using var usersStream = File.OpenRead(_filePath);

            var users = await JsonSerializer.DeserializeAsync<List<User>>(usersStream);

            return users;
        }

        private async Task WriteUsersToFileAsync(List<User> users)
        {
            using var usersStream = File.Open(_filePath, FileMode.Create, FileAccess.Write);

            await JsonSerializer.SerializeAsync(usersStream, users);
        }
    }
}
