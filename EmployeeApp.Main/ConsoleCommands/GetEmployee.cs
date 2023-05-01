using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main.ConsoleCommands
{
    internal sealed class GetEmployee
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<GetEmployee> _logger;
        
        public GetEmployee(IEmployeeService employeeService, ILogger<GetEmployee> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        public void Execute(CommandLineApplication app)
        {
            app.Description = "Show existing employee by id\n  Usage: get -id <int>\n";

            var idOption = app.Option("-id", "Id", CommandOptionType.SingleValue);

            app.OnExecute(
                async () =>
                {
                    if (idOption.Value() != null)
                    {
                        if (!int.TryParse(idOption.Value(), out var idInt))
                        {
                            _logger.LogError("\nId format must be int.\n");
                            return 1;
                        }

                        var employee = await _employeeService.GetEmployee(idInt);

                        _logger.LogInformation($"\nId = {employee.Id}\nFirstName = {employee.FirstName}\nLastName = {employee.LastName}\nSalaryPerHour = {employee.SalaryPerHour}\n");
                        return 0;
                    }
                    app.ShowHelp();
                    return 1;
                }
            );
        }
    }
}