using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main.ConsoleCommands;

internal sealed class AddEmployee
{

    private readonly IEmployeeService _employeeService;
    private readonly ILogger<AddEmployee> _logger;


    public AddEmployee(IEmployeeService employeeService, ILogger<AddEmployee> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }


    public void Execute(CommandLineApplication app)
    {

        app.Description = "Creates an employee in file\n  Usage: add -firstName <string> -lastName <string> -salary <decimal>\n";
        
        var firstNameOption = app.Option("-firstName", "FirstName", CommandOptionType.SingleValue);
        var lastNameOption = app.Option("-lastName", "LastName", CommandOptionType.SingleValue);
        var salaryOption = app.Option("-salary", "SalaryPerHour", CommandOptionType.SingleValue);


        app.OnExecute(
            async () =>
            {
                if (firstNameOption.Value() != null && lastNameOption.Value() != null && salaryOption.Value() != null)
                {
                    if (!decimal.TryParse(salaryOption.Value(), out var salaryDecimal))
                    {
                        _logger.LogError("\nSalary format must be decimal.");
                        return 1;
                    }

                    var employee = await _employeeService.AddEmployee(firstNameOption.Value(), lastNameOption.Value(), salaryDecimal);

                    _logger.LogInformation($"\nEmployee has been successfully created\n\nId: {employee.Id}\nFirstName: {employee.FirstName}\nLastName: {employee.LastName}\nSalaryPerHour: {employee.SalaryPerHour}\n");

                    return 0;
                }

                app.ShowHelp();
                return 1;
            }
        );

    }
}