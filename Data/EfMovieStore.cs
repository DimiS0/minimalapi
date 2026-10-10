using Microsoft.EntityFrameworkCore;
public class EfMovieStore : IMovieStore
{
    private readonly MovieDb _db;

    public EfMovieStore(MovieDb db)
    {
       _db = db;
    }
    public async Task<List<Movie>> GetAllAsync()
    {
        return await _db.Movies.ToListAsync();
    }
    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _db.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
    }
    public async Task<Movie> AddAsync(string title, int year)
    {
        var newMovie = new Movie
        {
            Title = title,
             Year = year
        };
        _db.Movies.Add(newMovie);
        await _db.SaveChangesAsync();

        return newMovie;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _db.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

        if (movie is null)
        {
            return false;
        }
        
        _db.Movies.Remove(movie);
        await _db.SaveChangesAsync();
        return true;
    }
    public async Task <bool> UpdateAsync(int id, string title, int year)
    {
        var movie = await _db.Movies.FirstOrDefaultAsync(movie => movie.Id == id);

        if (movie is null)
        {
            return false;
        }

        movie.Title = title;
        movie.Year = year;

        await _db.SaveChangesAsync();
        return true;
    }
}