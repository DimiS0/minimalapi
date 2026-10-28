using Microsoft.EntityFrameworkCore;

public class EfBlogStore : IBlogStore
{
    private readonly ProjectDb _db;

    public EfBlogStore(ProjectDb db)
    {
        _db = db;
    }

    public async Task<List<Blog>> GetBlogsAsync()
    {
        return await _db.Blogs.ToListAsync();
    }

    public async Task<Blog?> GetBlogAsync(int id)
    {
        return await _db.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);
    }
}