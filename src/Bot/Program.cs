using System.IO;
using Bot.Domain;
using Bot.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bot
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureAppConfiguration(conf =>
                {
                    conf.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true);
                    //.AddEnvironmentVariables();
                })
                .ConfigureServices(service =>
                {
                    service
                        .AddSingleton<BoardMaster>()
                        .AddTransient<Messenger>()
                        .AddTransient<Nomination>()
                        .AddTransient<NominationHandler>();

                })
                .Build();
            host.Run();
        }
    }
}