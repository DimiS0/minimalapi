public interface IBlogStore
{
    Task<List<Blog>> GetBlogsAsync();

    Task<Blog?> GetBlogAsync(int id);

}