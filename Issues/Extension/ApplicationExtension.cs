using Microsoft.AspNetCore.Identity;
using Issues.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.Extension
{
    public static class ApplicationExtension
    {
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin";

            //User Manager
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            //Role Manager
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if(user == null) 
            {
                user = new IdentityUser()
                {
                    Email = "admin12345@gmail.com",
                    UserName = adminUser,
                };

                var result = await userManager.CreateAsync(user,adminPassword);

                if (!result.Succeeded)
                    throw new Exception("Admin user could not created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                        roleManager
                            .Roles
                            .Select (r => r.Name)
                            .ToList()
                    );
                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination");
            }
        }
    }
}
