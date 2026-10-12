public static class BlogEndpoints{
    
    public static RouteGroupBuilder MapBlogEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var blogs = endpoints.MapGroup("/Blogs");
        
        blogs.MapGetBlogEndpoint();
        blogs.MapGetBlogsEndpoint();

        return blogs;
    }
}