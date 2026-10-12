public static class UpdateProjectEndpoint
{
    public static RouteHandlerBuilder MapUpdateProjectEndpoint(this RouteGroupBuilder group)
    {
        return group.MapPut("/{id:int}", UpdateProjectHandler.Handle);
    }
}