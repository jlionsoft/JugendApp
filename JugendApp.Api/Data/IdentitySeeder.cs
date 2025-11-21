// JugendApp.Api/Data/IdentitySeeder.cs
using Microsoft.AspNetCore.Identity;
using JugendApp.Api.Identity;
using JugendApp.SharedModels.Person;

namespace JugendApp.Api.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
    {
        var roles = new[] { "Admin", "Organizer", "Member" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
        var admin = await userManager.FindByNameAsync("admin");
        if (admin == null)
        {
            var person = new Person(firstname: "System", lastname: "Admin", [], []);
            var user = new ApplicationUser { UserName = "admin", Person = person };
            await userManager.CreateAsync(user, "Admin123!");
            await userManager.AddToRoleAsync(user, "Admin");
        }

    }
}