public interface IMovieStore
{
    Task<List<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie> AddAsync(string title, int year);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, string title, int year);
}