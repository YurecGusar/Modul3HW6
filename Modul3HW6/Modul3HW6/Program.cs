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
                .AddTransient<ILoggerService, LoggerService>()
                .AddTransient<IConfigService, ConfigService>()
                .AddSingleton<IFileService, FileService>()
                .AddTransient<Starter>()
                .BuildServiceProvider();

            var starter = serviseProvider.GetService<Starter>();
            starter.Run();
        }
    }
}
