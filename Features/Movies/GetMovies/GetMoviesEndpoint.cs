public static class GetMoviesEndpoint
{
    public static RouteHandlerBuilder MapGetMoviesEndpoint (this RouteGroupBuilder group)
    {
        return group.MapGet("",GetMoviesHandler.handle);
    }
}