using System;
using System.IO;

namespace QuestionaireHelper
{
    class Program
    {
        public static string Directory => Path.GetDirectoryName(
            typeof(Program).Assembly.Location);

        static void Main(string[] args)
        {
            try
            {
                var log4netConfig = Path.Combine(Program.Directory, "Configs/log4net.config");
                log4net.Config.XmlConfigurator.Configure(new FileInfo(log4netConfig));

                //log4net.LogManager.GetLogger("App").Debug("Hello WOrld");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"App failed to start: {exception.Message}");
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
