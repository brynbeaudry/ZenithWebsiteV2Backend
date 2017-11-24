using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZenithWebsite.Data
{
    public class DummyData
    {

        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public static void Initialize(ApplicationDbContext db, IServiceProvider services)
        {
            _roleManager = services.
        }

        public async void UserSeedAsync(ApplicationDbContext context, IServiceProvider services)
        {

            bool x = await _roleManager.RoleExistsAsync("Admin");
            if (!x)
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "default";
                user.Email = "default@default.com";

                string userPWD = "somepassword";

                IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }
            if (!roleManager.RoleExists("Member"))
            {
                roleManager.Create(new IdentityRole("Member"));
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<Models.ApplicationUser>(context));
            if (userManager.FindByName("a") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "a@a.a",
                    UserName = "a"
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (userManager.FindByName("m") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "m@m.m",
                    UserName = "m"
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Member");
                }
            }
        }
    }
}
