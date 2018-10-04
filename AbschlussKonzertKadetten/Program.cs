using AbschlussKonzertKadetten.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;

namespace AbschlussKonzertKadetten
{
    public class Program
    {
        private static ILogger<Program> Log;
        public static void Main(string[] args)
        {
            var loggerFactory = new LoggerFactory().AddConsole((l, ll) => true);
            Log = loggerFactory.CreateLogger<Program>();
            Log.LogInformation("Start application");
            BuildWebHost(args).Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseCloudFoundryHosting()
                .AddCloudFoundry()
                .UseStartup<Startup>()
                .Build();
    }
}
