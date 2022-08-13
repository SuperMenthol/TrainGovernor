using Domain.Entity.UserAuth;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces.Context
{
    public interface IUserAuthContext
    {
        DbSet<Applications> Applications { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<RolesForApplications> RolesForApplications { get; set; }
        DbSet<UserBaseInfo> UserBaseInfo { get; set; }
        DbSet<UserPersonalInfo> UserPersonalInfo { get; set; }
        DbSet<UsersRolesForApplications> UsersRolesForApplications { get; set; }
    }
}