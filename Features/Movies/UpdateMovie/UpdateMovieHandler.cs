public static class UpdateMovieHandler
{
    public static async Task<IResult> Handle (int id, CreateMovieRequest request, IMovieStore store)
    {
     var isUpdated = await store.UpdateAsync(id,request.Title,request.Year);

    if(isUpdated)
    {
    return Results.NoContent();
    }
    return Results.NotFound();
    }
}