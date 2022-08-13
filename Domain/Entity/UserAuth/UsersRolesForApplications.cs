using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.UserAuth
{
    public class UsersRolesForApplications
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleForApplicationId { get; set; }

        [ForeignKey("UserId")]
        public UserBaseInfo UserInfo { get; set; }

        [ForeignKey("RoleForApplicationId")]
        public RolesForApplications RolesForApplication { get; set; }
    }
}