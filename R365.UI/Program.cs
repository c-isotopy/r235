using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using R365.Services.Calculator;
using R365.Services.Evaluation;
using R365.Services.Input;
using R365.Services.Output;
using R365.Services.Parsing;
using R365.Services.UX;
using R365.UI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureServices(ConfigureServices)
                       .Build()
                       .RunAsync();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddScoped<IInputService, ConsoleInputService>();
            services.AddScoped<IOutputService, ConsoleOutputService>();
            services.AddScoped<INumericalParserService, NumericalParserService>();
            services.AddScoped<IExpressionParserService, ExpressionParserService>();
            services.AddScoped<IExpressionEvaluationService, ExpressionEvaluationService>();
            services.AddScoped<ICalculatorService, CalculatorService>();
            services.AddScoped<IUXService, ConsoleUXService>();
        }
    }
}
