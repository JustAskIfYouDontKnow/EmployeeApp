using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main.ConsoleCommands
{
    internal sealed class DeleteEmployee
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<DeleteEmployee> _logger;
        
        public DeleteEmployee(IEmployeeService employeeService, ILogger<DeleteEmployee> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        
        public void Execute(CommandLineApplication app)
        {
            app.Description = "Remove a user from the list\n  Usage: delete -id <int>\n";

            var idOption = app.Option("-id", "Id", CommandOptionType.SingleValue);

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

                        await _employeeService.DeleteEmployee(idInt);

                        _logger.LogInformation($"User: Id {idInt} removed successfully");
                        return 0;
                    }

                    app.ShowHelp();
                    return 1;
                }
            );
        }
    }
}