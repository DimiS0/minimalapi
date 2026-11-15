using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

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
     public async Task<Blog> PostBlogAsync(string title, string beschrijving, DateOnly datum, string contentLink)
    {
       var newProject = new Blog
       {
           Title = title,
           Beschrijving = beschrijving,
           Datum = datum,
           ContentLink = contentLink
       };
       _db.Blogs.Add(newProject);
       await _db.SaveChangesAsync();
    
        return newProject;
    }
      public async Task<bool> PutAsync(int id, string title, string beschrijving, DateOnly datum, string contentLink)
    {
        var blog = await _db.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);

        if (blog is null)
        {
          return false;  
        }
        blog.Title = title;
        blog.Beschrijving = beschrijving;
        blog.Datum = datum;
        blog.ContentLink = contentLink;

        await _db.SaveChangesAsync();

        return true;
    }
    public async Task<bool> DeleteAsync (int id)
    {
        var blog = await _db.Blogs.FirstOrDefaultAsync(blog => blog.Id == id);

        if (blog is null)
        {
        return false;
        }
        _db.Blogs.Remove(blog);
        await _db.SaveChangesAsync();

        return true;
    }
}
