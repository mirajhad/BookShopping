using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Server.Services
{
    public class DbInitilaizer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitilaizer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task Initialize()
        {
           if(!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult()) { 
            
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();

                var user = new IdentityUser
                {
                    UserName = "super@gmail.com",
                    Email = "super@gmail.com"
                };
                _userManager.CreateAsync(user, "Admin@123").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

            }
           return Task.CompletedTask;
        }
    }
}
