using Microsoft.EntityFrameworkCore;
using PMSAT.Entities;
using System.Data;

namespace PMSAT.Data
{
    public class PmsatDbContext : DbContext
    {
        public PmsatDbContext(DbContextOptions<PmsatDbContext> options) : base(options) 
        {
        
        }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithOne(u => u.UserRole)
            .HasForeignKey<User>(u => u.UserRoleID);

            // Data seeding for Roles
            modelBuilder.Entity<UserRole>().HasData(
                new  { UserRoleID = "AD", UserRoleName = "Admin" },
                new  { UserRoleID = "MD", UserRoleName = "Moderator" },
                new  { UserRoleID = "US", UserRoleName = "User" }
            );

            // Data seeding for Users
            modelBuilder.Entity<User>().HasData(
                new 
                {
                    UserID = "AD00",
                    UserRoleID = "AD",
                    UserName = "Admin",
                    UserEmail = "admin@email.com",
                    UserPassword = "Password", // Make sure to hash this
                    UserStatus = true,
                },
                
                new
                {
                    UserID = "MD00",
                    UserRoleID = "MD",
                    UserName = "Moderator",
                    UserEmail = "mod@email.com",
                    UserPassword = "Password", // Make sure to hash this
                    UserStatus = true,
                },

                new
                {
                    UserID = "US000000",
                    UserRoleID = "US",
                    UserName = "User",
                    UserEmail = "user@email.com",
                    UserPassword = "Password", // Make sure to hash this
                    UserStatus = true,
                }

            );
        }
    }
}
