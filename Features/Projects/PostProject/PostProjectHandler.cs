
public static class PostProjectHandler
{
    public static async Task<IResult> Handle(CreateMovieRequest request,IProjectStore store)
    {
        var newProject = await store.AddAsync(request.Title, request.Beschrijving, request.Afbeedling, request.Alt, request.ProjectLink, request.Tag, request.Datum);

        var response = new PostProjectResponse(
        newProject.Id,
        newProject.Title,
        newProject.Beschrijving,
        newProject.Afbeedling,
        newProject.Alt,
        newProject.ProjectLink,
        newProject.Tag,
        newProject.Datum
        );


        return Results.Created($"/Projects/{response.Id}", response);
    }
}
