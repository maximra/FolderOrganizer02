using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganizerSoftware
{
    public class AppLogger 
    {
        private static readonly Lazy<AppLogger> _instance = new(() => new AppLogger());
        public static AppLogger Instance => _instance.Value;

        public ILogger Logger { get; }
        private AppLogger()
        {
            Directory.CreateDirectory("logs");
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/userOperations_.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

        }
        public static string GetCurrentLogFileName()        // name required for encryption
        {
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            string fileName = $"userOperations_{DateTime.Now:yyyyMMdd}.txt";
            return Path.Combine(logDirectory, fileName);
        }

        public void Information(string message, params object[] args)       // params just changes the logging syntax a bit (without it we would have to create many versions for args or use templates)
        {
            Logger.Information(message, args);
        }

        public void CloseAndFlush()     // Default flush did NOT work because we were flushing and empty log that wasn't doing anything. Logger can't be directly accessed because it's ILogger.
        {
            if (Logger is IDisposable disposableLogger)
            {
                disposableLogger.Dispose(); // This will release file handles
            }
        }

    }
}
