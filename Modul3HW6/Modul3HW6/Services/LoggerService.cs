using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW6.Enums;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfigService _config;
        private readonly IFileService _fileService;
        private int _countLines;
        public LoggerService(
            IFileService fileService,
            IConfigService config)
        {
            _fileService = fileService;
            _config = config;
            _countLines = 0;

            FileServiceInit();
        }

        public event Action BackUpСondition;

        public void CreateLog(LogStatus logType, string message)
        {
            var log = $"{DateTime.UtcNow.ToString(_config.LoggerConfig.FileNameFormat)} {logType}: {message}";
            Console.WriteLine(log);
            _fileService.WriteToFileAsync(log);
            _countLines++;
            BackUpControl(_countLines);
        }

        private void BackUpControl(int countLines)
        {
            if (countLines % _config.LoggerConfig.NumbeOfRowsToBackUp == 0)
            {
            BackUpСondition.Invoke();
            }
        }

        private void FileServiceInit()
        {
            var dirName = _config.LoggerConfig.DirectoryName;
            var fileName = DateTime.UtcNow.ToString(_config.LoggerConfig.TimeFormat);
            var fileExtencion = _config.LoggerConfig.FileExtension;
            _fileService.Init(dirName, fileName, fileExtencion);
        }
    }
}
