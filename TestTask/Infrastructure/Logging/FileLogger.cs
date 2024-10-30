using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Interfaces;

namespace TestTask.Infrastructure.Logging
{
    public class FileLogger: ILogger
    {
        private readonly string  _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }
        public void Log(string logLevel, string message)
        {
            try
            {
                var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}";
                File.AppendAllText(_logFilePath, logEntry + "\n");
            }
            catch(IOException ex)
            {
                Console.WriteLine("Ошибка логирования");
            }
        }
    }
}
