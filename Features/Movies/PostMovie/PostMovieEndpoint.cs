
public static class PostMovieEndpoint
{
    public static RouteHandlerBuilder MapPostMovieEndpoint(
        this RouteGroupBuilder group)
    {
        return group.MapPost("", PostMovieHandler.Handle);
    }
}