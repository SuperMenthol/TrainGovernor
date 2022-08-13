using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.UserAuth
{
    public class UserPersonalInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        [ForeignKey("Id")]
        public UserBaseInfo User { get; set; }
    }
}