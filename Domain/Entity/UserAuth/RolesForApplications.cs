using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.UserAuth
{
    public class RolesForApplications
    {
        [Key]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Roles Role { get; set; }

        [ForeignKey("ApplicationId")]
        public Applications Application { get; set; }

        public ICollection<UsersRolesForApplications> UsersRolesForApplications { get; set; }
    }
}