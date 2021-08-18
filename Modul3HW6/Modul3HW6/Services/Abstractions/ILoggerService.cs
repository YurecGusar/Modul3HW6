using System;
using Modul3HW6.Enums;

namespace Modul3HW6.Services.Abstractions
{
    public interface ILoggerService
    {
        public event Action BackUpСondition;
        public void CreateLog(LogStatus logType, string message);
    }
}
