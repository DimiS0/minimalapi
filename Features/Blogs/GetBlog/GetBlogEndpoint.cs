public static class GetBlogEndpoint
{
    public static RouteHandlerBuilder MapGetBlogEndpoint (this RouteGroupBuilder group)
    {
        return group.MapGet("/{id:int}", GetBlogHandler.Handle);
    }
}