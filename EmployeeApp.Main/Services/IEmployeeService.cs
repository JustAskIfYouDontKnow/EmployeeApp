using EmployeeApp.Main.Models;

namespace EmployeeApp.Main.Services;

public interface IEmployeeService
{
    public Task<Employee> AddEmployee(string firstName, string lastName, decimal salary);
    public Task<Employee> GetEmployee(int id);
    public Task<bool> DeleteEmployee(int id);
    public Task<bool> UpdateEmployee(int id, string? firstName, string? lastName, decimal? salary);
    public Task<List<Employee>> ListEmployees();

}