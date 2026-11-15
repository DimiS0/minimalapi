public interface IBlogStore
{
    Task<List<Blog>> GetBlogsAsync();

    Task<Blog?> GetBlogAsync(int id);

    Task<Blog> PostBlogAsync(string title, string beschrijving, DateOnly datum, string contentLink);

    Task<bool> PutAsync(int id, string title, string beschrijving, DateOnly datum, string contentLink);

    Task<bool> DeleteAsync(int id);
}