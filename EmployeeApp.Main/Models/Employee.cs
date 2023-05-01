namespace EmployeeApp.Main.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal SalaryPerHour { get; set; }


    public static Employee CreateModel(string firstName, string lastName, decimal salaryPerHour)
    {
        return new Employee()
        {
            FirstName = firstName,
            LastName = lastName,
            SalaryPerHour = salaryPerHour
        };
    }
    
    public void Update(string? firstName, string? lastName, decimal salary)
    {
        FirstName = firstName;
        LastName = lastName;
        SalaryPerHour = salary;
    }
}