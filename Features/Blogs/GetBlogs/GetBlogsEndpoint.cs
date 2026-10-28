public static class GetBlogsEndpoint
{
    public static RouteHandlerBuilder MapGetBlogsEndpoint(this RouteGroupBuilder group)
    {
        return group.MapGet("", GetBlogsHandler.Handle);
    }
}