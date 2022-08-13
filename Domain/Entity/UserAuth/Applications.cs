using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.UserAuth
{
    public class Applications
    {
        [Key]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string InternalName { get; set; }

        public ICollection<RolesForApplications> RolesForApplications { get; set; }
    }
}