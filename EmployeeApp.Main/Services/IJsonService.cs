
namespace EmployeeApp.Main.Services;

public interface IJsonService
{
    public Task<List<T>> DeserializeAsync<T>(string path);
    public Task SaveChangesAsync<T>(List<T> data, string path);
}