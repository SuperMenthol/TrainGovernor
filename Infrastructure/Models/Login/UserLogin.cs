namespace Infrastructure.Models.Login
{
    public class UserLogin
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public UserLogin(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}