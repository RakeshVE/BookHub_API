using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using ShoppingCart.Interfaces;

namespace ShoppingCart.Logger
{
    public class LoggerService:ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message) => logger.Debug(message);

        public void LogInfo(string message) => logger.Info(message);
        public void LogError(string message) => logger.Error(message);
        public void LogWarning(string message) => logger.Warn(message);

    }
}
