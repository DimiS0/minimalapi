public static class DeleteProjectEndpoint
{
    public static RouteHandlerBuilder MapDeleteProjectEndpoint(this RouteGroupBuilder group)
    {
        return group.MapDelete("/{id:int}", DeleteProjectHandler.Handle);
    }
}