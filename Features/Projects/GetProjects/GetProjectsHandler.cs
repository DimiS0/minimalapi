    public static class GetProjectsHandler
{
    public static async Task<IResult> Handle (IProjectStore store)

    {
     var projects = await store.GetAllAsync();

        var response = projects.Select(project =>
            new GetProjectsResponse(
                project.Id,
                project.Title,
                project.Beschrijving,
                project.Afbeedling,
                project.Alt,
                project.ProjectLink,
                project.Tag,
                project.Datum
                )
              );

        return Results.Ok(response);
    }
}