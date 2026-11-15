public static class DeleteBlogEndpoint
{
    public static RouteHandlerBuilder MapDeleteBlogEndpoint (this RouteGroupBuilder group)
    {
        return group.MapDelete("/{id:int}", DeleteBlogHandler.Handle).RequireAuthorization("Admin");
    }
}