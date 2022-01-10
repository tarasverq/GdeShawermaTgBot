using Telegram.Bot.Types.ReplyMarkups;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class GdeCommandRequestHandler : IRequestHandler<GdeCommandRequest, Message>
{
    private readonly ITelegramBotClient _botClient;

    public GdeCommandRequestHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task<Message> Handle(GdeCommandRequest request, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup requestReplyKeyboard = new(
            new[]
            {
                KeyboardButton.WithRequestLocation(Texts.ShareGeo),
            });
        return await _botClient.SendTextMessageAsync(chatId: request.ChatId, text: Texts.GdeResponse,
            replyMarkup: requestReplyKeyboard, cancellationToken: cancellationToken);
    }
}