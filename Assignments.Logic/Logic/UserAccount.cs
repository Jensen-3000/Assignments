namespace Assignments.Logic.Password
{
    internal class UserAccount
    {
        public UserAccount()
        {
        }

        public UserAccount(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}
