namespace Infrastructure.Models.Login
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string? Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}