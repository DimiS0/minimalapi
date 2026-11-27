public static class UserLogInHandler
{
    public static async Task<IResult> Handle (LogInRequest request, IUserStore store)
    {
        return await store.LogInAsync(request.Email, request.Password) ? Results.Ok() : Results.Unauthorized();
    }
}