using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancialManager.Identity;
using Microsoft.AspNetCore.Identity;

namespace FinancialManager.Infra.CrossCutting.Identity.Persistence
{
    public static class IdentityContextSeed
    {
        public static class ApplicationDbContextSeed
        {
            public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
            {
				try
				{
					var administratorRole = new IdentityRole<Guid>("Administrator");

					if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
					{
						await roleManager.CreateAsync(administratorRole);
					}

					var administrator = new ApplicationUser { UserName = "rafampo@hotmail.com", Email = "rafampo@hotmail.com" };

					if (userManager.Users.All(u => u.UserName != administrator.UserName))
					{
						await userManager.CreateAsync(administrator, "123456");
						await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
					}
				}
				catch (Exception e)
				{

					throw e;
				}
            }
        }
    }
}
