using System;
using Microsoft.Extensions.DependencyInjection;

namespace Modul3HW6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviseProvider = new ServiceCollection()
                .AddTransient<Starter>()
                .BuildServiceProvider();

            var starter = serviseProvider.GetService<Starter>();
            starter.Run();
        }
    }
}
