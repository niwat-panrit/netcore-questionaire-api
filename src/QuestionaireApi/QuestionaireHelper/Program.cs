using System;
using System.IO;

namespace QuestionaireHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo("Configs/log4net.config"));


            }
            catch (Exception exception)
            {
                Console.WriteLine($"App failed to start: {exception.Message}");
                Console.WriteLine(exception.StackTrace);
            }
        }
    }
}
