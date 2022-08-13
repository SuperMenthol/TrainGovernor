using Domain.Entity.UserAuth;
using Domain.Interfaces.Context;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context.UserAuthContext
{
    public class UserAuthContext : DbContext, IUserAuthContext
    {
        public UserAuthContext()
            : base()
        {
        }

        public UserAuthContext(DbContextOptions<UserAuthContext> options)
            : base(options)
        {
        }

        public DbSet<Applications> Applications { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesForApplications> RolesForApplications { get; set; }
        public DbSet<UserBaseInfo> UserBaseInfo { get; set; }
        public DbSet<UserPersonalInfo> UserPersonalInfo { get; set; }
        public DbSet<UsersRolesForApplications> UsersRolesForApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public override int SaveChanges() => base.SaveChanges();
    }
}