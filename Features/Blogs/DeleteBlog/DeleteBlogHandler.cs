using Microsoft.EntityFrameworkCore.Metadata.Internal;

public static class DeleteBlogHandler
{
    public static async Task<IResult> Handle (int id, IBlogStore store)
    {
        var isDeleted = await store.DeleteAsync(id);

        if (!isDeleted)
        {
            return Results.NotFound();
        }
        return Results.NoContent();
        
    }
}