using Microsoft.AspNetCore.Identity;

public class EfUserStore(UserManager<User> userManager, SignInManager<User> signInManager) : IUserStore
{
       public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new User {UserName = email, Email = email};

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return result;
        }
        return await userManager.AddToRoleAsync(user, "Gast");
    }

    public async Task<bool> LogInAsync(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false,false);
        return result.Succeeded;
    }
} 