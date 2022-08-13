using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserAuth
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolesForApplications> RolesForApplications { get; set; }
    }
}