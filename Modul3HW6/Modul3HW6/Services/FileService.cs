using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6.Services
{
    public class FileService : IFileService
    {
        private readonly IConfigService _config;
        private readonly IComparer _comparer;
        private DirectoryInfo _dirInfo;
        private StreamWriter _streamWriter;
        private string _filePath;

        public FileService(
            IComparer comparer,
            IConfigService config)
        {
            _comparer = comparer;
            _config = config;

            CreateDirectory();
            _filePath = GetFilePath();
        }

        public void WriteToFile(string value)
        {
            _streamWriter = new StreamWriter(_filePath, true);
            _streamWriter.WriteLine(value);
            _streamWriter.Close();
        }

        private void CreateDirectory()
        {
            _dirInfo = new DirectoryInfo(_config.LoggerConfig.DirectoryName);
            if (!_dirInfo.Exists)
            {
                _dirInfo.Create();
            }
        }

        private string GetFilePath()
        {
            var dirName = _config.LoggerConfig.DirectoryName;
            var fileName = DateTime.UtcNow.ToString(_config.LoggerConfig.FileNameFormat);
            var fileExtension = _config.LoggerConfig.FileExtension;

            return $"{dirName}{fileName}{fileExtension}";
        }
    }
}
