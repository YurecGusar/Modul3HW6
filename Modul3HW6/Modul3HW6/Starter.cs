using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW6.Services.Abstractions;

namespace Modul3HW6
{
    public class Starter
    {
        private IConfigService _config;
        public Starter(IConfigService config)
        {
            _config = config;
        }

        public void Run()
        {
            Console.WriteLine(_config.LoggerConfig.DirectoryName);
        }
    }
}
