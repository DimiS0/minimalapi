public static class UserSignInEndpoint
{
    public static RouteHandlerBuilder MapUserSignInEndpoint (this RouteGroupBuilder group)
    {
        return group.MapPost("/register", UserSignInHandler.Handle);
    }
}