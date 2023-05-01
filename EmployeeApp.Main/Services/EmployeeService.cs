using EmployeeApp.Main.Models;
namespace EmployeeApp.Main.Services;

public class EmployeeService : IEmployeeService
{
    private const string Path = "employees.json";

    private readonly IJsonService _jsonService;

    public EmployeeService(IJsonService jsonService)
    {
        _jsonService = jsonService;
    }
    
    public async Task<Employee> AddEmployee(string firstName, string lastName, decimal salary)
    {
        var employee = Employee.CreateModel(firstName, lastName, salary);
        
        var employees = await _jsonService.DeserializeAsync<Employee>(Path);
        
        employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
        
        employees.Add(employee);

        await _jsonService.SaveChangesAsync(employees, Path);

        return employee;
    }

    public async Task<Employee> GetEmployee(int id)
    {
        var employees = await _jsonService.DeserializeAsync<Employee>(Path);

        var employee = FindEmployeeByIdInList(employees, id);

        return employee;
    }

    public async Task<bool> DeleteEmployee(int id)
    {
        var employees = await _jsonService.DeserializeAsync<Employee>(Path);

        var employee = FindEmployeeByIdInList(employees, id);

        employees.Remove(employee);
        
        await _jsonService.SaveChangesAsync(employees, Path);
        
        return true;
    }
    
    public async Task<bool> UpdateEmployee(int id, string? firstName, string? lastName, decimal? salary)
    {
        var employees = await _jsonService.DeserializeAsync<Employee>(Path);

        var employee = FindEmployeeByIdInList(employees, id);
        
        employee.Update(firstName ?? employee.FirstName, lastName ?? employee.LastName, salary ?? employee.SalaryPerHour);

        await _jsonService.SaveChangesAsync(employees, Path);
        
        return true;
    }
    
    public async Task<List<Employee>> ListEmployees()
    {
        return await _jsonService.DeserializeAsync<Employee>(Path);
    }
    
    private static Employee FindEmployeeByIdInList(IEnumerable<Employee> employees, int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            throw new Exception($"Employee with ID {id} not found.");
        }

        return employee;
    }
}