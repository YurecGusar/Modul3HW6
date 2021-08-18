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
        private static readonly StreamWriter _streamWriter = new StreamWriter("text");
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
            for (var i = 0; i <= 50; i++)
            {
                _loggerService.CreateLog(LogStatus.INFO, $"qwerty {i}");
            }
        }

        private void BackUp()
        {
            Console.WriteLine("BackUp");
            _fileService.MakeBackUp();
        }

        private static async Task WriteAsync(string text)
        {
            await _semaphoreSlim.WaitAsync();

            await _streamWriter.WriteLineAsync(text);
            await _streamWriter.FlushAsync();

            _semaphoreSlim.Release();
        }
    }
}
