public static class GetProjectHandler
{
    public static async Task<IResult> Handle(int id, IProjectStore store)
    {
        var project = await store.GetByIdAsync(id);

        if (project is null)
        {
            return Results.NotFound();
        }

        var response = new GetProjectResponse(
                project.Id,
                project.Title,
                project.Beschrijving,
                project.Afbeedling,
                project.Alt,
                project.ProjectLink,
                project.Tag,
                project.Datum
                
            );

        return Results.Ok(response);
    }
}