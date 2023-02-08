using System.Net;
using System.Threading.Tasks;
using Bot.Domain;
using Bot.Dtos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bot;

public class Nomination
{
    private readonly IPersistence _persistence;
    private readonly Messenger _messenger;
    private readonly ILogger _logger;

    public Nomination(
        ILoggerFactory loggerFactory,
        IPersistence persistence,
        Messenger messenger)
    {
        _persistence = persistence;
        _messenger = messenger;
        _logger = loggerFactory.CreateLogger<Nomination>();
    }

    [Function("Nomination_Timer")]
    public async Task<Notification> NominationTimer(
        [TimerTrigger("0 */5 * * * *"
#if DEBUG
        ,RunOnStartup = true
#endif
        )] object myTimer)
    {
        _ = myTimer;

        var result = await Handle();

        return result.Result;
    }

    [Function("Nomination_WebHook")]
    public async Task<HttpResponseData> NominationWebHook(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "nomination")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var result = await Handle();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(result);

        return response;
    }


    public async Task<HandlerResponse<Notification>> Handle()
    {
        var boardMaster = await BoardMaster.Get(_persistence);

        var nominee = boardMaster.Pick();

        var notification = _messenger.Nudge(nominee);

        await boardMaster.Save(_persistence);

        return new HandlerResponse<Notification>(notification);
    }
}