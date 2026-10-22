public static class UpdateProjectHandler
{
    public static async Task<IResult> Handle (int id, CreateProjectRequest request, IProjectStore store)
    {
     var isUpdated = await store.UpdateAsync(id,request.Title, request.Beschrijving, request.Afbeedling, request.Alt, request.ProjectLink, request.Tag, request.Datum);

    if(isUpdated)
    {
    return Results.NoContent();
    }
    return Results.NotFound();
    }
}