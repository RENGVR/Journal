namespace Journal.Data
{
    public class User
    {
        public string Username { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public DateOnly Birthday { get; set; } = default!;

        public User(string username, string firstName, string lastName, string email, string password, DateOnly birthday)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Birthday = birthday;
        }
    }
}
