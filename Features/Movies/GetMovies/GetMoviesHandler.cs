    public static class GetMoviesHandler
{
    public static async Task<IResult> handle (IMovieStore store)

    {
     var movies = await store.GetAllAsync();

        var response = movies.Select(movie =>
            new GetMoviesResponse(
                movie.Id,
                movie.Title,
                movie.Year));

        return Results.Ok(response);
    }
}