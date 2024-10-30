using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Core.Interfaces
{
    public interface ILogger
    {
        void Log(string logLevel, string message);

    }
}
