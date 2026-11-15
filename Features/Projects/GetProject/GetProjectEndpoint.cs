
public static class GetProjectEndpoint
{
    public static RouteHandlerBuilder MapGetProjectEndpoint(
        this RouteGroupBuilder group)
    {
        return group.MapGet("/{id:int}", GetProjectHandler.Handle);
    }
}