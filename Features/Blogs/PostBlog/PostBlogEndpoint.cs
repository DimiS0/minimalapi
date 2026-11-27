public static class PostBlogEndpoint
{
    public static RouteHandlerBuilder MapPostBlogEndpoint (this RouteGroupBuilder group)
    {
        return group.MapPost("", PostBlogHandler.Handle).RequireAuthorization("Admin");
    }
}