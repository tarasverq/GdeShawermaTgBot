using GdeShawerma.Core.Api.Dtos;
using GdeShawerma.Core.Helpers;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace GdeShawerma.Core.Handlers;

[UsedImplicitly]
internal class SendRestaurantInfoRequestHandler : IRequestHandler<SendRestaurantInfoRequest, Message>
{
    private readonly ITelegramBotClient _botClient;
    private readonly Config _config;
    private readonly ILogger<SendRestaurantInfoRequestHandler> _logger;

    public SendRestaurantInfoRequestHandler(ITelegramBotClient botClient, IOptions<Config> config,
        ILogger<SendRestaurantInfoRequestHandler> logger)
    {
        _botClient = botClient;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<Message> Handle(SendRestaurantInfoRequest request, CancellationToken cancellationToken)
    {
        var restaurant = request.Restaurant;
        RestaurantInfoDescriber describer = new RestaurantInfoDescriber(restaurant, request.UserLocation);
        string text = describer.Describe();

        InlineKeyboardMarkup inlineKeyboardMarkup = GetMarkup(restaurant);

        Message message = await Send(restaurant, request.ChatId, text, inlineKeyboardMarkup, cancellationToken);

        return message;
    }

    private InlineKeyboardMarkup GetMarkup(Restaurant restaurant)
    {
        InlineKeyboardMarkup inlineKeyboardMarkup;
        if (restaurant is RestaurantExtended extended)
        {
            inlineKeyboardMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl(Texts.More_app,
                        new Uri(new(_config.ShawermaBaseUrl), $"place/{restaurant.Id}").ToString()),
                } // buttons in row 3
            });
        }
        else
        {
            inlineKeyboardMarkup = new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(Texts.More_bot, $"restaurant|{restaurant.Id}"),
                }, // buttons in row 1
                new[]
                {
                    InlineKeyboardButton.WithUrl(Texts.More_app,
                        new Uri(new(_config.ShawermaBaseUrl), $"place/{restaurant.Id}").ToString()),
                } // buttons in row 3
            });
        }

        return inlineKeyboardMarkup;
    }


    private async Task<Message> Send(Restaurant restaurant, long chatId, string text,
        InlineKeyboardMarkup inlineKeyboardMarkup, CancellationToken cancellationToken)
    {
        Message message;
        if (!string.IsNullOrWhiteSpace(restaurant.Picture))
        {
            try
            {
                message = await _botClient.SendPhotoAsync(chatId: chatId,
                    new InputOnlineFile(new Uri(new(_config.ShawermaBaseUrl), restaurant.Picture)),
                    parseMode: ParseMode.Markdown,
                    caption: text,
                    replyMarkup: inlineKeyboardMarkup, cancellationToken: cancellationToken);
            }
            catch (ApiRequestException e)
            {
                _logger.LogError(e, "Error on sending Restaurant");
                message = await SendText(chatId, text, inlineKeyboardMarkup, cancellationToken);
            }
        }
        else
        {
            message = await SendText(chatId, text, inlineKeyboardMarkup, cancellationToken);
        }

        return message;
    }

    private async Task<Message> SendText(long chatId, string text, InlineKeyboardMarkup inlineKeyboardMarkup,
        CancellationToken cancellationToken)
    {
        Message message = await _botClient.SendTextMessageAsync(chatId: chatId, text: text,
            parseMode: ParseMode.Markdown,
            replyMarkup: inlineKeyboardMarkup, cancellationToken: cancellationToken);
        return message;
    }
}