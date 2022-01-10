using GdeShawerma.Core.Db;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class TextMessageHandler : IRequestHandler<TextMessageRequest, Message>
{
    private readonly IMediator _mediator;
    private readonly ILogger<TextMessageHandler> _logger;
    private readonly ITelegramBotClient _botClient;

    public TextMessageHandler(IMediator mediator, ILogger<TextMessageHandler> logger, ITelegramBotClient botClient)
    {
        _mediator = mediator;
        _logger = logger;
        _botClient = botClient;
    }

    public async Task<Message> Handle(TextMessageRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new LogUserRequest(request.Message.From), cancellationToken);
        
        Task<Message> action = request.Message.Text!.Split(' ')[0] switch
        {
            "/gde" => _mediator.Send(new GdeCommandRequest(request.Message.Chat.Id), cancellationToken),
            _ => _mediator.Send(new UsageMessageRequest(request.Message), cancellationToken),
        };
        Message sentMessage = await action;
        _logger.LogInformation("The message was sent with id: {sentMessageId}", sentMessage.MessageId);
        return sentMessage;
    }
}