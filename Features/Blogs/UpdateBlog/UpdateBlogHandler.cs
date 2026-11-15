public static class UpdateBlogHandler
{
    public static async Task<IResult> Handle (int id, UpdateBlogRequest request, IBlogStore store)
    {
        var isUpdated = await store.PutAsync(id, request.Title, request.Beschrijving, request.Datum,request.ContentLink);

        if (!isUpdated)
        {
        return Results.NotFound();
        }
        return Results.NoContent();
    }
}