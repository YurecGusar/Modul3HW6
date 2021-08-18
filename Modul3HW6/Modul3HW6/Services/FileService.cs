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
        private DirectoryInfo _dirInfo;
        private StreamWriter _streamWriter;
        private string _filePath;
        private int _unicValue;

        public FileService(
            IConfigService config)
        {
            _unicValue = 1;
            _config = config;
            CreateDirectory(_config.LoggerConfig.DirectoryName);
            _filePath = GetFilePath(_config.LoggerConfig.DirectoryName);
        }

        public void WriteToFile(string value)
        {
            _streamWriter = new StreamWriter(_filePath, true);
            _streamWriter.WriteLine(value);
            _streamWriter.Close();
        }

        public void MakeBackUp()
        {
            var dirName = _config.LoggerConfig.BackUpDirectoryName;
            CreateDirectory(dirName);
            File.Copy(_filePath, GetFilePath(dirName, _unicValue.ToString()));
            _unicValue++;
        }

        private void CreateDirectory(string dirName)
        {
            _dirInfo = new DirectoryInfo(dirName);
            if (!_dirInfo.Exists)
            {
                _dirInfo.Create();
            }
        }

        private string GetFilePath(string dirName, string unicValue = "")
        {
            if (unicValue != string.Empty)
            {
                unicValue = $"({unicValue})";
            }

            var fileName = DateTime.UtcNow.ToString(_config.LoggerConfig.FileNameFormat);
            var fileExtension = _config.LoggerConfig.FileExtension;

            return $"{dirName}{fileName}{unicValue}{fileExtension}";
        }
    }
}
