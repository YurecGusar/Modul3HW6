using System;
using Microsoft.Extensions.DependencyInjection;
using Modul3HW6.Services;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviseProvider = new ServiceCollection()
                .AddTransient<Starter>()
                .AddTransient<IConfigService, ConfigService>()
                .AddTransient<IFileService, FileService>()
                .AddTransient<ILoggerService, LoggerService>()
                .BuildServiceProvider();

            var starter = serviseProvider.GetService<Starter>();
            starter.Run();
        }
    }
}
