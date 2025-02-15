using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public static class AdminSeed
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
    {
        var adminUsername = "admin";
        
        if (await userManager.FindByNameAsync(adminUsername) == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminUsername,
                Email = "admin@dava.com",
                EmailConfirmed = true,
                AdSoyad = "Sistem Yöneticisi"
            };

            var result = await userManager.CreateAsync(admin, "admin");
            
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
} 