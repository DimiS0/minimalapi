public static class DeleteProjectHandler
{
    public static async Task<IResult> Handle (int id, IProjectStore store)
    {
    var IsDeleted = await store.DeleteAsync(id);

    if (IsDeleted)
    {
        return Results.NoContent();
    }
    return Results.NotFound();
}
}