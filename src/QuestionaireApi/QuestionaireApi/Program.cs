using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace QuestionaireApi
{
    public class Program
    {
        public static string Directory => Path.GetDirectoryName(
            typeof(Program).Assembly.Location);

        public static void Main(string[] args)
        {
            var log4netConfig = Path.Combine(Program.Directory, "Configs/log4net.config");
            log4net.Config.XmlConfigurator.Configure(new FileInfo(log4netConfig));
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
