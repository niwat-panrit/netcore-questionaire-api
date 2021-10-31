using System;
using System.IO;
using CommandLine;

namespace QuestionaireHelper
{
    class Program
    {
        public static string Directory => Path.GetDirectoryName(
            typeof(Program).Assembly.Location);

        public class Options
        {
            [Option("load-countries", Required = false, HelpText = "Load country list as a choice group.")]
            public bool LoadCountries { get; set; }
        }

        static void Main(string[] args)
        {
            try
            {
                var log4netConfig = Path.Combine(
                    Program.Directory, "Configs/log4net.config");
                log4net.Config.XmlConfigurator
                    .Configure(new FileInfo(log4netConfig));
                log4net.LogManager.GetLogger("App")?.Info("Starting up...");

                Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       if (o.LoadCountries)
                           (new CountryListBuilder()).Run(o);
                   })
                   .WithNotParsed<Options>(o =>
                   {
                       log4net.LogManager.GetLogger("App")?
                           .Info($"Couldn't parse command line arguments: {string.Join(" ", args)}.");
                   });

                log4net.LogManager.GetLogger("App")?.Info("Shutting down...");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
                Console.WriteLine(exception.StackTrace);
                log4net.LogManager.GetLogger("App")?.Error(exception);
            }
        }
    }
}
