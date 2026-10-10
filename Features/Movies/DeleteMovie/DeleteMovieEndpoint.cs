public static class DeleteMovieEndpoint
{
    public static RouteHandlerBuilder MapDeleteMovieEndpoint(this RouteGroupBuilder group)
    {
        return group.MapDelete("/{id:int}", DeleteMovieHandler.Handle);
    }
}