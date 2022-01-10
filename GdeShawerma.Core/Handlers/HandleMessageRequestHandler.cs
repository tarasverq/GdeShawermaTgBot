using GdeShawerma.Core.Api;
using GdeShawerma.Core.Db;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class HandleMessageRequestHandler : IRequestHandler<HandleMessageRequest>
{
    private readonly ILogger<HandleMessageRequestHandler> _logger;
    private readonly IMediator _mediator;

    public HandleMessageRequestHandler(
        ILogger<HandleMessageRequestHandler> logger,
       
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(HandleMessageRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {messageType}", request.Message.Type);

        
        //await _mediator.Send(new SetCultureRequest(request.Message.From));
        
        switch (request.Message.Type)
        {
            case MessageType.Text:
                await _mediator.Send(new TextMessageRequest(request.Message), cancellationToken);
                break;
            
            case MessageType.Location:
                await _mediator.Send(new LocationMessageRequest(request.Message), cancellationToken);
                break;
            default:
                return Unit.Value;
        }
            
        
        return Unit.Value;
    }
    
    
    
}