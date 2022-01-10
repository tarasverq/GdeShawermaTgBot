using GdeShawerma.Core.Helpers;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class UpdateHandler : IRequestHandler<UpdateRequest>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UpdateHandler> _logger;
    private readonly ITelegramBotClient _botClient;

    public UpdateHandler(IMediator mediator, ILogger<UpdateHandler> logger, ITelegramBotClient botClient)
    {
        _mediator = mediator;
        _logger = logger;
        _botClient = botClient;
    }

    public async Task<Unit> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        await _botClient.SendChatActionAsync(request.Update.GetChatId(), ChatAction.Typing,
            cancellationToken: cancellationToken);
        Task handler = request.Update.Type switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            UpdateType.Message => _mediator.Send(new HandleMessageRequest(request.Update.Message!), cancellationToken),
            // UpdateType.EditedMessage => BotOnMessageReceived(update.EditedMessage!),
            UpdateType.CallbackQuery => _mediator.Send(new CallbackQueryRequest(request.Update.CallbackQuery!),
                cancellationToken),
            UpdateType.InlineQuery => _mediator.Send(new InlineQueryRequest(request.Update.InlineQuery!),
                cancellationToken),
            // UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult!),
            _ => UnknownUpdateHandlerAsync(request.Update)
        };

        try
        {
            await handler;
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(exception);
        }

        return Unit.Value;
    }

    private Task HandleErrorAsync(Exception exception)
    {
        string errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogError(exception, "HandleError: {ErrorMessage}", errorMessage);
        return Task.CompletedTask;
    }


    private Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}