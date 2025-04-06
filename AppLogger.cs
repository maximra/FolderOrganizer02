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

        public void Information(string message, params object[] args)       // params just changes the logging syntax a bit (without it we would have to create many versions for args or use templates)
        {
            Logger.Information(message, args);
        }

        public void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }
    }
}
