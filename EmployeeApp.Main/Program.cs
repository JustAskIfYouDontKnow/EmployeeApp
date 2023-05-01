using EmployeeApp.Main.ConsoleCommands;
using EmployeeApp.Main.Services;
using Microsoft.Extensions.CommandLineUtils;

namespace EmployeeApp.Main
{
    static internal class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IEmployeeService, EmployeeService>();
                    services.AddSingleton<IJsonService, JsonServices>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .Build();
            
            var app = new CommandLineApplication();
            ConfigureCommands(app, host.Services);
            
            app.OnExecute(() => 
                {
                    app.ShowHelp();
                    return 0;
                }
            );

            app.Execute(args);
        }
        private static void ConfigureCommands(CommandLineApplication app, IServiceProvider serviceProvider)
        {
            app.Command("add", new AddEmployee(
                serviceProvider.GetRequiredService<IEmployeeService>(),
                serviceProvider.GetRequiredService<ILogger<AddEmployee>>()
                ).Execute);

            app.Command("getall", new GetAllEmployees(
                serviceProvider.GetRequiredService<IEmployeeService>(), 
                serviceProvider.GetRequiredService<ILogger<GetAllEmployees>>()
            ).Execute);
            
            app.Command("update", new UpdateEmployee(
                serviceProvider.GetRequiredService<IEmployeeService>(), 
                serviceProvider.GetRequiredService<ILogger<UpdateEmployee>>()
            ).Execute);
            
            app.Command("get", new GetEmployee(
                serviceProvider.GetRequiredService<IEmployeeService>(), 
                serviceProvider.GetRequiredService<ILogger<GetEmployee>>()
            ).Execute);
            
            app.Command("delete", new DeleteEmployee(
                serviceProvider.GetRequiredService<IEmployeeService>(), 
                serviceProvider.GetRequiredService<ILogger<DeleteEmployee>>()
            ).Execute);
            
        }
    }
}