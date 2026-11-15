using Microsoft.AspNetCore.Identity;

public static class UserSignInHandler
{
    public static async Task<IResult> Handle (SignInRequest request, IUserStore store)
    {
        var user = await store.RegisterAsync(request.Email, request.Password);

        if (user is null)
        {
            return Results.BadRequest(user.Errors);
        }
        return Results.Ok();
    } 
}