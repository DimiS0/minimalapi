public interface IProjectStore
{
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(int id);
    Task<Project> AddAsync(string Title, string Beschrijving, string Afbeedling, string alt,string Link,string  Tag, DateTime Datum);
    Task<bool> DeleteAsync(int id);
    Task<bool> UpdateAsync(int id, string Title, string Beschrijving, string Afbeedling, string alt,string Link,string  Tag, DateTime Datum);
}