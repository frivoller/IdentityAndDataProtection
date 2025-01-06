using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityAndDataProtection.Models;
using System;
using System.Data;
using System.Reflection.Emit;
using YourNamespace.Models;

namespace IdentityAndDataProtection.Datas
{
    public class Context : IdentityDbContext<User, Role, Guid>
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users"); 
            builder.Entity<Role>().ToTable("Roles"); 
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles"); 
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims"); 
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims"); 
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens"); 
            ConfigureUserEntity(builder);
            ConfigureRoleEntity(builder);
        }

        private void ConfigureUserEntity(ModelBuilder builder)
        {
            c.
            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired() 
                .HasMaxLength(256); 

            builder.Entity<User>()
                .Property(u => u.UserName)
                .IsRequired() 
                .HasMaxLength(256); 

        }

        private void ConfigureRoleEntity(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired() 
                .HasMaxLength(256); 

        }
    }

}