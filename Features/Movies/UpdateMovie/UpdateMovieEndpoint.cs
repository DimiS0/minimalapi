public static class UpdateMovieEndpoint
{
    public static RouteHandlerBuilder MapUpdateMovieEndpoint(this RouteGroupBuilder group)
    {
        return group.MapPut("/{id:int}", UpdateMovieHandler.Handle);
    }
}