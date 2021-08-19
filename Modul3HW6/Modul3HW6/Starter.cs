using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modul3HW6.Enums;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6
{
    public class Starter
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        private IConfigService _config;
        private ILoggerService _loggerService;
        private IFileService _fileService;
        public Starter(
            IConfigService config,
            ILoggerService loggerService,
            IFileService fileService)
        {
            _config = config;
            _loggerService = loggerService;
            _fileService = fileService;
        }

        public void Run()
        {
            _loggerService.BackUpСondition += BackUp;

            Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    var k = i;
                    Task.Run(async () => { await WriteAsync($"Method 2 {k}"); });
                }
            });

            Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    var k = i;
                    Task.Run(async () => { await WriteAsync($"Method 1 {k}"); });
                }
            });

            Console.ReadLine();
        }

        private void BackUp()
        {
            Console.WriteLine("BackUp");
            _fileService.MakeBackUp();
        }

        private async Task WriteAsync(string message)
        {
            await _semaphoreSlim.WaitAsync();

            await Task.Run(() => _loggerService.CreateLog(LogStatus.INFO, message));

            _semaphoreSlim.Release();
        }
    }
}
