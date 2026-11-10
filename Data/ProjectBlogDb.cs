using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class ProjectDb : IdentityDbContext<User>
{
    public ProjectDb (DbContextOptions<ProjectDb> options)
        : base(options)
    {
        
    }
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}