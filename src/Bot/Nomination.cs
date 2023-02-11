using System.Net;
using System.Threading.Tasks;
using Bot.Domain;
using Bot.State;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bot;

public class Nomination
{
    private readonly IPersistence _persistence;
    private readonly Messenger _messenger;
    private readonly ILogger<Nomination> _logger;
    private readonly BoardMaster _boardMaster;

    public Nomination(
        ILogger<Nomination> logger, BoardMaster boardMaster, Messenger messenger)
    {
        _messenger = messenger;
        _logger = logger;
        _boardMaster = boardMaster;
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
        var nominee = await _boardMaster.Pick();

        var notification = _messenger.Nudge(nominee);

        return new HandlerResponse<Notification>(notification);
    }
}