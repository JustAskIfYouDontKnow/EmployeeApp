using Newtonsoft.Json;

namespace EmployeeApp.Main.Services;
public class JsonServices : IJsonService
{
    private readonly ILogger<JsonServices> _logger;

    public JsonServices(ILogger<JsonServices> logger)
    {
        _logger = logger;
    }

    public async Task<List<T>> DeserializeAsync<T>(string path)
    {
        if (!File.Exists(path))
        {
            await using (File.Create(path)) { }
            _logger.LogInformation($"File {path} created.");
        }

        var json = await File.ReadAllTextAsync(path);

        try
        {
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        catch (Exception e)
        {
            _logger.LogError($"Error deserializing object {typeof(T)} from {path}. \n\nException:\n{e}");
            return new List<T>();
        }
    }
    
    public async Task SaveChangesAsync<T>(List<T> data, string path)
    {
        try
        {
            var json = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(path, json);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error serializing object {typeof(T)} in {path}");
        }
    }
}