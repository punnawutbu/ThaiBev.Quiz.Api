using NLog.Web;

namespace ThaiBev.Quiz.Api
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                          .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
                          .AddJsonFile("secrets.json", optional: true, reloadOnChange: false)
                          .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .UseKestrel(k =>
                              {
                                  k.AddServerHeader = false;
                              });
                })
                .UseNLog();
    }
}