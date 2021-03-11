using System.Threading.Tasks;
using FinancialManager.Identity;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;

namespace FinancialManager.Infra.CrossCutting.Identity.Persistence
{
	public static class IdentityContextSeed
	{
		public static class ApplicationDbContextSeed
		{
			public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
															RoleManager<IdentityRole> roleManager)
			{
				var administratorRole = new IdentityRole("Administrator");

				if (await roleManager.Roles.AnyAsync(r => r.Name == administratorRole.Name) is false)
					await roleManager.CreateAsync(administratorRole);

				ApplicationUser administrator = ApplicationUser.NewUser("Rafael","Monteiro Porto","rafampo@hotmail.com", "62981238109");

				if (await userManager.Users?.AnyAsync(u => u.UserName == administrator.UserName) is false)
				{
					await userManager.CreateAsync(administrator, "123456");
					await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
				}

			}
		}
	}
}
