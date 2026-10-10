public static class DeleteMovieHandler
{
    public static async Task<IResult> Handle (int id, IMovieStore store)
    {
    var IsDeleted = await store.DeleteAsync(id);

    if (IsDeleted)
    {
        return Results.NoContent();
    }
    return Results.NotFound();
}
}