using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserAuth
{
    public class UserBaseInfo
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserPersonalInfo PersonalInfo { get; set; }
        public ICollection<UsersRolesForApplications> UserRoles { get; set; }
    }
}