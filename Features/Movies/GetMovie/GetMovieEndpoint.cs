using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
public static class GetMovieEndpoint
{
    public static RouteHandlerBuilder MapGetMovieEndpoint(
        this RouteGroupBuilder group)
    {
        return group.MapGet("/{id:int}", GetMovieHandler.Handle);
    }
}