using System;
using System.IO;
using Azure.Data.Tables;
using Bot.Domain;
using Bot.State;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bot;

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

                var boardMaster = BoardMaster
                    .GetInstance(persistenceService)
                    .GetAwaiter().GetResult();

                service
                    .AddSingleton(boardMaster)
                    .AddSingleton<IPersistence>(persistenceService)
                    .AddTransient<Messenger>()
                    .AddTransient<Nomination>();

            })
            .Build();
        host.Run();
    }
}