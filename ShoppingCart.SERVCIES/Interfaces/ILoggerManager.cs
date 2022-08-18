using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface ILoggerManager
    {
        void LogInfo(string messgae);
        void LogWarning(string messgae);
        void LogDebug(string message);
        void LogError(string message);
    }
}
