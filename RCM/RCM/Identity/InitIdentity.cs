using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RCM.Model;

namespace RCM.Identity
{
    public class InitIdentity
    {
        public async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "Admin", "Manager", "Collector" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            string userName = "sa";
            //Here you could create a super user who will maintain the web app
            var sa = new User
            {
                UserName = userName,
            };
            //Ensure you have these values in your appsettings.json file
            var _user = await UserManager.FindByNameAsync(userName);

            if (_user == null)
            {
                var createSuperAdmin = await UserManager.CreateAsync(sa, "zaq@123");
                if (createSuperAdmin.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(sa, "Admin");
                }
            }
            string userNameManager = "manager";
            //Here you could create a super user who will maintain the web app
            var manager = new User
            {
                UserName = userNameManager,
            };
            //Ensure you have these values in your appsettings.json file
            var _userManager = await UserManager.FindByNameAsync(userNameManager);

            if (_userManager == null)
            {
                var createSuperAdmin = await UserManager.CreateAsync(manager, "zaq@123");
                if (createSuperAdmin.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(manager, "Manager");
                }
            }
        }

    }
}
