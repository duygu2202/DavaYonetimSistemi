using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public static class RoleSeed
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Admin", "Avukat", "Personel" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
} 