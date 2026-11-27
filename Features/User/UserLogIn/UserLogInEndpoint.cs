using Microsoft.Identity.Client;

public static class UserLogInEndpoint
{
    public static RouteHandlerBuilder MapUserLogInEndpoint(this RouteGroupBuilder group)
    {
        return group.MapPost("/login", UserLogInHandler.Handle);
    }
}