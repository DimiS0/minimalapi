public static class GetBlogHandler
{
    public static async Task<IResult> Handle(int id, IBlogStore store)
    {
        var blog = await store.GetBlogAsync(id);

        if (blog is null)
        {
            return Results.NotFound();
        }

        var response = new GetBlogResponse(
            blog.Id,
            blog.Title,
            blog.Beschrijving,
            blog.Datum,
            blog.ContentLink);

        return Results.Ok(response);
    }
}