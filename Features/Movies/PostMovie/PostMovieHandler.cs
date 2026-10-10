
public static class PostMovieHandler
{
    public static async Task<IResult> Handle(CreateMovieRequest request,IMovieStore store)
    {
        var newMovie = await store.AddAsync(request.Title, request.Year);

        var response = new PostMovieResponse(
        newMovie.Id,
        newMovie.Title,
        newMovie.Year);

        return Results.Created($"/movies/{response.Id}", response);
    }
}
