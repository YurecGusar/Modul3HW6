using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6.Services
{
    public class FileService : IFileService
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
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
        }

        public void Init(string dirName, string fileName, string fileExtension)
        {
            var path = GetFilePath(_config.LoggerConfig.DirectoryName); /*$"{dirName}{fileName}{fileExtension}";*/
            _filePath = path;
            CreateDirectory(dirName);
            _streamWriter = new StreamWriter(path, true);
        }

        public void WriteToFile(string value)
        {
            _streamWriter.WriteLine(value);
        }

        public async Task WriteToFileAsync(string value)
        {
            await _semaphoreSlim.WaitAsync();
            await _streamWriter.WriteLineAsync(value);
            await _streamWriter.FlushAsync();
            _semaphoreSlim.Release();
        }

        public void MakeBackUp()
        {
            var dirName = _config.LoggerConfig.BackUpDirectoryName;
            CreateDirectory(dirName);
            File.Copy(_filePath, GetFilePath(dirName, _unicValue.ToString()));
            _unicValue++;
        }

        public void ClearFolders()
        {
            _dirInfo.Delete();
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
