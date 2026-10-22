
public static class PostProjectEndpoint
{
    public static RouteHandlerBuilder MapPostProjectEndpoint(
        this RouteGroupBuilder group)
    {
        return group.MapPost("", PostProjectHandler.Handle);
    }
}