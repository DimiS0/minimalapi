public static class MovieEndpoints
{
    public static RouteGroupBuilder MapMovieEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var movies = endpoints.MapGroup("/movies");

        movies.MapGetMoviesEndpoint(); 
        movies.MapGetMovieEndpoint(); 
        movies.MapPostMovieEndpoint();
        movies.MapDeleteMovieEndpoint();
        movies.MapUpdateMovieEndpoint();
            return movies;
    }
}