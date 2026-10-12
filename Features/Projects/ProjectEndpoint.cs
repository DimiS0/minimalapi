public static class MovieEndpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var projects = endpoints.MapGroup("/projects");

        projects.MapGetProjectsEndpoint(); 
        projects.MapGetProjectEndpoint(); 
        projects.MapPostProjectEndpoint();
        projects.MapDeleteProjectEndpoint();
        projects.MapUpdateProjectEndpoint();
            return projects;
    }
}