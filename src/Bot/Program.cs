using System;
using System.IO;
using Azure.Data.Tables;
using Bot.Domain;
using Bot.Handlers;
using Bot.State;
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
                    .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
                })
                .ConfigureServices(service =>
                {
                    var persistenceService = new Persistence(
                        new TableServiceClient(
                            Environment.GetEnvironmentVariable("StorageAccountConnectionString")));

                    service
                        .AddSingleton<IPersistence>(persistenceService)
                        .AddTransient<BoardMaster>()
                        .AddTransient<Messenger>()
                        .AddTransient<Nomination>()
                        .AddTransient<NominationHandler>();

                })
                .Build();
            host.Run();
        }
    }
}