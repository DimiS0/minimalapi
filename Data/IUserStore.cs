using Microsoft.AspNetCore.Identity;

public interface IUserStore
{
    Task<IdentityResult> RegisterAsync (string email, string password);
    Task<bool> LogInAsync (string email, string password);
    
}