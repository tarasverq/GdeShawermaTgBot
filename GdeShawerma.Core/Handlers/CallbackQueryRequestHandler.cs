namespace GdeShawerma.Core.Handlers;
[UsedImplicitly]
internal class CallbackQueryRequestHandler : IRequestHandler<CallbackQueryRequest>
{
    private readonly IMediator _mediator;
    private readonly ITelegramBotClient _botClient;

    public CallbackQueryRequestHandler(IMediator mediator, ITelegramBotClient botClient)
    {
        _mediator = mediator;
        _botClient = botClient;
    }

    public async Task<Unit> Handle(CallbackQueryRequest request, CancellationToken cancellationToken)
    {
        var splited = request.CallbackQuery.Data?.Split('|');
        switch (splited?[0])
        {
            case "restaurant":
               await _mediator.Send(new SendRestaurantExtendedInfoRequest(request.CallbackQuery.Message.Chat.Id,
                    long.Parse(splited[1])), cancellationToken);
                break;
            default:
                return Unit.Value;
        }
        await _botClient.AnswerCallbackQueryAsync(request.CallbackQuery.Id, Texts.LookNewMessages, 
            cancellationToken: cancellationToken);

        return Unit.Value;
    }
}