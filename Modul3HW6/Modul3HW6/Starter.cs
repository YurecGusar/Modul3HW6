using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW6.Enums;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6
{
    public class Starter
    {
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
            _loggerService.BackUpСondition += Test;
            for (var i = 0; i <= 50; i++)
            {
                _loggerService.CreateLog(LogStatus.INFO, $"qwerty {i}");
            }
        }

        private void Test()
        {
            Console.WriteLine("BackUp");
            _fileService.MakeBackUp();
        }
    }
}
