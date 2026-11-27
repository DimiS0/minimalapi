public static class PostBlogHandler
{
    public static async Task<IResult> Handle (PostBlogRequest request,IBlogStore store)
    {
        var newProject = await store.PostBlogAsync(request.Title, request.Beschrijving, request.Datum, request.ContentLink);

        if (newProject is null)
        {
         return Results.NotFound();
        }

        var response = new PostBlogResponse(
            newProject.Id,
            newProject.Title,
            newProject.Beschrijving,
            newProject.Datum,
            newProject.ContentLink
            );
            
            return Results.Created($"/blogs/{response.Id}",response);

    }
}