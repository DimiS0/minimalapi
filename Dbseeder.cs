using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
public static class DbSeeder
{
    public static async Task Seeder(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

       var UserManager =  scope.ServiceProvider.GetRequiredService<UserManager<User>>();
       var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
       var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

       foreach (var rol in new [] {"Gast", "Admin"}){

       if (!await RoleManager.RoleExistsAsync(rol)){

            await RoleManager.CreateAsync(new IdentityRole(rol));
        }
       }

       var wachtwoord = config["Seed:AdminPassword"];
       if (wachtwoord == null) {return;}

        var admin = await UserManager.FindByEmailAsync("dimistoyanov8@gmail.com");

        if (admin == null){

            admin = new User
            {
                UserName = "dimistoyanov8@gmail.com",
                Email = "dimistoyanov8@gmail.com",
                EmailConfirmed = true
            };

            await UserManager.CreateAsync(admin, wachtwoord);
        }

             if (! await UserManager.IsInRoleAsync(admin, "Admin"))
            {
                await UserManager.AddToRoleAsync(admin, "Admin");
            }
    }
}