using System.Net;
using Bot.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Bot;

public class NominateController
{
    private readonly ILogger _logger;
    private readonly BoardMaster _boardMaster;
    private readonly Messenger _messenger;

    public NominateController(
        ILoggerFactory loggerFactory, 
        BoardMaster boardMaster,
        Messenger messenger)
    {
        _logger = loggerFactory.CreateLogger<NominateController>();
        _boardMaster = boardMaster;
        _messenger = messenger;
    }

    [Function("NominateController")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var teamMember = _boardMaster.Nominate();
        var notification = _messenger.Nudge(teamMember);

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteAsJsonAsync(notification);

        return response;
    }
}
