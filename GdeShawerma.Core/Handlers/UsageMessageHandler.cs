using GdeShawerma.Core.Resources;
using Telegram.Bot.Types.ReplyMarkups;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class UsageMessageHandler : IRequestHandler<UsageMessageRequest, Message>
{
    private readonly ITelegramBotClient _botClient;
    private readonly IMediator _mediator;

    public UsageMessageHandler(ITelegramBotClient botClient, IMediator mediator)
    {
        _botClient = botClient;
        _mediator = mediator;
    }

    public async Task<Message> Handle(UsageMessageRequest request, CancellationToken cancellationToken)
    {
        var message = await _botClient.SendTextMessageAsync(request.Message.Chat.Id,
            Texts.StartResponse, replyMarkup: new ReplyKeyboardRemove(), parseMode: ParseMode.Markdown,
            cancellationToken: cancellationToken);

        //await _mediator.Send(new GdeCommandRequest(request.Message.Chat.Id), cancellationToken);
        return message;
    }
}