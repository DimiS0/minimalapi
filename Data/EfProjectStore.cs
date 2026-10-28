using Microsoft.EntityFrameworkCore;
public class EfProjectStore : IProjectStore
{
    private readonly ProjectDb _db;

    public EfProjectStore(ProjectDb db)
    {
       _db = db;
    }
    public async Task<List<Project>> GetAllAsync()
    {
        return await _db.Projects.ToListAsync();
    }
    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _db.Projects.FirstOrDefaultAsync(movie => movie.Id == id);
    }
    public async Task<Project> AddAsync(string title, string beschrijving, string afbeedling, string alt,string link,string  tag, DateTime datum)
    {
        var newProject = new Project
        {
            Title = title,
             Beschrijving = beschrijving,
             Afbeedling = afbeedling,
             Alt = alt,
             ProjectLink = link,
             Tag = tag,
             Datum = datum 
        };

        _db.Projects.Add(newProject);
        await _db.SaveChangesAsync();

        return newProject;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(movie => movie.Id == id);

        if (project is null)
        {
            return false;
        }
        
        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task <bool> UpdateAsync(int id, string title, string beschrijving, string afbeedling, string alt,string link,string  tag, DateTime datum)
    {
        var project = await _db.Projects.FirstOrDefaultAsync(movie => movie.Id == id);

        if (project is null)
        {
            return false;
        }

        project.Title = title;
        project.Beschrijving = beschrijving;
        project.Afbeedling = afbeedling;
        project.Alt = alt;
        project.ProjectLink = link;
        project.Tag = tag;
        project.Datum = datum;

        await _db.SaveChangesAsync();
        return true;
    }
}