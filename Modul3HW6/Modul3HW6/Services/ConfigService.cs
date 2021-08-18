using System.IO;
using Modul3HW6.Configs;
using Modul3HW6.Services.Abstractions;
using Newtonsoft.Json;

namespace Modul3HW6.Services
{
    public class ConfigService : IConfigService
    {
        private const string _jsonFileName = "config.json";
        public ConfigService()
        {
            var config = DeSerialization();
            LoggerConfig = config.LoggerConfig;
        }

        public LoggerConfig LoggerConfig { get; set; }
        private Config GetConfig()
        {
            return new Config
            {
                LoggerConfig = new LoggerConfig
                {
                    DirectoryName = @"Logs\",
                    FileExtension = ".txt",
                    FileNameFormat = "hh.mm.ss dd.MM.yyyy",
                    TimeFormat = "hh:mm:ss",
                    NumbeOfRowsToBackUp = 5,
                    BackUpDirectoryName = @"BackUp\"
                }
            };
        }

        private void Serialization(Config newConfig)
        {
            var json = JsonConvert.SerializeObject(newConfig);
            File.WriteAllText(_jsonFileName, json);
        }

        private Config DeSerialization()
        {
            var readFile = File.ReadAllText(_jsonFileName);
            var config = JsonConvert.DeserializeObject<Config>(readFile);
            return config;
        }
    }
}
