using System.Text;
using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main.ConsoleCommands
{
    internal sealed class GetAllEmployees
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<GetAllEmployees> _logger;
        
        public GetAllEmployees(IEmployeeService employeeService, ILogger<GetAllEmployees> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }
        
        public void Execute(CommandLineApplication app)
        {
            app.Description = "Show existing employees\n  Usage: getall\n";

            app.OnExecute(async () =>
            {
                var employees = await _employeeService.ListEmployees();

                var sb = new StringBuilder();
                sb.AppendLine("\nEmployees:");

                foreach (var employee in employees)
                {
                    sb.AppendLine($"\nId = {employee.Id}\nFirstName = {employee.FirstName}\nLastName = {employee.LastName}\nSalaryPerHour = {employee.SalaryPerHour}\n");
                }
                
                _logger.LogInformation(sb.ToString());
                return 1;
            });
        }
    }
}
