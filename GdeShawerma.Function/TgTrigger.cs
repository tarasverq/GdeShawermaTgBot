using System.Threading.Tasks;
using GdeShawerma.Core.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Telegram.Bot.Types;

namespace GdeShawerma.Function;

public class TgTrigger
{
    private readonly IMediator _mediator;

    public TgTrigger(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("tg")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        Update update)
    {
        await _mediator.Send(new UpdateRequest(update));
        return new OkResult();
    }   
    
    [FunctionName("test")]
    public async Task<IActionResult> Test(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        Update update)
    {
        return new OkResult();
    }
}