using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main.ConsoleCommands;

internal sealed class UpdateEmployee
{

    private readonly IEmployeeService _employeeService;
    private readonly ILogger<UpdateEmployee> _logger;


    public UpdateEmployee(IEmployeeService employeeService, ILogger<UpdateEmployee> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }


    public void Execute(CommandLineApplication app)
    {
        app.Description =
            "Update user data by ID\n  Usage: update -id <int> -firstName <string> -lastName <string> -salary <decimal>\n";

        var idOption = app.Option("-id", "Id", CommandOptionType.SingleValue);
        var firstNameOption = app.Option("-firstName", "FirstName", CommandOptionType.SingleValue);
        var lastNameOption = app.Option("-lastName", "LastName", CommandOptionType.SingleValue);
        var salaryOption = app.Option("-salary", "SalaryPerHour", CommandOptionType.SingleValue);

        app.OnExecute(
            async () =>
            {
                if (idOption.Value() != null)
                {
                    if (!int.TryParse(idOption.Value(), out var idInt))
                    {
                        _logger.LogError("\nId format must be int.");
                        return 1;
                    }

                    decimal? salary = null;

                    if (decimal.TryParse(salaryOption.Value(), out var salaryDecimal))
                    {
                        salary = salaryDecimal;
                    } else
                    {
                        _logger.LogWarning("\nSalary format must be decimal. Salary was not updated\n");
                    }

                    await _employeeService.UpdateEmployee(idInt, firstNameOption.Value(), lastNameOption.Value(), salary);

                    _logger.LogInformation($"\nEmployee with Id {idInt} successfully updated");

                    return 0;
                }

                app.ShowHelp();
                return 1;
            }
        );
    }
}