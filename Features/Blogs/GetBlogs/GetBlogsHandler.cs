
public static class GetBlogsHandler
{
    public static async Task<IResult> Handle (IBlogStore store)
    {
        var blogs = await store.GetBlogsAsync();

        var response = blogs.Select(blog => new GetBlogResponse(        
         blog.Id,
         blog.Title,
         blog.Beschrijving,
         blog.Datum,
         blog.ContentLink
        )); 

        return Results.Ok(response);
    }
}