using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW6.Configs
{
    public class LoggerConfig
    {
        public string DirectoryName { get; set; }
        public string BackUpDirectoryName { get; set; }
        public string FileExtension { get; set; }
        public string FileNameFormat { get; set; }
        public string TimeFormat { get; set; }
        public int NumbeOfRowsToBackUp { get; set; }
    }
}
