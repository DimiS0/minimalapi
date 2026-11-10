using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
public static class DbSeeder
{
    public static async Task Seeder(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();

       var UserManager =  scope.ServiceProvider.GetRequiredService<UserManager<User>>();
       var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

       const string adminrole = "Admin";

       if (!await RoleManager.RoleExistsAsync(adminrole)){

            await RoleManager.CreateAsync(new IdentityRole(adminrole));
        }

        var admin = await UserManager.FindByEmailAsync("dimistoyanov8@gmail.com");

        if (admin == null){

            admin = new User
            {
                UserName = "dimistoyanov8@gmail.com",
                Email = "dimistoyanov8@gmail.com",
                EmailConfirmed = true
            };

            await UserManager.CreateAsync(admin, "Admin123!");
        }

             if (! await UserManager.IsInRoleAsync(admin, adminrole))
            {
                await UserManager.AddToRoleAsync(admin,adminrole);
            }
    }
}