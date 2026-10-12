
public static class PostMovieEndpoint
{
    public static RouteHandlerBuilder MapPostProjectEndpoint(
        this RouteGroupBuilder group)
    {
        return group.MapPost("", PostProjectHandler.Handle);
    }
}