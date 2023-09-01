using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloggWebSite.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed roles (create 3  roles user admin and superadmin)
            var adminRoleId = "b2e1013c-5a6c-45b7-a2cc-e9ea4530d0ae";
            var superAdminRoleId = "50133536-8d59-4366-8718-1c7f4b6ca38d";
            var userRoleId = "0c4856bd-7284-4cc2-94e5-d20cbaf9cd14";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Id = superAdminRoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "User",
                    ConcurrencyStamp = userRoleId
                }
             };

            builder.Entity<IdentityRole>().HasData(roles);

            //seed SuperAdminUser
            var superAdminId = "a2eda756-9e8a-47f0-8d7b-6d826783f1a6";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All Roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId ,
                    UserId =superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId  ,
                    UserId =superAdminId
                },

                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId =superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}