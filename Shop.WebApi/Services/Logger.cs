using System;

namespace Shop.WebApi.Services
{
    public class Logger
    {

        private static readonly Lazy<Logger> lazy =
        new Lazy<Logger>(() => new Logger());

        public static Logger Instance { get { return lazy.Value; } }

        private Logger()
        {
        }

        public void Info(string message)
        {
            Console.WriteLine("Info: " + message);
        }

        public void Error(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        public void Debug(string message)
        {
            Console.WriteLine("Debug: " + message);
        }
    }
}