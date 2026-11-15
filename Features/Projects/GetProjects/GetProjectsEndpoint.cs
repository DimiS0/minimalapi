public static class GetProjectsEndpoint
{
    public static RouteHandlerBuilder MapGetProjectsEndpoint (this RouteGroupBuilder group)
    {
        return group.MapGet("",GetProjectsHandler.Handle);
    }
}