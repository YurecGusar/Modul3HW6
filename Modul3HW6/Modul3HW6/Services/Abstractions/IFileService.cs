using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW6.Services.Abstractions
{
    public interface IFileService
    {
        public void WriteToFile(string value);
        public void MakeBackUp();
    }
}
