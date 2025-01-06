using Microsoft.AspNetCore.Identity;
using IdentityAndDataProtection.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using YourNamespace.Models;

namespace IdentityAndDataProtection.Datas
{

    public static class IdentitySeed
    {

        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {

            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "User" },
                new Role { Name = "Manager" }, 
                new Role { Name = "Guest" }   
            };

            foreach (var role in roles)
            {
                await CreateRoleIfNotExists(roleManager, role);
            }

            var adminUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = "Alparslan",
                Email = "admin@gmail.com",
                PhoneNumber = "123-456-7890", 
              
            };

            await CreateUserIfNotExists(userManager, adminUser, "A.lparslan123", "Admin");
        }


        private static async Task CreateRoleIfNotExists(RoleManager<Role> roleManager, Role role)
        {
            if (!await roleManager.RoleExistsAsync(role.Name!))
            {
                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    Console.WriteLine($"Failed to create role '{role.Name}': {string.Join(", ", roleResult.Errors)}");
                }
                else
                {
                    Console.WriteLine($"Role '{role.Name}' created successfully.");
                }
            }
            else
            {
                Console.WriteLine($"Role '{role.Name}' already exists.");
            }
        }

        private static async Task CreateUserIfNotExists(UserManager<User> userManager, User user, string password, string roleName)
        {
            switch (await userManager.FindByEmailAsync(user.Email))
            {
                case not null:
                    Console.WriteLine($"User '{user.UserName}' already exists.");
                    break;
                default:
                    {
                        var userResult = await userManager.CreateAsync(user, password);
                        if (userResult.Succeeded)
                        {
                            await userManager.AddToRoleAsync(user, roleName);
                            Console.WriteLine($"User '{user.UserName}' created and added to the '{roleName}' role.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to create user '{user.UserName}': {string.Join(", ", userResult.Errors)}");
                        }

                        break;
                    }
            }
        }
    }
}