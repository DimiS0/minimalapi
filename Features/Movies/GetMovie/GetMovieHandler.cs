public static class GetMovieHandler
{
    public static async Task<IResult> Handle(int id, IMovieStore store)
    {
        var movie = await store.GetByIdAsync(id);

        if (movie is null)
        {
            return Results.NotFound();
        }

        var response = new GetMovieResponse(
            movie.Id,
            movie.Title,
            movie.Year);

        return Results.Ok(response);
    }
}