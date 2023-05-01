using EmployeeApp.Main.Services;
using Microsoft.Extensions.Logging;
using Xunit;

namespace EmployeeApp.Test;

public class UserServiceTests
{
    [Fact]
    public async Task TestAddAndGetEmployee()
    {
        // Arrange
        var logger = new LoggerFactory().CreateLogger<JsonServices>();
        var jsonService = new JsonServices(logger);
        var userService = new EmployeeService(jsonService);

        var firstName = "John";
        var lastName = "Doe";
        var salary = 10.0m;

        // Act
        var addedEmployee = await userService.AddEmployee(firstName, lastName, salary);
        var retrievedEmployee = await userService.GetEmployee(addedEmployee.Id);

        // Assert
        Assert.Equal(firstName, addedEmployee.FirstName);
        Assert.Equal(lastName, addedEmployee.LastName);
        Assert.Equal(salary, addedEmployee.SalaryPerHour);

        Assert.Equal(addedEmployee.Id, retrievedEmployee.Id);
        Assert.Equal(firstName, retrievedEmployee.FirstName);
        Assert.Equal(lastName, retrievedEmployee.LastName);
        Assert.Equal(salary, retrievedEmployee.SalaryPerHour);
    }
    
    [Fact]
    public async Task TestUpdateEmployee()
    {
        // Arrange
        var logger = new LoggerFactory().CreateLogger<JsonServices>();
        var jsonService = new JsonServices(logger);
        var userService = new EmployeeService(jsonService);
        
        // Create a new employee
        var firstName = "John";
        var lastName = "Doe";
        var salary = 50.0m;
        
        var newEmployee = await userService.AddEmployee(firstName, lastName, salary);
        
        // Update the employee
        var updatedFirstName = "UpdateJohn";
        var updatedLastName = "Doe";
        decimal? updatedSalary = null;

        // Act
        await userService.UpdateEmployee(newEmployee.Id, updatedFirstName,updatedLastName,updatedSalary);
        
        var retrievedEmployee = await userService.GetEmployee(newEmployee.Id);
        
        // Assert
        Assert.Equal(updatedFirstName, retrievedEmployee.FirstName);
        Assert.Equal(updatedLastName, retrievedEmployee.LastName);
        Assert.Equal(salary, retrievedEmployee.SalaryPerHour);
    }
}