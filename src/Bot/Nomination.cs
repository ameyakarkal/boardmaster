using System.Net;
using Bot.Domain;
using Bot.Dtos;
using Bot.Handlers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bot;

public class Nomination
{
    private readonly NominationHandler _nominationHandler;
    private readonly ILogger _logger;

    public Nomination(
        ILoggerFactory loggerFactory,
        NominationHandler nominationHandler)
    {
        _nominationHandler = nominationHandler;
        _logger = loggerFactory.CreateLogger<Nomination>();
    }

    [Function("Nomination")]
    public Notification Run([TimerTrigger("0 */5 * * * *")] ScheduleDto myTimer)
    {
        var result = _nominationHandler.Handle();

        return result.Result;
    }

    [Function("NominateWebhook")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var result = _nominationHandler.Handle();

        var response = req.CreateResponse(HttpStatusCode.OK);

        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        
        response.WriteAsJsonAsync(result);

        return response;
    }
}