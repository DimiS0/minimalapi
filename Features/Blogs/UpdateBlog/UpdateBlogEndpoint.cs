public static class UpdateBlogEndpoint
{
    public static RouteHandlerBuilder MapUpdateBlogEndpoint (this RouteGroupBuilder group)
    {
        return group.MapPut("/{id:int}", UpdateBlogHandler.Handle).RequireAuthorization("Admin");
    }
}