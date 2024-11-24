using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace LMS.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    //Roles
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                    //Users
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    string adminUserEmail = "teddysmithdeveloper@gmail.com";

                    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                    if (adminUser == null)
                    {
                        var newAdminUser = new AppUser()
                        {
                            UserName = "teddysmithdev",
                            Email = adminUserEmail,
                            EmailConfirmed = true,
                            Address="Qena",
                            FirstName="fgfh",
                            LastName="Ali",
                            NationalID="66576585",
                            BitrthDate=DateTime.Now,
                        };
                        await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    }

                    string appUserEmail = "user@etickets.com";

                    var appUser = await userManager.FindByEmailAsync(appUserEmail);
                    if (appUser == null)
                    {
                        var newAppUser = new AppUser()
                        {
                            UserName = "user",
                            Email = appUserEmail,
                            EmailConfirmed = true,
                            Address = "Qena",
                            FirstName = "fgfh",
                            LastName = "Alo",
                            NationalID = "66234235",
                            BitrthDate = DateTime.Now,
                        };
                        await userManager.CreateAsync(newAppUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                    }
                }       
        }
    }
}
