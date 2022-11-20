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

    [Function("Nomination_Timer")]
    public Notification NominationTimer([TimerTrigger("0 */5 * * * *")] ScheduleDto myTimer)
    {
        var result = _nominationHandler.Handle();

        return result.Result;
    }

    [Function("Nomination_WebHook")]
    public HttpResponseData NominationWebHook(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "nomination")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var result = _nominationHandler.Handle();

        var response = req.CreateResponse(HttpStatusCode.OK);

        response.WriteAsJsonAsync(result);

        return response;
    }
}