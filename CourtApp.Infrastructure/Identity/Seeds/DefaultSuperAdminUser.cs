using CourtApp.Application.Constants;
using CourtApp.Application.Enums;
using CourtApp.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourtApp.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
                }
            }
        }

        private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
            await roleManager.AddPermissionClaim(adminRole, "Users");
            await roleManager.AddPermissionClaim(adminRole, "Products");
            await roleManager.AddPermissionClaim(adminRole, "Brands");
            await roleManager.AddPermissionClaim(adminRole, "Cases");
            await roleManager.AddPermissionClaim(adminRole, "Publishers");
            await roleManager.AddPermissionClaim(adminRole, "Subjects");
            await roleManager.AddPermissionClaim(adminRole, "BookTypes");
            await roleManager.AddPermissionClaim(adminRole, "Books");
            await roleManager.AddPermissionClaim(adminRole, "Clients");
            await roleManager.AddPermissionClaim(adminRole, "ProceedingHeads");
            await roleManager.AddPermissionClaim(adminRole, "ProceedingSubHeads");
            await roleManager.AddPermissionClaim(adminRole, "CaseWorks");
            await roleManager.AddPermissionClaim(adminRole, "Titles");
            await roleManager.AddPermissionClaim(adminRole, "Cadre");
            await roleManager.AddPermissionClaim(adminRole, "Complex");
        }

        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Dushyant",
                LastName = "Singh",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");

                    await userManager.AddToRoleAsync(defaultUser, Roles.Clerk.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Lawyer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Associate.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                }
                await roleManager.SeedClaimsForSuperAdmin();
            }
        }
    }
}