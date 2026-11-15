public static class UserEndpoints
{
    public static RouteGroupBuilder MapUserEndpoints (this IEndpointRouteBuilder endpoints)
    {
        var user =  endpoints.MapGroup("/auth");

        user.MapUserSignInEndpoint();
        user.MapUserLogInEndpoint();


        return user;
    }
}